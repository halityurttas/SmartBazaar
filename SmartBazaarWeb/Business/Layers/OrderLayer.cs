using AutoMapper;
using SmartBazaar.Web.Business.Workers;
using SmartBazaar.Web.Models.Internal;
using SmartBazaar.Web.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Business.Layers
{
    public class OrderLayer
    {
        public OrderHeaderProcessModel Order { get; set; }

        public OrderLayer()
        {
            if (HttpContext.Current.Session["Order"] == null)
            {
                this.Order = new OrderHeaderProcessModel();
            }
            else
            {
                this.Order = HttpContext.Current.Session["Order"] as OrderHeaderProcessModel;
            }
        }

        public void Sync()
        {
            HttpContext.Current.Session["Order"] = Order;
        }

        public static OrderLayer Create()
        {
            var catalogWorker = new CatalogWorker();
            var basket = BasketLayer.GetInstance();
            var basketSummary = BasketLayer.GetSummary();

            var order = new OrderHeaderProcessModel();
            order.CustomerId = CustomerLayer.Customer.Id;
            order.OrderTotal = basketSummary.BasketPriceTotal;
            order.TaxTotal = basketSummary.BasketTaxTotal;
            order.GrandTotal = basketSummary.BasketTotal;
            order.Status = 0;
            order.OrderDate = DateTime.Now;
            order.Lines = Mapper.Map<BasketModel[], List<OrderLineViewModel>>(basket.ToArray());
            order.MaxInstallment = catalogWorker.GetLayerMaxInstallment(order.Lines.Select(s => s.ProductId).ToArray());
            HttpContext.Current.Session["Order"] = order;
            return new OrderLayer();
        }
    }
}