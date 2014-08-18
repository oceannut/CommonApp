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

        public void SaveNotification(ICollection<BizNotification> col)
        {
            if (col == null)
            {
                throw new ArgumentNullException();
            }
            foreach (BizNotification item in col)
            {
                BizNotificationDao.Save(item);
            }
        }

        public void UpdateNotification(BizNotification bizNotification)
        {
            if (bizNotification == null)
            {
                throw new ArgumentNullException();
            }
            BizNotificationDao.Update(bizNotification);
        }

        public BizNotification GetNotification(long id)
        {
            if (id == 0)
            {
                throw new ArgumentException();
            }
            return BizNotificationDao.Get(id);
        }

        public IList<BizNotification> GetBizNotificationList(string resource, string resourceId)
        {
            return BizNotificationDao.GetList(null, null, null, null, null, resource, resourceId, false, 0, int.MaxValue);
        }

        public int GetUntreatedBizNotificationCountByReceiver(string receiver)
        {
            if (string.IsNullOrWhiteSpace(receiver))
            {
                throw new ArgumentNullException();
            }
            return BizNotificationDao.GetCount(null, null, false, null, receiver, null, null);
        }

        public IList<BizNotification> GetUntreatedBizNotificationListByReceiver(string receiver)
        {
            if (string.IsNullOrWhiteSpace(receiver))
            {
                throw new ArgumentNullException();
            }
            return BizNotificationDao.GetList(null, null, false, null, receiver, null, null, false, 0, int.MaxValue);
        }

        public IList<BizNotification> GetUntreatedBizNotificationListByReceiver(string receiver, string resource)
        {
            if (string.IsNullOrWhiteSpace(receiver))
            {
                throw new ArgumentNullException();
            }
            return BizNotificationDao.GetList(null, null, false, null, receiver, resource, null, false, 0, int.MaxValue);
        }

    }
}
