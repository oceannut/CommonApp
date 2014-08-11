using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.Net;

using System.Security.Principal;
using System.IdentityModel.Policy;
using System.IdentityModel.Claims;

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

            //List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
            //policies.Add(new CustomAuthorizationPolicy());
            //host.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();
            //host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;

            return host;
        }

        protected override System.ServiceModel.ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = base.CreateServiceHost(serviceType, baseAddresses);
            host.Authorization.ServiceAuthorizationManager = context.GetObject<ServiceAuthorizationManager>("SessionServiceAuthorizationManager");

            //List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
            //policies.Add(new CustomAuthorizationPolicy());
            //host.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();
            //host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;

            return host;  
        }


        //class CustomAuthorizationPolicy : IAuthorizationPolicy
        //{
        //    string id = Guid.NewGuid().ToString();

        //    public string Id
        //    {
        //        get { return this.id; }
        //    }

        //    public ClaimSet Issuer
        //    {
        //        get { return ClaimSet.System; }
        //    }

        //    public bool Evaluate(EvaluationContext context, ref object state)
        //    {
        //        object obj;
        //        if (!context.Properties.TryGetValue("Identities", out obj))
        //            return false;

        //        IList<IIdentity> identities = obj as IList<IIdentity>;
        //        if (obj == null || identities.Count <= 0)
        //            return false;

        //        context.Properties["Principal"] = new CustomPrincipal(identities[0]);
        //        return true;
        //    }
        //}

        //class CustomPrincipal : IPrincipal
        //{
        //    IIdentity identity;
        //    public CustomPrincipal(IIdentity identity)
        //    {
        //        this.identity = identity;
        //    }

        //    public IIdentity Identity
        //    {
        //        get { return this.identity; }
        //    }

        //    public bool IsInRole(string role)
        //    {
        //        return true;
        //    }
        //}

    }

}
