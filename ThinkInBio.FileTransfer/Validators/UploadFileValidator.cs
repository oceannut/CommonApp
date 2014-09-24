using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.FileTransfer
{

    public abstract class UploadFileValidator : IUploadFileInterceptor
    {

        internal UploadFileValidator Next { get; set; }

        public UploadFileValidator() { }

        public UploadFileValidator(UploadFileValidator next)
        {
            this.Next = next;
        }

        public void Handle(UploadFile uploadFile)
        {
            if (Validate(uploadFile))
            {
                if (Next != null)
                {
                    Next.Handle(uploadFile);
                }
            }
        }

        protected abstract bool Validate(UploadFile uploadFile);

    }
}
