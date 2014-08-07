using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Net;

using SSMA = Spring.ServiceModel.Activation;
using SC = Spring.Context;
using SCS = Spring.Context.Support;

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

        private static SC.IApplicationContext context = SCS.ContextRegistry.GetContext();

        public override System.ServiceModel.ServiceHostBase CreateServiceHost(string reference, Uri[] baseAddresses)
        {
            var host = base.CreateServiceHost(reference, baseAddresses);
            host.Authorization.ServiceAuthorizationManager = context.GetObject<ServiceAuthorizationManager>("SessionServiceAuthorizationManager");
            return host;  
        }

        protected override System.ServiceModel.ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = base.CreateServiceHost(serviceType, baseAddresses);
            host.Authorization.ServiceAuthorizationManager = context.GetObject<ServiceAuthorizationManager>("SessionServiceAuthorizationManager");
            return host;  
        }

    }

}
