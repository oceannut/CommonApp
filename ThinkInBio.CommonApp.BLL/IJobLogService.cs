using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.CommonApp;

namespace ThinkInBio.CommonApp.BLL
{

    public interface IJobLogService
    {

        void SaveJobLog(JobLog log);

        long GetJobLogCount(DateTime? startTime, DateTime? endTime, string scope);

        IList<JobLog> GetJobLogList(DateTime? startTime, DateTime? endTime, string scope,
            int startRowIndex, int maxRowsCount);

    }

}
