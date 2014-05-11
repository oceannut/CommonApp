using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.CommonApp.DAL;

namespace ThinkInBio.CommonApp.BLL.Impl
{

    public class CategoryService : ICategoryService
    {

        internal ICategoryDao CategoryDao { get; set; }

        public void SaveCategory(Category category)
        {
            throw new NotImplementedException();
        }

    }

}
