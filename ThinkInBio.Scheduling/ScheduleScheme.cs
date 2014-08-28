using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.ExceptionHandling;

namespace ThinkInBio.Scheduling
{

    /// <summary>
    /// 调度的进度状态。
    /// Inactive -> Active (start scheduling);
    /// Active -> Inactive (stop scheduling);
    /// Active -> Running (run job);
    /// Running -> Active (complete job once);
    /// Running -> Inactive (interrupt job and stop scheduling);
    /// Running -> Error (exception occured when job is running);
    /// Error -> Active (resume scheduling).
    /// </summary>
    public enum ScheduleState
    {
        /// <summary>
        /// 调度已启动。
        /// </summary>
        Inactive,
        /// <summary>
        /// 调度未启动。
        /// </summary>
        Active,
        /// <summary>
        /// 正在执行工作。
        /// </summary>
        Running,
        /// <summary>
        /// 异常。
        /// </summary>
        Error
    }

    /// <summary>
    /// 工作计划调度方案，安排工作的计划调度策略。
    /// </summary>
    public class ScheduleScheme : IDisposable
    {

        #region fields

        private ISchedule schedule;
        private IJob job;
        private ScheduleState state;
        private object lockObj = new object();

        #endregion

        #region events

        /// <summary>
        /// 工作计划调度的进度状态变化引发的事件。
        /// </summary>
        public event Action<ScheduleState> StateChanged;

        #endregion

        #region properties

        internal IExceptionHandler ExceptionHandler { get; set; }

        /// <summary>
        /// 名称，标识当前的工作计划调度。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 详细说明。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 为调度配置的工作计划。
        /// </summary>
        public ISchedule Schedule
        {
            get { return schedule; }
        }

        /// <summary>
        /// 要调度的工作。
        /// </summary>
        public IJob Job
        {
            get { return job; }
        }

        /// <summary>
        /// 工作计划调度的进度状态。
        /// </summary>
        public ScheduleState State
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

        /// <summary>
        /// 是否自动启动。
        /// </summary>
        public bool IsAutoStart { get; set; }

        /// <summary>
        /// 最近一次调度启动时间。
        /// </summary>
        public DateTime? LastStartTime { get; private set; }

        /// <summary>
        /// 最近一次调度停止时间。
        /// </summary>
        public DateTime? LastStopTime { get; private set; }

        #endregion

        #region constructors

        /// <summary>
        /// 构建一个工作计划调度方案。
        /// </summary>
        /// <param name="schedule">为调度配置的工作计划。</param>
        /// <param name="job">要调度的工作。</param>
        public ScheduleScheme(ISchedule schedule, IJob job)
        {
            if (schedule == null)
            {
                throw new ArgumentNullException("schedule");
            }
            if (job == null)
            {
                throw new ArgumentNullException("job");
            }
            this.schedule = schedule;
            this.job = job;
            this.state = ScheduleState.Inactive;

            this.job.Running += new Action(job_Running);
            this.job.Completed += new Action(job_Completed);
            this.job.Error += new Action<Exception>(job_Error);
        }

        /// <summary>
        /// 构建一个工作计划调度方案。
        /// </summary>
        /// <param name="name">调度的名称。</param>
        /// <param name="schedule">为调度配置的工作计划。</param>
        /// <param name="job">要调度的工作。</param>
        public ScheduleScheme(string name, ISchedule schedule, IJob job)
            : this(schedule, job)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }
            this.Name = name;
        }

        #endregion

        #region methods

        /// <summary>
        /// 启动调度。
        /// </summary>
        public void Start()
        {
            lock (lockObj)
            {
                if (ScheduleState.Inactive == this.State || ScheduleState.Error == this.State)
                {
                    this.LastStartTime = DateTime.Now;
                    this.LastStopTime = null;
                    this.State = ScheduleState.Active;
                    this.schedule.Start(this.job);
                }
            }
        }

        /// <summary>
        /// 停止调度。
        /// </summary>
        public void Stop()
        {
            lock (lockObj)
            {
                if (ScheduleState.Active == this.State || ScheduleState.Running == this.State)
                {
                    this.LastStopTime = DateTime.Now;
                    this.State = ScheduleState.Inactive;
                    this.schedule.Stop();
                }
            }
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

        void job_Running()
        {
            this.State = ScheduleState.Running;
        }

        void job_Completed()
        {
            this.State = ScheduleState.Active;
        }

        void job_Error(Exception ex)
        {
            this.State = ScheduleState.Error;
            if (ExceptionHandler != null)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        #endregion

    }

}
