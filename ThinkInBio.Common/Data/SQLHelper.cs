using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Common.Data
{
    public static class SQLHelper
    {

        public static void AppendOp(StringBuilder buffer, ICollection parameters)
        {
            if (parameters.Count == 0)
            {
                buffer.Append(" where ");
            }
            else
            {
                buffer.Append(" and ");
            }
        }

    }
}
