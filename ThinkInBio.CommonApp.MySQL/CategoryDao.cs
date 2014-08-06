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

    public class CategoryDao : GenericDao<Category>, ICategoryDao
    {

        private string dataSource;

        public CategoryDao(string dataSource)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentNullException();
            }
            this.dataSource = dataSource;
        }

        public override bool Save(Category entity)
        {
            return DbTemplate.Save(dataSource,
                (command) =>
                {
                    if (entity.ParentId > 0)
                    {
                        command.CommandText = @"insert into cyCategory (id,code,name,description,icon,parentId,sequence,scope) 
                                                    values (NULL,@code,@name,@description,@icon,@parentId,@sequence,@scope)";
                    }
                    else
                    {
                        command.CommandText = @"insert into cyCategory (id,code,name,description,icon,sequence,scope) 
                                                    values (NULL,@code,@name,@description,@icon,@sequence,@scope)";
                    }
                    command.Parameters.Add(DbFactory.CreateParameter("code", entity.Code));
                    command.Parameters.Add(DbFactory.CreateParameter("name", entity.Name));
                    command.Parameters.Add(DbFactory.CreateParameter("description", entity.Description));
                    command.Parameters.Add(DbFactory.CreateParameter("icon", entity.Icon));
                    if (entity.ParentId > 0)
                    {
                        command.Parameters.Add(DbFactory.CreateParameter("parentId", entity.ParentId));
                    }
                    command.Parameters.Add(DbFactory.CreateParameter("sequence", entity.Sequence));
                    command.Parameters.Add(DbFactory.CreateParameter("scope", entity.Scope));
                },
                (id) =>
                {
                    entity.Id = id;
                });
        }

        public override bool Update(Category entity)
        {
            return DbTemplate.UpdateOrDelete(dataSource,
                (command) =>
                {
                    if (entity.ParentId > 0)
                    {
                        command.CommandText = @"update cyCategory 
                                                set code=@code,name=@name,description=@description,icon=@icon,parentId=@parentId,sequence=@sequence
                                                where id=@id";
                    }
                    else
                    {
                        command.CommandText = @"update cyCategory 
                                                set code=@code,name=@name,description=@description,icon=@icon,parentId=null,sequence=@sequence
                                                where id=@id";
                    }
                    command.Parameters.Add(DbFactory.CreateParameter("code", entity.Code));
                    command.Parameters.Add(DbFactory.CreateParameter("name", entity.Name));
                    command.Parameters.Add(DbFactory.CreateParameter("description", entity.Description));
                    command.Parameters.Add(DbFactory.CreateParameter("icon", entity.Icon));
                    if (entity.ParentId > 0)
                    {
                        command.Parameters.Add(DbFactory.CreateParameter("parentId", entity.ParentId));
                    }
                    command.Parameters.Add(DbFactory.CreateParameter("sequence", entity.Sequence));
                    command.Parameters.Add(DbFactory.CreateParameter("id", entity.Id));
                });
        }

        public override bool Delete(Category entity)
        {
            return DbTemplate.UpdateOrDelete(dataSource,
                (command) =>
                {
                    command.CommandText = @"delete from cyCategory 
                                                where id=@id";
                    command.Parameters.Add(DbFactory.CreateParameter("id", entity.Id));
                });
        }

        public override Category Get(object id)
        {
            return DbTemplate.Get<Category>(dataSource,
                (command) =>
                {
                    command.CommandText = @"select id,code,name,description,icon,parentId,sequence,scope from cyCategory 
                                                where id=@id";
                    command.Parameters.Add(DbFactory.CreateParameter("id", id));
                },
                (reader) =>
                {
                    return Populate(reader);
                });
        }


        public Category Get(string scope, string code)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            IList<Category> list = DbTemplate.GetList<Category>(dataSource,
                (command) =>
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select id,code,name,description,icon,parentId,sequence,scope from cyCategory ");
                    BuildSql(sql, parameters, scope, code, 0);
                    command.CommandText = sql.ToString();
                },
                parameters,
                (reader) =>
                {
                    return Populate(reader);
                });
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public IList<Category> GetList(string scope, long? parentId, bool? asc)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            return DbTemplate.GetList<Category>(dataSource,
                (command) =>
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select id,code,name,description,icon,parentId,sequence,scope from cyCategory ");
                    BuildSql(sql, parameters, scope, null, parentId);

                    if (asc.HasValue)
                    {
                        sql.Append(" order by sequence ");
                        if (!asc.Value)
                        {
                            sql.Append(" desc ");
                        }
                    }
                    else
                    {
                        sql.Append(" order by code ");
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
            string scope, string code, long? parentId)
        {
            if (!string.IsNullOrWhiteSpace(scope))
            {
                SQLHelper.AppendOp(sql, parameters);
                sql.Append(" scope=@scope ");
                parameters.Add(new KeyValuePair<string, object>("scope", scope));
            }
            if (!string.IsNullOrWhiteSpace(code))
            {
                SQLHelper.AppendOp(sql, parameters);
                sql.Append(" code=@code ");
                parameters.Add(new KeyValuePair<string, object>("code", code));
            }
            if (parentId.HasValue)
            {
                if (parentId.Value == 0)
                {
                    SQLHelper.AppendOp(sql, parameters);
                    sql.Append(" parentId is null ");
                }
                else
                {
                    SQLHelper.AppendOp(sql, parameters);
                    sql.Append(" parentId=@parentId ");
                    parameters.Add(new KeyValuePair<string, object>("parentId", parentId.Value));
                }
            }
        }

        private Category Populate(IDataReader reader)
        {
            Category entity = new Category();
            entity.Id = reader.GetInt64(0);
            entity.Code = reader.GetString(1);
            entity.Name = reader.GetString(2);
            entity.Description = reader.IsDBNull(3) ? null : reader.GetString(3);
            entity.Icon = reader.IsDBNull(4) ? null : reader.GetString(4);
            entity.ParentId = reader.IsDBNull(5) ? 0 : reader.GetInt64(5);
            entity.Sequence = reader.GetInt32(6);
            entity.Scope = reader.GetString(7);

            return entity;
        }

    }

}
