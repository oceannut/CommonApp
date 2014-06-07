using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.CommonApp;
using ThinkInBio.CommonApp.DAL;

namespace ThinkInBio.CommonApp.BLL.Impl
{
    public class BizNotificationService : IBizNotificationService
    {

        internal IBizNotificationDao BizNotificationDao { get; set; }

        public void SaveNotification(BizNotification bizNotification)
        {
            if (bizNotification == null)
            {
                throw new ArgumentNullException();
            }
            BizNotificationDao.Save(bizNotification);
        }

        public void UpdateNotification(BizNotification bizNotification)
        {
            if (bizNotification == null)
            {
                throw new ArgumentNullException();
            }
            BizNotificationDao.Update(bizNotification);
        }

        public IList<BizNotification> GetUnreceivedBizNotificationByReceiver(string receiver)
        {
            return GetUnreceivedBizNotificationByReceiver(receiver, null);
        }

        public IList<BizNotification> GetUnreceivedBizNotificationByReceiver(string receiver, string resource)
        {
            if (string.IsNullOrWhiteSpace(receiver))
            {
                throw new ArgumentNullException();
            }
            return BizNotificationDao.GetList(null, null, false, null, receiver, resource, false, 0, int.MaxValue);
        }

    }
}
