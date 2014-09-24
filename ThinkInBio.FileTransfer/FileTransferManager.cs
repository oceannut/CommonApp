using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using ThinkInBio.Common.Exceptions;

namespace ThinkInBio.FileTransfer
{

    public class FileTransferManager
    {

        private LocalStorage defaultStorage;

        internal UploadFileValidator Validator { get; set; }
        internal IDictionary<string, IStorage> StorageMap { get; set; }

        public FileTransferManager()
            : this(null)
        {
        }

        public FileTransferManager(string defaultRootDir)
        {
            string rootDir = defaultRootDir;
            if (string.IsNullOrWhiteSpace(rootDir))
            {
                rootDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
                if (!Directory.Exists(rootDir))
                {
                    Directory.CreateDirectory(rootDir);
                }
            }
            this.defaultStorage = new LocalStorage(rootDir);
        }

        public void Upload(UploadFile uploadFile, Stream stream)
        {
            Upload(null, uploadFile, stream);
        }

        public void Upload(string scope, UploadFile uploadFile, Stream stream)
        {
            if (Validator != null)
            {
                Validator.Handle(uploadFile);
            }
            if (!string.IsNullOrWhiteSpace(uploadFile.Error))
            {
                return;
            }
            IStorage storage = GetStorage(scope);
            storage.Save(uploadFile, stream);
        }

        private IStorage GetStorage(string scope)
        {
            IStorage storage = null;
            if (string.IsNullOrWhiteSpace(scope)
                || StorageMap == null || StorageMap.Count == 0)
            {
                storage = defaultStorage;
            }
            else
            {
                StorageMap.TryGetValue(scope, out storage);
            }
            if (storage == null)
            {
                throw new ObjectNotFoundException(scope);
            }
            return storage;
        }

    }

}
