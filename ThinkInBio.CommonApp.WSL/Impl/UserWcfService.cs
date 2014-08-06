using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ServiceModel.Web;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.Common.ExceptionHandling;
using ThinkInBio.CommonApp.BLL;
using R = ThinkInBio.CommonApp.WSL.Properties.Resources;

namespace ThinkInBio.CommonApp.WSL.Impl
{

    public class UserWcfService : IUserWcfService
    {

        internal IUserService UserService { get; set; }
        internal IExceptionHandler ExceptionHandler { get; set; }

        public User UpdateUser(string username, string name, string group)
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
                user.Name = name;
                user.Group = group;
                user.Update((e) =>
                {
                    UserService.UpdateUser(e);
                });

                return user;
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public void DeleteUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new WebFaultException<string>(R.EmptyUsername, HttpStatusCode.BadRequest);
            }
            try
            {
                UserService.DeleteUser(username);
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public User GetUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new WebFaultException<string>(R.EmptyUsername, HttpStatusCode.BadRequest);
            }
            try
            {
                return UserService.GetUser(username);
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public User[] GetUserList()
        {
            try
            {
                IList<User> list = UserService.GetUserList();
                if (list != null)
                {
                    return list.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public void AssignRole(string username, string role)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new WebFaultException<string>(R.EmptyUsername, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new WebFaultException<string>(R.EmptyRole, HttpStatusCode.BadRequest);
            }
            try
            {
                UserService.SaveRole(username, role);
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public void UnassignRole(string username, string role)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new WebFaultException<string>(R.EmptyUsername, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new WebFaultException<string>(R.EmptyRole, HttpStatusCode.BadRequest);
            }
            try
            {
                UserService.DeleteRole(username, role);
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

    }

}
