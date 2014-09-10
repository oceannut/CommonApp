using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ServiceModel.Web;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.Common.ExceptionHandling;
using ThinkInBio.CommonApp.BLL;
using R = ThinkInBio.CommonApp.WSL.Properties.Resources;

namespace ThinkInBio.CommonApp.WSL.Impl
{


    public class BizNotificationWcfService : IBizNotificationWcfService
    {

        internal IBizNotificationService BizNotificationService { get; set; }
        internal IExceptionHandler ExceptionHandler { get; set; }

        public BizNotification SendBizNotification(string user, string to,
            string content, string resource, string resourceId)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                throw new WebFaultException<string>(R.EmptyUser, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(to))
            {
                throw new WebFaultException<string>(R.EmptyTo, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(resource))
            {
                throw new WebFaultException<string>(R.EmptyResource, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(resourceId))
            {
                throw new WebFaultException<string>(R.EmptyResourceId, HttpStatusCode.BadRequest);
            }

            BizNotification notification = new BizNotification(user, to);
            notification.Content = content;
            notification.Resource = resource;
            notification.ResourceId = resourceId;
            try
            {
                notification.Send(
                    (e) =>
                    {
                        BizNotificationService.SaveNotification((BizNotification)e);
                    });
                return notification;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public BizNotification CheckBizNotification(string user, string notificationId)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                throw new WebFaultException<string>(R.EmptyUser, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(notificationId))
            {
                throw new WebFaultException<string>(R.EmptyNotificationId, HttpStatusCode.BadRequest);
            }
            long idLong = 0;
            try
            {
                idLong = Convert.ToInt64(notificationId);
            }
            catch
            {
                throw new WebFaultException<string>(R.InvalidId, HttpStatusCode.BadRequest);
            }

            try
            {
                BizNotification notification = BizNotificationService.GetNotification(idLong);
                if (notification == null)
                {
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
                notification.Receive(user,
                    (e) =>
                    {
                        BizNotificationService.UpdateNotification((BizNotification)e);
                    });
                return notification;
            }
            catch (WebFaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public void CheckBizNotificationCol(string user, string[] notificationIds)
        {
            if (notificationIds != null && notificationIds.Length > 0)
            {
                foreach (var id in notificationIds)
                {
                    CheckBizNotification(user, id);
                }
            }
        }

        public int GetUntreatedBizNotificationCount(string user)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                throw new WebFaultException<string>(R.EmptyUser, HttpStatusCode.BadRequest);
            }

            try
            {
                return BizNotificationService.GetUntreatedBizNotificationCountByReceiver(user);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public BizNotification[] GetUntreatedBizNotificationList(string user)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                throw new WebFaultException<string>(R.EmptyUser, HttpStatusCode.BadRequest);
            }

            try
            {
                IList<BizNotification> list = BizNotificationService.GetUntreatedBizNotificationListByReceiver(user);
                if (list != null)
                {
                    return list.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public BizNotification[] GetBizNotificationList(string user, string resource, string resourceId)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                throw new WebFaultException<string>(R.EmptyUser, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(resource))
            {
                throw new WebFaultException<string>(R.EmptyResource, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(resourceId))
            {
                throw new WebFaultException<string>(R.EmptyResourceId, HttpStatusCode.BadRequest);
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
            try
            {
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
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        //public int GetBizNotificationCount(string box, string user, string date, string span)
        //{
        //    throw new NotImplementedException();
        //}

        public BizNotification[] GetBizNotificationList(string box, string user, string date, string span, string start, string count)
        {
            string sender = null;
            string receiver = null;
            switch (box)
            {
                case "inbox":
                    receiver = user;
                    break;
                case "outbox":
                    sender = user;
                    break;
                case "both":
                    sender = user;
                    receiver = user;
                    break;
                default:
                    break;
            }

            DateTime d = DateTime.MinValue;
            int spanInt = 0;
            if ("null" != date && "null" != span)
            {
                try
                {
                    d = DateTime.Parse(date);
                }
                catch
                {
                    throw new WebFaultException<string>("date", HttpStatusCode.BadRequest);
                }
                try
                {
                    spanInt = Convert.ToInt32(span);
                }
                catch
                {
                    throw new WebFaultException<string>("span", HttpStatusCode.BadRequest);
                }
            }

            int startInt = 0;
            try
            {
                startInt = Convert.ToInt32(start);
            }
            catch
            {
                throw new WebFaultException<string>("start", HttpStatusCode.BadRequest);
            }
            int countInt = 0;
            try
            {
                countInt = Convert.ToInt32(count);
            }
            catch
            {
                throw new WebFaultException<string>("count", HttpStatusCode.BadRequest);
            }

            DateTime? startTime = null;
            DateTime? endTime = null;
            if ("null" != date && "null" != span)
            {
                if (spanInt < 0)
                {
                    startTime = d.AddDays(spanInt + 1);
                    endTime = new DateTime(d.Year, d.Month, d.Day, 23, 59, 59);
                }
                else
                {
                    startTime = new DateTime(d.Year, d.Month, d.Day);
                    endTime = d.AddDays(spanInt).AddSeconds(-1);
                }
            }

            try
            {
                IList<BizNotification> list = BizNotificationService.GetBizNotificationList(startTime, endTime, sender, receiver, null, startInt, countInt);
                if (list != null)
                {
                    return list.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

    }
}
