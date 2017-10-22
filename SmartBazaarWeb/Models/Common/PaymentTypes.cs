using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Common
{
    public struct PaymentTypesFieldLangs
    {
        public const string Method = "Metod";
        public const string CommissionRate = "Komisyon Oranı";
        public const string ProcessFee = "İşlem Bedeli";
        public const string Title = "Tanım";
        public const string Status = "Durum";
        public const string Description = "Ekstra Bilgi";
        public const string PosType = "Pos Tipi";
    }

    public static class PaymentListProvider
    {
        public static Dictionary<short, string> GetMethods()
        {
            return new Dictionary<short, string> {
                {1, "Nakit Ödeme"},
                {2, "Pos İle Ödeme"}
            };
        }
    }
}