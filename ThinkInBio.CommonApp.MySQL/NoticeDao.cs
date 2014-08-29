using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using ThinkInBio.Common.Data;
using ThinkInBio.MySQL;
using ThinkInBio.CommonApp;
using ThinkInBio.CommonApp.DAL;

namespace ThinkInBio.CommonApp.MySQL
{
    public class NoticeDao : GenericDao<Notice>, INoticeDao
    {

        private string dataSource;

        public NoticeDao(string dataSource)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentNullException();
            }
            this.dataSource = dataSource;
        }

        public override bool Save(Notice entity)
        {
            return DbTemplate.Save(dataSource,
                (command) =>
                {
                    command.CommandText = @"insert into cyNotice (id,title,content,creator,creation,modification) 
                                                values (NULL,@title,@content,@creator,@creation,@modification)";
                    command.Parameters.Add(DbFactory.CreateParameter("title", entity.Title));
                    command.Parameters.Add(DbFactory.CreateParameter("content", entity.Content));
                    command.Parameters.Add(DbFactory.CreateParameter("creator", entity.Creator));
                    command.Parameters.Add(DbFactory.CreateParameter("creation", entity.Creation));
                    command.Parameters.Add(DbFactory.CreateParameter("modification", entity.Modification));
                },
                (id) =>
                {
                    entity.Id = id;
                });
        }

        public override bool Update(Notice entity)
        {
            return DbTemplate.UpdateOrDelete(dataSource,
                (command) =>
                {
                    command.CommandText = @"update cyNotice 
                                                set title=@title,content=@content,modification=@modification
                                                where id=@id";
                    command.Parameters.Add(DbFactory.CreateParameter("title", entity.Title));
                    command.Parameters.Add(DbFactory.CreateParameter("content", entity.Content));
                    command.Parameters.Add(DbFactory.CreateParameter("modification", entity.Modification));
                    command.Parameters.Add(DbFactory.CreateParameter("id", entity.Id));
                });
        }

        public override bool Delete(Notice entity)
        {
            return DbTemplate.UpdateOrDelete(dataSource,
                (command) =>
                {
                    command.CommandText = @"delete from cyNotice 
                                                where id=@id";
                    command.Parameters.Add(DbFactory.CreateParameter("id", entity.Id));
                });
        }

        public override Notice Get(object id)
        {
            return DbTemplate.Get<Notice>(dataSource,
                (command) =>
                {
                    command.CommandText = @"select id,title,content,creator,creation,modification from cyNotice 
                                                where id=@id";
                    command.Parameters.Add(DbFactory.CreateParameter("id", id));
                },
                (reader) =>
                {
                    Notice entity = new Notice();
                    entity.Id = reader.GetInt64(0);
                    entity.Title = reader.GetString(1);
                    entity.Content = reader.IsDBNull(2) ? null : reader.GetString(2);
                    entity.Creator = reader.GetString(3);
                    entity.Creation = reader.GetDateTime(4);
                    entity.Modification = reader.GetDateTime(5);

                    return entity;
                });
        }

        public long GetCount(DateTime? startTime, DateTime? endTime)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            return DbTemplate.GetCount(dataSource,
                (command) =>
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select count(id) from cyNotice ");
                    BuildSql(sql, parameters, startTime, endTime);
                    command.CommandText = sql.ToString();
                },
                parameters);
        }

        public IList<Notice> GetList(DateTime? startTime, DateTime? endTime, bool asc, int startRowIndex, int maxRowsCount)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            return DbTemplate.GetList<Notice>(dataSource,
                (command) =>
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select id,title,creator,creation,modification from cyNotice ");
                    BuildSql(sql, parameters, startTime, endTime);
                    sql.Append(" order by modification ");
                    if (!asc)
                    {
                        sql.Append(" desc ");
                    }
                    if (maxRowsCount < int.MaxValue)
                    {
                        sql.Append(" limit ").Append(startRowIndex).Append(",").Append(maxRowsCount);
                    }
                    command.CommandText = sql.ToString();
                },
                parameters,
                (reader) =>
                {
                    Notice entity = new Notice();
                    entity.Id = reader.GetInt64(0);
                    entity.Title = reader.GetString(1);
                    entity.Creator = reader.GetString(2);
                    entity.Creation = reader.GetDateTime(3);
                    entity.Modification = reader.GetDateTime(4);

                    return entity;
                });
        }

        private void BuildSql(StringBuilder sql, List<KeyValuePair<string, object>> parameters,
            DateTime? startTime, DateTime? endTime)
        {
            if (startTime.HasValue && startTime.Value != DateTime.MinValue
                    && endTime.HasValue && endTime.Value != DateTime.MinValue
                    && endTime.Value > startTime.Value)
            {
                SQLHelper.AppendOp(sql, parameters);
                sql.Append(" creation between @startTime and @endTime ");
                parameters.Add(new KeyValuePair<string, object>("startTime", startTime.Value));
                parameters.Add(new KeyValuePair<string, object>("endTime", endTime.Value));
            }
        }

    }
}
