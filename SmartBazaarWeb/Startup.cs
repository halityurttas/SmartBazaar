using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartBazaar.Web.Startup))]
namespace SmartBazaar.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
