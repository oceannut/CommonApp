using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    /// <summary>
    /// 具有业务单信息的通知。
    /// </summary>
    public class BizNotification : Notification
    {

        /// <summary>
        /// 资源。
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// 资源的编号。
        /// </summary>
        public string ResourceId { get; set; }

        /// <summary>
        /// 构建一个通知。
        /// </summary>
        public BizNotification() : base() { }

        /// <summary>
        /// 构建一个通知。
        /// </summary>
        /// <param name="sender">发送人。</param>
        /// <param name="receiver">接收人。</param>
        public BizNotification(string sender, string receiver)
            : base(sender, receiver)
        {
        }

        /// <summary>
        /// 构建一个通知。
        /// </summary>
        /// <param name="sender">发送人。</param>
        /// <param name="receiver">接收人。</param>
        /// <param name="resource">资源。</param>
        /// <param name="resourceId">资源的编号。</param>
        public BizNotification(string sender, string receiver,
            string resource, string resourceId)
            : base(sender, receiver)
        {
            if (string.IsNullOrWhiteSpace(resource)
                || string.IsNullOrWhiteSpace(resourceId))
            {
                throw new ArgumentNullException();
            }

            this.Resource = resource;
            this.ResourceId = resourceId;
        }

    }

}
