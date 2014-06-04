using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    public interface IAuthProvider
    {

        /// <summary>
        /// 验证用户的合法性，如登录输入的密码验证。
        /// </summary>
        /// <param name="pwd">输入的验证码。</param>
        /// <param name="user">要核对参考的用户信息。</param>
        /// <returns>返回true表示用户验证通过；否则返回false表示未通过验证，用户不合法。</returns>
        bool Authenticate(string pwd, User user);

    }

}
