using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Data;

namespace ThinkInBio.CommonApp.DAL
{

    public interface INoticeDao : IDao<Notice>
    {

        long GetCount(DateTime? startTime, DateTime? endTime);

        IList<Notice> GetList(DateTime? startTime, DateTime? endTime,
            bool asc, int startRowIndex, int maxRowsCount);

    }

}
