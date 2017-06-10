using System.Web.Configuration;

namespace Gift.Core
{
    public static class CoreSettings
    {
        public static string BaseUrl
        {
            get { return WebConfigurationManager.AppSettings["WebBaseUrl"]; }
        }
        public static string DashboardBaseUrl
        {
            get { return WebConfigurationManager.AppSettings["DashboardBaseUrl"]; }
        }
    }
}
