using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Train2.GroupsMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "User",
                url: "user/{action}/{id}",
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Group",
                url: "group/{action}/{id}",
                defaults: new { controller = "Group", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Role",
                url: "role/{action}/{id}",
                defaults: new { controller = "Role", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "404-PageNotFound",
                url: "{*url}",
                defaults: new { controller = "StaticContent", action = "PageNotFound" }
            );
        }
    }
}
