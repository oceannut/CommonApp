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

        public SimpleSchedule(int repeatSeconds, S.IJob job)
        {
            this.repeatSeconds = repeatSeconds;
            this.timer = new Timer(repeatSeconds * 1000);
            this.timer.Elapsed += (o, e) =>
            {
                job.Run();
            };
        }

        public void Start()
        {
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

    }

}
