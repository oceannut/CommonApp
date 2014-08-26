using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Scheduling
{

    /// <summary>
    /// 工作计划，定义了工作调度的细节，例如调度的时间、次数等。
    /// </summary>
    public interface ISchedule
    {

        /// <summary>
        /// 工作开始调度的启动时间；如果不设置，一般根据当前时间即时启动调度。
        /// </summary>
        DateTime? StartTime { get; }

        /// <summary>
        /// 重复执行工作的间隔秒数。
        /// </summary>
        int RepeatSeconds { get; }

        /// <summary>
        /// 重复执行工作的次数；如果是0的话，表示无限重复执行。
        /// </summary>
        int RepeatCount { get; }

        /// <summary>
        /// 自定义调度表达式。
        /// </summary>
        string Expression { get; }

        /// <summary>
        /// 启动调度。
        /// </summary>
        /// <param name="job">计划要调度的工作。</param>
        void Start(IJob job);

        /// <summary>
        /// 停止调度。
        /// </summary>
        void Stop();

    }

}
