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

        void UpdateNotification(BizNotification bizNotification);

        IList<BizNotification> GetUnreceivedBizNotificationByReceiver(string receiver);

        IList<BizNotification> GetUnreceivedBizNotificationByReceiver(string receiver, string resource);

    }

}
