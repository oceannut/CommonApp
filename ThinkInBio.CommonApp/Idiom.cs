using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Utilities;

namespace ThinkInBio.CommonApp
{

    /// <summary>
    /// 常用语。
    /// </summary>
    public class Idiom
    {

        /// <summary>
        /// 编号。
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 使用范围。
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 拼音。
        /// </summary>
        public string Spell { get; set; }

        /// <summary>
        /// 时间。
        /// </summary>
        public DateTime Modification { get; set; }

        /// <summary>
        /// 保存。
        /// </summary>
        /// <param name="action"></param>
        public void Save(Action<Idiom> action)
        {
            if (string.IsNullOrWhiteSpace(this.Scope) 
                || string.IsNullOrWhiteSpace(this.Content))
            {
                throw new InvalidOperationException();
            }

            string contentSegment = this.Content.Length <= 16 ? this.Content : this.Content.Substring(0, 16);
            this.Spell = ChineseToInitial.GetChineseInitial(contentSegment).ToLower();
            this.Modification = DateTime.Now;

            if (action != null)
            {
                action(this);
            }
        }

        /// <summary>
        /// 更新。
        /// </summary>
        /// <param name="action"></param>
        public void Update(Action<Idiom> action)
        {
            if (this.Id == 0
                || string.IsNullOrWhiteSpace(this.Scope)
                || string.IsNullOrWhiteSpace(this.Content))
            {
                throw new InvalidOperationException();
            }

            string contentSegment = this.Content.Length <= 16 ? this.Content : this.Content.Substring(0, 16);
            this.Spell = ChineseToInitial.GetChineseInitial(contentSegment).ToLower();
            this.Modification = DateTime.Now;

            if (action != null)
            {
                action(this);
            }
        }

    }

}
