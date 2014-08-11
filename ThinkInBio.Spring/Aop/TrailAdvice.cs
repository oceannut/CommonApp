using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SAop = Spring.Aop;

using ThinkInBio.Common.ExceptionHandling;

namespace ThinkInBio.Spring.Aop
{
    public class TrailAdvice : SAop.IAfterReturningAdvice
    {

        internal IExceptionHandler ExceptionHandler { get; set; }

        public void AfterReturning(object returnValue, System.Reflection.MethodInfo method, object[] args, object target)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("\n[AfterAdvice]  method : " + method.Name);
            buffer.Append("\n    Target  : " + target);
            buffer.Append("\n    The arguments : ");
            if (args != null)
            {
                foreach (object arg in args)
                {
                    buffer.Append(arg + "\n ");
                }
            }
            buffer.Append("\n    The return value is : " + returnValue);
            var a = Attribute.GetCustomAttribute(method, typeof(ThinkInBio.Common.Audit.TrailAttribute), false) as ThinkInBio.Common.Audit.TrailAttribute;
            int threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            buffer.Append("\n    ").Append(a.Resource).Append(".").Append(a.MethodType);

            ExceptionHandler.HandleException(new Exception(buffer.ToString()));
        }
    }
}
