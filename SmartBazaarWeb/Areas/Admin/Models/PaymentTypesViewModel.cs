using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    

    public class PaymentTypesListViewModel
    {
        public int Id { get; set; }
        [Display(Name = PaymentTypesFieldLangs.Method)]
        public string Method { get; set; }
        [Display(Name = PaymentTypesFieldLangs.CommissionRate)]
        public double CommissionRate { get; set; }
        [Display(Name = PaymentTypesFieldLangs.ProcessFee)]
        public decimal ProcessFee { get; set; }
        [Display(Name = PaymentTypesFieldLangs.Title)]
        public string Title { get; set; }
        [UIHint("StatusBadge")]
        [Display(Name = PaymentTypesFieldLangs.Status)]
        public short Status { get; set; }

    }

    public class PaymentTypesEditViewModel
    {
        public PaymentTypesEditViewModel()
        {
            PaymentInstallments = new List<PaymentInstallmentsEditViewModel>();
        }
        public int Id { get; set; }
        [Display(Name = PaymentTypesFieldLangs.Method)]
        [UIHint("ShortDropDown")]
        public short Method { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = PaymentTypesFieldLangs.CommissionRate)]
        [UIHint("ShortDouble")]
        public double CommissionRate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = PaymentTypesFieldLangs.ProcessFee)]
        [UIHint("ShortDecimal")]
        public decimal ProcessFee { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = PaymentTypesFieldLangs.Title)]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [UIHint("LongString")]
        public string Caption { get; set; }
        [Display(Name = PaymentTypesFieldLangs.Status)]
        [UIHint("ShortDropDown")]
        public short Status { get; set; }
        [Display(Name = PaymentTypesFieldLangs.PosType)]
        [UIHint("ShortDropDown")]
        public string PosType { get; set; }
        [Display(Name = PaymentTypesFieldLangs.Description)]
        [UIHint("Summernote")]
        public string Description { get; set; }

        public virtual List<PaymentInstallmentsEditViewModel> PaymentInstallments { get; set; }

    }

    public class PaymentTypesLangViewModel
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

        [Display(Name = PaymentTypesFieldLangs.Description)]
        [UIHint("Summernote")]
        public string Description { get; set; }
    }

}