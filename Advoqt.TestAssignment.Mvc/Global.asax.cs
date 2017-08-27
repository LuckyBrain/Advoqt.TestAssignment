namespace Advoqt.TestAssignment.Mvc
{
    using System.Security.Claims;
    using System.Threading;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest()
        {
            var transformedPrincipal = new ClaimsTransformer(null)
                .Authenticate(resourceName: string.Empty, incomingPrincipal: ClaimsPrincipal.Current);
            Thread.CurrentPrincipal = transformedPrincipal;
            HttpContext.Current.User = transformedPrincipal;
        }
    }
}
