using SmartBazaar.Data.Entities;
using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public class CatalogProductListViewModel
    {
        public int Id { get; set; }
        [Display(Name = CatalogProductFieldNames.ProductCode)]
        public string ProductCode { get; set; }
        [Display(Name = CatalogProductFieldNames.Title)]
        public string Title { get; set; }
        [Display(Name = CatalogProductFieldNames.CategoryId)]
        public string CategoryTitle { get; set; }
        [Display(Name = CatalogProductFieldNames.BrandId)]
        public string BrandTitle { get; set; }
        [Display(Name = CatalogProductFieldNames.Stock)]
        public int Stock { get; set; }
        [UIHint("StatusBadge")]
        [Display(Name = CatalogProductFieldNames.Status)]
        public short Status { get; set; }
        
        
    }

    public class CatalogProductPropertiesEditViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        public string Title { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        public string Value { get; set; }

    }

    public class CatalogProductPropertiesLangViewModel
    {
        public int Id { get; set; }
        public int RefId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [StringLength(10)]
        public string Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        public string Title { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        public string Value { get; set; }
    }

    public class CatalogProductImagesEditViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        public string ImageUrl { get; set; }
        public int Sort { get; set; }
    }

    public class CatalogProductEditViewModel
    {
        public CatalogProductEditViewModel()
        {
            Properties = new List<CatalogProductPropertiesEditViewModel>();
            Images = new List<CatalogProductImagesEditViewModel>();
            Relations = new List<CatalogProductsRelationsEditViewModel>();
        }

        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(50, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.ProductCode)]
        [UIHint("ShortString")]
        public string ProductCode { get; set; }
        
        [Display(Name = CatalogProductFieldNames.CategoryId)]
        public string FakeCategoryId { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = CatalogProductFieldNames.CategoryId)]
        public int CategoryId { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(200, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.Title)]
        [UIHint("LongString")]
        public string Caption { get; set; }
        
        [Display(Name = CatalogProductFieldNames.Description)]
        [UIHint("SummernoteSingle")]
        public string Description { get; set; }
        
        [Display(Name = CatalogProductFieldNames.BrandId)]
        [UIHint("ShortDropDown")]
        public int? BrandId { get; set; }
        
        [MaxLength(250, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.ImageUrl)]
        public string ImageUrl { get; set; }
        
        [Display(Name = CatalogProductFieldNames.Stock)]
        [UIHint("ShortInt")]
        public int Stock { get; set; }
        
        [MaxLength(200, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.SearchText)]
        [UIHint("ShortString")]
        public string SearchText { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = CatalogProductFieldNames.Price1)]
        [UIHint("ShortDecimal")]
        public decimal Price1 { get; set; }
        
        [Display(Name = CatalogProductFieldNames.Price2)]
        [UIHint("ShortDecimal")]
        public decimal? Price2 { get; set; }
        
        [Display(Name = CatalogProductFieldNames.Price3)]
        [UIHint("ShortDecimal")]
        public decimal? Price3 { get; set; }
        
        [Display(Name = CatalogProductFieldNames.Price4)]
        [UIHint("ShortDecimal")]
        public decimal? Price4 { get; set; }
        
        [Display(Name = CatalogProductFieldNames.Price5)]
        [UIHint("ShortDecimal")]
        public decimal? Price5 { get; set; }
        
        [Display(Name = CatalogProductFieldNames.TaxGroup)]
        [UIHint("ShortDropDown")]
        public int TaxGroup { get; set; }
        
        [Display(Name = CatalogProductFieldNames.Status)]
        [UIHint("ShortDropDown")]
        public short Status { get; set; }
        
        [Display(Name = CatalogProductFieldNames.IsFeatured)]
        [UIHint("ShortCheck")]
        public bool IsFeatured { get; set; }
        
        [Display(Name = CatalogProductFieldNames.IsNew)]
        [UIHint("ShortCheck")]
        public bool IsNew { get; set; }

        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.MetaTitle)]
        [UIHint("ShortString")]
        public string MetaTitle { get; set; }

        [MaxLength(150, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.MetaKeywords)]
        [UIHint("ShortString")]
        public string MetaKeywords { get; set; }

        [MaxLength(150, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.MetaDescription)]
        [UIHint("LongString")]
        public string MetaDescription { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = CatalogProductFieldNames.MaxInstallment)]
        [UIHint("ShortInt")]
        public int MaxInstallment { get; set; }

        [Display(Name = CatalogProductFieldNames.ShortDescription)]
        [UIHint("ShortText")]
        public string ShortDescription { get; set; }

        [Display(Name = CatalogProductFieldNames.ManagerNotes)]
        [UIHint("ShortText")]
        public string ManagerNotes { get; set; }

        [Display(Name = CatalogProductFieldNames.IsHomepage)]
        [UIHint("ShortCheck")]
        public bool IsHomepage { get; set; }

        [MaxLength(25, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.SKU)]
        [UIHint("ShortString")]
        public string SKU { get; set; }

        [MaxLength(25, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.MPN)]
        [UIHint("ShortString")]
        public string MNP { get; set; }

        [MaxLength(250, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.Barcode)]
        [UIHint("ShortString")]
        public string Barcode { get; set; }

        [Display(Name = CatalogProductFieldNames.IsBuyable)]
        [UIHint("ShortCheck")]
        public bool IsBuyable { get; set; }

        [Display(Name = CatalogProductFieldNames.IsShipable)]
        [UIHint("ShortCheck")]
        public bool IsShipable { get; set; }

        [Display(Name = CatalogProductFieldNames.IsFreeShip)]
        [UIHint("ShortCheck")]
        public bool IsFreeShip { get; set; }

        [Display(Name = CatalogProductFieldNames.Tare)]
        [UIHint("ShortDouble")]
        public double Tare { get; set; }

        [UIHint("ShortString")]
        [MaxLength(250, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.ExternalRef1)]
        public string ExternalRef1 { get; set; }

        [UIHint("ShortString")]
        [MaxLength(250, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.ExternalRef2)]
        public string ExternalRef2 { get; set; }

        [UIHint("ShortString")]
        [MaxLength(250, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.ExternalRef3)]
        public string ExternalRef3 { get; set; }

        [UIHint("ShortString")]
        [MaxLength(250, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.MetaUrl)]
        public string MetaUrl { get; set; }

        public virtual List<CatalogProductPropertiesEditViewModel> Properties { get; set; }

        public virtual List<CatalogProductImagesEditViewModel> Images { get; set; }

        public virtual List<CatalogProductsRelationsEditViewModel> Relations { get; set; }

    }

    public class CatalogProductLangViewModel
    {
        public int Id { get; set; }
        public int RefId { get; set; }
        public string Code { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(200, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.Title)]
        [UIHint("LongString")]
        public string Caption { get; set; }

        [Display(Name = CatalogProductFieldNames.Description)]
        [UIHint("SummernoteSingle")]
        public string Description { get; set; }

        [Display(Name = CatalogProductFieldNames.ShortDescription)]
        [UIHint("ShortText")]
        public string ShortDescription { get; set; }

        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.MetaTitle)]
        [UIHint("ShortString")]
        public string MetaTitle { get; set; }

        [MaxLength(150, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.MetaKeywords)]
        [UIHint("ShortString")]
        public string MetaKeywords { get; set; }

        [MaxLength(150, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.MetaDescription)]
        [UIHint("LongString")]
        public string MetaDescription { get; set; }

        [UIHint("ShortString")]
        [MaxLength(250, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogProductFieldNames.MetaUrl)]
        public string MetaUrl { get; set; }

        public virtual List<CatalogProductPropertiesLangViewModel> Properties { get; set; }
    }

    public class CatalogProductPairListViewModel
    {
        public int id { get; set; }
        public string text { get; set; }

        public static CatalogProductPairListViewModel Import(int id, string text)
        {
            return new CatalogProductPairListViewModel
            {
                id = id,
                text = text
            };
        }

        public static List<CatalogProductPairListViewModel> Imports(List<Catalog_Products> list)
        {
            return list.Select(item => Import(item.Id, item.Title)).ToList();
        }
    }
}