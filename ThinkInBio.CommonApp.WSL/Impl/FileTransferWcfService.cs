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
        

        public void DeleteFile(UploadFile uploadFile)
        {
            if (uploadFile == null || string.IsNullOrWhiteSpace(uploadFile.Path))
            {
                throw new WebFaultException<string>("empty path", HttpStatusCode.BadRequest);
            }
            try
            {
                FileTransferLogService.DeleteFile(uploadFile.Path);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public FileTransferLog[] SaveUploadLog(string user, UploadFile[] uploadFiles)
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
                if (logList != null)
                {
                    return logList.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public void UpdateUploadLog4DeleteFile(string id)
        {
            int idLong = 0;
            try
            {
                idLong = Convert.ToInt32(id);
            }
            catch
            {
                throw new WebFaultException<string>(R.InvalidId, HttpStatusCode.BadRequest);
            }
            try
            {
                FileTransferLog log = FileTransferLogService.GetFileTransferLog(idLong);
                if (log == null)
                {
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
                FileTransferLogService.UpdateFileTransferLog4DeleteFile(log);
            }
            catch (WebFaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public FileTransferLog[] GetUploadFileList(string user, string date, string span, string start, string count)
        {
            string userInput = null;
            if ("null" != user)
            {
                userInput = user;
            }

            DateTime d = DateTime.MinValue;
            int spanInt = 0;
            if ("null" != date && "null" != span)
            {
                try
                {
                    d = DateTime.Parse(date);
                }
                catch
                {
                    throw new WebFaultException<string>("date", HttpStatusCode.BadRequest);
                }
                try
                {
                    spanInt = Convert.ToInt32(span);
                }
                catch
                {
                    throw new WebFaultException<string>("span", HttpStatusCode.BadRequest);
                }
            }

            int startInt = 0;
            try
            {
                startInt = Convert.ToInt32(start);
            }
            catch
            {
                throw new WebFaultException<string>("start", HttpStatusCode.BadRequest);
            }
            int countInt = 0;
            try
            {
                countInt = Convert.ToInt32(count);
            }
            catch
            {
                throw new WebFaultException<string>("count", HttpStatusCode.BadRequest);
            }

            DateTime? startTime = null;
            DateTime? endTime = null;
            if ("null" != date && "null" != span)
            {
                if (spanInt < 0)
                {
                    startTime = d.AddDays(spanInt + 1);
                    endTime = new DateTime(d.Year, d.Month, d.Day, 23, 59, 59);
                }
                else
                {
                    startTime = new DateTime(d.Year, d.Month, d.Day);
                    endTime = d.AddDays(spanInt).AddSeconds(-1);
                }
            }

            try
            {
                IList<FileTransferLog> list = FileTransferLogService.GetFileTransferLogList(startTime, endTime, userInput, FileTransferDirection.Upload,
                    startInt, countInt);
                if (list != null)
                {
                    return list.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

    }
}
