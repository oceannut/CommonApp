using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Data;

namespace ThinkInBio.CommonApp.DAL
{

    public interface IIdiomDao : IDao<Idiom>
    {

        IList<Idiom> GetList(DateTime? startTime, DateTime? endTime, string scope,
            bool asc, int startRowIndex, int maxRowsCount);

    }

}
