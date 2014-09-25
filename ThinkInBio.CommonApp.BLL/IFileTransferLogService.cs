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

    }
}
