using System.Collections.Generic;

namespace SmartBazaar.Web.Models.Site
{
    

    public class ProductDetailViewModel
    {
        public ProductDetailViewModel()
        {
            Props = new List<ProductPropertyViewModel>();
            PaymentTypes = new List<PaymentTypeViewModel>();
            Pivots = new List<ProductPivotsViewModel>();
            RPivots = new List<ProductPivotsViewModel>();
            Images = new List<ProductDetailImageViewModel>();
            Comments = new List<CommentsListViewModel>();
            Relations = new List<ProductListDisplayModel>();
            SameProducts = new List<ProductListDisplayModel>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        public int MaxInstallment { get; set; }
        public Helpers.Contents.CatalogHelper.PriceDetail Price { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public bool IsBuyable { get; set; }
        public string ShortDescription { get; set; }
        public string ExternalRef1 { get; set; }
        public string ExternalRef2 { get; set; }

        public List<ProductDetailImageViewModel> Images { get; set; }
        public List<ProductPropertyViewModel> Props { get; set; }
        public List<PaymentTypeViewModel> PaymentTypes { get; set; }
        public List<ProductPivotsViewModel> Pivots { get; set; }
        public List<ProductPivotsViewModel> RPivots { get; set; }
        public List<CommentsListViewModel> Comments { get; set; }
        public List<ProductListDisplayModel> Relations { get; set; }
        public List<ProductListDisplayModel> SameProducts { get; set; }
    }


    public class ProductDetailImageViewModel
    {
        public string ImageUrl { get; set; }

        public int Sort { get; set; }
    }
}