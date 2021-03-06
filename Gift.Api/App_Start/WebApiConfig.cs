﻿using System.Web.Http;
using AutoMapper;
using Gift.Api.Gift.Api;
using Newtonsoft.Json.Serialization;

namespace Gift.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Mapper.Initialize(x => {
                x.CreateMissingTypeMaps = true;
            });
            config.Filters.Add(new GlobalExceptionFilterAttribute());
            config.MessageHandlers.Add(new LocalizationMessageHandler());
            var cors = new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*");

            // Web API configuration and services
            config.EnableCors(cors);
            // Web API routes
            config.MapHttpAttributeRoutes();

            // Register Unity with Web API.
            var container = UnityConfig.GetConfiguredContainer();
            config.DependencyResolver = new UnityResolver(container);

            //var formatters = GlobalConfiguration.Configuration.Formatters;
            //var jsonFormatter = formatters.JsonFormatter;
            //var settings = jsonFormatter.SerializerSettings;
            //settings.Formatting = Formatting.Indented;
            //settings.ContractResolver = new CamelCasePropertyNamesContractResolver();


            
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;            
        }
    }
}
