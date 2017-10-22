using System.Web.Mvc;

namespace SmartBazaar.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_main",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Manager", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}