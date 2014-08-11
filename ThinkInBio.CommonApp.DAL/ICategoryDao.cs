using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Data;
using ThinkInBio.CommonApp;

namespace ThinkInBio.CommonApp.DAL
{

    public interface ICategoryDao : IDao<Category>
    {

        Category Get(string scope, string code);

        IList<Category> GetList(string scope, long? parentId, bool? isDisused, bool? asc);

    }

}
