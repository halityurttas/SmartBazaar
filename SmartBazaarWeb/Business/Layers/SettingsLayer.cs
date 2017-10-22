using SmartBazaar.Data;
using SmartBazaar.Web.Models.Layer;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Business.Layers
{
    public class SettingsLayer
    {

        public static void RegisterAppSettings()
        {
            HttpContext.Current.Application["AppSettings"] = new SiteSettingModel();

            var ctx = new ContentContext();
            var query = from s in ctx.Settings
                        select s;
            SiteSettingModel settings = HttpContext.Current.Application["AppSettings"] as SiteSettingModel;
            if (query.Any())
            {
                settings.WorkingStock = query.FirstOrDefault(f => f.Id == SiteSettingModel.WorkingStockId).Value == "1";
                settings.ShowUnstockItem = query.FirstOrDefault(f => f.Id == SiteSettingModel.ShowUnstockItemId).Value == "1";
                settings.PriceIndcludeTax = query.FirstOrDefault(f => f.Id == SiteSettingModel.PriceIncludeTaxId).Value == "1";
                settings.ShowComments = query.FirstOrDefault(f => f.Id == SiteSettingModel.ShowCommentsId).Value == "1";
                settings.UseFacebookComments = query.FirstOrDefault(f => f.Id == SiteSettingModel.UseFacebookCommentsId).Value == "1";
            }
        }

        public static SiteSettingModel SiteSetting
        {
            get
            {
                return HttpContext.Current.Application["AppSettings"] as SiteSettingModel;
            }
            set
            {
                HttpContext.Current.Application["AppSettings"] = value;
            }
        }
    }
}