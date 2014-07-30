using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ServiceModel.Web;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.Common.ServiceModel;
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

        public void SignIn(string username, string pwd)
        {
            if (string.IsNullOrWhiteSpace(username)
                || string.IsNullOrWhiteSpace(pwd))
            {
                throw new WebFaultException<ServiceResponse>(ServiceResponse.Build(ServiceResponseCode.ArgumentNullException, R.EmptyUsernameOrPwd),
                    HttpStatusCode.BadRequest);
            }

            User user = UserService.GetUser(username);
            if (user == null)
            {
                throw new WebFaultException(HttpStatusCode.NotFound);
            }

            if (!user.Authenticate(pwd, AuthProvider))
            {
                throw new WebFaultException(HttpStatusCode.Forbidden);
            }
        }

        public void SignUp(string username, string pwd, string name)
        {
            if (string.IsNullOrWhiteSpace(username)
                || string.IsNullOrWhiteSpace(pwd))
            {
                throw new WebFaultException<ServiceResponse>(ServiceResponse.Build(ServiceResponseCode.ArgumentNullException, R.EmptyUsernameOrPwd),
                    HttpStatusCode.BadRequest);
            }

            try
            {
                User user = new User(username, pwd, PasswordProvider);
                user.Name = name;
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
        }
    }

}
