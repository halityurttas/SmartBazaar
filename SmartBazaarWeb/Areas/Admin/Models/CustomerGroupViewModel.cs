using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Resources;
using System.ComponentModel.DataAnnotations;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public class CustomerGroupListViewModel
    {
        public int Id { get; set; }
        [Display(Name = CustomerGroupFieldNames.Title)]
        public string Title { get; set; }
        [Display(Name = CustomerGroupFieldNames.PriceIndex)]
        public string PriceIndex { get; set; }
        [UIHint("StatusBadge")]
        [Display(Name = CustomerGroupFieldNames.Status)]
        public short Status { get; set; }

    }

    public class CustomerGroupEditViewModel
    {
        public int Id { get; set; }
        [UIHint("LongString")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(150, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CustomerGroupFieldNames.Title)]
        public string Caption { get; set; }
        [UIHint("ShortDropDown")]
        [Display(Name = CustomerGroupFieldNames.PriceIndex)]
        public short PriceIndex { get; set; }
        [UIHint("ShortDropDown")]
        [Display(Name = CustomerGroupFieldNames.Status)]
        public short Status { get; set; }

        
    }
}