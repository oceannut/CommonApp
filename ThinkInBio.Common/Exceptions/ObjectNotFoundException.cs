using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using R = ThinkInBio.Common.Properties.Resources;

namespace ThinkInBio.Common.Exceptions
{
    public class ObjectNotFoundException : BusinessLayerException
    {

        public object Id { get; set; }

        public ObjectNotFoundException(object id)
            : base(string.Format(R.ObjectNotFoundEx, id))
        {
            this.Id = id;
        }

    }
}
