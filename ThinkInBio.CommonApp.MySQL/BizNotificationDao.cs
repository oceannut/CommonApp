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

    public class BizNotificationDao : GenericDao<BizNotification>, IBizNotificationDao
    {

        private string dataSource;

        public BizNotificationDao(string dataSource)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentNullException();
            }
            this.dataSource = dataSource;
        }

        public override bool Save(BizNotification entity)
        {
            return DbTemplate.Save(dataSource,
                (command) =>
                {
                    command.CommandText = @"insert into cyBizNotification (id,sender,receiver,content,creation,_resource,resourceId) 
                                                values (NULL,@sender,@receiver,@content,@creation,@resource,@resourceId)";
                    command.Parameters.Add(DbFactory.CreateParameter("sender", entity.Sender));
                    command.Parameters.Add(DbFactory.CreateParameter("receiver", entity.Receiver));
                    command.Parameters.Add(DbFactory.CreateParameter("content", entity.Content));
                    command.Parameters.Add(DbFactory.CreateParameter("creation", entity.Creation));
                    command.Parameters.Add(DbFactory.CreateParameter("resource", entity.Resource));
                    command.Parameters.Add(DbFactory.CreateParameter("resourceId", entity.ResourceId));
                },
                (id) =>
                {
                    entity.Id = id;
                });
        }

        public override void Save(ICollection<BizNotification> col)
        {
            DbTemplate.Save(dataSource,
                (command) =>
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.Append("insert into cyBizNotification (id,sender,receiver,content,creation,_resource,resourceId) values ");
                    for (int i = 0; i < col.Count; i++)
                    {
                        BizNotification notification = col.ElementAt(i);
                        buffer.Append("(NULL,")
                            .Append("@sender").Append(i).Append(",")
                            .Append("@receiver").Append(i).Append(",")
                            .Append("@content").Append(i).Append(",")
                            .Append("@creation").Append(i).Append(",")
                            .Append("@resource").Append(i).Append(",")
                            .Append("@resourceId").Append(i).Append("),");
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("sender{0}", i), notification.Sender));
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("receiver{0}", i), notification.Receiver));
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("content{0}", i), notification.Content));
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("creation{0}", i), notification.Creation));
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("resource{0}", i), notification.Resource));
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("resourceId{0}", i), notification.ResourceId));
                    }
                    buffer.Length = buffer.Length - 1;
                    command.CommandText = buffer.ToString();
                });
        }

        public override bool Update(BizNotification entity)
        {
            return DbTemplate.UpdateOrDelete(dataSource,
                (command) =>
                {
                    command.CommandText = @"update cyBizNotification 
                                                set review=@review
                                                where id=@id";
                    command.Parameters.Add(DbFactory.CreateParameter("review", entity.Review));
                    command.Parameters.Add(DbFactory.CreateParameter("id", entity.Id));
                });
        }

        public override bool Delete(BizNotification entity)
        {
            return DbTemplate.UpdateOrDelete(dataSource,
                (command) =>
                {
                    command.CommandText = @"delete from cyBizNotification 
                                                where id=@id";
                    command.Parameters.Add(DbFactory.CreateParameter("id", entity.Id));
                });
        }

        public override BizNotification Get(object id)
        {
            return DbTemplate.Get<BizNotification>(dataSource,
                (command) =>
                {
                    command.CommandText = @"select id,sender,receiver,content,creation,review,_resource,resourceId from cyBizNotification 
                                                where id=@id";
                    command.Parameters.Add(DbFactory.CreateParameter("id", id));
                },
                (reader) =>
                {
                    return Populate(reader);
                });
        }

        public int GetCount(DateTime? startTime, DateTime? endTime, bool? isReceived,
            string sender, string receiver, string resource, string resourceId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            return DbTemplate.GetCount(dataSource,
                (command) =>
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select count(t.id) from cyBizNotification t ");
                    BuildSql(sql, parameters, startTime, endTime, isReceived, sender, receiver, resource, resourceId);
                    command.CommandText = sql.ToString();
                },
                parameters);
        }

        public IList<BizNotification> GetList(DateTime? startTime, DateTime? endTime, bool? isReceived,
            string sender, string receiver, string resource, string resourceId, 
            bool asc, int startRowIndex, int maxRowsCount)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            return DbTemplate.GetList<BizNotification>(dataSource,
                (command) =>
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select t.id,t.sender,t.receiver,t.content,t.creation,t.review,t._resource,t.resourceId from cyBizNotification t ");
                    BuildSql(sql, parameters, startTime, endTime, isReceived, sender, receiver, resource, resourceId);
                    sql.Append(" order by t.creation ");
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

        private void BuildSql(StringBuilder sql, List<KeyValuePair<string, object>> parameters,
            DateTime? startTime, DateTime? endTime, bool? isReceived,
            string sender, string receiver, string resource, string resourceId)
        {
            if (startTime.HasValue && startTime.Value != DateTime.MinValue
                    && endTime.HasValue && endTime.Value != DateTime.MinValue
                    && endTime.Value > startTime.Value)
            {
                SQLHelper.AppendOp(sql, parameters);
                sql.Append(" t.creation between @startTime and @endTime ");
                parameters.Add(new KeyValuePair<string, object>("startTime", startTime.Value));
                parameters.Add(new KeyValuePair<string, object>("endTime", endTime.Value));
            }
            if (!string.IsNullOrWhiteSpace(sender))
            {
                SQLHelper.AppendOp(sql, parameters);
                sql.Append(" t.sender=@sender ");
                parameters.Add(new KeyValuePair<string, object>("sender", sender));
            }
            if (!string.IsNullOrWhiteSpace(receiver))
            {
                SQLHelper.AppendOp(sql, parameters);
                sql.Append(" t.receiver=@receiver ");
                parameters.Add(new KeyValuePair<string, object>("receiver", receiver));
            }
            if (!string.IsNullOrWhiteSpace(resourceId))
            {
                SQLHelper.AppendOp(sql, parameters);
                sql.Append(" t.resourceId=@resourceId ");
                parameters.Add(new KeyValuePair<string, object>("resourceId", resourceId));
            }
            if (!string.IsNullOrWhiteSpace(resource))
            {
                SQLHelper.AppendOp(sql, parameters);
                sql.Append(" t._resource=@resource ");
                parameters.Add(new KeyValuePair<string, object>("resource", resource));
            }
            if (isReceived.HasValue)
            {
                SQLHelper.AppendOp(sql, parameters);
                if (isReceived.Value)
                {
                    sql.Append(" t.review is not null ");
                }
                else
                {
                    sql.Append(" t.review is null ");
                }
            }
        }

        private BizNotification Populate(IDataReader reader)
        {
            BizNotification entity = new BizNotification();
            entity.Id = reader.GetInt64(0);
            entity.Sender = reader.GetString(1);
            entity.Receiver = reader.GetString(2);
            entity.Content = reader.IsDBNull(3) ? null : reader.GetString(3);
            entity.Creation = reader.GetDateTime(4);
            entity.Review = reader.IsDBNull(5) ? new DateTime?() : reader.GetDateTime(5);
            entity.Resource = reader.GetString(6);
            entity.ResourceId = reader.GetString(7);

            return entity;
        }

    }

}
