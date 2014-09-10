using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

using S = ThinkInBio.Scheduling;

namespace Test.ThinkInBio.Scheduling
{

    internal class SimpleSchedule : S.ISchedule, IDisposable
    {

        private int repeatSeconds;
        private Timer timer;

        public DateTime? StartTime
        {
            get { throw new NotImplementedException(); }
        }

        public int RepeatSeconds
        {
            get { return repeatSeconds; }
        }

        public int RepeatCount
        {
            get { throw new NotImplementedException(); }
        }

        public string Expression
        {
            get { throw new NotImplementedException(); }
        }

        public SimpleSchedule(int repeatSeconds)
        {
            this.repeatSeconds = repeatSeconds;
            this.timer = new Timer(repeatSeconds * 1000);
        }

        public void Start(S.IJob job)
        {
            this.timer.Elapsed += (o, e) =>
            {
                job.Run();
            };
            this.timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
        }

        public void Dispose()
        {
            if (this.timer != null)
            {
                this.timer.Dispose();
            }
        }


        public int DelayedSeconds
        {
            get { throw new NotImplementedException(); }
        }
    }

}
