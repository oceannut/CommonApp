using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                    command.CommandText = @"insert into cyUser (username,pwd,email,name,group,creation,modification) 
                                                values (@username,@pwd,@email,@name,@group,@creation,@modification)";
                    command.Parameters.Add(DbFactory.CreateParameter("username", entity.Name));
                    command.Parameters.Add(DbFactory.CreateParameter("pwd", entity.EncryptPwd));
                    command.Parameters.Add(DbFactory.CreateParameter("email", entity.Email));
                    command.Parameters.Add(DbFactory.CreateParameter("name", entity.Name));
                    command.Parameters.Add(DbFactory.CreateParameter("group", entity.Group));
                    command.Parameters.Add(DbFactory.CreateParameter("creation", entity.Creation));
                    command.Parameters.Add(DbFactory.CreateParameter("modification", entity.Modification));
                });
        }

    }

}
