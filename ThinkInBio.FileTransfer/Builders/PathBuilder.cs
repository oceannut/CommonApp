using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace ThinkInBio.FileTransfer
{
    public class PathBuilder: UploadFileBuilder
    {

        private static int count;

        public PathBuilder() { }

        public PathBuilder(UploadFileBuilder next)
        {
            this.Next = next;
        }

        protected override void Build(UploadFile uploadFile)
        {
            DateTime timeStamp;
            if (uploadFile.TimeStamp.HasValue)
            {
                timeStamp = uploadFile.TimeStamp.Value;
            }
            else
            {
                timeStamp = DateTime.Now;
            }
            int number = NextNumber();
            uploadFile.Path = Path.Combine(timeStamp.ToString("yyyy"), 
                timeStamp.ToString("MM"), 
                timeStamp.ToString("dd"),
                string.Concat(timeStamp.ToString("yyyyMMddHHmmss"), number.ToString("D3"), Path.GetExtension(uploadFile.Name)));
        }

        private int NextNumber()
        {
            Interlocked.CompareExchange(ref count, 0, 1000);
            int number = Interlocked.Increment(ref count);
            if (number >= 1000)
            {
                return NextNumber();
            }
            else
            {
                return number;
            }
        }

    }
}
