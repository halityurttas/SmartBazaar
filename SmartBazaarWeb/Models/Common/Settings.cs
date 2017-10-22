using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Common
{
    public struct SettingsFieldNames
    {
        public const string WorkingStock = "Stoklu Çalışılsın";
        public const string ShowUnstockItem = "Stoğu Tükenen Ürünleri Göster";
        public const string PriceIncludeTax = "Ürün Liste ve Detayında Vergi Dahil Göster";
        public const string ShowComments = "Ürün Yorumlama Aktif";
        public const string UseFacebookComments = "Ürün Yorumlama Facebooktan Yapılsın";
    }

    public static class SettingValues
    {
        public static Dictionary<string, string> GetYesNo()
        {
            return new Dictionary<string, string> {
                    {"1", "Evet"}, {"2", "Hayır"}
                };
        }
    }
}