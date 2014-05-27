using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Common.ExceptionHandling
{

    public interface IExceptionHandler
    {

        bool HandleException(Exception ex);

    }

}
