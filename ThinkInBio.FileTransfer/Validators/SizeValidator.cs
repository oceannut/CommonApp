using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using R = ThinkInBio.FileTransfer.Properties.Resources;

namespace ThinkInBio.FileTransfer
{
    public class SizeValidator : UploadFileValidator
    {

        internal long MaxSize { get; set; }

        public SizeValidator() { }

        public SizeValidator(UploadFileValidator next)
            : base(next)
        {
        }

        public SizeValidator(long maxSize)
        {
            this.MaxSize = maxSize;
        }

        public SizeValidator(UploadFileValidator next, long maxSize)
            : base(next)
        {
            this.MaxSize = maxSize;
        }

        protected override bool Validate(UploadFile uploadFile)
        {
            if (this.MaxSize > 0 && (uploadFile.Size > this.MaxSize))
            {
                uploadFile.Error = R.ExceedMaxSize;
                return false;
            }
            return true;
        }
    }
}
