using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Scheduling
{

    /// <summary>
    /// Inactive -> Active (start scheduling);
    /// Active -> Inactive (stop scheduling);
    /// Active -> Running (run job);
    /// Running -> Active (complete job once);
    /// Running -> Inactive (interrupt job and stop scheduling);
    /// Running -> Error (exception occured when job is running);
    /// Error -> Active (resume scheduling).
    /// </summary>
    public enum SchedulerState
    {
        Inactive,
        Active,
        Running,
        Error
    }

    public class Scheduler : IDisposable
    {

        private ISchedule schedule;
        private IJob job;
        private SchedulerState state;
        private object lockObj = new object();

        public event Action<SchedulerState> StateChanged;

        public string Name { get; set; }

        public SchedulerState State
        {
            get { return state; }
            private set
            {
                if (state != value)
                {
                    state = value;
                    if (StateChanged != null)
                    {
                        StateChanged(state);
                    }
                }
            }
        }

        public bool IsAutoStart { get; set; }

        public DateTime? LastStartTime { get; private set; }

        public DateTime? LastStopTime { get; private set; }

        public Scheduler(ISchedule schedule, IJob job)
        {
            if (schedule == null)
            {
                throw new ArgumentNullException();
            }
            if (job == null)
            {
                throw new ArgumentNullException();
            }
            this.schedule = schedule;
            this.job = job;
            this.state = SchedulerState.Inactive;

            this.job.Running += new Action(job_Running);
            this.job.Completed += new Action(job_Completed);
            this.job.Error += new Action<Exception>(job_Error);
        }

        public Scheduler(string name, ISchedule schedule, IJob job)
            : this(schedule, job)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }
            this.Name = name;
        }

        public void Start()
        {
            lock (lockObj)
            {
                if (SchedulerState.Inactive == this.State || SchedulerState.Error == this.State)
                {
                    this.LastStartTime = DateTime.Now;
                    this.LastStopTime = null;
                    this.State = SchedulerState.Active;
                    this.schedule.Start();
                }
            }
        }

        public void Stop()
        {
            lock (lockObj)
            {
                if (SchedulerState.Active == this.State || SchedulerState.Running == this.State)
                {
                    this.LastStopTime = DateTime.Now;
                    this.State = SchedulerState.Inactive;
                    this.schedule.Stop();
                }
            }
        }

        void job_Running()
        {
            this.State = SchedulerState.Running;
        }

        void job_Completed()
        {
            this.State = SchedulerState.Active;
        }

        void job_Error(Exception obj)
        {
            this.State = SchedulerState.Error;
        }

        public void Dispose()
        {
            if (this.job != null)
            {
                this.job.Running -= new Action(job_Running);
                this.job.Completed -= new Action(job_Completed);
                this.job.Error -= new Action<Exception>(job_Error);
            }
        }

    }

}
