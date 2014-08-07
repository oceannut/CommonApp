using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.Common.ExceptionHandling;

using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace ThinkInBio.Entlib.Caching
{
    public class LogRefreshAction : ICacheItemRefreshAction
    {

        internal IExceptionHandler ExceptionHandler { get; set; }

        public void Refresh(string removedKey, object expiredValue, CacheItemRemovedReason removalReason)
        {
            if (removalReason == CacheItemRemovedReason.Scavenged)
            {
                ExceptionHandler.HandleException(new CacheException("The cache is too small to caches object any more."));
            }
        }

    }
}
