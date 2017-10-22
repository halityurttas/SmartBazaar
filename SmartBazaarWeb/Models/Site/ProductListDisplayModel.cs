using System.ComponentModel.DataAnnotations;
using SmartBazaar.Web.Helpers.SEO;

namespace SmartBazaar.Web.Models.Site
{
    public class ProductListDisplayModel
    {
        public int Id { get; set; }

        [Display(Name = "Marka")]
        public string Brand { get; set; }

        [Display(Name = "Ürün Adı")]
        public string Title { get; set; }

        [Display(Name = "Ürün Kodu")]
        public string ProductCode { get; set; }

        public SmartBazaar.Web.Helpers.Contents.CatalogHelper.PriceDetail Price { get; set; }

        [Display(Name = "Stok")]
        public int Stock { get; set; }

        public string ImageUrl { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public bool IsBuyable { get; set; }

        public string ExternalRef1 { get; set; }

        public string ExternalRef2 { get; set; }

        public string MetaUrl { get; set; }

        public string SEOUrl()
        {
            if (MetaUrl == null)
            {
                return Id.ToString() + "-" + SeoFriendlyUrl.Convert(Title); 
            }
            else
            {
                return Id.ToString() + "-" + MetaUrl;
            }
        }

    }
}