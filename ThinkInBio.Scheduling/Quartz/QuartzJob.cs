using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Q = Quartz;

namespace ThinkInBio.Scheduling.Quartz
{
    public abstract class QuartzJob : IJob, Q.IJob
    {

        internal Q.IJob Job { get; set; }

        public event Action Running;

        public event Action Completed;

        public event Action<Exception> Error;

        public abstract void Run();

        public void Execute(Q.IJobExecutionContext context)
        {
            try
            {
                if (Running != null)
                {
                    Running();
                }
                this.Run();
                if (Completed != null)
                {
                    Completed();
                }
            }
            catch (Exception ex)
            {
                if (Error != null)
                {
                    Error(ex);
                }
            }
        }

    }
}
