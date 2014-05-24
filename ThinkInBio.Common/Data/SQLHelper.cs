using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Common.Data
{

    /// <summary>
    /// 
    /// </summary>
    public static class SQLHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="parameters"></param>
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
