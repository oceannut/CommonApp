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
            return DbTemplate.Save(dataSource,
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
        }

        public override bool Delete(User entity)
        {
            return DbTemplate.UpdateOrDelete(dataSource,
                (command) =>
                {
                    command.CommandText = @"delete from cyUser 
                                                where username=@username";
                    command.Parameters.Add(DbFactory.CreateParameter("username", entity.Username));
                });
        }

        public override User Get(object id)
        {
            return DbTemplate.Get<User>(dataSource,
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
