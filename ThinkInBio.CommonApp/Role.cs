using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    public class Role
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public IList<string> Users { get; set; }

        public IList<string> Premissions { get; set; }

    }

}
