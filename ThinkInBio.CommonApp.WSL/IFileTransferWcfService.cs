using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

using ThinkInBio.FileTransfer;
using ThinkInBio.CommonApp;

namespace ThinkInBio.CommonApp.WSL
{

    [ServiceContract]
    public interface IFileTransferWcfService
    {

        [OperationContract]
        [WebInvoke(Method = "PUT",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/upload/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void DeleteFile(UploadFile uploadFile);

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/uploadLog/{user}/0/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void SaveUploadLog(string user, UploadFile[] uploadFiles);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/uploadLog/{user}/:id/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void UpdateUploadLog4DeleteFile(string user, string id);

        [OperationContract]
        [WebGet(UriTemplate = "/uploadLog/{user}/time/{date}/{span}/range/{start}/{count}/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        FileTransferLog[] GetUploadFileList(string user, string date, string span, string start, string count);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    BodyStyle = WebMessageBodyStyle.WrappedRequest,
        //    UriTemplate = "/download/{user}/",
        //    RequestFormat = WebMessageFormat.Json,
        //    ResponseFormat = WebMessageFormat.Json)]
        //byte[] DownloadFile(string user, string fileId);

        //[OperationContract]
        //[WebGet(UriTemplate = "/download/{user}/time/{date}/{span}/range/{start}/{count}/",
        //    RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //FileTransferLog[] GetDownloadFileList(string user, string date, string span, string start, string count);

    }

}
