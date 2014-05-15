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
        /// 编号。
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户名。
        /// </summary>
        public string Username { get; internal set; }

        /// <summary>
        /// 明文验证码。
        /// </summary>
        public string PlainPwd { get; internal set; }

        /// <summary>
        /// 密文验证码。
        /// </summary>
        public string EncryptPwd { get; internal set; }

        /// <summary>
        /// 电子邮件。
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 角色。
        /// </summary>
        public IList<long> Roles { get; set; }

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
        public DateTime Creation { get; internal set; }

        /// <summary>
        /// 修改时间。
        /// </summary>
        public DateTime Modification { get; internal set; }

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        public User() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        public User(string username, string pwd)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException();
            }
            this.Username = username;
            this.PlainPwd = pwd;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isUserExisted"></param>
        /// <param name="action"></param>
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
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void Update(Action<User> action)
        {
            if (this.Id == 0 || string.IsNullOrWhiteSpace(this.Username))
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

    }

}
