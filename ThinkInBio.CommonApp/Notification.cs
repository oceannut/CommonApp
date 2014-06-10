using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Exceptions;

namespace ThinkInBio.CommonApp
{

    /// <summary>
    /// 通知。
    /// </summary>
    public class Notification
    {

        /// <summary>
        /// 编号。
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 发送人。
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// 接收人。
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 内容。
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建时间。
        /// </summary>
        public DateTime Creation { get; set; }

        /// <summary>
        /// 检阅时间。
        /// </summary>
        public DateTime? Review { get; set; }

        /// <summary>
        /// 构建一个通知。
        /// </summary>
        public Notification() { }

        /// <summary>
        /// 构建一个通知。
        /// </summary>
        /// <param name="sender">发送人。</param>
        /// <param name="receiver">接收人。</param>
        public Notification(string sender, string receiver)
            : this()
        {
            if (string.IsNullOrWhiteSpace(sender)
                || string.IsNullOrWhiteSpace(receiver))
            {
                throw new ArgumentNullException();
            }

            this.Sender = sender;
            this.Receiver = receiver;
        }

        /// <summary>
        /// 发送通知。
        /// </summary>
        /// <param name="action">保存通知的操作定义。</param>
        public void Send(Action<Notification> action)
        {
            if (string.IsNullOrWhiteSpace(this.Sender)
                || string.IsNullOrWhiteSpace(this.Receiver))
            {
                throw new InvalidOperationException();
            }

            DateTime now = DateTime.Now;
            this.Creation = now;
            if (action != null)
            {
                action(this);
            }
        }

        /// <summary>
        /// 发送通知。
        /// </summary>
        /// <param name="content">通知内容。</param>
        /// <param name="action">保存通知的操作定义。</param>
        public void Send(string content, Action<Notification> action)
        {
            this.Content = content;
            Send(action);
        }

        /// <summary>
        /// 检阅通知。
        /// </summary>
        /// <param name="user">检阅人，一般是当前登录系统的用户。</param>
        /// <param name="action">更新通知的操作定义。</param>
        public void Receive(string user, 
            Action<Notification> action)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                throw new ArgumentNullException();
            }
            if (this.Id == 0)
            {
                throw new InvalidOperationException();
            }
            if (user != this.Receiver)
            {
                throw new AuthorizationException(user, "review the task not assigned to him.");
            }

            DateTime now = DateTime.Now;
            this.Review = now;
            if (action != null)
            {
                action(this);
            }
        }

    }

}
