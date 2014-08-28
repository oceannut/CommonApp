using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Exceptions;

namespace ThinkInBio.Scheduling
{

    public class ScheduleException : BusinessLayerException
    {

        public ScheduleException()
            : base()
        {
        }

        public ScheduleException(string message)
            : base(message)
        {
        }

        public ScheduleException(string message, Exception exception) :
            base(message, exception)
        {
        }

        public ScheduleException(Exception exception) :
            base("", exception)
        {
        }

    }

}
