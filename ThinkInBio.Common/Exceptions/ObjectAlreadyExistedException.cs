using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using R = ThinkInBio.Common.Properties.Resources;

namespace ThinkInBio.Common.Exceptions
{
    public class ObjectAlreadyExistedException : BusinessLayerException
    {

        public object Id { get; set; }

        public ObjectAlreadyExistedException(object id)
            : base(string.Format(R.ObjectAlreadyExistedEx, id))
        {
            this.Id = id;
        }

    }
}
