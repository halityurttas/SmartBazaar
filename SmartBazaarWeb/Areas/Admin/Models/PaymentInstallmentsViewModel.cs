using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Resources;
using System.ComponentModel.DataAnnotations;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    

    public class PaymentInstallmentsEditViewModel
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = PaymentInstallmentsFieldNames.NumberOf)]
        public int NumberOf { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = PaymentInstallmentsFieldNames.Rate)]
        public double Rate { get; set; }


    }
}