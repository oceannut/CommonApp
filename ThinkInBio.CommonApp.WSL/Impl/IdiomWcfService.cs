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
    public class IdiomWcfService : IIdiomWcfService
    {

        internal IIdiomService IdiomService { get; set; }
        internal IExceptionHandler ExceptionHandler { get; set; }

        public Idiom SaveIdiom(string scope, string content)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new WebFaultException<string>(R.EmptyScope, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new WebFaultException<string>(R.EmptyContent, HttpStatusCode.BadRequest);
            }

            try
            {
                Idiom idiom = new Idiom();
                idiom.Scope = scope;
                idiom.Content = content;
                idiom.Save((e) =>
                {
                    IdiomService.SaveIdiom(e);
                });
                return idiom;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public Idiom UpdateIdiom(string scope, string id, string content)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new WebFaultException<string>(R.EmptyScope, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new WebFaultException<string>(R.EmptyContent, HttpStatusCode.BadRequest);
            }
            long idLong = 0;
            try
            {
                idLong = Convert.ToInt64(id);
            }
            catch
            {
                throw new WebFaultException<string>(R.InvalidId, HttpStatusCode.BadRequest);
            }

            try
            {
                Idiom idiom = IdiomService.GetIdiom(idLong);
                idiom.Scope = scope;
                idiom.Content = content;
                idiom.Update((e) =>
                {
                    IdiomService.UpdateIdiom(e);
                });
                return idiom;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public void DeleteIdiom(string scope, string id)
        {
            long idLong = 0;
            try
            {
                idLong = Convert.ToInt64(id);
            }
            catch
            {
                throw new WebFaultException<string>(R.InvalidId, HttpStatusCode.BadRequest);
            }

            try
            {
                IdiomService.DeleteIdiom(idLong);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public Idiom[] GetIdiomList(string scope)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new WebFaultException<string>(R.EmptyScope, HttpStatusCode.BadRequest);
            }

            try
            {
                IList<Idiom> list = IdiomService.GetIdiomList(scope);
                if (list != null)
                {
                    return list.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

    }
}
