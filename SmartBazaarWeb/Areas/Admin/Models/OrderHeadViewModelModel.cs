using SmartBazaar.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    

    public class OrderHeadsListViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = OrderHeadsFieldNames.CustomerName)]
        public string CustomerName { get; set; }

        [Display(Name = OrderHeadsFieldNames.OrderDate)]
        public DateTime OrderDate { get; set; }

        [Display(Name = OrderHeadsFieldNames.OrderTotal)]
        public decimal OrderTotal { get; set; }

        [Display(Name = OrderHeadsFieldNames.Status)]
        public short Status { get; set; }
        public string StatusTitle { get; set; }
        public string StatusCss { get; set; }

    }

    public class OrderLinesListViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int? CampaignId { get; set; }

        [Display(Name = OrderHeadsFieldNames.Price)]
        public decimal Price { get; set; }

        [Display(Name = OrderHeadsFieldNames.Quantity)]
        public int Quantity { get; set; }

        [Display(Name = OrderHeadsFieldNames.TaxRate)]
        public double TaxRate { get; set; }

        [Display(Name = OrderHeadsFieldNames.TaxCost)]
        public decimal TaxCost { get { return Price * Quantity * (decimal)(TaxRate / 100); } }

        [Display(Name = OrderHeadsFieldNames.Total)]
        public decimal Total { get; set; }

        [Display(Name = OrderHeadsFieldNames.Campaign)]
        public string Campaign { get; set; }

        [Display(Name = OrderHeadsFieldNames.ProductCode)]
        public string ProductCode { get; set; }

        [Display(Name = OrderHeadsFieldNames.ProductName)]
        public string ProductName { get; set; }


    }

    public class OrderHeadsEditViewModel
    {
        public OrderHeadsEditViewModel()
        {
            OrderLines = new List<OrderLinesListViewModel>();
            Payments = new List<PaymentEntitiesListViewModel>();
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ShipAddressId { get; set; }

        public int? InvoiceAddressId { get; set; }

        public int ShipmentTypeId { get; set; }

        [Display(Name = OrderHeadsFieldNames.OrderDate)]
        public DateTime OrderDate { get; set; }

        [Display(Name = OrderHeadsFieldNames.OrderTotal)]
        public decimal OrderTotal { get; set; }

        [Display(Name = OrderHeadsFieldNames.TaxTotal)]
        public decimal TaxTotal { get; set; }

        [Display(Name = OrderHeadsFieldNames.ShipCost)]
        public decimal ShipCost { get; set; }

        [Display(Name = OrderHeadsFieldNames.PaymentFee)]
        public decimal PaymentFee { get; set; }

        [Display(Name = OrderHeadsFieldNames.InstallmentFee)]
        public decimal InstallmentFee { get; set; }

        [Display(Name = OrderHeadsFieldNames.GrandTotal)]
        public decimal GrandTotal { get; set; }

        [Display(Name = OrderHeadsFieldNames.Status)]
        public short Status { get; set; }

        [Display(Name = OrderHeadsFieldNames.CustomerName)]
        public string CustomerName { get; set; }

        [Display(Name = OrderHeadsFieldNames.Note)]
        public string Note { get; set; }

        public virtual CustomerEntityEditViewModel Customer { get; set; }

        public virtual CustomerAddressEditViewModel ShipmentAddress { get; set; }

        public virtual CustomerAddressEditViewModel InvoiceAddress { get; set; }

        public virtual ShipmentTypesEditViewModel ShipmentTypes { get; set; }

        public virtual List<OrderLinesListViewModel> OrderLines { get; set; }

        public virtual List<PaymentEntitiesListViewModel> Payments { get; set; }
        
    }
}