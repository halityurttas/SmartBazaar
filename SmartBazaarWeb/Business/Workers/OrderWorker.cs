using AutoMapper;
using SmartBazaar.Data;
using SmartBazaar.Data.Entities;
using SmartBazaar.Web.Areas.Admin.Models;
using SmartBazaar.Web.Business.Layers;
using SmartBazaar.Web.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartBazaar.Web.Business.Workers
{
    public class OrderWorker
    {
        private readonly ContentContext m_ContentContext;
        public OrderWorker()
        {
            m_ContentContext = new ContentContext();
        }

        public List<OrderHeadsListViewModel> GetManagerOrderList(short? status)
        {
            var query = from o in m_ContentContext.Order_Heads
                        where (o.Status == status || status == null)
                        orderby o.OrderDate descending
                        select o;
            return Mapper.Map<Order_Heads[], List<OrderHeadsListViewModel>>(query.ToArray());
        }

        public List<OrderHeadsListViewModel> GetManagerOrderList(short? status, DateTime? start, DateTime? end)
        {
            end = end.HasValue ? end.Value.AddDays(1) : end;
            var query = from o in m_ContentContext.Order_Heads
                        where (status == null || o.Status == status)
                            && (start == null || o.OrderDate > start)
                            && (end == null || o.OrderDate < end)
                        orderby o.OrderDate descending
                        select o;
            return Mapper.Map<Order_Heads[], List<OrderHeadsListViewModel>>(query.ToArray());
        }

        public List<OrderHeadsListViewModel> GetManagerOrderList(int CustomerId)
        {
            var query = from o in m_ContentContext.Order_Heads
                        where o.CustomerId == CustomerId
                        orderby o.OrderDate descending
                        select o;
            return Mapper.Map<Order_Heads[], List<OrderHeadsListViewModel>>(query.ToArray());
        }

        public OrderHeadsEditViewModel GetManagerOrderEdit(int id)
        {
            var query = from o in m_ContentContext.Order_Heads
                        where o.Id == id
                        select o;
            var item = query.FirstOrDefault();
            return Mapper.Map<OrderHeadsEditViewModel>(item);
        }

        public void UpdateOrderStatus(int id, short status)
        {
            var query = from o in m_ContentContext.Order_Heads
                        where o.Id == id
                        select o;
            var item = query.FirstOrDefault();
            item.Status = status;
            m_ContentContext.SaveChanges();
        }

        public int InsertSiteOrder(OrderHeaderProcessModel model)
        {
            var item = Mapper.Map<Order_Heads>(model);
            m_ContentContext.Order_Heads.Add(item);
            m_ContentContext.SaveChanges();
            foreach (var orderModel in model.Lines)
            {
                var orderItem = Mapper.Map<Order_Lines>(orderModel);
                orderItem.OrderId = item.Id;
                m_ContentContext.Order_Lines.Add(orderItem);
            }
            m_ContentContext.SaveChanges();
            
            return item.Id;
        }

        public List<OrderHeaderListViewModel> GetSiteOrders(short? status, DateTime? startDate)
        {
            var query = from o in m_ContentContext.Order_Heads
                        where (!status.HasValue || o.Status == status)
                            && (!startDate.HasValue || o.OrderDate > startDate)
                            && o.CustomerId == CustomerLayer.Customer.Id
                        select o;
            return Mapper.Map<Order_Heads[], List<OrderHeaderListViewModel>>(query.ToArray());
        }

        public OrderHeaderDetailViewModel GetSiteOrder(int id)
        {
            var query = from o in m_ContentContext.Order_Heads
                        where o.Id == id && o.CustomerId == CustomerLayer.Customer.Id
                        select o;
            return Mapper.Map<OrderHeaderDetailViewModel>(query.FirstOrDefault());
        }

        public void UpdateSiteOrderStatus(int id, short status)
        {
            var query = from o in m_ContentContext.Order_Heads
                        where o.Id == id && o.CustomerId == CustomerLayer.Customer.Id
                        select o;
            var item = query.FirstOrDefault();
            item.Status = status;
            m_ContentContext.SaveChanges();
        }

        #region Statistic

        public DashboardSummaryViewModel GetDashboardSummary()
        {
            DateTime monthFirstDate = DateTime.Today.AddDays(1 - DateTime.Today.Day);
            var query = from o in m_ContentContext.Order_Heads
                        where o.OrderDate > monthFirstDate
                        select o;
            var summary = new DashboardSummaryViewModel();
            summary.MonthlyOrder = query.Count();
            summary.MonthlyPayment = query.Select(s => (decimal?)s.GrandTotal).Sum(s => s).GetValueOrDefault();
            summary.DailyOrder = query.Where(w => w.OrderDate > DateTime.Today).Count();
            summary.DailyPayment = query.Where(w => w.OrderDate > DateTime.Today).Select(s => (decimal?)s.GrandTotal).Sum(s => s).GetValueOrDefault();
            return summary;
        }

        public Dictionary<long, decimal> GetDashboardSums()
        {
            TimeSpan span = new TimeSpan(DateTime.Parse("1/1/1970").Ticks);
            DateTime monthAgo = DateTime.Today.AddMonths(-1);
            var query = from o in m_ContentContext.Order_Heads
                        where o.OrderDate >= monthAgo
                        group o by new { o.OrderDate.Year, o.OrderDate.Month, o.OrderDate.Day} into g
                        select new {
                            year = g.Key.Year,
                            month = g.Key.Month,
                            day = g.Key.Day,
                            total = g.Sum(s => s.GrandTotal)
                        };
            return query.ToDictionary(d => new DateTime(d.year, d.month, d.day).Subtract(span).Ticks / 10000, d => d.total);
        }

        public Dictionary<long, int> GetDashboardCounts()
        {
            TimeSpan span = new TimeSpan(DateTime.Parse("1/1/1970").Ticks);
            DateTime monthAgo = DateTime.Today.AddMonths(-1);
            var query = from o in m_ContentContext.Order_Heads
                        where o.OrderDate >= monthAgo
                        group o by new { o.OrderDate.Year, o.OrderDate.Month, o.OrderDate.Day } into g
                        select new
                        {
                            year = g.Key.Year,
                            month = g.Key.Month,
                            day = g.Key.Day,
                            total = g.Count()
                        };
            return query.ToDictionary(d => new DateTime(d.year, d.month, d.day).Subtract(span).Ticks / 10000, d => d.total);
        }

        public List<Areas.Admin.Models.NotificViewModel> GetNotificList()
        {
            var query = from o in m_ContentContext.Order_Heads
                        where o.Status == 0 || o.Status == 98
                        select o;
            return Mapper.Map<Order_Heads[], List<NotificViewModel>>(query.ToArray());
        }

        #endregion

    }
}