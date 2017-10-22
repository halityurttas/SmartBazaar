using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Site
{
    public class OrderLineViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public double TaxRate { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public int? CampaignId { get; set; }
        public int? RelatedId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
    }
}