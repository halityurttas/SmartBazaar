using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Internal
{
    public class PaymentResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }

    public class PaymentModel
    {
        public string CardType { get; set; }
        public string CCNumber { get; set; }
        public string Ccv { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string PayerName { get; set; }
        public string Pos { get; set; }
        public string Installment { get; set; }
    }
}