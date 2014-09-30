using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp.BLL
{
    public interface IFileTransferLogService
    {

        void SaveFileTransferLog(FileTransferLog fileTransferLog);

        void SaveFileTransferLog(ICollection<FileTransferLog> col);

        void UpdateFileTransferLog4DeleteFile(FileTransferLog fileTransferLog);

        void DeleteFile(string path);

        FileTransferLog GetFileTransferLog(long id);

        FileTransferLog GetFileTransferLog(string path);

        long GetFileTransferLogCount(DateTime? startTime, DateTime? endTime, string user, FileTransferDirection? direction);

        IList<FileTransferLog> GetFileTransferLogList(DateTime? startTime, DateTime? endTime, string user, FileTransferDirection? direction,
            int startRowIndex, int maxRowsCount);

    }
}
