using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Net;

using ThinkInBio.Common.Caching;

namespace ThinkInBio.Spring.ServiceModel
{
    public class SessionServiceAuthorizationManager : ServiceAuthorizationManager
    {

        internal ICache Session { get; set; }

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            var ctx = WebOperationContext.Current;
            var auth = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization];
            bool allowed = true;
            if (!string.IsNullOrWhiteSpace(auth))
            {
                int index = auth.IndexOf(' ');
                string authType = index > 0 ? auth.Substring(0, index) : string.Empty;
                if ("Basic" == authType)
                {
                    int index2 = auth.IndexOf(' ', index + 1);
                    string username = (index2 > 0 && index2 > index) ? auth.Substring(index + 1, index2 - index - 1) : string.Empty;
                    if (!string.IsNullOrWhiteSpace(username))
                    {
                        string pwd = Session.Get(username) as string;
                        if (string.IsNullOrWhiteSpace(pwd))
                        {
                            //登录session超时，要求重新登录验证。
                            ctx.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;
                            return false;
                        }
                        else
                        {
                            string signature = auth.Substring(index2 + 1);
                            if (string.IsNullOrEmpty(signature) || signature != pwd)
                            {
                                allowed = false;
                            }
                        }
                    }
                    else
                    {
                        allowed = false;
                    }
                }
                else
                {
                    allowed = false;
                }
            }
            else
            {
                allowed = false;
            }

            if (!allowed)
            {
                ctx.OutgoingResponse.StatusCode = HttpStatusCode.MethodNotAllowed;
            }

            return allowed;
        }

    }
}
