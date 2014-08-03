using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.ExceptionHandling;

namespace ThinkInBio.Entlib.ExceptionHandling
{
    public class EntlibExceptionHandler
    {

        public static string EXCEPTION_POLICY_LOG_ONLY = "Log Only Policy";
        public static string EXCEPTION_POLICY_WRAP = "Wrap Policy";

        internal IExceptionHandler InnerExceptionHandler { get; set; }

        protected bool HandleException(string policyName, Exception ex)
        {
            string policy = policyName;
            if (string.IsNullOrWhiteSpace(policy))
            {
                policy = EXCEPTION_POLICY_LOG_ONLY;
            }
            try
            {
                Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex, policy);
                return true;
            }
            catch (Exception innerEx)
            {
                if (InnerExceptionHandler != null)
                {
                    InnerExceptionHandler.HandleException(innerEx);
                }
                return false;
            }
        }

    }
}
