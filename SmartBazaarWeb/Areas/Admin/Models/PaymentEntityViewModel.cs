using SmartBazaar.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    

    public class PaymentEntitiesListViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = PaymentEntityFieldNames.OrderId)]
        public int OrderId { get; set; }

        [Display(Name = PaymentEntityFieldNames.PaymentType)]
        public string PaymentType { get; set; }

        [Display(Name = PaymentEntityFieldNames.PaymentInstallment)]
        public string PaymentInstallment { get; set; }

        [Display(Name = PaymentEntityFieldNames.Total)]
        public decimal Total { get; set; }

        [Display(Name = PaymentEntityFieldNames.Status)]
        public short Status { get; set; }
    }



    public class PaymentEntitiesEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = PaymentEntityFieldNames.OrderId)]
        public int OrderId { get; set; }

        [Display(Name = PaymentEntityFieldNames.PaymentType)]
        public string PaymentType { get; set; }

        [Display(Name = PaymentEntityFieldNames.PaymentInstallment)]
        public string PaymentInstallment { get; set; }

        [Display(Name = PaymentEntityFieldNames.OrderPrice)]
        public decimal OrderPrice { get; set; }

        [Display(Name = PaymentEntityFieldNames.PaymentFee)]
        public decimal PaymentFee { get; set; }

        [Display(Name = PaymentEntityFieldNames.InstallmentFee)]
        public decimal InstallmentFee { get; set; }

        [Display(Name = PaymentEntityFieldNames.ShipmentFee)]
        public decimal ShipmentFee { get; set; }

        [Display(Name = PaymentEntityFieldNames.Total)]
        public decimal Total { get; set; }

        [Display(Name = PaymentEntityFieldNames.PaymentDate)]
        public DateTime PaymentDate { get; set; }

        [UIHint("ShortDropDown")]
        [Display(Name = PaymentEntityFieldNames.Status)]
        public short Status { get; set; }

    }
}