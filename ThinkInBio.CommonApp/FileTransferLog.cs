using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ThinkInBio.CommonApp
{

    /// <summary>
    /// 文件传输方向。
    /// </summary>
    public enum FileTransferDirection
    {
        /// <summary>
        /// 上传。
        /// </summary>
        Upload = 0,
        /// <summary>
        /// 下载。
        /// </summary>
        Download = 1
    }

    /// <summary>
    /// 文件传输日志。
    /// </summary>
    public class FileTransferLog
    {

        /// <summary>
        /// 编号。
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 传输方向。
        /// </summary>
        public FileTransferDirection Direction { get; set; }

        /// <summary>
        /// 显示标题（一般为上传前可读性好的文件名）。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文件标识（系统统一分配的新文件名或路径）。
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// 传输断点。
        /// </summary>
        public long Position { get; set; }

        /// <summary>
        /// 文件尺寸（字节数）。
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 指示上传的文件是否已被移除。
        /// </summary>
        public bool IsRemoved { get; set; }

        ///// <summary>
        ///// 传输用户。
        ///// </summary>
        //public string User { get; set; }

        /// <summary>
        /// 创建时间。
        /// </summary>
        public DateTime Creation { get; set; }

        /// <summary>
        /// 修改时间。
        /// </summary>
        public DateTime Modification { get; set; }

        public FileTransferLog() { }

        public FileTransferLog(string user, string filename, long totalBytes)
        {
        }

        public FileTransferLog(string user, FileInfo fileInfo)
        {
        }

        public void Save(Action action)
        {
        }

    }

}
