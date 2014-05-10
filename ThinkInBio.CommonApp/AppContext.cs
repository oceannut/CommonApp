﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    public class AppContext
    {

        private GenericGuid genericGuid = new GenericGuid();
        private IGuid guid;

        public AppContext()
        {
            guid = genericGuid;
        }

        public IGuid Guid
        {
            get { return guid; }
            set { guid = value; }
        }

        public GenericGuid GenericGuid
        {
            get { return genericGuid; }
        }

    }

}
