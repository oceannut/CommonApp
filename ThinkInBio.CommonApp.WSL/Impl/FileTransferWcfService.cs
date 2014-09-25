using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.ServiceModel.Web;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.Common.ExceptionHandling;
using ThinkInBio.FileTransfer;
using ThinkInBio.CommonApp.BLL;
using R = ThinkInBio.CommonApp.WSL.Properties.Resources;

namespace ThinkInBio.CommonApp.WSL.Impl
{
    public class FileTransferWcfService : IFileTransferWcfService
    {

        internal IExceptionHandler ExceptionHandler { get; set; }
        internal IFileTransferLogService FileTransferLogService { get; set; }
        internal FileTransferManager FileTransferManager { get; set; }

        public void DeleteFile(UploadFile uploadFile)
        {
            if (uploadFile == null || string.IsNullOrWhiteSpace(uploadFile.Path))
            {
                throw new WebFaultException<string>("empty path", HttpStatusCode.BadRequest);
            }
            try
            {
                FileTransferManager.Delete(uploadFile.Path);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public void SaveUploadLog(string user, UploadFile[] uploadFiles)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                throw new WebFaultException<string>(R.EmptyUser, HttpStatusCode.BadRequest);
            }
            if (uploadFiles == null)
            {
                throw new WebFaultException<string>("No files selected.", HttpStatusCode.BadRequest);
            }
            try
            {
                List<FileTransferLog> logList = new List<FileTransferLog>();
                if (uploadFiles.Length > 0)
                {
                    foreach (UploadFile item in uploadFiles)
                    {
                        FileTransferLog log = new FileTransferLog(user,
                            item.Name, item.Size, item.Path,
                            item.TimeStamp.HasValue ? item.TimeStamp.Value : DateTime.Now);
                        log.Direction = FileTransferDirection.Upload;
                        logList.Add(log);
                    }
                    FileTransferLogService.SaveFileTransferLog(logList);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public void UpdateUploadLog4DeleteFile(string user, string id)
        {
            throw new NotImplementedException();
        }

        public FileTransferLog[] GetUploadFileList(string user, string date, string span, string start, string count)
        {
            throw new NotImplementedException();
        }

    }
}
