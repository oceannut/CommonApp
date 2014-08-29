using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Data;

namespace ThinkInBio.CommonApp.DAL
{

    public interface IJobLogDao : IDao<JobLog>
    {

        long GetCount(DateTime? startTime, DateTime? endTime, string scope);

        IList<JobLog> GetList(DateTime? startTime, DateTime? endTime, string scope,
            bool asc, int startRowIndex, int maxRowsCount);

    }

}
