using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.CommonApp.DAL;

namespace ThinkInBio.CommonApp.BLL.Impl
{
    public class FileTransferLogService : IFileTransferLogService
    {

        internal IFileTransferLogDao FileTransferLogDao { get; set; }

        public void SaveFileTransferLog(FileTransferLog fileTransferLog)
        {
            if (fileTransferLog == null)
            {
                throw new ArgumentNullException();
            }
            FileTransferLogDao.Save(fileTransferLog);
        }

        public void SaveFileTransferLog(ICollection<FileTransferLog> col)
        {
            if (col == null || col.Count == 0)
            {
                throw new ArgumentNullException();
            }
            FileTransferLogDao.Save(col);
        }

    }
}
