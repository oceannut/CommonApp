using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ServiceModel.Web;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.Common.ExceptionHandling;
using ThinkInBio.CommonApp.BLL;
using R = ThinkInBio.CommonApp.WSL.Properties.Resources;

namespace ThinkInBio.CommonApp.WSL.Impl
{
    public class CategoryWcfService : ICategoryWcfService
    {

        internal ICategoryService CategoryService { get; set; }
        internal IExceptionHandler ExceptionHandler { get; set; }

        public Category SaveCategory(string scope, string parentId, string name, string code,
            string description, string icon, string sequence)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new WebFaultException<string>(R.EmptyScope, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new WebFaultException<string>(R.EmptyName, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new WebFaultException<string>(R.EmptyCode, HttpStatusCode.BadRequest);
            }

            int sequenceInt = 0;
            try
            {
                sequenceInt = Convert.ToInt32(sequence);
            }
            catch
            {
                throw new WebFaultException<string>(R.InvalidSequence, HttpStatusCode.BadRequest);
            }

            long parentLong = 0;
            if (!string.IsNullOrWhiteSpace(parentId))
            {
                try
                {
                    parentLong = Convert.ToInt64(parentId);
                }
                catch
                {
                    throw new WebFaultException<string>(R.InvalidId, HttpStatusCode.BadRequest);
                }
            }

            try
            {
                Category category = new Category();
                category.Scope = scope;
                category.ParentId = parentLong;
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
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public Category UpdateCategory(string scope, string id, string parentId, string name, string code, string description, string icon, string sequence)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new WebFaultException<string>(R.EmptyScope, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new WebFaultException<string>(R.EmptyName, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new WebFaultException<string>(R.EmptyCode, HttpStatusCode.BadRequest);
            }

            int idLong = 0;
            try
            {
                idLong = Convert.ToInt32(id);
            }
            catch
            {
                throw new WebFaultException<string>(R.InvalidId, HttpStatusCode.BadRequest);
            }

            int sequenceInt = 0;
            try
            {
                sequenceInt = Convert.ToInt32(sequence);
            }
            catch
            {
                throw new WebFaultException<string>(R.InvalidSequence, HttpStatusCode.BadRequest);
            }

            long parentLong = 0;
            if (!string.IsNullOrWhiteSpace(parentId))
            {
                try
                {
                    parentLong = Convert.ToInt64(parentId);
                }
                catch
                {
                    throw new WebFaultException<string>(R.InvalidId, HttpStatusCode.BadRequest);
                }
            }

            try
            {
                Category category = CategoryService.GetCategory(idLong);
                if (category == null)
                {
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
                category.ChangeParent(parentLong, null, null);
                category.Name = name;
                category.Code = code;
                category.Description = description;
                category.ChangeSequence(sequenceInt);
                category.Save(null,
                    (e) =>
                    {
                        CategoryService.UpdateCategory(e);
                    });

                return category;
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public void DeleteCategory(string scope, string id)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new WebFaultException<string>(R.EmptyScope, HttpStatusCode.BadRequest);
            }

            int idLong = 0;
            try
            {
                idLong = Convert.ToInt32(id);
            }
            catch
            {
                throw new WebFaultException<string>(R.InvalidId, HttpStatusCode.BadRequest);
            }

            try
            {
                CategoryService.DeleteCategory(idLong);
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public Category GetCategory(string scope, string id)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new WebFaultException<string>(R.EmptyScope, HttpStatusCode.BadRequest);
            }

            int idLong = 0;
            try
            {
                idLong = Convert.ToInt32(id);
            }
            catch
            {
                throw new WebFaultException<string>(R.InvalidId, HttpStatusCode.BadRequest);
            }

            try
            {
                return CategoryService.GetCategory(idLong);
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public Category GetCategoryByCode(string scope, string code)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new WebFaultException<string>(R.EmptyScope, HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrWhiteSpace(code))
            {
                throw new WebFaultException<string>(R.EmptyCode, HttpStatusCode.BadRequest);
            }

            try
            {
                return CategoryService.GetCategory(scope, code);
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public Category[] GetCategoryList(string scope)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new WebFaultException<string>(R.EmptyScope, HttpStatusCode.BadRequest);
            }

            try
            {
                IList<Category> list = CategoryService.GetCategoryList(scope, null);
                if (list != null)
                {
                    return list.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

    }

}
