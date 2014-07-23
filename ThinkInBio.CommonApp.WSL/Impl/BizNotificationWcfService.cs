using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.CommonApp.BLL;

namespace ThinkInBio.CommonApp.WSL.Impl
{


    public class BizNotificationWcfService : IBizNotificationWcfService
    {

        internal IBizNotificationService BizNotificationService { get; set; }

        public BizNotification SendBizNotification(string user, string to,
            string content, string resource, string resourceId)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                throw new ArgumentNullException("user");
            }
            /*
             * 验证用户的合法性逻辑暂省略。
             * */
            if (string.IsNullOrWhiteSpace(to) 
                || string.IsNullOrWhiteSpace(resource) 
                || string.IsNullOrWhiteSpace(resourceId))
            {
                throw new ArgumentNullException();
            }

            BizNotification notification = new BizNotification(user, to);
            notification.Content = content;
            notification.Resource = resource;
            notification.ResourceId = resourceId;
            notification.Send(
                (e) =>
                {
                    BizNotificationService.SaveNotification((BizNotification)e);
                });
            return notification;
        }

        public BizNotification ReceiveBizNotification(string user, string notificationId)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                throw new ArgumentNullException("user");
            }
            /*
             * 验证用户的合法性逻辑暂省略。
             * */
            if (string.IsNullOrWhiteSpace(notificationId))
            {
                throw new ArgumentNullException("notificationId");
            }

            BizNotification notification = BizNotificationService.GetNotification(Convert.ToInt64(notificationId));
            if (notification == null)
            {
                throw new ObjectNotFoundException(notificationId);
            }
            notification.Receive(user,
                (e) =>
                {
                    BizNotificationService.UpdateNotification((BizNotification)e);
                });
            return notification;
        }

        public BizNotification[] GetBizNotificationList(string user)
        {
            throw new NotImplementedException();
        }

        public BizNotification[] GetBizNotificationList(string user, string resource)
        {
            throw new NotImplementedException();
        }

        public BizNotification[] GetUntreatedBizNotificationList(string user)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                throw new ArgumentNullException("user");
            }
            /*
             * 验证用户的合法性逻辑暂省略。
             * */
            IList<BizNotification> list = BizNotificationService.GetUntreatedBizNotificationByReceiver(user);
            if (list != null)
            {
                return list.ToArray();
            }
            else
            {
                return null;
            }
        }

        public BizNotification[] GetAllBizNotificationList(string user, string resource, string resourceId)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                throw new ArgumentNullException("user");
            }
            /*
             * 验证用户的合法性逻辑暂省略。
             * */
            if (string.IsNullOrWhiteSpace(resource) || string.IsNullOrWhiteSpace(resourceId))
            {
                throw new ArgumentNullException();
            }
            string resourceLocal = resource;
            if ("null" == resourceLocal)
            {
                resourceLocal = null;
            }
            string resourceIdLocal = resourceId;
            if ("null" == resourceIdLocal || "0" == resourceIdLocal)
            {
                resourceIdLocal = null;
            }
            IList<BizNotification> list = BizNotificationService.GetBizNotificationList(resourceLocal, resourceIdLocal);
            if (list != null)
            {
                return list.ToArray();
            }
            else
            {
                return null;
            }
        }

    }
}
