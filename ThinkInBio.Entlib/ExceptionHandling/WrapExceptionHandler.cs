using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.ExceptionHandling;

namespace ThinkInBio.Entlib.ExceptionHandling
{

    public class WrapExceptionHandler : EntlibExceptionHandler, IExceptionHandler
    {
        public bool HandleException(Exception ex)
        {
            return HandleException(EntlibExceptionHandler.EXCEPTION_POLICY_WRAP, ex);
        }
    }

}
