using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.CommonApp.BLL;

namespace ThinkInBio.CommonApp.WSL.Impl
{

    public class UserWcfService : IUserWcfService
    {

        internal IUserService UserService { get; set; }

        public User[] GetUserList()
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

    }

}
