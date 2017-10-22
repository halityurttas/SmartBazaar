using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Common
{
    public struct CatalogCampaignFieldNames
    {
        public const string CampaignMethod = "Kampanya Yöntemi";
        public const string StartDate = "Başlangıç Tarihi";
        public const string EndDate = "Bitiş Tarihi";
        public const string DiscountMethod = "İndirim Şekli";
        public const string DiscountValue = "İndirim Değeri";
        public const string SourceProduct = "Kaynak Ürün";
        public const string DestinationProduct = "Hedef Ürün";
        public const string Title = "Kampanya Adı";
        public const string Description = "Kampanya Detayı";
        public const string SlideUrl = "Slider Resmi";
        public const string Status = "Durum";
        public const string MaxUsage = "Katılım Miktarı";
    }

    public static class CatalogCampaignsListProvider
    {
        public static Dictionary<short, string> GetCapaignMethodList()
        {
            return new Dictionary<short, string> {
                {1, "İndirim Kampanyası"}, {2, "Çapraz Ürün Kampanyası"}
            };
        }

        public static Dictionary<short, string> GetDiscountMethodList()
        {
            return new Dictionary<short, string> {
                {1, "İndirim Oranı"}, {2, "İndirim Tutarı"}
            };
        }
    }
}