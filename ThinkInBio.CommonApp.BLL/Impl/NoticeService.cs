using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.CommonApp;
using ThinkInBio.CommonApp.DAL;

namespace ThinkInBio.CommonApp.BLL.Impl
{
    public class NoticeService : INoticeService
    {

        internal INoticeDao NoticeDao { get; set; }
        internal IBizNotificationDao BizNotificationDao { get; set; }

        public void SaveNotice(Notice notice)
        {
            SaveNotice(notice, null);
        }

        public void SaveNotice(Notice notice, ICollection<BizNotification> notificationList)
        {
            if (notice == null)
            {
                throw new ArgumentNullException();
            }

            NoticeDao.Save(notice);
            if (notificationList != null && notificationList.Count > 0)
            {
                BizNotificationDao.Save(notificationList);
            }
        }

        public void UpdateNotice(Notice notice)
        {
            UpdateNotice(notice, null);
        }

        public void UpdateNotice(Notice notice, ICollection<BizNotification> notificationList)
        {
            if (notice == null)
            {
                throw new ArgumentNullException();
            }

            NoticeDao.Update(notice);
            if (notificationList != null && notificationList.Count > 0)
            {
                BizNotificationDao.Save(notificationList);
            }
        }

        public void DeleteNotice(long id)
        {
            DeleteNotice(id, null);
        }

        public void DeleteNotice(long id, ICollection<BizNotification> notificationList)
        {
            if (id == 0)
            {
                throw new ArgumentNullException();
            }

            Notice notice = GetNotice(id);
            if (notice == null)
            {
                throw new ObjectNotFoundException(id);
            }
            NoticeDao.Delete(notice);
            if (notificationList != null && notificationList.Count > 0)
            {
                BizNotificationDao.Save(notificationList);
            }
        }

        public void DeleteNotice(Notice notice)
        {
            if (notice == null)
            {
                throw new ArgumentNullException();
            }
            NoticeDao.Delete(notice);
        }

        public Notice GetNotice(long id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException();
            }

            return NoticeDao.Get(id);
        }

        public long GetNoticeCount(DateTime? startTime, DateTime? endTime)
        {
            return NoticeDao.GetCount(startTime, endTime);
        }

        public IList<Notice> GetNoticeList(DateTime? startTime, DateTime? endTime, int startRowIndex, int maxRowsCount)
        {
            return NoticeDao.GetList(startTime, endTime, false, startRowIndex, maxRowsCount);
        }

    }
}
