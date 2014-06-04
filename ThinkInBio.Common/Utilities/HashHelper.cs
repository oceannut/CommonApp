using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace ThinkInBio.Common.Utilities
{

    /// <summary>
    /// Hash工具。
    /// </summary>
    public static class HashHelper
    {

        private static HashAlgorithm algorithm = new SHA1Managed();

        /// <summary>
        /// Hash加密方法
        /// </summary>
        /// <param name="source">待加密的字符串</param>
        /// <returns>经过加密的字符串</returns>
        public static string Encrypt(string source)
        {
            return Encrypt(source, Encoding.UTF8);
        }

        /// <summary>
        /// Hash加密方法
        /// </summary>
        /// <param name="source">待加密的字符串</param>
        /// <param name="encoding">编码。</param>
        /// <returns>经过加密的字符串</returns>
        public static string Encrypt(string source, string encoding)
        {
            return Encrypt(source, Encoding.GetEncoding(encoding));
        }

        /// <summary>
        /// Hash加密方法
        /// </summary>
        /// <param name="source">待加密的字符串</param>
        /// <param name="encoding">编码。</param>
        /// <returns>经过加密的字符串</returns>
        public static string Encrypt(string source, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentNullException();
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            byte[] bytIn = encoding.GetBytes(source);
            byte[] bytOut = algorithm.ComputeHash(bytIn);
            return Convert.ToBase64String(bytOut);
        }

    }

}
