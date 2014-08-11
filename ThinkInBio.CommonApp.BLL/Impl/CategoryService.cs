using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.CommonApp.DAL;

namespace ThinkInBio.CommonApp.BLL.Impl
{

    public class CategoryService : ICategoryService
    {

        internal ICategoryDao CategoryDao { get; set; }

        public void SaveCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException();
            }
            CategoryDao.Save(category);
        }

        public void UpdateCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException();
            }
            CategoryDao.Update(category);
        }

        public void DeleteCategory(long id)
        {
            Category category = GetCategory(id);
            if (category == null)
            {
                throw new ObjectNotFoundException(id);
            }
            CategoryDao.Delete(category);
        }

        public Category GetCategory(long id)
        {
            if (id < 1)
            {
                throw new ArgumentException();
            }
            return CategoryDao.Get(id);
        }

        public Category GetCategory(string scope, string code)
        {
            if (string.IsNullOrWhiteSpace(scope) || string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException();
            }

            return CategoryDao.Get(scope, code);
        }

        public bool IsCodeExist(string scope, string code)
        {
            Category category = GetCategory(scope, code);
            return (category != null);
        }

        public IList<Category> GetCategoryList(string scope)
        {
            return GetCategoryList(scope, null, null);
        }

        public IList<Category> GetCategoryList(string scope, long? parentId)
        {
            return GetCategoryList(scope, parentId, null);
        }

        public IList<Category> GetCategoryList(string scope, long? parentId, bool? isDisused)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new ArgumentNullException();
            }

            return CategoryDao.GetList(scope, parentId, isDisused, null);
        }

        public IList<Category> GetOrderedCategoryList(string scope)
        {
            return GetOrderedCategoryList(scope, null, null);
        }

        public IList<Category> GetOrderedCategoryList(string scope, long? parentId)
        {
            return GetOrderedCategoryList(scope, parentId, null);
        }

        public IList<Category> GetOrderedCategoryList(string scope, long? parentId, bool? isDisused)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new ArgumentNullException();
            }

            return CategoryDao.GetList(scope, parentId, isDisused, true);
        }


    }

}
