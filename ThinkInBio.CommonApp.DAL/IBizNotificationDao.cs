using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Data;

namespace ThinkInBio.CommonApp.DAL
{

    public interface IBizNotificationDao : IDao<BizNotification>
    {

        int GetCount(DateTime? startTime, DateTime? endTime, bool? isReceived,
            string sender, string receiver, string resource, string resourceId);

        IList<BizNotification> GetList(DateTime? startTime, DateTime? endTime, bool? isReceived,
            string sender, string receiver, string resource, string resourceId,
            bool asc, int startRowIndex, int maxRowsCount);

    }

}
