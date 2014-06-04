using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp.BLL
{
    public interface IUserService
    {

        void SaveUser(User user);

        User GetUser(string username);

        bool IsUserExist(string username);

        string GetPwd(string username);

    }
}
