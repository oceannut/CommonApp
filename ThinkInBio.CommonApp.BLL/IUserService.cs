using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp.BLL
{
    public interface IUserService
    {

        void SaveUser(User user);

        void UpdateUser(User user);

        bool UpdateUserPassword(string username, string oldPwd, string newPwd);

        void DeleteUser(string username);

        User GetUser(string username);

        bool IsUserExist(string username);

        IList<User> GetUserList();

        string GetPwd(string username);

        void SaveRole(string username, string role);

        void DeleteRole(string username, string role);

    }
}
