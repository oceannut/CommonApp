using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

using ThinkInBio.CommonApp;
using ThinkInBio.CommonApp.BLL;

namespace ThinkInBio.CommonApp.WSL
{

    [ServiceContract]
    public interface IBizNotificationWcfService
    {

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/notification/biz/outbox/{user}/receiver/{to}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BizNotification SendBizNotification(string user, string to, 
            string content, string resource, string resourceId);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/notification/biz/inbox/{user}/{notificationId}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BizNotification ReceiveBizNotification(string user, string notificationId);

        [OperationContract]
        [WebGet(UriTemplate = "/notification/biz/inbox/{user}/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BizNotification[] GetBizNotificationList(string user);

        [OperationContract(Name = "GetBizNotificationList4Resource")]
        [WebGet(UriTemplate = "/notification/biz/inbox/{user}/resource/{resource}/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BizNotification[] GetBizNotificationList(string user, string resource);

        [OperationContract]
        [WebGet(UriTemplate = "/notification/biz/inbox/{user}/untreated/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BizNotification[] GetUntreatedBizNotificationList(string user);

    }

}
