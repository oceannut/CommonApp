using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    /// <summary>
    /// 操作痕迹。
    /// </summary>
    public class Trail
    {

        /// <summary>
        /// 编号。
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 资源。
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// 操作。
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 次数。
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 备注。
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 操作人。
        /// </summary>
        public string Actor { get; set; }

        /// <summary>
        /// 时间。
        /// </summary>
        public DateTime Creation { get; set; }

    }

}
