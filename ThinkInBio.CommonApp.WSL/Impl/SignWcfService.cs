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

        internal IPasswordProvider PasswordProvider { get; set; }
        internal IExceptionHandler ExceptionHandler { get; set; }
        internal IList<string> DefaultRoles { get; set; }
        internal IUserService UserService { get; set; }
        internal ISignService SignService { get; set; }

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
                bool authenticated = SignService.SignIn(user, pwd);
                if (!authenticated)
                {
                    throw new WebFaultException(HttpStatusCode.Forbidden);
                }
                else
                {
                    return user;
                }
            }
            catch (WebFaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public void SignOut(string username, string pwd)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new WebFaultException<string>(R.EmptyUsername, HttpStatusCode.BadRequest);
            }

            try
            {
                User user = UserService.GetUser(username);
                if (user == null)
                {
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
                bool authenticated = SignService.SignOut(user, pwd);
                if (!authenticated)
                {
                    throw new WebFaultException(HttpStatusCode.Forbidden);
                }
            }
            catch (WebFaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

    }

}
