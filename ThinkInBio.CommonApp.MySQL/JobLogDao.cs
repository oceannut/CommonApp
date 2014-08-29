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
    public class JobLogDao : GenericDao<JobLog>, IJobLogDao
    {

        private string dataSource;

        public JobLogDao(string dataSource)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentNullException();
            }
            this.dataSource = dataSource;
        }

        public override bool Save(JobLog entity)
        {
            return DbTemplate.Save(dataSource,
                (command) =>
                {
                    command.CommandText = @"insert into cyJobLog (id,scope,count,timestamp,creation) 
                                                values (NULL,@scope,@count,@timestamp,@creation)";
                    command.Parameters.Add(DbFactory.CreateParameter("scope", entity.Scope));
                    command.Parameters.Add(DbFactory.CreateParameter("count", entity.Count));
                    command.Parameters.Add(DbFactory.CreateParameter("timestamp", entity.Timestamp));
                    command.Parameters.Add(DbFactory.CreateParameter("creation", entity.Creation));
                },
                (id) =>
                {
                    entity.Id = id;
                });
        }

        public long GetCount(DateTime? startTime, DateTime? endTime, string scope)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            return DbTemplate.GetCount(dataSource,
                (command) =>
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select count(id) from cyJobLog ");
                    BuildSql(sql, parameters, startTime, endTime, scope);
                    command.CommandText = sql.ToString();
                },
                parameters);
        }

        public IList<JobLog> GetList(DateTime? startTime, DateTime? endTime, string scope, bool asc, int startRowIndex, int maxRowsCount)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            return DbTemplate.GetList<JobLog>(dataSource,
                (command) =>
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select id,scope,count,timestamp,creation from cyJobLog ");
                    BuildSql(sql, parameters, startTime, endTime, scope);
                    sql.Append(" order by timestamp ");
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
                    JobLog entity = new JobLog();
                    entity.Id = reader.GetInt64(0);
                    entity.Scope = reader.GetString(1);
                    entity.Count = reader.GetInt32(2);
                    entity.Timestamp = reader.GetDateTime(3);
                    entity.Creation = reader.GetDateTime(4);

                    return entity;
                });
        }

        private void BuildSql(StringBuilder sql, List<KeyValuePair<string, object>> parameters,
            DateTime? startTime, DateTime? endTime, string scope)
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
            if (!string.IsNullOrWhiteSpace(scope))
            {
                SQLHelper.AppendOp(sql, parameters);
                sql.Append(" scope=@scope ");
                parameters.Add(new KeyValuePair<string, object>("scope", scope));
            }
        }

    }
}
