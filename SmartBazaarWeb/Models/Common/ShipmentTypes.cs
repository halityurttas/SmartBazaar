using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Common
{
    public struct ShipmentTypesFieldNames
    {
        public const string Title = "Nakliye Yöntemi";
        public const string Status = "Durum";
        public const string PricingMethod = "Fiyatlandırma Yöntemi";
        public const string PricingValue = "Fiyatlandırma Değeri";
    }

    public static class ShipmentListProvider
    {
        public static Dictionary<short, string> GetMethodList()
        {
            return new Dictionary<short, string> {
                {1, "Sabit Fiyat"},
                {2, "Sipariş Bedelinin Oranı"},
                {3, "Fiyat x Ürün Sayısı"},
                {4, "Fiyat x Dara" }
            };
        }
    }
}