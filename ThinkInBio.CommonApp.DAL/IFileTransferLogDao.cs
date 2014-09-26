using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Data;
using ThinkInBio.CommonApp;

namespace ThinkInBio.CommonApp.DAL
{
    public interface IFileTransferLogDao : IDao<FileTransferLog>
    {

        long GetCount(DateTime? startTime, DateTime? endTime, string user, FileTransferDirection? direction);

        IList<FileTransferLog> GetList(DateTime? startTime, DateTime? endTime, string user, FileTransferDirection? direction,
            bool asc, int startRowIndex, int maxRowsCount);

    }
}
