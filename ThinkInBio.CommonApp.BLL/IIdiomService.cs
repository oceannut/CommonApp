using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.CommonApp;

namespace ThinkInBio.CommonApp.BLL
{
    public interface IIdiomService
    {

        void SaveIdiom(Idiom idiom);

        void UpdateIdiom(Idiom idiom);

        void DeleteIdiom(long id);

        Idiom GetIdiom(long id);

        IList<Idiom> GetIdiomList(string scope);

    }
}
