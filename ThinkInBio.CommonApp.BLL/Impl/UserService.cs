﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Audit;
using ThinkInBio.Common.Caching;
using ThinkInBio.Common.Exceptions;
using ThinkInBio.CommonApp;
using ThinkInBio.CommonApp.DAL;

namespace ThinkInBio.CommonApp.BLL.Impl
{
    public class UserService : IUserService
    {

        internal IAuthProvider AuthProvider { get; set; }
        internal ICache Session { get; set; }
        internal IUserDao UserDao { get; set; }

        public void SaveUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            UserDao.Save(user);
        }

        [Trail("User", TrailMethodType.Update)]
        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            UserDao.Update(user);
        }

        public bool UpdateUserPassword(string username, string oldPwd, string newPwd)
        {
            if (string.IsNullOrWhiteSpace(username)
                || string.IsNullOrWhiteSpace(oldPwd)
                || string.IsNullOrWhiteSpace(newPwd))
            {
                throw new ArgumentNullException();
            }
            User user = GetUser(username);
            if (user == null)
            {
                throw new ObjectNotFoundException(username);
            }
            user.Pwd = UserDao.GetPwd(user.Username);
            bool authenticated = user.Authenticate(oldPwd, AuthProvider);
            if (authenticated)
            {
                UserDao.UpdatePwd(DateTime.Now, username, newPwd);
                Session.Remove(username);
                Session.Add(username, newPwd);
            }
            return authenticated;
        }

        public void DeleteUser(string username)
        {
            User user = GetUser(username);
            if (user == null)
            {
                throw new ObjectNotFoundException(username);
            }
            UserDao.Delete(user);
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

        public IList<User> GetUserList()
        {
            return UserDao.GetList();
        }

        public string GetPwd(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException();
            }

            return UserDao.GetPwd(username);
        }

        [Trail("Role", TrailMethodType.Create)]
        public void SaveRole(string username, string role)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentNullException();
            }

            UserDao.SaveRole(username, role);
        }

        [Trail("Role", TrailMethodType.Delete)]
        public void DeleteRole(string username, string role)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentNullException();
            }

            UserDao.DeleteRole(username, role);
        }


    }
}
