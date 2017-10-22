using SmartBazaar.Web.Helpers.SEO;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartBazaar.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Admin_area",
                url: "admin",
                defaults: new { controller = "Manager", action = "Index", area = "Admin" }
                );

            routes.MapRoute(
                name: "Page_hakkimizda",
                url: "hakkimizda",
                defaults: new { controller = "Page", action = "Index", id = 1 }
                );

            routes.MapRoute(
                name: "Page_iadegaranti",
                url: "iade-garanti",
                defaults: new { controller = "Page", action = "Index", id = 2 }
                );

            routes.MapRoute(
                name: "Page_guvenliticaret",
                url: "guvenliticaret",
                defaults: new { controller = "Page", action = "Index", id = 3 }
                );

            routes.MapRoute(
                name: "Contact",
                url: "iletisim",
                defaults: new { controller = "Contact", action = "Index" }
                );

            routes.Add("Page_default", 
                new SeoFriendlyRoute(
                    "Icerik/{id}",
                    new RouteValueDictionary(new { controller = "Page", action = "Index"}),
                    new MvcRouteHandler()
                    )
                );

            routes.Add("Product_list",
                new SeoFriendlyRoute(
                    "urunler/{id}",
                    new RouteValueDictionary(new { controller = "Product", action = "Index", id = UrlParameter.Optional }),
                    new MvcRouteHandler()
                    )
                );

            routes.Add("Product_detail",
                new SeoFriendlyRoute(
                    "urun/{id}",
                    new RouteValueDictionary(new { controller = "Product", action = "Detail" }),
                    new MvcRouteHandler()
                    )
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
