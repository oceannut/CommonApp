using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

using ThinkInBio.CommonApp;

namespace ThinkInBio.CommonApp.WSL
{

    [ServiceContract]
    public interface IFileTransferWcfService
    {

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/upload/")]
        void UploadFile(Stream stream);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "/upload/{user}/{fileId}/",
            RequestFormat = WebMessageFormat.Json)]
        void DeleteFile(string user, string fileId);

        [OperationContract]
        [WebGet(UriTemplate = "/upload/{user}/time/{date}/{span}/range/{start}/{count}/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        FileTransferLog[] GetUploadFileList(string user, string date, string span, string start, string count);

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/download/{user}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        byte[] DownloadFile(string user, string fileId);

        [OperationContract]
        [WebGet(UriTemplate = "/download/{user}/time/{date}/{span}/range/{start}/{count}/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        FileTransferLog[] GetDownloadFileList(string user, string date, string span, string start, string count);

    }

}
