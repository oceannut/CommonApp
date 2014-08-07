using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using R = ThinkInBio.Common.Properties.Resources;

namespace ThinkInBio.Common.Exceptions
{
    public class CacheException : BusinessLayerException
    {

        public CacheException(string message)
            : base(message)
        {
        }

        public CacheException(string message, Exception exception)
            : base(message, exception)
        {
        }

    }
}
