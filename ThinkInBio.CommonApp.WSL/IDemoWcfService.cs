using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ThinkInBio.CommonApp.WSL
{

    [ServiceContract]
    public interface IDemoWcfService
    {

        [OperationContract]
        [WebGet(UriTemplate = "/demo/echo/{name}/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string Echo(string name);

        [OperationContract]
        [WebGet(UriTemplate = "/demo/service/path/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetServicePath();

    }

}
