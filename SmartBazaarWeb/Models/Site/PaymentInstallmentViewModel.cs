using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Site
{
    public class PaymentInstallmentViewModel
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public int NumberOf { get; set; }
        public double Rate { get; set; }
    }
}