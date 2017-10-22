using System;
using System.Collections.Generic;

namespace SmartBazaar.Web.Models.Site
{
    
    public class OrderHeaderProcessModel
    {
        public OrderHeaderProcessModel()
        {
            Lines = new List<OrderLineViewModel>();
            Addresses = new List<CustomerAddressViewModel>();
            Shipments = new List<ShipmentTypeViewModel>();
            Payments = new List<PaymentTypeViewModel>();
        }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? ShipAddressId { get; set; }
        public int PaymentTypeId { get; set; }
        public int InstallmentId { get; set; }
        public int? InvoiceAddressId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public int ShipmentTypeId { get; set; }
        public decimal ShipCost { get; set; }
        public decimal PaymentFee { get; set; }
        public decimal InstallmentFee { get; set; }
        public decimal GrandTotal { get; set; }
        public short Status { get; set; }
        public int MaxInstallment { get; set; }
        public string OrderMessage { get; set; }
        public string Note { get; set; }
        public List<OrderLineViewModel> Lines { get; set; }
        public List<CustomerAddressViewModel> Addresses { get; set; }
        public List<ShipmentTypeViewModel> Shipments { get; set; }
        public List<PaymentTypeViewModel> Payments { get; set; }
    }

    
}