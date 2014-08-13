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
            catch (BusinessLayerException ex)
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
            catch (BusinessLayerException ex)
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
            catch (BusinessLayerException ex)
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
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public int GetBizNotificationCount(string box, string user, string date, string span)
        {
            throw new NotImplementedException();
        }

        public BizNotification[] GetBizNotificationList(string box, string user, string date, string span, string start, string count)
        {
            throw new NotImplementedException();
        }

    }
}
