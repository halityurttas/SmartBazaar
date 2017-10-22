using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SmartBazaar.Web.Models.Common;

namespace SmartBazaar.Web.Models.Site
{
    public class OrderHeaderDetailViewModel
    {
        public OrderHeaderDetailViewModel()
        {
            Lines = new List<OrderLineViewModel>();
            PaymentTypes = new List<PaymentTypeViewModel>();
            PaymentEntities = new List<PaymentEntityViewModel>();
        }
        public int Id { get; set; }
        [Display(Name= OrderHeadsFieldNames.OrderDate)]
        public DateTime OrderDate { get; set; }
        [Display(Name= OrderHeadsFieldNames.OrderTotal)]
        public decimal OrderTotal { get; set; }
        [Display(Name= OrderHeadsFieldNames.TaxTotal)]
        public decimal TaxTotal { get; set; }
        [Display(Name= OrderHeadsFieldNames.ShipCost)]
        public decimal ShipCost { get; set; }
        [Display(Name= OrderHeadsFieldNames.PaymentFee)]
        public decimal PaymentFee { get; set; }
        [Display(Name= OrderHeadsFieldNames.GrandTotal)]
        public decimal GrandTotal { get; set; }
        [Display(Name= OrderHeadsFieldNames.Status)]
        public string StatusText { get; set; }
        [Display(Name= OrderHeadsFieldNames.Message)]
        public string OrderMessage { get; set; }
        public short Status { get; set; }

        public CustomerAddressViewModel ShipmentAddress { get; set; }
        public CustomerAddressViewModel InvoiceAddress { get; set; }
        public ShipmentTypeViewModel Shipment { get; set; }
        public List<PaymentTypeViewModel> PaymentTypes { get; set; }
        public List<PaymentEntityViewModel> PaymentEntities { get; set; }
        public List<OrderLineViewModel> Lines { get; set; }
    }
}