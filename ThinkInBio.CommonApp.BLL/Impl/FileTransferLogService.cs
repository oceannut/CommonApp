using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.FileTransfer;
using ThinkInBio.Common.Exceptions;
using ThinkInBio.CommonApp.DAL;

namespace ThinkInBio.CommonApp.BLL.Impl
{
    public class FileTransferLogService : IFileTransferLogService
    {

        internal IFileTransferLogDao FileTransferLogDao { get; set; }
        internal FileTransferManager FileTransferManager { get; set; }

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

        public void UpdateFileTransferLog4DeleteFile(FileTransferLog fileTransferLog)
        {
            if (fileTransferLog == null)
            {
                throw new ArgumentNullException();
            }
            FileTransferManager.Delete(fileTransferLog.Path);
            fileTransferLog.IsRemoved = true;
            FileTransferLogDao.Update(fileTransferLog);
        }

        public void DeleteFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException();
            }
            FileTransferManager.Delete(path);
        }

        public FileTransferLog GetFileTransferLog(long id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException();
            }
            return FileTransferLogDao.Get(id);
        }

        public long GetFileTransferLogCount(DateTime? startTime, DateTime? endTime, string user, FileTransferDirection? direction)
        {
            return FileTransferLogDao.GetCount(startTime, endTime, user, direction);
        }

        public IList<FileTransferLog> GetFileTransferLogList(DateTime? startTime, DateTime? endTime, string user, FileTransferDirection? direction, 
            int startRowIndex, int maxRowsCount)
        {
            return FileTransferLogDao.GetList(startTime, endTime, user, direction, false, startRowIndex, maxRowsCount);
        }

    }
}
