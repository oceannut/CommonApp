using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.CommonApp;

namespace ThinkInBio.CommonApp.BLL
{

    public interface INoticeService
    {

        void SaveNotice(Notice notice);

        void SaveNotice(Notice notice, ICollection<BizNotification> notificationList);

        void UpdateNotice(Notice notice);

        void UpdateNotice(Notice notice, ICollection<BizNotification> notificationList);

        void DeleteNotice(long id);

        void DeleteNotice(long id, ICollection<BizNotification> notificationList);

        void DeleteNotice(Notice notice);

        Notice GetNotice(long id);

        long GetNoticeCount(DateTime? startTime, DateTime? endTime);

        IList<Notice> GetNoticeList(DateTime? startTime, DateTime? endTime,
            int startRowIndex, int maxRowsCount);

    }

}
