﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    public interface IPasswordProvider
    {

        string Encrypt(string source);

    }

}
