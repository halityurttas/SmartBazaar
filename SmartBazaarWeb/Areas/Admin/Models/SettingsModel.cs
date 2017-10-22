using SmartBazaar.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    

    public class SettingsEditViewModel
    {
        public Guid WorkingStockId { get { return Guid.Parse(AppConfig.SETTING_WORKING_STOCK_ID); } }
        public Guid ShowUnstockItemId { get { return Guid.Parse(AppConfig.SETTING_SHOW_UNSTOCK_ITEMS_ID); } }
        public Guid PriceIncludeTaxId { get { return Guid.Parse(AppConfig.SETTING_PRICE_INCLUDE_TAX_ID); } }
        public Guid ShowCommentsId { get { return Guid.Parse(AppConfig.SETTING_SHOW_COMMENTS_ID); } }
        public Guid UseFacebookCommentsId { get { return Guid.Parse(AppConfig.SETTING_USE_FACEBOOK_COMMENTS_ID); } }

        [Display(Name = SettingsFieldNames.WorkingStock)]
        [UIHint("ShortDropDown")]
        public string WorkingStock { get; set; }
        [Display(Name = SettingsFieldNames.ShowUnstockItem)]
        [UIHint("ShortDropDown")]
        public string ShowUnstockItem { get; set; }
        [Display(Name = SettingsFieldNames.PriceIncludeTax)]
        [UIHint("ShortDropDown")]
        public string PriceIncludeTax { get; set; }
        [Display(Name = SettingsFieldNames.ShowComments)]
        [UIHint("ShortDropDown")]
        public string ShowComments { get; set; }
        [Display(Name = SettingsFieldNames.UseFacebookComments)]
        [UIHint("ShortDropDown")]
        public string UseFacebookComments { get; set; }
    }
    
}