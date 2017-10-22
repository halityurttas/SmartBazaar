using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Common
{
    public struct PaymentEntityFieldNames
    {
        public const string OrderId = "Sipariş No";
        public const string PaymentType = "Ödeme Yöntemi";
        public const string PaymentInstallment = "Taksit";
        public const string OrderPrice = "Sipariş Fiyatı";
        public const string PaymentFee = "Tahsilat Ücreti";
        public const string InstallmentFee = "Taksit Ücreti";
        public const string ShipmentFee = "Nakliye Tutarı";
        public const string Total = "Toplam";
        public const string PaymentDate = "İşlem Tarihi";
        public const string Status = "Durum";
    }

    public static class PaymentEntitiesListsProvider
    {
        public static Dictionary<short, string> GetStatuses()
        {
            return new Dictionary<short, string> {
                {0, "Yeni"},
                {1, "Onaylanmış"},
                {2, "Tamamlanmış"},
                {99, "Reddedilmiş"}
            };
        }

        public static Dictionary<short, string> GetStatusCss()
        {
            return new Dictionary<short, string> {
                {0, "warning"},
                {1, "info"},
                {2, "success"},
                {99, "danger"}
            };
        }
    }
}