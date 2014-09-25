using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ThinkInBio.FileTransfer
{

    public interface IStorage
    {

        string RootDir { get; }

        void Save(UploadFile uploadFile, Stream stream);

        void Save(UploadFile uploadFile, byte[] bytes);

        void Delete(string path);

    }

}
