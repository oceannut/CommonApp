using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.FileTransfer
{
    public class TimeStampBuilder : UploadFileBuilder
    {

        public TimeStampBuilder() { }

        public TimeStampBuilder(UploadFileBuilder next)
        {
            this.Next = next;
        }

        protected override void Build(UploadFile uploadFile)
        {
            uploadFile.TimeStamp = DateTime.Now;
        }

    }
}
