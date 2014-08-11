using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Common.Audit
{

    public class TrailAttribute : Attribute
    {

        public string Resource { get; set; }
        public TrailMethodType MethodType { get; set; }

        public TrailAttribute(string resource)
        {
            if (string.IsNullOrWhiteSpace(resource))
            {
                throw new ArgumentNullException();
            }
            this.Resource = resource;
        }

        public TrailAttribute(string resource, TrailMethodType methodType)
            : this(resource)
        {
            this.MethodType = methodType;
        }

    }

}
