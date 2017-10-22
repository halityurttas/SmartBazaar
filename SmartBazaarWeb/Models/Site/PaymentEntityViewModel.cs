using System;

namespace SmartBazaar.Web.Models.Site
{
    public class PaymentEntityViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PaymentTypeId { get; set; }
        public int? InstallmentId { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal PaymentFee { get; set; }
        public decimal InstallmentFee { get; set; }
        public decimal ShipmentFee { get; set; }
        public decimal Total { get; set; }
        public DateTime PaymentDate { get; set; }
        public short Status { get; set; }
        public string Log { get; set; }
        
    }
}