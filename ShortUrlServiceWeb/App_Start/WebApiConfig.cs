using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ShortUrlServiceWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{str}",
                defaults: new { str = RouteParameter.Optional}
            );

            config.Routes.MapHttpRoute(
                name: "ApiHome",
                routeTemplate: "Home/api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
