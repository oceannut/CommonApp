using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ThinkInBio.Common.ExceptionHandling
{

    public class SimpleFileLogExceptionHandler : IExceptionHandler
    {

        private string filename = "user_log.log";

        internal string Filename
        {
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    filename = value;
                }
            }
        }

        public bool HandleException(Exception ex)
        {
            try
            {
                if (ex != null)
                {
                    string path = Utilities.PathHelper.RootFileNameAndEnsureTargetFolderExists(filename);
                    using (FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        string s = string.Format("{0}\n{1}\n\n", DateTime.Now, ex);
                        byte[] bytes = Encoding.UTF8.GetBytes(s);
                        fileStream.Write(bytes, 0, bytes.Length);
                    }
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

    }

}
