using System.Web.Http;
using AutoMapper;
using Gift.Api.Gift.Api;

namespace Gift.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Mapper.Initialize(x => {
                x.CreateMissingTypeMaps = true;
            });

            var cors = new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*");


            // Web API configuration and services
            config.EnableCors(cors);
            // Web API routes
            config.MapHttpAttributeRoutes();

            // Register Unity with Web API.
            var container = UnityConfig.GetConfiguredContainer();
            config.DependencyResolver = new UnityResolver(container);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
