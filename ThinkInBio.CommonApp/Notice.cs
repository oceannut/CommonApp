using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    /// <summary>
    /// 公告。
    /// </summary>
    public class Notice
    {

        /// <summary>
        /// 编号。
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容。
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发布人。
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间。
        /// </summary>
        public DateTime Creation { get; set; }

        /// <summary>
        /// 修改时间。
        /// </summary>
        public DateTime Modification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void Save(Action<Notice> action)
        {
            DateTime timeStamp = DateTime.Now;
            this.Creation = timeStamp;
            this.Modification = timeStamp;
            if (action != null)
            {
                action(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void Update(Action<Notice> action)
        {
            DateTime timeStamp = DateTime.Now;
            this.Modification = timeStamp;
            if (action != null)
            {
                action(this);
            }
        }

    }

}
