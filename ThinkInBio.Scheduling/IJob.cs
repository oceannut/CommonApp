using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Scheduling
{

    /// <summary>
    /// 工作，定义为达到某一目标的行动操作。
    /// </summary>
    public interface IJob
    {

        /// <summary>
        /// 工作（准备）运行前引发的事件。
        /// </summary>
        event Action Running;

        /// <summary>
        /// 工作完成引发的事件。
        /// </summary>
        event Action Completed;

        /// <summary>
        /// 工作运行过程中异常引发的事件。
        /// </summary>
        event Action<Exception> Error;

        /// <summary>
        /// 运行工作。
        /// </summary>
        void Run();

    }

}
