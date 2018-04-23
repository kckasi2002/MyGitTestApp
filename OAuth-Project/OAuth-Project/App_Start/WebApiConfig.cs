using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OAuth_Project
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //config.EnableCors(new EnableCorsAttribute(origins: "http://localhost:4200", headers: "*", methods:"*"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            //To provide the default API routes
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new AuthorizeAttribute());
        }
    }
}
