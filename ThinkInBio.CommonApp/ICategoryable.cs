using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    /// <summary>
    /// 定义了分类的特性。
    /// </summary>
    public interface ICategoryable
    {

        /// <summary>
        /// 上级分类标识。
        /// </summary>
        long ParentId { get; }

        /// <summary>
        /// 显示的排序序号。
        /// </summary>
        int Sequence { get; }

    }

}
