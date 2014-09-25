using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ThinkInBio.FileTransfer
{

    /// <summary>
    /// 上传文件定义。
    /// </summary>
    public class UploadFile
    {

        /// <summary>
        /// 文件名。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件尺寸。
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 上传后的文件路径。
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 上传中发生的错误。
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 时间戳。
        /// </summary>
        public DateTime? TimeStamp { get; set; }

    }

}
