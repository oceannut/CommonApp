using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.CommonApp;
using ThinkInBio.CommonApp.BLL;

namespace ThinkInBio.CommonApp.WSL
{

    public interface ICategoryWcfService
    {

        Category SaveCategory(string scope, string name);

    }

}
