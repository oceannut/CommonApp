using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    /// <summary>
    /// 定义了用户信息及相关操作。
    /// </summary>
    public class User
    {

        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PlainPwd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EncryptPwd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<long> Roles { get; set; }

    }

}
