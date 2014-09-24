using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ThinkInBio.FileTransfer
{

    public class LocalStorage : IStorage
    {

        private string rootDir;

        public string RootDir
        {
            get { return rootDir; }
        }

        public LocalStorage(string rootDir)
        {
            if (string.IsNullOrWhiteSpace(rootDir))
            {
                throw new ArgumentNullException();
            }
            this.rootDir = rootDir;
            if (!Directory.Exists(this.rootDir))
            {
                Directory.CreateDirectory(this.rootDir);
            }
        }

        public void Save(UploadFile uploadFile, Stream stream)
        {
            if (uploadFile == null || stream == null)
            {
                throw new ArgumentNullException();
            }
            string path = Path.Combine(this.rootDir, uploadFile.Name);
            bool append = File.Exists(path) || uploadFile.Size > stream.Length;

            if (append)
            {
                using (FileStream fs = File.Open(path, FileMode.Append))
                {
                    stream.CopyTo(fs);
                    fs.Flush();
                }
            }
            else
            {
                using (FileStream fs = File.OpenWrite(path))
                {
                    stream.CopyTo(fs);
                    fs.Flush();
                }

            }
        }

        public void Save(UploadFile uploadFile, byte[] bytes)
        {
            throw new NotImplementedException();
        }

    }

}
