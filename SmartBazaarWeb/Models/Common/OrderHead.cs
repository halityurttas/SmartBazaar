using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Common
{
    public struct OrderHeadsFieldNames
    {
        public const string CustomerName = "Müşteri";
        public const string OrderDate = "Sipariş Tarihi";
        public const string OrderTotal = "Toplam";
        public const string TaxTotal = "KDV Toplamı";
        public const string ShipCost = "Nakliye Tutarı";
        public const string PaymentFee = "Tahsilat Ücreti";
        public const string InstallmentFee = "Taksit Ücreti";
        public const string GrandTotal = "Genel Toplam";
        public const string Status = "Durum";
        public const string ProductName = "Ürün Adı";
        public const string ProductCode = "Ürün Kodu";
        public const string Quantity = "Miktar";
        public const string Price = "Fiyat";
        public const string TaxRate = "KDV%";
        public const string TaxCost = "KDV";
        public const string Total = "Toplam";
        public const string Campaign = "Kampanya";
        public const string Message = "Sipariş Mesajı";
        public const string Note = "Not";
    }

    public static class OrderHeadsListProvider
    {
        public static Dictionary<short, string> GetStatuses()
        {
            return new Dictionary<short, string> {
                {0, "Yeni Sipariş"},
                {1, "İşlemde"},
                {2, "Gönderildi"},
                {98, "İptal İsteği"},
                {99, "İptal"}
            };
        }

        public static Dictionary<short, string> GetStatusCss()
        {
            return new Dictionary<short, string>
            {
                {0, "label-warning" },
                {1, "label-info" },
                {2, "label-success" },
                {98, "label-danger"},
                {99, "label-danger" }
            };
        }
    }
}