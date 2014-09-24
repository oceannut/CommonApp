using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.ServiceModel.Web;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.Common.ExceptionHandling;
using ThinkInBio.CommonApp.BLL;
using R = ThinkInBio.CommonApp.WSL.Properties.Resources;

namespace ThinkInBio.CommonApp.WSL.Impl
{
    public class FileTransferWcfService : IFileTransferWcfService
    {

        internal IExceptionHandler ExceptionHandler { get; set; }

        public void UploadFile(Stream stream)
        {
            //WebOperationContext context = WebOperationContext.Current;
        //    Response.Clear();
        //Response.AddHeader("Pragma", "no-cache");
        //Response.AddHeader("Cache-Control", "no-store, no-cache, must-revalidate");
        //Response.AddHeader("Content-Disposition", "inline; filename=\"files.json\"");
        //Response.AddHeader("X-Content-Type-Options", "nosniff");
        //Response.AddHeader("Access-Control-Allow-Origin", "*");
        //Response.AddHeader("Access-Control-Allow-Methods", "OPTIONS, HEAD, GET, POST, PUT, DELETE");
        //Response.AddHeader("Access-Control-Allow-Headers", "X-File-Name, X-File-Type, X-File-Size");
            //context.OutgoingResponse.Headers.Clear();
            //context.OutgoingResponse.Headers.Add("Pragma", "no-cache");
            //context.OutgoingResponse.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate");
            //context.OutgoingResponse.Headers.Add("Content-Disposition", "inline; filename=\"files.json\"");
            //context.OutgoingResponse.Headers.Add("X-Content-Type-Options", "nosniff");
            //context.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            //context.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "OPTIONS, HEAD, GET, POST, PUT, DELETE");
            //context.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "X-File-Name, X-File-Type, X-File-Size");

            long length = 0;
            int readCount;
            var buffer = new byte[8192];
            while ((readCount = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                length += readCount;
            }

            FileTransferLog log = new FileTransferLog();
            log.FileSize = length;
            //return log;
        }

        public void DeleteFile(string user, string fileId)
        {
            throw new NotImplementedException();
        }

        public FileTransferLog[] GetUploadFileList(string user, string date, string span, string start, string count)
        {
            throw new NotImplementedException();
        }

        public byte[] DownloadFile(string user, string fileId)
        {
            throw new NotImplementedException();
        }

        public FileTransferLog[] GetDownloadFileList(string user, string date, string span, string start, string count)
        {
            throw new NotImplementedException();
        }

    }
}
