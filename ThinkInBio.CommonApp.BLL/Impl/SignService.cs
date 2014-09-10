using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Caching;
using ThinkInBio.CommonApp.DAL;

namespace ThinkInBio.CommonApp.BLL.Impl
{
    public class SignService : ISignService
    {

        internal IAuthProvider AuthProvider { get; set; }
        internal ICache Session { get; set; }
        internal IUserDao UserDao { get; set; }

        public bool SignIn(User user, string authPwd)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            if (string.IsNullOrWhiteSpace(user.Username))
            {
                throw new InvalidOperationException();
            }

            user.Pwd = UserDao.GetPwd(user.Username);
            if (!user.Authenticate(authPwd, AuthProvider))
            {
                return false;
            }
            else
            {
                Session.Add(user.Username, user.Pwd);
                return true;
            }
        }

        public bool SignOut(User user, string authPwd)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            if (string.IsNullOrWhiteSpace(user.Username))
            {
                throw new InvalidOperationException();
            }

            bool authenticated = true;
            if (!string.IsNullOrWhiteSpace(authPwd))
            {
                user.Pwd = UserDao.GetPwd(user.Username);
                authenticated = user.Authenticate(authPwd, AuthProvider);
            }
            if(authenticated)
            {
                Session.Remove(user.Username);
            }
            return authenticated;
        }

    }
}
