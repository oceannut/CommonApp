using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp.BLL
{

    public interface ISignService
    {

        bool SignIn(User user, string authPwd);

        bool SignOut(User user, string authPwd);

    }

}
