using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using workmanship_rest_net.Controllers;

namespace workmanship_rest_net
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "ProsjekterForBruker",
                routeTemplate: "api/brukere/{brukerId}/prosjekter",
                defaults: new { controller = "Prosjekter" }
            );

            config.Routes.MapHttpRoute(
                name: "BrukereForProsjekt",
                routeTemplate: "api/prosjekter/{prosjektId}/brukere",
                defaults: new { controller = "Brukere" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Fjerner XML formatter
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}
