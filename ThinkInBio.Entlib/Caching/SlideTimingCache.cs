using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace ThinkInBio.Entlib.Caching
{

    public class SlideTimingCache : GenericCache
    {

        private double timeout;

        public SlideTimingCache(double timeout)
            : base()
        {
            this.timeout = timeout;
        }

        public SlideTimingCache(string cacheName, double timeout)
            : base(cacheName)
        {
            this.timeout = timeout;
        }

        internal ICacheItemRefreshAction CacheItemRefreshAction { get; set; }

        public override void Add(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            try
            {
                cache.Add(key, value, CacheItemPriority.Normal, CacheItemRefreshAction, new SlidingTime(TimeSpan.FromMinutes(timeout)));
            }
            catch (Exception ex)
            {
                throw new ThinkInBio.Common.Exceptions.CacheException(
                    string.Format("The exception occured when an object of key \"{0}\" add to the cache.", key), ex);
            }
        }

    }

}
