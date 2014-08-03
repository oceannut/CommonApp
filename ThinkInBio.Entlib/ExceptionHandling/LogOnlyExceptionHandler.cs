using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.ExceptionHandling;

namespace ThinkInBio.Entlib.ExceptionHandling
{
    public class LogOnlyExceptionHandler : EntlibExceptionHandler, IExceptionHandler
    {

        public bool HandleException(Exception ex)
        {
            return HandleException(EntlibExceptionHandler.EXCEPTION_POLICY_LOG_ONLY, ex);
        }

    }
}
