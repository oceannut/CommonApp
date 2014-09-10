using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.CommonApp.DAL;

namespace ThinkInBio.CommonApp.BLL.Impl
{
    public class TimeStampService : ITimeStampService
    {

        internal ITimeStampDao TimeStampDao { get; set; }

        public DateTime? NextTimeStamp()
        {
            return TimeStampDao.Next();
        }

    }
}
