using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Common.Caching
{

    public interface ICache
    {

        void Add(string key, object value);

        void Remove(string key);

        object Get(string key);

    }

}
