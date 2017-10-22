using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayPal.Api;
using SmartBazaar.Web.Business.Layers;
using SmartBazaar.Web.Models.Internal;
using System.Globalization;

namespace SmartBazaar.Web.Components.Payment.Paypal
{
    public class Controller
    {
        private APIContext apiCtx;

        public Controller(Dictionary<string, string> config)
        {
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            apiCtx = new APIContext(accessToken);
            
        }

        public string Payment()
        {
            var culture = CultureInfo.GetCultureInfo("en-US");
            IFormatProvider numericFormatProvider = culture.NumberFormat;


            var orderLayer = new OrderLayer();
            
            var itemList = new ItemList();
            itemList.items = new List<Item>();
            foreach (var line in orderLayer.Order.Lines)
            {
                var item = new Item
                {
                    currency = "TRY",
                    description = line.ProductName,
                    name = line.ProductName,
                    price = line.Price.ToString("F2", numericFormatProvider),
                    quantity = line.Quantity.ToString(),
                    sku = line.ProductId.ToString(),
                    tax = line.Tax.ToString("F2", numericFormatProvider)
                };
                itemList.items.Add(item);
            }
            var payer = new Payer { payment_method = "paypal" };
            var baseUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/Order/PaymentCallback/Paypal";
            var redirUrl = new RedirectUrls
            {
                return_url = baseUrl + "?result=ok",
                cancel_url = baseUrl + "?result=fail"
            };
            var details = new Details
            {
                fee = (orderLayer.Order.InstallmentFee + orderLayer.Order.PaymentFee).ToString("F2", numericFormatProvider),
                shipping = orderLayer.Order.ShipCost.ToString("F2", numericFormatProvider),
                tax = orderLayer.Order.TaxTotal.ToString("F2", numericFormatProvider),
                subtotal = orderLayer.Order.OrderTotal.ToString("F2", numericFormatProvider)
            };
            var amount = new Amount
            {
                currency = "TRY",
                total = orderLayer.Order.GrandTotal.ToString("F2", numericFormatProvider),
                details = details
            };
            var transactionList = new List<Transaction>();
            transactionList.Add(
                new Transaction
                {
                    invoice_number = orderLayer.Order.Id.ToString("00000000"),
                    amount = amount,
                    item_list = itemList
                }
            );
            var payment = new PayPal.Api.Payment
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrl
            };
            var paymenter = payment.Create(apiCtx);
            var links = paymenter.links.GetEnumerator();
            while (links.MoveNext())
            {
                var link = links.Current;
                if (link.rel.ToLower().Trim().Equals("approval_url"))
                {
                    return link.href;
                }
            }
            return string.Empty;
        }

        public PaymentResult PaymentExcution(string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new PayPal.Api.Payment { id = paymentId };
            var executedPayment = payment.Execute(apiCtx, paymentExecution);
            
            return new PaymentResult
            {
                Status = executedPayment.state == "approved",
                Message = executedPayment.state,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(executedPayment)
            };
        }

        public PaymentResult Refund(string log)
        {
            dynamic logData = Newtonsoft.Json.JsonConvert.DeserializeObject(log);
            var refund = new Refund();
            PayPal.Api.Payment payment = PayPal.Api.Payment.Get(apiCtx, logData.id.ToString());
            var response = payment.transactions.FirstOrDefault().related_resources.FirstOrDefault().sale.Refund(apiCtx, refund);
            return new PaymentResult
            {
                Status = response.state == "completed",
                Message = response.state,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(response)
            };
        }
    }
}