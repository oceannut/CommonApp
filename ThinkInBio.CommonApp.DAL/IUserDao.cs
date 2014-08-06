using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Data;

namespace ThinkInBio.CommonApp.DAL
{

    public interface IUserDao : IDao<User>
    {

        string GetPwd(string username);

        bool UpdateWithRoles(User entity);

        IList<User> GetList();

        bool SaveRole(string username, string role);

        bool DeleteRole(string username, string role);

    }

}
