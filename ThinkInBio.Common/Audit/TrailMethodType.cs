using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Common.Audit
{
    public enum TrailMethodType
    {

        /// <summary>
        /// 登录。
        /// </summary>
        SignIn = 101,
        /// <summary>
        /// 登出。
        /// </summary>
        SignOut = 102,
        /// <summary>
        /// 注册。
        /// </summary>
        SignUp = 103,
        /// <summary>
        /// 创建。
        /// </summary>
        Create = 1,
        /// <summary>
        /// 更新
        /// </summary>
        Update = 2,
        /// <summary>
        /// 读取。
        /// </summary>
        Read = 3,
        /// <summary>
        /// 删除。
        /// </summary>
        Delete = 4

    }
}
