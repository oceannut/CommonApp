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

    /// <summary>
    /// 业务通知的Wcf服务。
    /// </summary>
    [ServiceContract]
    public interface IBizNotificationWcfService
    {

        /// <summary>
        /// 发送通知。
        /// </summary>
        /// <param name="user">发送方。</param>
        /// <param name="to">接收方。</param>
        /// <param name="content">通知内容。</param>
        /// <param name="resource">业务资源。</param>
        /// <param name="resourceId">业务资源标识。</param>
        /// <returns>返回通知。</returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/notification/biz/outbox/{user}/receiver/{to}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BizNotification SendBizNotification(string user, string to, 
            string content, string resource, string resourceId);

        /// <summary>
        /// 签收通知。
        /// </summary>
        /// <param name="user">签收方（接收方）。</param>
        /// <param name="notificationId">通知的标识。</param>
        /// <returns>返回通知。</returns>
        [OperationContract]
        [WebInvoke(Method = "PUT",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/notification/biz/inbox/{user}/untreated/{notificationId}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BizNotification CheckBizNotification(string user, string notificationId);

        /// <summary>
        /// 签收通知。
        /// </summary>
        /// <param name="user">签收方（接收方）。</param>
        /// <param name="notificationId">通知的标识。</param>
        /// <returns>返回通知。</returns>
        [OperationContract]
        [WebInvoke(Method = "PUT",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/notification/biz/inbox/{user}/untreated/n/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void CheckBizNotificationCol(string user, string[] notificationIds);

        /// <summary>
        /// 获取未签收的所有通知。
        /// </summary>
        /// <param name="user">签收方（接收方）。</param>
        /// <returns>返回未签收的通知集合。</returns>
        [OperationContract]
        [WebGet(UriTemplate = "/notification/biz/inbox/{user}/untreated/count/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        int GetUntreatedBizNotificationCount(string user);

        /// <summary>
        /// 获取未签收的所有通知。
        /// </summary>
        /// <param name="user">签收方（接收方）。</param>
        /// <returns>返回未签收的通知集合。</returns>
        [OperationContract]
        [WebGet(UriTemplate = "/notification/biz/inbox/{user}/untreated/list/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BizNotification[] GetUntreatedBizNotificationList(string user);

        /// <summary>
        /// 获取某类业务的所有通知。
        /// </summary>
        /// <param name="user">通知的参与方（发送方或接收方）。</param>
        /// <param name="resource">业务资源。</param>
        /// <param name="resourceId">业务资源标识。</param>
        /// <returns>返回业务相关的通知集合。</returns>
        [OperationContract(Name = "GetBizNotificationList4Resource")]
        [WebGet(UriTemplate = "/notification/biz/both/{user}/resource/{resource}/resourceId/{resourceId}/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BizNotification[] GetBizNotificationList(string user, string resource, string resourceId);

        /// <summary>
        /// 获取符合一定条件的通知记录个数。
        /// </summary>
        /// <param name="box">inbox-收件箱，outbox-发件箱，both-所有的。</param>
        /// <param name="user">通知的参与方（发送方或接收方）。</param>
        /// <param name="date">起始日期。</param>
        /// <param name="span">时间范围。</param>
        /// <returns>返回通知记录的个数。</returns>
        //[OperationContract]
        //[WebGet(UriTemplate = "/notification/biz/{box}/{user}/time/{date}/{span}/",
        //    RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //int GetBizNotificationCount(string box, string user, string date, string span);

        /// <summary>
        /// 获取符合一定条件的通知记录。
        /// </summary>
        /// <param name="box">inbox-收件箱，outbox-发件箱，both-所有的。</param>
        /// <param name="user">通知的参与方（发送方或接收方）。</param>
        /// <param name="date">起始日期。</param>
        /// <param name="span">时间范围。</param>
        /// <param name="start">返回记录所在集合的起始索引位置。</param>
        /// <param name="count">返回记录的个数。</param>
        /// <returns>返回通知记录集合。</returns>
        [OperationContract]
        [WebGet(UriTemplate = "/notification/biz/{box}/{user}/time/{date}/{span}/range/{start}/{count}/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BizNotification[] GetBizNotificationList(string box, string user, string date, string span, string start, string count);

    }

}
