using SmartBazaar.Web.Areas.Admin.Models;
using SmartBazaar.Web.Business.Walkers;
using SmartBazaar.Web.Business.Workers;
using SmartBazaar.Web.Helpers.Extensions;
using SmartBazaar.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SmartBazaar.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    public class PaymentManagerController : Controller
    {
        private readonly PaymentWorker m_paymentWorker;
        private readonly PosWorker m_posWorker;
        public PaymentManagerController()
        {
            m_paymentWorker = new PaymentWorker();
            m_posWorker = new PosWorker();
        }

        // GET: Admin/PaymentManager
        public ActionResult Index(int page = 1, short Status = 1)
        {
            this.StatusList(Status);
            ViewBag.CurrentPage = page;
            var records = m_paymentWorker.GetManagerPaymentTypesList(Status);
            ViewBag.TotalPage = records.Count;
            var model = records.OrderBy(o => o.Id).Skip((page - 1) * AppConfig.PAGE_SIZE).Take(AppConfig.PAGE_SIZE);
            return View(model);
        }

        //GET: Admin/PaymentManager/Create
        public ActionResult Create()
        {
            this.StatusList(1);
            this.Pair2List<PaymentTypesEditViewModel, short, short>(PaymentListProvider.GetMethods(), m => m.Method, null);
            this.Pair2List<PaymentTypesEditViewModel, string, string>(m_posWorker.GetPosForList(), m => m.PosType, "");
            var model = new PaymentTypesEditViewModel
            {
                Status = 1
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(PaymentTypesEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_paymentWorker.InsertManagerPaymentTypesEdit(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            this.StatusList(model.Status);
            this.Pair2List<PaymentTypesEditViewModel, short, short>(PaymentListProvider.GetMethods(), m => m.Method, model.Method.ToString());
            return View(model);
        }

        //GET: Admin/PaymentManager/Edit
        public ActionResult Edit(int id)
        {
            try
            {
                var model = m_paymentWorker.GetManagerPaymentTypesEdit(id);
                this.StatusList(model.Status);
                this.Pair2List<PaymentTypesEditViewModel, short, short>(PaymentListProvider.GetMethods(), m => m.Method, model.Method.ToString());
                this.Pair2List<PaymentTypesEditViewModel, string, string>(m_posWorker.GetPosForList(), m => m.PosType, model.PosType);
                return View(model);
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
                return null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(PaymentTypesEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_paymentWorker.UpdateManagerPaymentTypesEdit(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            this.StatusList(model.Status);
            this.Pair2List<PaymentTypesEditViewModel, short, short>(PaymentListProvider.GetMethods(), m => m.Method, model.Method.ToString());
            this.Pair2List<PaymentTypesEditViewModel, string, string>(m_posWorker.GetPosForList(), m => m.PosType, model.PosType);
            return View(model);
        }

        //GET: Admin/PaymentManager/Delete
        public ActionResult Delete(int id)
        {
            try
            {
                m_paymentWorker.DeleteManagerPaymentTypesDelete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
                return null;
            }
        }

        //POST: Admin/PaymentManager/UpdateStatus
        [HttpPost]
        public ActionResult UpdateStatus(int id, int payid, short PayStatuses)
        {
            try
            {
                var paymentWorker = new PaymentWorker();
                var payments = paymentWorker.GetManagerPaymentsByOrderId(id);
                string log = null;
                if (payments.Any(a => a.Payment_Types.Method == 2))
                {
                    var posWorker = new PosWorker();
                    var payment = payments.FirstOrDefault(f => f.Payment_Types.Method == 2);
                    dynamic settings = null;
                    switch (payment.Payment_Types.PosType.ToLower())
                    {
                        case "paypal":
                            settings = posWorker.GetSitePosSetting("paypal");
                            var paypal = new Components.Payment.Paypal.Controller(settings.Parameters);
                            var result = paypal.Refund(payment.Log);
                            if (result.Status)
                            {
                                log = result.Data;
                            }
                            break;
                        case "isbank":
                            settings = posWorker.GetSitePosSetting("isbank");
                            log = CancelPayflex(settings, payment);
                            break;
                        case "vakifbank":
                            settings = posWorker.GetSitePosSetting("vakifbank");
                            log = CancelPayflex(settings, payment);
                            break;
                        case "ziraatbank":
                            settings = posWorker.GetSitePosSetting("ziraatbank");
                            log = CancelPayflex(settings, payment);
                            break;
                        default:
                            break;
                    }
                }
                paymentWorker.UpdatePaymentStatus(payid, PayStatuses, log);
                return RedirectToAction("Detail", "OrderManager", new { id = id });
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
                return null;
            }
            
        }

        [NonAction]
        private string CancelPayflex(dynamic settings, SmartBazaar.Data.Entities.Payment_Entities payment)
        {
            var payflex = new Components.Payment.PayFlex.Controller();
            var payflexLog = Components.Converters.StringSerializer.Deserialize<Components.Payment.PayFlex.Models.VInstallmentResponse>(payment.Log);
            if (payment.PaymentDate.AddDays(-1) < DateTime.Today)
            {
                payflex.VCancel(settings.Parameters["checkouturl"], new Components.Payment.PayFlex.Models.VCancelRequest
                {
                    BankId = settings.Parameters["bankid"],
                    MerchantId = payflexLog.MerchantId,
                    Password = settings.Parameters["password"],
                    ReferenceTransactionId = payflexLog.TransactionId,
                    TransactionId = Guid.NewGuid().ToString(),
                    TransactionType = "Cancel"
                });
                Components.Payment.PayFlex.Models.VCancelResponse cancelresult = payflex.CheckoutResult as Components.Payment.PayFlex.Models.VCancelResponse;
                return Components.Converters.StringSerializer.Serialize<Components.Payment.PayFlex.Models.VCancelResponse>(cancelresult);
            }
            else
            {
                payflex.VRefund(settings.Parameters["checkouturl"], new Components.Payment.PayFlex.Models.VRefundRequest
                {
                    BankId = settings.Parameters["bankid"],
                    MerchantId = payflexLog.MerchantId,
                    Password = settings.Parameters["password"],
                    ReferenceTransactionId = payflexLog.TransactionId,
                    TransactionId = Guid.NewGuid().ToString(),
                    TransactionType = "Cancel"
                });
                Components.Payment.PayFlex.Models.VRefundRequest refundresult = payflex.CheckoutResult as Components.Payment.PayFlex.Models.VRefundRequest;
                return Components.Converters.StringSerializer.Serialize<Components.Payment.PayFlex.Models.VRefundRequest>(refundresult);
            }
        }

    }
}