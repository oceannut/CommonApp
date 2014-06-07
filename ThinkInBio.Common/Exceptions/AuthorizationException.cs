using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using R = ThinkInBio.Common.Properties.Resources;

namespace ThinkInBio.Common.Exceptions
{
    public class AuthorizationException : BusinessLayerException
    {

        public AuthorizationException(string user, string permission)
            : base(string.Format(R.AuthorizationEx, user, permission))
        {
        }

    }
}
