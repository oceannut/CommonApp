using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    public interface ICategoryable
    {

        long ParentId { get; }

        int Sequence { get; }

    }

}
