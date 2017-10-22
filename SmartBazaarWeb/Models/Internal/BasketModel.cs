using System.ComponentModel.DataAnnotations;

namespace SmartBazaar.Web.Models.Internal
{
    public class BasketModel
    {
        public int ProductId { get; set; }
        public int? RelatedId { get; set; }
        public int? CampaignId { get; set; }
        public string ProductImage { get; set; }
        [Display(Name = "Ürün Adı")]
        public string ProductName { get; set; }
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }
        [Display(Name = "Miktar")]
        public int Quantity { get; set; }
        [Display(Name = "KDV")]
        public double TaxRate { get; set; }
        [Display(Name = "Toplam Fiyat")]
        public decimal TotalPrice { get { return Price * Quantity; } }
        [Display(Name = "Toplam KDV")]
        public decimal TotalTax { get { return TotalPrice * (decimal)(TaxRate / 100); } }
        [Display(Name = "Genel Toplam")]
        public decimal Total { get { return TotalPrice + TotalTax; } }
        public double Tare { get; set; }
    }
}