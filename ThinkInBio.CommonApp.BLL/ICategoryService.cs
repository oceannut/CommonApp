using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp.BLL
{

    public interface ICategoryService
    {

        void SaveCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteCategory(long id);

        Category GetCategory(long id);

        Category GetCategory(string scope, string code);

        bool IsCodeExist(string scope, string code);

        IList<Category> GetCategoryList(string scope);

        IList<Category> GetCategoryList(string scope, long? parentId);

        IList<Category> GetOrderedCategoryList(string scope);

        IList<Category> GetOrderedCategoryList(string scope, long? parentId);

    }

}
