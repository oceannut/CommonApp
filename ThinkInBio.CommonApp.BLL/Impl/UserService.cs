using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.CommonApp;
using ThinkInBio.CommonApp.DAL;

namespace ThinkInBio.CommonApp.BLL.Impl
{
    public class UserService : IUserService
    {

        internal IUserDao UserDao { get; set; }

        public void SaveUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            UserDao.Save(user);
        }

        public User GetUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException();
            }

            return UserDao.Get(username);
        }

        public bool IsUserExist(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException();
            }

            return UserDao.IsExist(username);
        }

        public string GetPwd(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException();
            }

            return UserDao.GetPwd(username);
        }

    }
}
