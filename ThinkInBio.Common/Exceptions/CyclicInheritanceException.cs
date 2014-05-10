using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using R = ThinkInBio.Common.Properties.Resources;

namespace ThinkInBio.Common.Exceptions
{

    public class CyclicInheritanceException : BusinessLayerException
    {

        public object Id { get; set; }

        public CyclicInheritanceException()
            : base(R.CyclicInheritanceEx)
        {
        }

        public CyclicInheritanceException(object id)
            : base(string.Format("{0}: The id is {1}.", R.CyclicInheritanceEx, id))
        {
            this.Id = id;
        }

    }

}
