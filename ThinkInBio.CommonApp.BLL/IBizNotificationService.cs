using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.CommonApp;

namespace ThinkInBio.CommonApp.BLL
{

    public interface IBizNotificationService
    {

        void SaveNotification(BizNotification bizNotification);

        void SaveNotification(ICollection<BizNotification> col);

        void UpdateNotification(BizNotification bizNotification);

        BizNotification GetNotification(long id);

        IList<BizNotification> GetBizNotificationList(string resource, string resourceId);

        int GetUntreatedBizNotificationCountByReceiver(string receiver);

        IList<BizNotification> GetUntreatedBizNotificationListByReceiver(string receiver);

        IList<BizNotification> GetUntreatedBizNotificationListByReceiver(string receiver, string resource);

        IList<BizNotification> GetBizNotificationList(DateTime? startTime, DateTime? endTime,
            string sender, string receiver, string resource,
            int startRowIndex, int maxRowsCount);

    }

}
