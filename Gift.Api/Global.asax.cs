using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Gift.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    public static class ApiStarter
    {
#if DEBUG
        public static string BaseUrl
        {
            get { return WebConfigurationManager.AppSettings["DebugWebBaseUrl"]; }
        }
        public static string DashboardBaseUrl
        {
            get { return WebConfigurationManager.AppSettings["DebugDashboardBaseUrl"]; }
        }
#else
        public static string BaseUrl
        {
            get { return WebConfigurationManager.AppSettings["WebBaseUrl"]; }
        }
        public static string DashboardBaseUrl
        {
            get { return WebConfigurationManager.AppSettings["DashboardBaseUrl"]; }
        }
#endif
    }
}