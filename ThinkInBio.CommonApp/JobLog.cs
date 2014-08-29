using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    /// <summary>
    /// 工作日志。
    /// </summary>
    public class JobLog
    {

        /// <summary>
        /// 编号。
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 数据范围。
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// 数据个数。
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 时间戳。
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 创建时间。
        /// </summary>
        public DateTime Creation { get; set; }

    }
}
