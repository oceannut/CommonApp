using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    /// <summary>
    /// 联系方式。
    /// </summary>
    public class Contact
    {

        /// <summary>
        /// 编号。
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 姓名。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 电子邮件。
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话。
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 手机。
        /// </summary>
        public string Cell { get; set; }

        /// <summary>
        /// 传真。
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 例如QQ、微信等账号。
        /// </summary>
        public string Im1 { get; set; }

        /// <summary>
        /// 例如QQ、微信等账号。
        /// </summary>
        public string Im2 { get; set; }

        /// <summary>
        /// 例如QQ、微信等账号。
        /// </summary>
        public string Im3 { get; set; }

        /// <summary>
        /// 地址。
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 邮编。
        /// </summary>
        public string ZipCode { get; set; }

    }

}
