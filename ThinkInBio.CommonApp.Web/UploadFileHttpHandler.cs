using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using ThinkInBio.Common.Utilities;
using ThinkInBio.FileTransfer;

namespace ThinkInBio.CommonApp.Web
{
    public class UploadFileHttpHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            HttpFileCollection httpFiles = request.Files;
            List<UploadFile> uploadFileList = new List<UploadFile>();
            if (httpFiles != null)
            {
                for(int i=0;i< httpFiles.Count;i++)
                {
                    HttpPostedFile file = httpFiles[i]; 
                    long fileSize = file.InputStream.Length;
                    if (request.Headers["X-File-Size"] != null)
                    {
                        fileSize = long.Parse(request.Headers["X-File-Size"].ToString());
                    }
                    UploadFile uploadFile = UploadFileHelper.Handle(file, fileSize);
                    uploadFileList.Add(uploadFile);
                }
                response.Clear();
                response.AddHeader("Vary", "Accept");
                string json = JsonHelper.Serialize<List<UploadFile>>(uploadFileList);
                string redirect = null;
                if (request["redirect"] != null)
                {
                    redirect = request["Redirect"];
                }
                if (redirect != null)
                {
                    response.AddHeader("Location,", string.Format(redirect, context.Server.UrlEncode(json)));
                    response.End();
                }
                else
                {
                    if (request.ServerVariables["HTTP_ACCEPT"] != null
                        && request.ServerVariables["HTTP_ACCEPT"].ToString().IndexOf("application/json") >= 0)
                    {
                        response.AddHeader("Content-type", "application/json");
                    }
                    else
                    {
                        response.AddHeader("Content-type", "text/plain");
                    }
                    response.Write(json);
                    response.End();
                }
            }
        }
    }
}
