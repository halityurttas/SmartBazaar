using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Resources;
using System.ComponentModel.DataAnnotations;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    

    public class ProductTaxListViewModel
    {
        public int Id { get; set; }
        [Display(Name = ProductTaxFieldNames.Title)]
        public string Title { get; set; }
        [Display(Name = ProductTaxFieldNames.Rate)]
        public double Rate { get; set; }
        [Display(Name = ProductTaxFieldNames.Status)]
        [UIHint("StatusBadge")]
        public short Status { get; set; }

        
    }

    public class ProductTaxEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = ProductTaxFieldNames.Title)]
        [MaxLength(150, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [UIHint("LongString")]
        public string Caption { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = ProductTaxFieldNames.Rate)]
        [UIHint("ShortDouble")]
        public double Rate { get; set; }
        [Display(Name = ProductTaxFieldNames.Status)]
        [UIHint("ShortDropDown")]
        public short Status { get; set; }

    }
}