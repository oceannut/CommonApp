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
            if (string.IsNullOrWhiteSpace(uploadFile.Path))
            {
                throw new ArgumentException();
            }
            string path = Path.Combine(this.rootDir, uploadFile.Path);
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
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
            if (uploadFile == null || bytes == null || bytes.Length == 0)
            {
                throw new ArgumentNullException();
            }
            Save(uploadFile, new MemoryStream(bytes));
        }

        public void Delete(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException();
            }
            string filename = Path.Combine(this.rootDir, path);
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }

    }

}
