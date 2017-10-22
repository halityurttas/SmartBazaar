using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    
    public class ShipmentTypesListViewModel
    {
        public int Id { get; set; }
        [Display(Name = ShipmentTypesFieldNames.Title)]
        public string Title { get; set; }
        [Display(Name = ShipmentTypesFieldNames.PricingMethod)]
        public string PricingMethod { get; set; }
        [Display(Name = ShipmentTypesFieldNames.PricingValue)]
        public double PricingValue { get; set; }
        [Display(Name = ShipmentTypesFieldNames.Status)]
        [UIHint("StatusBadge")]
        public short Status { get; set; }

    }

    public class ShipmentTypesEditViewModel
    {
        
        public int Id { get; set; }
        [UIHint("LongString")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(250, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = ShipmentTypesFieldNames.Title)]
        public string Caption { get; set; }
        [UIHint("ShortDropDown")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = ShipmentTypesFieldNames.PricingMethod)]
        public short PricingMethod { get; set; }
        [UIHint("ShortDouble")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = ShipmentTypesFieldNames.PricingValue)]
        public double PricingValue { get; set; }
        [UIHint("ShortDropDown")]
        [Display(Name = ShipmentTypesFieldNames.Status)]
        public short Status { get; set; }

    }

    public class ShipmentTypesLangViewModel
    {
        public int Id { get; set; }

        public int RefId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [StringLength(10)]
        public string Code { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = PaymentTypesFieldLangs.Title)]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [UIHint("LongString")]
        public string Caption { get; set; }
    }

}