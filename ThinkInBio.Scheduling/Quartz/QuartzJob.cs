using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Q = Quartz;

namespace ThinkInBio.Scheduling.Quartz
{

    public class QuartzJob : Q.IJob
    {

        public void Execute(Q.IJobExecutionContext context)
        {
            IJob job = context.JobDetail.JobDataMap["job"] as IJob;
            if (job != null)
            {
                job.Run();
            }
        }

    }

}
