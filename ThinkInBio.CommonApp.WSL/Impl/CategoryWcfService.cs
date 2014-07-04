using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.CommonApp.BLL;

namespace ThinkInBio.CommonApp.WSL.Impl
{
    public class CategoryWcfService : ICategoryWcfService
    {

        internal ICategoryService CategoryService { get; set; }

        public Category SaveCategory(string scope, string name, string code, 
            string description, string icon, string sequence)
        {
            return SaveCategory(scope, null, name, code, description, icon, sequence);
        }

        public Category SaveCategory(string scope, string parentId, string name, string code, 
            string description, string icon, string sequence)
        {
            if (string.IsNullOrWhiteSpace(scope) ||
                string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }
            if (!CategoryService.IsScopeExist(scope))
            {
                throw new ArgumentException();
            }
            int sequenceInt= 0;
            try
            {
                sequenceInt = Convert.ToInt32(sequence);
            }
            catch (FormatException)
            {
            }
            catch (OverflowException)
            {
            }
            long parentLong = 0;
            if (!string.IsNullOrWhiteSpace(parentId))
            {
                try
                {
                    parentLong = Convert.ToInt64(parentId);
                }
                catch (FormatException)
                {
                }
                catch (OverflowException)
                {
                }
            }

            Category category = new Category();
            category.Scope = scope;
            category.ChangeParent(parentLong, null, null);
            category.Name = name;
            category.Code = code;
            category.Description = description;
            category.ChangeSequence(sequenceInt);
            category.Save(null, 
                (e) =>
                {
                    CategoryService.SaveCategory(e);
                });

            return category;
        }



        public Category[] GetCategoryList(string scope)
        {
            return GetCategoryList(scope, null);
        }

        public Category[] GetCategoryList(string scope, string parentId)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new ArgumentNullException();
            }

            long? _parentId = string.IsNullOrWhiteSpace(parentId) ? null : new long?(Convert.ToInt64(parentId));
            IList<Category> list = CategoryService.GetCategoryList(scope, _parentId);
            if(list != null){
                   return list.ToArray();
            }else{
                return null;
            }
        }
    }

}
