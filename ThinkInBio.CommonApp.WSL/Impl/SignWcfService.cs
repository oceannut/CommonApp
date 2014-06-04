using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public ServiceResponse SignIn(string username, string pwd)
        {
            if (string.IsNullOrWhiteSpace(username)
                || string.IsNullOrWhiteSpace(pwd))
            {
                return ServiceResponse.Build(ServiceResponseCode.ArgumentNullException, R.EmptyUsernameOrPwd);
            }

            User user = UserService.GetUser(username);
            if (user == null)
            {
                return ServiceResponse.Build(ServiceResponseCode.ObjectNotFoundException, R.NotFoundUser);
            }

            bool valid = user.Authenticate(pwd, AuthProvider);
            if (valid)
            {
                return ServiceResponse.BuildNormal();
            }
            else
            {
                return ServiceResponse.Build(ServiceResponseCode.AuthenticationFailure, R.WrongPwd);
            }
        }

        public ServiceResponse SignUp(string username, string pwd, string name)
        {
            if (string.IsNullOrWhiteSpace(username)
                || string.IsNullOrWhiteSpace(pwd))
            {
                return ServiceResponse.Build(ServiceResponseCode.ArgumentNullException, R.EmptyUsernameOrPwd);
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
                return ServiceResponse.BuildNormal();
            }
            catch (ObjectAlreadyExistedException)
            {
                return ServiceResponse.Build(ServiceResponseCode.ObjectAlreadyExistedException, R.ExistedUser);
            }
        }
    }

}
