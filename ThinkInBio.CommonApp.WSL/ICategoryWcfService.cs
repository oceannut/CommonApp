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
    public interface ICategoryWcfService
    {

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/category/{scope}/0/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Category SaveCategory(string scope, string parentId, string name, string code, string description, string icon, string sequence);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/category/{scope}/{id}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Category UpdateCategory(string scope, string id, string parentId, string name, string code, string description, string icon, string sequence);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "/category/{scope}/{id}/",
            RequestFormat = WebMessageFormat.Json)]
        void DeleteCategory(string scope, string id);

        [OperationContract]
        [WebGet(UriTemplate = "/category/{scope}/{id}/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Category GetCategory(string scope, string id);

        [OperationContract]
        [WebGet(UriTemplate = "/category/{scope}/code/{code}/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Category GetCategoryByCode(string scope, string code);

        [OperationContract]
        [WebGet(UriTemplate = "/category/{scope}/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Category[] GetCategoryList(string scope);

    }

}
