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

        IList<BizNotification> GetUntreatedBizNotificationByReceiver(string receiver);

        IList<BizNotification> GetUntreatedBizNotificationByReceiver(string receiver, string resource);

    }

}
