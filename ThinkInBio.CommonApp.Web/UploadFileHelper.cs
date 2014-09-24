using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

using Spring.Context.Support;

using ThinkInBio.FileTransfer;

namespace ThinkInBio.CommonApp.Web
{
    public static class UploadFileHelper
    {

        public static UploadFile Handle(HttpPostedFile httpPostedFile, long fileSize)
        {
            UploadFile uploadFile = new UploadFile();
            uploadFile.Name = Path.GetFileName(httpPostedFile.FileName);
            uploadFile.Size = fileSize;

            FileTransferManager fileTransferManager = ContextRegistry.GetContext().GetObject<FileTransferManager>();
            fileTransferManager.Upload(uploadFile, httpPostedFile.InputStream);

            return uploadFile;
        }

    }
}
