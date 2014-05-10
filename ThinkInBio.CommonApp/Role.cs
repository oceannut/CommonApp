using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    public class Role
    {

        public long Id { get; set; }

        public string Name { get; set; }

        public IList<long> Users { get; set; }

        public IList<long> Premissions { get; set; }

    }

}
