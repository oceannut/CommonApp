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
    public interface IUserWcfService
    {

        [OperationContract]
        [WebInvoke(Method = "PUT",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/user/{username}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        User UpdateUser(string username, string name, string group);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "/user/{username}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void DeleteUser(string username);

        [OperationContract]
        [WebGet(UriTemplate = "/user/{username}/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        User GetUser(string username);

        [OperationContract]
        [WebGet(UriTemplate = "/user/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        User[] GetUserList();

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/user/{username}/role/{role}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void AssignRole(string username, string role);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "/user/{username}/role/{role}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void UnassignRole(string username, string role);

    }

}
