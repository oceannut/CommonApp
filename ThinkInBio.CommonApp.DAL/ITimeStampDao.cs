﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp.DAL
{

    public interface ITimeStampDao
    {

        DateTime? Next();

    }

}
