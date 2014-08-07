using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

using ThinkInBio.Common.Caching;

namespace ThinkInBio.Entlib.Caching
{

    public class GenericCache : ICache
    {

        protected readonly ICacheManager cache;

        public GenericCache()
        {
            this.cache = CacheFactory.GetCacheManager();
        }

        public GenericCache(string cacheName)
        {
            if (cacheName == null)
            {
                throw new ArgumentNullException();
            }
            this.cache = CacheFactory.GetCacheManager(cacheName);
        }

        public virtual void Add(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            cache.Add(key, value);
        }

        public void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }
            cache.Remove(key);
        }

        public object Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }
            return cache.GetData(key);
        }

    }

}
