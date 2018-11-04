using System.Web.Mvc;
using System.Web.Routing;

namespace HighSchool.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "HomePage",
                url: "HomePage",
                defaults: new { controller = "Home", action = "HomePage" }
            );

            routes.MapRoute(
                name: "popup",
                url: "popup",
                defaults: new { controller = "Home", action = "popup" }
            );

            routes.MapRoute(
                name: "Logout",
                url: "Logout",
                defaults: new { controller = "Home", action = "Logout" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
