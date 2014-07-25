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
            UriTemplate = "/notification/biz/inbox/{user}/untreated/{notificationId}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BizNotification CheckBizNotification(string user, string notificationId);

        [OperationContract]
        [WebGet(UriTemplate = "/notification/biz/inbox/{user}/untreated/all/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BizNotification[] GetUntreatedBizNotificationList(string user);

        [OperationContract(Name = "GetBizNotificationList4Resource")]
        [WebGet(UriTemplate = "/notification/biz/both/{user}/resource/{resource}/resourceId/{resourceId}/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BizNotification[] GetBizNotificationList(string user, string resource, string resourceId);

        [OperationContract]
        [WebGet(UriTemplate = "/notification/biz/{box}/{user}/time/{date}/{span}/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        int GetBizNotificationCount(string user, string box, string date, string span);

        [OperationContract]
        [WebGet(UriTemplate = "/notification/biz/{box}/{user}/time/{date}/{span}/range/{start}/{count}/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BizNotification[] GetBizNotificationList(string user, string box, string date, string span, string start, string count);

    }

}
