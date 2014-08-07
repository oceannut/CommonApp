using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Exceptions;

namespace ThinkInBio.CommonApp
{

    /// <summary>
    /// 定义了用户信息及相关操作。
    /// </summary>
    public class User
    {

        #region properties

        /// <summary>
        /// 用户名。
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 验证码。
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 角色。
        /// </summary>
        public IList<string> Roles { get; set; }

        /// <summary>
        /// 显示姓名。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属组织或部门。
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 创建时间。
        /// </summary>
        public DateTime Creation { get; set; }

        /// <summary>
        /// 修改时间。
        /// </summary>
        public DateTime Modification { get; set; }

        /// <summary>
        /// 最近一次登录时间。
        /// </summary>
        public DateTime? LastLogin { get; set; }

        #endregion

        #region constructors

        /// <summary>
        /// 构建一个用户。
        /// </summary>
        public User() { }

        /// <summary>
        /// 构建一个用户。
        /// </summary>
        /// <param name="username">用户名。</param>
        /// <param name="pwd">验证码。</param>
        /// <param name="passwordProvider">验证码验证服务。</param>
        public User(string username, string pwd, 
            IPasswordProvider passwordProvider)
        {
            if (string.IsNullOrWhiteSpace(username) 
                || string.IsNullOrWhiteSpace(pwd))
            {
                throw new ArgumentNullException();
            }
            this.Username = username;
            this.Pwd = passwordProvider.Encrypt(pwd);
        }

        /// <summary>
        /// 构建一个用户。
        /// </summary>
        /// <param name="username"></param>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <param name="creation"></param>
        /// <param name="modification"></param>
        public User(string username,
            string name, string group,
            DateTime creation, 
            DateTime modification)
        {
            if (string.IsNullOrWhiteSpace(username)
                || creation == DateTime.MinValue
                || modification == DateTime.MinValue)
            {
                throw new ArgumentException();
            }
            this.Username = username;
            this.Name = name;
            this.Group = group;
            this.Creation = creation;
            this.Modification = modification;
        }

        #endregion

        /// <summary>
        /// 保存用户。
        /// </summary>
        /// <param name="isUserExisted">判断用户是否存在的操作定义。</param>
        /// <param name="action">保存操作定义。</param>
        public void Save(Func<string, bool> isUserExisted, 
            Action<User> action)
        {
            if (string.IsNullOrWhiteSpace(this.Username))
            {
                throw new InvalidOperationException();
            }
            if (isUserExisted != null && isUserExisted(this.Username))
            {
                throw new ObjectAlreadyExistedException(this.Username);
            }
            DateTime timeStamp = DateTime.Now;
            this.Creation = timeStamp;
            this.Modification = timeStamp;
            if (action != null)
            {
                action(this);
            }
        }

        /// <summary>
        /// 更新用户。
        /// </summary>
        /// <param name="action">更新操作定义。</param>
        public void Update(Action<User> action)
        {
            if (string.IsNullOrWhiteSpace(this.Username))
            {
                throw new InvalidOperationException();
            }
            DateTime timeStamp = DateTime.Now;
            this.Modification = timeStamp;
            if (action != null)
            {
                action(this);
            }
        }

        /// <summary>
        /// 验证用户的合法性，如登录输入的密码验证。
        /// </summary>
        /// <param name="pwd">输入的验证码。</param>
        /// <param name="provider">验证服务。</param>
        /// <returns>返回true表示用户验证通过；否则返回false表示未通过验证，用户不合法。</returns>
        public bool Authenticate(string pwd, IAuthProvider provider)
        {
            if (string.IsNullOrWhiteSpace(pwd))
            {
                throw new ArgumentNullException("pwd");
            }
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            return provider.Authenticate(pwd, this);
        }

    }

}
