using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp.WSL.Impl
{
    public class CategoryWcfService : ICategoryWcfService
    {

        public Category SaveCategory(string scope, string name)
        {
            if (string.IsNullOrWhiteSpace(scope) ||
                string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }

            return null;
        }

    }
}
