using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Gift.Api;
using Gift.Api.ViewModel;
using Gift.Core.EntityParams;
using Gift.Data.Models;
using Gift.Framework.Extensions;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Gift.Api
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {

            Mapper.Initialize(cfg => cfg.CreateMap<RegisterViewModel, ApplicationUser>().Ignore());
            Mapper.Initialize(cfg => cfg.CreateMap<UserInfoViewModel, ApplicationUser>().Ignore());
            Mapper.Initialize(cfg => cfg.CreateMap<FriendRequestViewModel, FriendParams>().Ignore());
            Mapper.Initialize(cfg => cfg.CreateMap<GiftItemModel, GiftItemParams>().Ignore());
            Mapper.Initialize(cfg => cfg.CreateMap<List<GiftItemModel>, List<GiftItemParams>>().Ignore());
            Mapper.Initialize(cfg => cfg.CreateMap<EventViewModel, EventParams>().Ignore());

            Mapper.Configuration.AssertConfigurationIsValid();
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            config.EnableCors(new EnableCorsAttribute("*", "*", "GET, POST, OPTIONS, PUT, DELETE"));
            WebApiConfig.Register(config);
           
            app.UseCors(CorsOptions.AllowAll);
            
            app.UseWebApi(config);
        }
    }
}