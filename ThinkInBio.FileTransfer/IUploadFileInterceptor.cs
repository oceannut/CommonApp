using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.FileTransfer
{

    public interface IUploadFileInterceptor
    {

        void Handle(UploadFile uploadFile);

    }

}
