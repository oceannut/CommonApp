using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ThinkInBio.CommonApp.WSL
{

    [ServiceContract]
    public interface ITimeStampWcfService
    {

        [OperationContract]
        [WebGet(UriTemplate = "/timestamp/next/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DateTime? GetTimeStamp();

        [OperationContract]
        [WebGet(UriTemplate = "/timestamp/local/next/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DateTime? GetLocalTimeStamp();

    }

}
