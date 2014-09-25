using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.FileTransfer
{

    public abstract class UploadFileBuilder : IUploadFileInterceptor
    {

        internal UploadFileBuilder Next { get; set; }

        public UploadFileBuilder() { }

        public UploadFileBuilder(UploadFileBuilder next)
        {
            this.Next = next;
        }

        public void Handle(UploadFile uploadFile)
        {
            Build(uploadFile);
            if (Next != null)
            {
                Next.Handle(uploadFile);
            }
        }

        protected abstract void Build(UploadFile uploadFile);

    }

}
