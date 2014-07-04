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

        public bool IsScopeExist(string scope)
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetCategoryList(string scope)
        {
            return GetCategoryList(scope, null);
        }

        public IList<Category> GetCategoryList(string scope, long? parentId)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new ArgumentNullException();
            }

            return CategoryDao.GetList(scope, parentId, null);
        }

        public IList<Category> GetOrderedCategoryList(string scope)
        {
            return GetOrderedCategoryList(scope, null);
        }

        public IList<Category> GetOrderedCategoryList(string scope, long? parentId)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new ArgumentNullException();
            }

            return CategoryDao.GetList(scope, parentId, true);
        }

    }

}
