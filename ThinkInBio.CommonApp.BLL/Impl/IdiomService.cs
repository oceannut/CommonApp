using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.CommonApp;
using ThinkInBio.CommonApp.DAL;

namespace ThinkInBio.CommonApp.BLL.Impl
{
    public class IdiomService : IIdiomService
    {

        internal IIdiomDao IdiomDao { get; set; }

        public void SaveIdiom(Idiom idiom)
        {
            if (idiom == null)
            {
                throw new ArgumentNullException();
            }

            IdiomDao.Save(idiom);
        }

        public void UpdateIdiom(Idiom idiom)
        {
            if (idiom == null)
            {
                throw new ArgumentNullException();
            }

            IdiomDao.Update(idiom);
        }

        public void DeleteIdiom(long id)
        {
            Idiom idiom = GetIdiom(id);
            if (idiom == null)
            {
                throw new ObjectNotFoundException(id);
            }
            IdiomDao.Delete(idiom);
        }

        public Idiom GetIdiom(long id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException();
            }

            return IdiomDao.Get(id);
        }

        public IList<Idiom> GetIdiomList(string scope)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new ArgumentNullException();
            }
            return IdiomDao.GetList(null, null, scope, true, 0, int.MaxValue);
        }

    }
}
