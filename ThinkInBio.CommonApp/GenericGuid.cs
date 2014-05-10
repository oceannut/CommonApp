using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    public class GenericGuid : IGuid
    {

        public object Next()
        {
            return Guid.NewGuid().ToString();
        }

    }

}
