using System.ComponentModel.DataAnnotations;
using SmartBazaar.Web.Resources;
using SmartBazaar.Web.Models.Common;

namespace SmartBazaar.Web.Areas.Admin.Models
{

    public class CatalogBrandListViewModel
    {
        public int Id { get; set; }
        [Display(Name = CatalogBrandFieldNames.Title)]
        public string Title { get; set; }
        [UIHint("StatusBadge")]
        [Display(Name = CatalogBrandFieldNames.Status)]
        public short Status { get; set; }

    }

    public class CatalogBrandEditViewModel
    {
        public int Id { get; set; }
        [UIHint("LongString")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogBrandFieldNames.Title)]
        public string Caption { get; set; }
        [UIHint("ShortDropDown")]
        [Display(Name = CatalogBrandFieldNames.Status)]
        public short Status { get; set; }
        [UIHint("ShortString")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogBrandFieldNames.ExternalRef1)]
        public string ExternalRef1 { get; set; }
        [UIHint("ShortString")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogBrandFieldNames.ExternalRef2)]
        public string ExternalRef2 { get; set; }
        [UIHint("ShortString")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogBrandFieldNames.ExternalRef3)]
        public string ExternalRef3 { get; set; }

    }

    public class CatalogBrandLangViewModel
    {
        public int Id { get; set; }
        public int RefId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [StringLength(10)]
        public string Code { get; set; }
        [UIHint("LongString")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogBrandFieldNames.Title)]
        public string Caption { get; set; }
    }
}