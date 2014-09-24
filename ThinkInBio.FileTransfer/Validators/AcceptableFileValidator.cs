using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using R = ThinkInBio.FileTransfer.Properties.Resources;

namespace ThinkInBio.FileTransfer
{
    public class AcceptableFileValidator : UploadFileValidator
    {

        internal string AcceptableFiles { get; set; }

        public AcceptableFileValidator() { }

        public AcceptableFileValidator(UploadFileValidator next)
            : base(next)
        {
        }

        public AcceptableFileValidator(string acceptableFiles)
        {
            this.AcceptableFiles = acceptableFiles;
        }

        public AcceptableFileValidator(UploadFileValidator next, string acceptableFiles)
            : base(next)
        {
            this.AcceptableFiles = acceptableFiles;
        }

        protected override bool Validate(UploadFile uploadFile)
        {
            bool valid = true;
            if (string.IsNullOrWhiteSpace(uploadFile.Name))
            {
                uploadFile.Error = R.EmptyFilename;
                valid = false;
            }
            else if (!string.IsNullOrWhiteSpace(AcceptableFiles) 
                && !Regex.IsMatch(uploadFile.Name, this.AcceptableFiles, RegexOptions.Multiline | RegexOptions.IgnoreCase))
            {
                uploadFile.Error = R.NotAcceptableFiles;
                valid = false;
            }
            return valid;
        }

    }
}
