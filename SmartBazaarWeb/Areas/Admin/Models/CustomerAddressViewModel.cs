using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Resources;
using System.ComponentModel.DataAnnotations;

namespace SmartBazaar.Web.Areas.Admin.Models
{

    public class CustomerAddressListViewModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Display(Name = CustomerAddressFieldNames.Title)]
        public string Title { get; set; }

        [Display(Name = CustomerAddressFieldNames.IsDefault)]
        public bool IsDefault { get; set; }

        [Display(Name = CustomerAddressFieldNames.Status)]
        public short Status { get; set; }
    }

    public class CustomerAddressEditViewModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Display(Name = CustomerAddressFieldNames.Title)]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(150, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [UIHint("LongString")]
        public string Title { get; set; }

        [Display(Name = CustomerAddressFieldNames.City)]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(50, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [UIHint("ShortString")]
        public string City { get; set; }

        [Display(Name = CustomerAddressFieldNames.District)]
        [MaxLength(50, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [UIHint("ShortString")]
        public string District { get; set; }

        [Display(Name = CustomerAddressFieldNames.Town)]
        [MaxLength(50, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [UIHint("ShortString")]
        public string Town { get; set; }

        [Display(Name = CustomerAddressFieldNames.Detail)]
        [UIHint("LongText")]
        public string Detail { get; set; }

        [Display(Name = CustomerAddressFieldNames.PostalCode)]
        [MaxLength(15, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [UIHint("ShortString")]
        public string PostalCode { get; set; }

        [Display(Name = CustomerAddressFieldNames.IsDefault)]
        [UIHint("ShortCheck")]
        public bool IsDefault { get; set; }

        [Display(Name = CustomerAddressFieldNames.Status)]
        [UIHint("ShortDropDown")]
        public short Status { get; set; }
    }

}