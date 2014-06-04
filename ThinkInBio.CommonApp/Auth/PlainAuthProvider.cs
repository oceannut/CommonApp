﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    public class PlainAuthProvider : IAuthProvider
    {
        public bool Authenticate(string pwd, User user)
        {
            if (string.IsNullOrWhiteSpace(pwd)
                || user == null)
            {
                throw new ArgumentNullException();
            }

            return pwd == user.Pwd;
        }
    }

}
