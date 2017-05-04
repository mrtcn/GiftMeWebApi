using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gift.Web.Startup))]
namespace Gift.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
