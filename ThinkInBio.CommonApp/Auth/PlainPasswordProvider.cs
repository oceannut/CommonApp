using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Utilities;

namespace ThinkInBio.CommonApp
{
    public class PlainPasswordProvider : IPasswordProvider
    {
        public string Encrypt(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentNullException();
            }

            return HashHelper.Encrypt(source);
        }
    }
}
