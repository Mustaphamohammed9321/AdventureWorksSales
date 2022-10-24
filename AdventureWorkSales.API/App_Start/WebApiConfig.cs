using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AdventureWorkSales.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Adventure Work Sales API",  //Swagger UI
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new Swashbuckle.Application.RedirectHandler(Swashbuckle.Application.SwaggerDocsConfig.DefaultRootUrlResolver,
                                                "swagger/ui/index")
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
