using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public class DashboardSummaryViewModel
    {
        public decimal MonthlyPayment { get; set; }
        public int MonthlyOrder { get; set; }
        public decimal DailyPayment { get; set; }
        public int DailyOrder { get; set; }
    }

    public class NotificViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime OrderDate { get; set; }
        public string StatusCss { get; set; }
    }
}