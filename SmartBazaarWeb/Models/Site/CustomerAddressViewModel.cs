using SmartBazaar.Web.Resources;
using System.ComponentModel.DataAnnotations;
using SmartBazaar.Web.Models.Common;

namespace SmartBazaar.Web.Models.Site
{
    public class CustomerAddressViewModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(150, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CustomerAddressFieldNames.Title)]
        public string Title { get; set; }

        [MaxLength(50, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CustomerAddressFieldNames.City)]
        public string City { get; set; }

        [MaxLength(50, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CustomerAddressFieldNames.District)]
        public string District { get; set; }

        [MaxLength(50, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CustomerAddressFieldNames.Town)]
        public string Town { get; set; }

        [MaxLength(15, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CustomerAddressFieldNames.PostalCode)]
        public string PostalCode { get; set; }

        [Display(Name = CustomerAddressFieldNames.Detail)]
        public string Detail { get; set; }

        public bool IsDefault { get; set; }

        public short Status { get; set; }
    }
}