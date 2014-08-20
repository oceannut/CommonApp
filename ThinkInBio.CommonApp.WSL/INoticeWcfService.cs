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
    public interface INoticeWcfService
    {

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/notice/0/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Notice SaveNotice(string title, string content, string creator);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/notice/{id}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Notice UpdateNotice(string id, string title, string content, string creator);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/notice/{id}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void DeleteNotice(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/notice/{id}/",
            RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json)]
        Notice GetNotice(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/notice/time/{date}/{span}/range/{start}/{count}/",
            RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json)]
        Notice[] GetNoticeList(string date, string span, string start, string count);

    }

}
