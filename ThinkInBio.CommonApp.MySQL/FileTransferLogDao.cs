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
    public class FileTransferLogDao : GenericDao<FileTransferLog>, IFileTransferLogDao
    {

        private string dataSource;

        public FileTransferLogDao(string dataSource)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentNullException();
            }
            this.dataSource = dataSource;
        }

        public override bool Save(FileTransferLog entity)
        {
            return DbTemplate.Save(dataSource,
                (command) =>
                {
                    command.CommandText = @"insert into cyFileTransferLog (id,direction,title,path,size,user,creation) 
                                                    values (NULL,@direction,@title,@path,@size,@user,@creation)";
                    command.Parameters.Add(DbFactory.CreateParameter("direction", entity.Direction));
                    command.Parameters.Add(DbFactory.CreateParameter("title", entity.Title));
                    command.Parameters.Add(DbFactory.CreateParameter("path", entity.Path));
                    command.Parameters.Add(DbFactory.CreateParameter("size", entity.Size));
                    command.Parameters.Add(DbFactory.CreateParameter("user", entity.User));
                    command.Parameters.Add(DbFactory.CreateParameter("creation", entity.Creation));
                },
                (id) =>
                {
                    entity.Id = id;
                });
        }

        public override void Save(ICollection<FileTransferLog> col)
        {
            DbTemplate.Save(dataSource,
                (command) =>
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.Append("insert into cyFileTransferLog (id,direction,title,path,size,user,creation) values ");
                    for (int i = 0; i < col.Count; i++)
                    {
                        FileTransferLog entity = col.ElementAt(i);
                        buffer.Append("(NULL,")
                            .Append("@direction").Append(i).Append(",")
                            .Append("@title").Append(i).Append(",")
                            .Append("@path").Append(i).Append(",")
                            .Append("@size").Append(i).Append(",")
                            .Append("@user").Append(i).Append(",")
                            .Append("@creation").Append(i).Append("),");
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("direction{0}", i), entity.Direction));
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("title{0}", i), entity.Title));
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("path{0}", i), entity.Path));
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("size{0}", i), entity.Size));
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("user{0}", i), entity.User));
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("creation{0}", i), entity.Creation));
                    }
                    buffer.Length = buffer.Length - 1;
                    command.CommandText = buffer.ToString();
                });
        }

        public override bool Update(FileTransferLog entity)
        {
            return DbTemplate.UpdateOrDelete(dataSource,
                (command) =>
                {
                    command.CommandText = @"update cyFileTransferLog 
                                                set isRemoved=@isRemoved
                                                where id=@id";
                    command.Parameters.Add(DbFactory.CreateParameter("isRemoved", entity.IsRemoved));
                    command.Parameters.Add(DbFactory.CreateParameter("id", entity.Id));
                });
        }

        public override FileTransferLog Get(object id)
        {
            return DbTemplate.Get<FileTransferLog>(dataSource,
                (command) =>
                {
                    command.CommandText = @"select id,direction,title,path,size,isRemoved,user,creation from cyFileTransferLog 
                                                where id=@id";
                    command.Parameters.Add(DbFactory.CreateParameter("id", id));
                },
                (reader) =>
                {
                    return Populate(reader);
                });
        }

        public FileTransferLog Get(string path)
        {
            return DbTemplate.Get<FileTransferLog>(dataSource,
                (command) =>
                {
                    command.CommandText = @"select id,direction,title,path,size,isRemoved,user,creation from cyFileTransferLog 
                                                where path=@path";
                    command.Parameters.Add(DbFactory.CreateParameter("path", path));
                },
                (reader) =>
                {
                    return Populate(reader);
                });
        }

        public long GetCount(DateTime? startTime, DateTime? endTime, string user, FileTransferDirection? direction)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            return DbTemplate.GetCount(dataSource,
                (command) =>
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select count(id) from cyFileTransferLog ");
                    BuildSql(sql, parameters, startTime, endTime, user, direction);
                    command.CommandText = sql.ToString();
                },
                parameters);
        }

        public IList<FileTransferLog> GetList(DateTime? startTime, DateTime? endTime, string user, FileTransferDirection? direction, 
            bool asc, int startRowIndex, int maxRowsCount)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            return DbTemplate.GetList<FileTransferLog>(dataSource,
                (command) =>
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select id,direction,title,path,size,isRemoved,user,creation from cyFileTransferLog ");
                    BuildSql(sql, parameters, startTime, endTime, user, direction);
                    sql.Append(" order by creation ");
                    if (!asc)
                    {
                        sql.Append(" desc ");
                    }
                    command.CommandText = sql.ToString();
                },
                parameters,
                (reader) =>
                {
                    return Populate(reader);
                });
        }

        private void BuildSql(StringBuilder sql, List<KeyValuePair<string, object>> parameters,
            DateTime? startTime, DateTime? endTime, string user, FileTransferDirection? direction)
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
            if (!string.IsNullOrWhiteSpace(user))
            {
                SQLHelper.AppendOp(sql, parameters);
                sql.Append(" user=@user ");
                parameters.Add(new KeyValuePair<string, object>("user", user));
            }
            if (direction.HasValue)
            {
                SQLHelper.AppendOp(sql, parameters);
                sql.Append(" direction=@direction ");
                parameters.Add(new KeyValuePair<string, object>("direction", direction.Value));
            }
        }

        private FileTransferLog Populate(IDataReader reader)
        {
            FileTransferLog entity = new FileTransferLog();
            entity.Id = reader.GetInt64(0);
            entity.Direction = (FileTransferDirection)reader.GetInt32(1);
            entity.Title = reader.GetString(2);
            entity.Path = reader.GetString(3);
            entity.Size = reader.GetInt64(4);
            entity.IsRemoved = reader.GetBoolean(5);
            entity.User = reader.GetString(6);
            entity.Creation = reader.GetDateTime(7);

            return entity;
        }

    }
}
