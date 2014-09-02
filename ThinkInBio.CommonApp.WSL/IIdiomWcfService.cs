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
    public interface IIdiomWcfService
    {

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/idiom/{scope}/0/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Idiom SaveIdiom(string scope, string content);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/idiom/{scope}/{id}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Idiom UpdateIdiom(string scope, string id, string content);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/idiom/{scope}/{id}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void DeleteIdiom(string scope, string id);

        [OperationContract]
        [WebGet(UriTemplate = "/idiom/{scope}/",
            RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json)]
        Idiom[] GetIdiomList(string scope);

    }

}
