using System;

namespace SmartBazaar.Web.Models.Layer
{
    public class SiteSettingModel
    {
        public static Guid WorkingStockId { get { return Guid.Parse(AppConfig.SETTING_WORKING_STOCK_ID); } }
        public static Guid ShowUnstockItemId { get { return Guid.Parse(AppConfig.SETTING_SHOW_UNSTOCK_ITEMS_ID); } }
        public static Guid PriceIncludeTaxId { get { return Guid.Parse(AppConfig.SETTING_PRICE_INCLUDE_TAX_ID); } }
        public static Guid ShowCommentsId { get { return Guid.Parse(AppConfig.SETTING_SHOW_COMMENTS_ID); } }
        public static Guid UseFacebookCommentsId { get { return Guid.Parse(AppConfig.SETTING_USE_FACEBOOK_COMMENTS_ID); } }

        public bool WorkingStock { get; set; }
        public bool ShowUnstockItem { get; set; }
        public bool PriceIndcludeTax { get; set; }
        public bool ShowComments { get; set; }
        public bool UseFacebookComments { get; set; }
    }
}