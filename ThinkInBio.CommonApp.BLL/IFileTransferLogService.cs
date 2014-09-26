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

        void UpdateFileTransferLog(FileTransferLog fileTransferLog);

        FileTransferLog GetFileTransferLog(long id);

        long GetFileTransferLogCount(DateTime? startTime, DateTime? endTime, string user, FileTransferDirection? direction);

        IList<FileTransferLog> GetFileTransferLogList(DateTime? startTime, DateTime? endTime, string user, FileTransferDirection? direction,
            int startRowIndex, int maxRowsCount);

    }
}
