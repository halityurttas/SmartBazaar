using AutoMapper;
using SmartBazaar.Web.Business.Layers;
using SmartBazaar.Web.Business.Walkers;
using SmartBazaar.Web.Business.Workers;
using SmartBazaar.Web.Helpers.Contents;
using SmartBazaar.Web.Models.Site;
using SmartBazaar.Web.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Configuration;

namespace SmartBazaar.Web.Controllers
{
    [Authorize()]
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index(short? status, DateTime? startDate)
        {
            var orderWorker = new OrderWorker();
            var model = orderWorker.GetSiteOrders(status, startDate);
            return View(model);
        }

        // GET: Order/Detail
        public ActionResult Detail(int id)
        {
            var orderWorker = new OrderWorker();
            var model = orderWorker.GetSiteOrder(id);
            return View(model);
        }

        // GET: Order/Cancel
        public ActionResult Cancel(int id)
        {
            var orderWorker = new OrderWorker();
            orderWorker.UpdateSiteOrderStatus(id, 98);
            return RedirectToAction("Index");
        }

        //GET: Begin
        public ActionResult Begin()
        {
            var customerWorker = new CustomerWorker();
            var shipmentWorker = new ShipmentWorker();

            ViewBag.Cities = LocationWalker.GetCities().Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Title });
            ViewBag.Districts = LocationWalker.GetDistrict(1).Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Title });

            var orderLayer = OrderLayer.Create();
            orderLayer.Order.Addresses = customerWorker.GetSiteCustomerAddresses(CustomerLayer.Customer.Id, 1);
            orderLayer.Order.Shipments = shipmentWorker.GetSiteShipmentTypes();

            return View(orderLayer.Order);
        }

        //POST: Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(FormCollection frm)
        {
            var shipmentWorker = new ShipmentWorker();
            var paymentWorker = new PaymentWorker();
            var customerWorker = new CustomerWorker();
            var orderLayer = new OrderLayer();

            if (frm["AddressId"] != null)
            {
                orderLayer.Order.ShipAddressId = int.Parse(frm["AddressId"]);
                if (frm["InvoiceSendPartial"] == "yes")
                    orderLayer.Order.InvoiceAddressId = int.Parse(frm["InvoiceAddressId"]);
                else orderLayer.Order.InvoiceAddressId = int.Parse(frm["AddressId"]);
            }

            orderLayer.Order.ShipmentTypeId = int.Parse(frm["ShipmentType"]);
            orderLayer.Order.Note = frm["Note"];

            if (orderLayer.Order.ShipCost == 0)
            {
                decimal shipCost = ShippingHelper.ShippingPrice(shipmentWorker.GetSiteShipmentType(orderLayer.Order.ShipmentTypeId));
                orderLayer.Order.TaxTotal += shipCost * 18 / 118;
                orderLayer.Order.ShipCost = shipCost * 100 / 118;
                orderLayer.Order.GrandTotal += shipCost;
            }

            orderLayer.Sync();
            orderLayer.Order.Payments = paymentWorker.GetSitePaymentTypes();

            ViewBag.ShipmentType = shipmentWorker.GetSiteShipmentType(orderLayer.Order.ShipmentTypeId);
            if (orderLayer.Order.ShipAddressId.HasValue)
            {
                ViewBag.ShipmentAddress = customerWorker.GetSiteCustomerAddress(orderLayer.Order.ShipAddressId.Value);
            }
            
            if (frm["InvoiceSendPartial"] == "yes")
            {
                ViewBag.InvoiceAddress = customerWorker.GetSiteCustomerAddress(orderLayer.Order.InvoiceAddressId.Value);
            }

            return View(orderLayer.Order);
        }

        

        //POST: Transaction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transaction(FormCollection frm)
        {
            var paymentWorker = new PaymentWorker();
            var orderWorker = new OrderWorker();
            var orderLayer = new OrderLayer();

            orderLayer.Order.PaymentTypeId = int.Parse(frm["paymentType"]);
            var paymentDetail = paymentWorker.GetSitePaymentType(orderLayer.Order.PaymentTypeId);
            
            //payment commission calculation
            decimal currentPaymentFee = paymentDetail.ProcessFee + (orderLayer.Order.GrandTotal * (decimal)(paymentDetail.CommissionRate / 100));
            decimal currentPaymentTax = orderLayer.Order.PaymentFee * 0.18M;
            currentPaymentFee -= currentPaymentTax;

            //installment commision calculation
            decimal currentInstallmentFee = 0M;
            decimal currentInstallmentTax = 0M;
            if (paymentDetail.Method == 2)
            {
                orderLayer.Order.InstallmentId = int.Parse(frm["installment"]);
                currentInstallmentFee = orderLayer.Order.GrandTotal * (decimal)(paymentDetail.Installments.FirstOrDefault(f => f.Id == orderLayer.Order.InstallmentId).Rate / 100);
                currentInstallmentTax = currentInstallmentFee * 0.18M;
                currentInstallmentFee -= currentInstallmentTax;
            }

            //join payment and installment to order
            orderLayer.Order.PaymentFee = currentPaymentFee;
            orderLayer.Order.InstallmentFee = currentInstallmentFee;

            //add tax and grand total
            orderLayer.Order.TaxTotal += currentPaymentTax + currentInstallmentTax;
            orderLayer.Order.GrandTotal += orderLayer.Order.PaymentFee + orderLayer.Order.InstallmentFee;
            orderLayer.Sync();

            orderLayer.Order.Id = orderWorker.InsertSiteOrder(orderLayer.Order);
            orderLayer.Sync();

            if (paymentDetail.Method == 1)
            {
                var paymentEntityModel = Mapper.Map<PaymentEntityViewModel>(orderLayer.Order);
                paymentEntityModel.PaymentDate = DateTime.Now;
                paymentEntityModel.Status = 0;
                paymentEntityModel.Log = "Peşin ödeme";
                paymentEntityModel.InstallmentId = null;
                paymentWorker.InsertSitePaymentEntity(paymentEntityModel);
                return RedirectToAction("Commit", new { id = paymentDetail.Method });
            }
            else
            {
                return RedirectToAction("Payment");
            }
            
        }

        //GET: Commit
        public ActionResult Commit(int id)
        {
            var orderLayer = new OrderLayer();

            string orderMail = this.RenderRazorView("Mails/Order", orderLayer.Order);
            var customerWorker = new CustomerWorker();
            var customerUser = customerWorker.GetCustomerUser(CustomerLayer.Customer.Id.ToString());
            var mailer = new Khaled.SmtpClient.SmtpMailClient();
            mailer.PostMail(ConfigurationManager.AppSettings["AdminEmail"], "Sipariş", orderMail);
            BasketLayer.RemoveAll();

            ViewBag.Method = id;

            return View(orderLayer.Order);
        }

        public ActionResult Payment(string message = null)
        {
            var orderLayer = new OrderLayer();
            var payment = orderLayer.Order.Payments.FirstOrDefault(f => f.Id == orderLayer.Order.PaymentTypeId);

            var posWorker = new PosWorker();
            var posSetting = posWorker.GetSitePosSetting(payment.PosType);

            switch (posSetting.AssemblyData)
            {
                case "paypal":
                    var paypal = new Components.Payment.Paypal.Controller(posSetting.Parameters);
                    return Redirect(paypal.Payment());
                case "payflex":
                    ViewData["Pos"] = posSetting.AssemblyData;
                    ViewData["Installment"] = payment.Installments.FirstOrDefault(f => f.Id == orderLayer.Order.InstallmentId).NumberOf;
                    ViewData["Message"] = message;
                    return View();
                default:
                    return null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Payment(Models.Internal.PaymentModel model)
        {
            if (ModelState.IsValid)
            {
                var posWorker = new PosWorker();
                var posSetting = posWorker.GetSitePosSetting(model.Pos);
                
                var orderLayer = new OrderLayer();
                var payment = orderLayer.Order.Payments.FirstOrDefault(f => f.Id == orderLayer.Order.PaymentTypeId);

                switch (model.Pos)
                {
                    case "payflex":
                        var akposRequest = new Components.Payment.PayFlex.Models.MPIStatusRequest
                        {
                            BankId = posSetting.Referance,
                            BrandName = model.CardType == "visa" ? "100" : model.CardType == "mastercard" ? "200" : "",
                            ExpiryDate = model.ExpiryMonth + model.ExpiryYear,
                            FailureUrl = Request.Url.Host + "/Order/PaymentCallback/payflex?result=fail",
                            MerchantId = posSetting.Parameters["merchantid"],
                            MerchantPassword = posSetting.Parameters["password"],
                            InstallmentCount = int.Parse(model.Installment) > 0 ? model.Installment : null,
                            Pan = model.CCNumber,
                            PurchaseAmount = string.Format("{0:F0}", orderLayer.Order.GrandTotal * 100),
                            SuccessUrl = Request.Url.Host + "/Order/PaymentCallback/payflex?result=ok",
                            VerifyEnrollmentRequestId = orderLayer.Order.Id.ToString()
                        };
                        var payFlex = new Components.Payment.PayFlex.Controller(akposRequest);
                        if (payFlex.Is3D(posSetting.Parameters["mpiurl"]))
                        {
                            payFlex.PaymentModel = model;
                            Session["PayFlexInstance"] = payFlex;
                            return View("PayFlexMpiPost", payFlex.MPIStatus);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(akposRequest.InstallmentCount))
                            {
                                //TODO: payflex TRY kodu girilecek
                                var vprepaidRequest = new Components.Payment.PayFlex.Models.VPrepaidRequest
                                {
                                    BankId = akposRequest.BankId,
                                    CurrencyAmount = akposRequest.Currency,
                                    CurrencyCode = "888",
                                    Cvv = model.Ccv,
                                    Expiry = akposRequest.ExpiryDate,
                                    MerchantId = akposRequest.MerchantId,
                                    Pan = akposRequest.Pan,
                                    Password = akposRequest.MerchantPassword,
                                    TransactionId = Guid.NewGuid().ToString(),
                                    TransactionType = "sale"
                                };
                                payFlex.VPrepaidCheckout(posSetting.Parameters["checkouturl"], vprepaidRequest);
                                if (payFlex.CheckoutResult.ResultCode == "0000")
                                {
                                    var paymentWorker = new PaymentWorker();
                                    var paymentEntityModel = Mapper.Map<PaymentEntityViewModel>(orderLayer.Order);
                                    paymentEntityModel.PaymentDate = DateTime.Now;
                                    paymentEntityModel.Status = 0;
                                    paymentEntityModel.Log = Components.Converters.StringSerializer.Serialize<Components.Payment.PayFlex.Models.VPrepaidResponse>(payFlex.CheckoutResult);
                                    paymentWorker.InsertSitePaymentEntity(paymentEntityModel);
                                    return RedirectToAction("Commit", new { id = orderLayer.Order.Id });
                                }
                                else
                                {
                                    return RedirectToAction("Payment", new { message = (new Components.Payment.PayFlex.ErrorCodes())[payFlex.CheckoutResult.ResultCode] });
                                }
                            }
                            else
                            {
                                //TODO: payflex TRY kodu girilecek
                                var vinstallmentRequest = new Components.Payment.PayFlex.Models.VInstallmentRequest
                                {
                                    BankId = akposRequest.BankId,
                                    CurrencyAmount = akposRequest.Currency,
                                    CurrencyCode = "888",
                                    Cvv = model.Ccv,
                                    Expiry = akposRequest.ExpiryDate,
                                    MerchantId = akposRequest.MerchantId,
                                    Pan = akposRequest.Pan,
                                    Password = akposRequest.MerchantPassword,
                                    TransactionId = Guid.NewGuid().ToString(),
                                    TransactionType = "sale",
                                    InstallmentCount = akposRequest.InstallmentCount
                                };
                                payFlex.VInstallmentCheckout(posSetting.Parameters["checkouturl"], vinstallmentRequest);
                                if (payFlex.CheckoutResult.ResultCode == "0000")
                                {
                                    var paymentWorker = new PaymentWorker();
                                    var paymentEntityModel = Mapper.Map<PaymentEntityViewModel>(orderLayer.Order);
                                    paymentEntityModel.PaymentDate = DateTime.Now;
                                    paymentEntityModel.Status = 0;
                                    paymentEntityModel.Log = Components.Converters.StringSerializer.Serialize<Components.Payment.PayFlex.Models.VInstallmentResponse>(payFlex.CheckoutResult);
                                    paymentWorker.InsertSitePaymentEntity(paymentEntityModel);
                                    return RedirectToAction("Commit", new { id = orderLayer.Order.Id });
                                }
                                else
                                {
                                    return RedirectToAction("Payment", new { message = (new Components.Payment.PayFlex.ErrorCodes())[payFlex.CheckoutResult.ResultCode] });
                                }
                            }
                        }
                    default:
                        return RedirectToAction("Begin");
                }
            }
            return View(model);
        }

        public ActionResult PaymentCallback(string id)
        {
            var orderLayer = new OrderLayer();
            var payment = orderLayer.Order.Payments.FirstOrDefault(f => f.Id == orderLayer.Order.PaymentTypeId);

            var posWorker = new PosWorker();
            var posSetting = posWorker.GetSitePosSetting(payment.PosType);


            switch (id.ToLower())
            {
                case "paypal":
                    if (Request.Params["result"] == "fail") return View("PaymentError");
                    var paypal = new Components.Payment.Paypal.Controller(posSetting.Parameters);
                    var paypalresult = paypal.PaymentExcution(Request.Params["payerId"], Request.Params["paymentId"]);
                    if (paypalresult.Status)
                    {
                        var paymentWorker = new PaymentWorker();
                        var paymentEntityModel = Mapper.Map<PaymentEntityViewModel>(orderLayer.Order);
                        paymentEntityModel.PaymentDate = DateTime.Now;
                        paymentEntityModel.Status = 0;
                        paymentEntityModel.Log = paypalresult.Data;
                        paymentWorker.InsertSitePaymentEntity(paymentEntityModel);
                    }
                    return RedirectToAction("Commit", new { id = orderLayer.Order.Id.ToString("000000") });
                case "payflex":
                    if (Request.Params["result"] == "fail") return RedirectToAction("Payment", "Bir hata oluştu lütfen tekrar deneyin!");
                    Components.Payment.PayFlex.Controller payFlex = Session["PayFlexInstance"] as Components.Payment.PayFlex.Controller;
                    Dictionary<string, string> formdata = new Dictionary<string, string>();
                    foreach (string prm in Request.Params.AllKeys)
                    {
                        formdata.Add(prm, Request.Params[prm]);
                    }
                    payFlex.MPITransaction = new Components.Payment.PayFlex.Models.MPITransactionResponse(formdata);
                    payFlex.MPIChekout(posSetting.Parameters["mpicheckouturl"]);
                    if (int.Parse(payFlex.PaymentModel.Installment) > 0)
                    {
                        var result = payFlex.CheckoutResult as Components.Payment.PayFlex.Models.VInstallmentResponse;
                        if (result.ResultCode == "0000")
                        {
                            var paymentWorker = new PaymentWorker();
                            var paymentEntityModel = Mapper.Map<PaymentEntityViewModel>(orderLayer.Order);
                            paymentEntityModel.PaymentDate = DateTime.Now;
                            paymentEntityModel.Status = 0;
                            paymentEntityModel.Log = Components.Converters.StringSerializer.Serialize<Components.Payment.PayFlex.Models.VInstallmentResponse>(payFlex.CheckoutResult);
                            paymentWorker.InsertSitePaymentEntity(paymentEntityModel);
                            return RedirectToAction("Commit", new { id = orderLayer.Order.Id });
                        }
                        else
                        {
                            return RedirectToAction("Payment", "Bir hata oluştu lütfen tekrar deneyin!");
                        }
                    }
                    else
                    {
                        var result = payFlex.CheckoutResult as Components.Payment.PayFlex.Models.VPrepaidResponse;
                        if (result.ResultCode == "0000")
                        {
                            var paymentWorker = new PaymentWorker();
                            var paymentEntityModel = Mapper.Map<PaymentEntityViewModel>(orderLayer.Order);
                            paymentEntityModel.PaymentDate = DateTime.Now;
                            paymentEntityModel.Status = 0;
                            paymentEntityModel.Log = Components.Converters.StringSerializer.Serialize<Components.Payment.PayFlex.Models.VPrepaidResponse>(payFlex.CheckoutResult);
                            paymentWorker.InsertSitePaymentEntity(paymentEntityModel);
                            return RedirectToAction("Commit", new { id = orderLayer.Order.Id });
                        }
                        else
                        {
                            return RedirectToAction("Payment", "Bir hata oluştu lütfen tekrar deneyin!");
                        }
                    }
                default:
                    return null;
            }
        }

        //POST: AddAddress
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAddress(FormCollection frm)
        {
            var model = new CustomerAddressViewModel
            {
                CustomerId = CustomerLayer.Customer.Id,
                City = LocationWalker.GetCities().FirstOrDefault(f => f.Id == int.Parse(frm["AddressCity"])).Title,
                Detail = frm["AddressDetail"],
                District = frm["AddressDistrict"],
                IsDefault = false,
                PostalCode = frm["AddressPostalCode"],
                Status = 1,
                Title = frm["AddressTitle"],
                Town = frm["AddressTown"]
            };
            CustomerWorker customerWorker = new CustomerWorker();
            customerWorker.InsertSiteCustomerAddress(model);

            return RedirectToAction("Begin");
        }

        //JSON Actions

        //GET: GetDistricts
        public ActionResult GetDistricts(int CityId)
        {
            return Json(
                LocationWalker.GetDistrict(CityId).Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Title })
                , JsonRequestBehavior.AllowGet);
        }
    }
}