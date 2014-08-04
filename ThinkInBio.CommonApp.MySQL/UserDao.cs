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

    public class UserDao : GenericDao<User>, IUserDao
    {

        private string dataSource;

        public UserDao(string dataSource)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentNullException();
            }
            this.dataSource = dataSource;
        }

        public override bool Save(User entity)
        {
            bool saved = DbTemplate.Save(dataSource,
                (command) =>
                {
                    command.CommandText = @"insert into cyUser (username,pwd,name,_group,creation,modification) 
                                                values (@username,@pwd,@name,@group,@creation,@modification)";
                    command.Parameters.Add(DbFactory.CreateParameter("username", entity.Username));
                    command.Parameters.Add(DbFactory.CreateParameter("pwd", entity.Pwd));
                    command.Parameters.Add(DbFactory.CreateParameter("name", entity.Name));
                    command.Parameters.Add(DbFactory.CreateParameter("group", entity.Group));
                    command.Parameters.Add(DbFactory.CreateParameter("creation", entity.Creation));
                    command.Parameters.Add(DbFactory.CreateParameter("modification", entity.Modification));
                });

            saved = saved & SaveRoles(entity.Username, entity.Roles);

            return saved;
        }

        public override bool Update(User entity)
        {
            bool modified = DbTemplate.UpdateOrDelete(dataSource,
                (command) =>
                {
                    command.CommandText = @"update cyUser
                                                set name=@name,_group=@group,modification=@modification";
                    command.Parameters.Add(DbFactory.CreateParameter("name", entity.Name));
                    command.Parameters.Add(DbFactory.CreateParameter("group", entity.Group));
                    command.Parameters.Add(DbFactory.CreateParameter("creation", entity.Creation));
                });

            modified = modified & DeleteRoles(entity.Username);
            modified = modified & SaveRoles(entity.Username, entity.Roles);

            return modified;
        }

        public override bool Delete(User entity)
        {
            bool removed = DbTemplate.UpdateOrDelete(dataSource,
                (command) =>
                {
                    command.CommandText = @"delete from cyUser 
                                                where username=@username";
                    command.Parameters.Add(DbFactory.CreateParameter("username", entity.Username));
                });

            removed = removed & DeleteRoles(entity.Username);

            return removed;
        }

        public override User Get(object id)
        {
            User user = DbTemplate.Get<User>(dataSource,
                (command) =>
                {
                    command.CommandText = @"select username,name,_group,creation,modification from cyUser 
                                                where username=@username";
                    command.Parameters.Add(DbFactory.CreateParameter("username", id));
                },
                (reader) =>
                {
                    return Populate(reader);
                });

            if (user != null)
            {
                user.Roles = GetRoles(user.Username);
            }

            return user;
        }

        public override bool IsExist(object id)
        {
            int count = DbTemplate.GetCount(dataSource,
                (command) =>
                {
                    command.CommandText = @"select count(username) from cyUser 
                                                where username=@username";
                    command.Parameters.Add(DbFactory.CreateParameter("username", id));
                });
            return (count > 0);
        }

        public IList<User> GetList()
        {
            return DbTemplate.GetList<User>(dataSource,
                (command) =>
                {
                    command.CommandText = "select username,name,_group,creation,modification from cyUser";
                },
                (reader) =>
                {
                    return Populate(reader);
                });
        }

        public string GetPwd(string username)
        {
            return DbTemplate.Get<string>(dataSource,
                (command) =>
                {
                    command.CommandText = @"select pwd from cyUser 
                                                        where username=@username";
                    command.Parameters.Add(DbFactory.CreateParameter("username", username));
                },
                (reader) =>
                {
                    return reader.IsDBNull(0) ? null : reader.GetString(0);
                });
        }

        private bool SaveRoles(string username, IList<string> roles)
        {
            if (roles != null && roles.Count > 0)
            {
                return DbTemplate.Save(dataSource,
                    (command) =>
                    {
                        StringBuilder buffer = new StringBuilder();
                        buffer.Append("insert into cyUserRole (username,_role) values ");
                        for (int i = 0; i < roles.Count; i++)
                        {
                            buffer.Append("('").Append(username).Append("',")
                                .Append("@role").Append(i).Append("),");
                            command.Parameters.Add(DbFactory.CreateParameter(string.Format("role{0}", i), roles[i]));
                        }
                        buffer.Length = buffer.Length - 1;
                        command.CommandText = buffer.ToString();
                    });
            }
            return true;
        }

        private bool DeleteRoles(string username)
        {
            return DbTemplate.UpdateOrDelete(dataSource,
                (command) =>
                {
                    command.CommandText = @"delete from cyUserRole 
                                                where username=@username";
                    command.Parameters.Add(DbFactory.CreateParameter("username", username));
                });
        }

        public IList<string> GetRoles(string username)
        {
            return DbTemplate.GetList<string>(dataSource,
                (command) =>
                {
                    command.CommandText = @"select _role from cyUserRole 
                                                where username=@username";
                    command.Parameters.Add(DbFactory.CreateParameter("username", username));
                },
                (reader) =>
                {
                    return reader.GetString(0);
                });
        }

        private User Populate(IDataReader reader)
        {
            User entity = new User(reader.GetString(0),
                reader.IsDBNull(1) ? null : reader.GetString(1),
                reader.IsDBNull(2) ? null : reader.GetString(2),
                reader.GetDateTime(3),
                reader.GetDateTime(4));

            return entity;
        }

    }

}
