using SmartBazaar.Web.Business.Layers;
using SmartBazaar.Web.App_Start;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System;

namespace SmartBazaar.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SettingsLayer.RegisterAppSettings();
            MapperConfig.RegisterMaps();

            ModelBinders.Binders.Add(typeof(DateTime), new Helpers.Binders.DatetimeBinder());
            ModelBinders.Binders.Add(typeof(Nullable<DateTime>), new Helpers.Binders.DatetimeBinder());

        }

        public void Application_BeginRequest(object sender, EventArgs e)
        {
            
        }
    }
}
