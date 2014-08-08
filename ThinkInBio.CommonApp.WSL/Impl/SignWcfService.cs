using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ServiceModel.Web;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.Common.ExceptionHandling;
using ThinkInBio.Common.Caching;
using ThinkInBio.CommonApp;
using ThinkInBio.CommonApp.BLL;
using R = ThinkInBio.CommonApp.WSL.Properties.Resources;

namespace ThinkInBio.CommonApp.WSL.Impl
{

    public class SignWcfService : ISignWcfService
    {

        internal IUserService UserService { get; set; }
        internal IAuthProvider AuthProvider { get; set; }
        internal IPasswordProvider PasswordProvider { get; set; }
        internal IExceptionHandler ExceptionHandler { get; set; }
        internal ICache Session { get; set; }
        internal IList<string> DefaultRoles { get; set; }

        public User SignIn(string username, string pwd)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new WebFaultException<string>(R.EmptyUsername, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(pwd))
            {
                throw new WebFaultException<string>(R.EmptyPwd, HttpStatusCode.BadRequest);
            }

            try
            {
                User user = UserService.GetUser(username);
                if (user == null)
                {
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
                user.Pwd = UserService.GetPwd(username);

                if (!user.Authenticate(pwd, AuthProvider))
                {
                    throw new WebFaultException(HttpStatusCode.Forbidden);
                }
                user.LastLogin = DateTime.Now;
                Session.Add(username, user.Pwd);

                return user;
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public bool IsUsernameExist(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new WebFaultException<string>(R.EmptyUsername, HttpStatusCode.BadRequest);
            }
            try
            {
                return UserService.IsUserExist(username);
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public void SignUp(string username, string pwd, string name)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new WebFaultException<string>(R.EmptyUsername, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(pwd))
            {
                throw new WebFaultException<string>(R.EmptyPwd, HttpStatusCode.BadRequest);
            }

            try
            {
                User user = new User(username, pwd, PasswordProvider);
                user.Name = name;
                user.Roles = DefaultRoles;
                user.Save(
                    (e) =>
                    {
                        return UserService.IsUserExist(e);
                    },
                    (e) =>
                    {
                        UserService.SaveUser(e);
                    });
            }
            catch (ObjectAlreadyExistedException)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.Forbidden);
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }
    }

}
