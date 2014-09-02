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
    public class IdiomDao : GenericDao<Idiom>, IIdiomDao
    {

        private string dataSource;

        public IdiomDao(string dataSource)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentNullException();
            }
            this.dataSource = dataSource;
        }

        public override bool Save(Idiom entity)
        {
            return DbTemplate.Save(dataSource,
                (command) =>
                {
                    command.CommandText = @"insert into cyIdiom (id,scope,content,spell,modification) 
                                                values (NULL,@scope,@content,@spell,@modification)";
                    command.Parameters.Add(DbFactory.CreateParameter("scope", entity.Scope));
                    command.Parameters.Add(DbFactory.CreateParameter("content", entity.Content));
                    command.Parameters.Add(DbFactory.CreateParameter("spell", entity.Spell));
                    command.Parameters.Add(DbFactory.CreateParameter("modification", entity.Modification));
                },
                (id) =>
                {
                    entity.Id = id;
                });
        }

        public override bool Update(Idiom entity)
        {
            return DbTemplate.UpdateOrDelete(dataSource,
                (command) =>
                {
                    command.CommandText = @"update cyIdiom 
                                                set content=@content,spell=@spell,modification=@modification
                                                where id=@id";
                    command.Parameters.Add(DbFactory.CreateParameter("content", entity.Content));
                    command.Parameters.Add(DbFactory.CreateParameter("spell", entity.Spell));
                    command.Parameters.Add(DbFactory.CreateParameter("modification", entity.Modification));
                    command.Parameters.Add(DbFactory.CreateParameter("id", entity.Id));
                });
        }

        public override bool Delete(Idiom entity)
        {
            return DbTemplate.UpdateOrDelete(dataSource,
                (command) =>
                {
                    command.CommandText = @"delete from cyIdiom 
                                                where id=@id";
                    command.Parameters.Add(DbFactory.CreateParameter("id", entity.Id));
                });
        }

        public override Idiom Get(object id)
        {
            return DbTemplate.Get<Idiom>(dataSource,
                (command) =>
                {
                    command.CommandText = @"select id,scope,content,spell,modification from cyIdiom 
                                                where id=@id";
                    command.Parameters.Add(DbFactory.CreateParameter("id", id));
                },
                (reader) =>
                {
                    return Populate(reader);
                });
        }

        public IList<Idiom> GetList(DateTime? startTime, DateTime? endTime, string scope, bool asc, int startRowIndex, int maxRowsCount)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            return DbTemplate.GetList<Idiom>(dataSource,
                (command) =>
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select id,scope,content,spell,modification from cyIdiom ");
                    if (startTime.HasValue && startTime.Value != DateTime.MinValue
                        && endTime.HasValue && endTime.Value != DateTime.MinValue
                        && endTime.Value > startTime.Value)
                    {
                        SQLHelper.AppendOp(sql, parameters);
                        sql.Append(" modification between @startTime and @endTime ");
                        parameters.Add(new KeyValuePair<string, object>("startTime", startTime.Value));
                        parameters.Add(new KeyValuePair<string, object>("endTime", endTime.Value));
                    }
                    if (!string.IsNullOrWhiteSpace(scope))
                    {
                        SQLHelper.AppendOp(sql, parameters);
                        sql.Append(" scope=@scope ");
                        parameters.Add(new KeyValuePair<string, object>("scope", scope));
                    }
                    sql.Append(" order by ");
                    if (startTime.HasValue && startTime.Value != DateTime.MinValue
                        && endTime.HasValue && endTime.Value != DateTime.MinValue
                        && endTime.Value > startTime.Value)
                    {
                        sql.Append(" modification ");
                    }
                    else
                    {
                        sql.Append(" spell ");
                    }
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
                    return Populate(reader);
                });
        }

        private Idiom Populate(IDataReader reader)
        {
            Idiom entity = new Idiom();
            entity.Id = reader.GetInt64(0);
            entity.Scope = reader.GetString(1);
            entity.Content = reader.GetString(2);
            entity.Spell = reader.GetString(3);
            entity.Modification = reader.GetDateTime(4);

            return entity;
        }

    }
}
