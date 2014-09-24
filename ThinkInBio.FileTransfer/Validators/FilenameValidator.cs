using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using R = ThinkInBio.FileTransfer.Properties.Resources;

namespace ThinkInBio.FileTransfer
{
    public class FilenameValidator : UploadFileValidator
    {

        public FilenameValidator() { }

        public FilenameValidator(UploadFileValidator next)
            : base(next)
        {
        }

        protected override bool Validate(UploadFile uploadFile)
        {
            bool valid = true;
            if (string.IsNullOrWhiteSpace(uploadFile.Name))
            {
                uploadFile.Error = R.EmptyFilename;
                valid = false;
            }
            else if (uploadFile.Name.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                uploadFile.Error = R.InvalidFilenameChars;
                valid =  false;
            }
            return valid;
        }
    }
}
