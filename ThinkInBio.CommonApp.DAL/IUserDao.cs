using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Data;

namespace ThinkInBio.CommonApp.DAL
{

    public interface IUserDao : IDao<User>
    {

        //string GetPwd(string username);

        IList<User> GetList();

    }

}
