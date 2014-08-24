using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Scheduling
{

    /// <summary>
    /// 工作计划。
    /// </summary>
    public interface ISchedule
    {

        /// <summary>
        /// 启动计划。
        /// </summary>
        void Start();

        /// <summary>
        /// 停止计划。
        /// </summary>
        void Stop();

    }

}
