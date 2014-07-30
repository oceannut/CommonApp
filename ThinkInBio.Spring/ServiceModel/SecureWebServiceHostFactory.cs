using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Net;

using SSMA = Spring.ServiceModel.Activation;

namespace ThinkInBio.Spring.ServiceModel
{

    /// <summary>
    /// WCF4.0 –- RESTful WCF Services (4) (Basic Security)
    /// http://blog.csdn.net/fangxing80/article/details/6263780
    /// 
    /// WCF, RESTful Web Services and custom authentication
    /// http://stackoverflow.com/questions/6021612/wcf-restful-web-services-and-custom-authentication
    /// </summary>
    public class SecureWebServiceHostFactory : SSMA.WebServiceHostFactory
    {

        public override System.ServiceModel.ServiceHostBase CreateServiceHost(string reference, Uri[] baseAddresses)
        {
            var host = base.CreateServiceHost(reference, baseAddresses);
            host.Authorization.ServiceAuthorizationManager = new MyServiceAuthorizationManager();
            return host;  
        }

        protected override System.ServiceModel.ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = base.CreateServiceHost(serviceType, baseAddresses);
            host.Authorization.ServiceAuthorizationManager = new MyServiceAuthorizationManager();
            return host;  
        }

    }

    public class MyServiceAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            var ctx = WebOperationContext.Current;
            var auth = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization];
            if (string.IsNullOrEmpty(auth) || auth != "fangxing/123")
            {
                ctx.OutgoingResponse.StatusCode = HttpStatusCode.MethodNotAllowed;
                return false;
            }
            return true;
        }
    }

}
