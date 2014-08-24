using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Scheduling
{

    /// <summary>
    /// 工作任务定义。
    /// </summary>
    public interface IJob
    {

        /// <summary>
        /// 工作运行引发的事件。
        /// </summary>
        event Action Running;

        /// <summary>
        /// 工作完成引发的事件。
        /// </summary>
        event Action Completed;

        /// <summary>
        /// 工作运行错误引发的事件。
        /// </summary>
        event Action<Exception> Error;

        /// <summary>
        /// 运行工作。
        /// </summary>
        void Run();

    }

}
