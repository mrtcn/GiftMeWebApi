using System.Web.Configuration;

namespace Gift.Core
{
    public static class CoreSettings
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
