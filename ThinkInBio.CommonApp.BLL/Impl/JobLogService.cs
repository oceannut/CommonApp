using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.CommonApp;
using ThinkInBio.CommonApp.DAL;

namespace ThinkInBio.CommonApp.BLL.Impl
{
    public class JobLogService : IJobLogService
    {

        internal IJobLogDao JobLogDao { get; set; }

        public void SaveJobLog(JobLog log)
        {
            if (log == null)
            {
                throw new ArgumentNullException();
            }

            JobLogDao.Save(log);
        }

        public long GetJobLogCount(DateTime? startTime, DateTime? endTime, string scope)
        {
            return JobLogDao.GetCount(startTime, endTime, scope);
        }

        public IList<JobLog> GetJobLogList(DateTime? startTime, DateTime? endTime, string scope, int startRowIndex, int maxRowsCount)
        {
            return JobLogDao.GetList(startTime, endTime, scope, false, startRowIndex, maxRowsCount);
        }

    }
}
