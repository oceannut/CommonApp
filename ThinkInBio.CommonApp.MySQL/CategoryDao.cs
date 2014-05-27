using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Data;
using ThinkInBio.CommonApp;
using ThinkInBio.CommonApp.DAL;

namespace ThinkInBio.CommonApp.MySQL
{

    public class CategoryDao : GenericDao<Category>, ICategoryDao
    {

        public override bool Save(Category entity)
        {
            throw new NotImplementedException();
        }

    }

}
