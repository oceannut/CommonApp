using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Spring.Aop;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.Common.ExceptionHandling;

namespace ThinkInBio.Spring.Aop
{
    public class ThrowsAdvice : IThrowsAdvice
    {

        internal IExceptionHandler ExceptionHandler { get; set; }

        public void AfterThrowing(Exception ex)
        {
            bool handle = true; //指示是否对异常作处理。

            //先测试当前异常是否为业务逻辑异常。
            //注明：业务逻辑异常都继承于BusinessLayerException。
            Type exceptionType = typeof(Exception);
            Type businessLayerExceptionType = typeof(BusinessLayerException);
            Type type = ex.GetType();
            while (!exceptionType.Equals(type))
            {
                if (businessLayerExceptionType.Equals(type))
                {
                    //此处不对自定义异常进行异常处理，因为自定义异常一般交给呈现层（界面）处理。
                    handle = false;
                    break;
                }
                type = type.BaseType;
            }

            //处理异常。
            if (handle)
            {
                bool rethrow = ExceptionHandler.HandleException(ex);
                if (rethrow)
                {
                    throw ex;
                }
            }
        }

    }
}
