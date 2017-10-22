using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Common
{
    public struct CustomerEntityFieldNames
    {
        public const string Title = "Ad Soyad/Ünvan";
        public const string TaxNr = "Vergi No";
        public const string TaxOffice = "Vergi Dairesi";
        public const string CustomerType = "Müşteri Tipi";
        public const string BirthDate = "Doğum Günü";
        public const string Gender = "Cinsiyet";
        public const string Company = "Firma";
        public const string ContactPhone = "Telefon";
        public const string Status = "Durum";
        public const string Created = "Açılış Tarihi";
        public const string GroupTitle = "Grup";
        public const string Email = "E-Posta";
    }

    public static class CustomerEntityListProvider
    {
        public static Dictionary<short, string> GetGenderList()
        {
            return new Dictionary<short, string> {
                {1, "Bay"}, {2, "Bayan"}
            };
        }

        public static Dictionary<short, string> GetCustomerTypeList()
        {
            return new Dictionary<short, string> {
                {1, "Bireysel"}, {2, "Kurumsal"}
            };
        }
    }
}