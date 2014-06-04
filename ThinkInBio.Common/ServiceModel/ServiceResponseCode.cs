using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Common.ServiceModel
{
    
    /// <summary>
    /// 服务响应编码。
    /// </summary>
    public enum ServiceResponseCode
    {

        /// <summary>
        /// 正常。
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 系统异常。
        /// </summary>
        Exception = 1,

        ArgumentException = 10,

        ArgumentNullException = 11,

        ArgumentOutOfRangeException = 12,

        ObjectNotFoundException = 20,

        ObjectAlreadyExistedException = 21,

        AuthenticationFailure = 101

    }

}
