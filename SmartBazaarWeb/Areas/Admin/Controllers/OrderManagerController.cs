using SmartBazaar.Web.Areas.Admin.Models;
using SmartBazaar.Web.Business.Workers;
using SmartBazaar.Web.Helpers.Extensions;
using SmartBazaar.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartBazaar.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    public class OrderManagerController : Controller
    {
        // GET: Admin/Order
        public ActionResult Index(short? Status = null, DateTime? Start = null, DateTime? End = null, int page = 1)
        {
            if (!Start.HasValue)
            {
                Start = DateTime.Today.AddMonths(-1);
            }
            var cult = System.Threading.Thread.CurrentThread.CurrentCulture;
            var orderWorker = new OrderWorker();
            this.Pair2List<OrderHeadsEditViewModel, short, short>(OrderHeadsListProvider.GetStatuses(), p => p.Status, Status.ToString());
            ViewBag.Start = Start.HasValue ? Start.Value.ToShortDateString() : "";
            ViewBag.End = End.HasValue ? End.Value.ToShortDateString() : "";
            ViewBag.CurrentPage = page;
            var records = orderWorker.GetManagerOrderList(Status, Start, End);
            ViewBag.TotalPage = records.Count;
            var model = records.OrderByDescending(o => o.OrderDate).Skip((page - 1) * AppConfig.PAGE_SIZE).Take(AppConfig.PAGE_SIZE);
            return View(model);
        }

        // GET: Admin/Order/Detail
        public ActionResult Detail(int id)
        {
            try
            {
                ViewBag.Id = id;

                var orderWorker = new OrderWorker();
                var paymentWorker = new PaymentWorker();
                var customerWorker = new CustomerWorker();

                var model = orderWorker.GetManagerOrderEdit(id);
                var paymodel = paymentWorker.GetManagerPaymentEntitiesByOrder(id);
            
                ViewBag.User = customerWorker.GetCustomerUser(model.Customer.UserId);

                ViewBag.Statuses = SmartBazaar.Web.Models.Common.OrderHeadsListProvider.GetStatuses().Select(s => new SelectListItem { Text = s.Value, Value = s.Key.ToString(), Selected = s.Key == model.Status }).AsEnumerable();
                if (paymodel != null)
                {
                    ViewBag.Payment = paymodel;
                    ViewBag.PayId = paymodel.Id;
                    ViewBag.PayStatuses = SmartBazaar.Web.Models.Common.PaymentEntitiesListsProvider.GetStatuses().Select(s => new SelectListItem { Text = s.Value, Value = s.Key.ToString(), Selected = s.Key == paymodel.Status });
                }
                else
                {
                    ViewBag.PayStatuses = SmartBazaar.Web.Models.Common.PaymentEntitiesListsProvider.GetStatuses().Select(s => new SelectListItem { Text = s.Value, Value = s.Key.ToString()});
                }

                return View(model);
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
                return null;
            }
        }

        // POST: Admin/Order/UpdateStatus
        public ActionResult UpdateStatus(int id, short Statuses)
        {
            try
            {
                if (Statuses == 99)
                {
                    var paymentWorker = new PaymentWorker();
                    var payments = paymentWorker.GetManagerPaymentEntitiesByOrder(id);
                    if (payments.Status != 99)
                    {
                        return RedirectToAction("Detail", new { id = id });
                    }
                }

                var orderWorker = new OrderWorker();
                orderWorker.UpdateOrderStatus(id, Statuses);
                return RedirectToAction("Detail", new { id = id });
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
                return null;
            }
            
        }

        // GET Test pos
        [NonAction]
        public ActionResult TestPos()
        {
            var payflex = new Components.Payment.PayFlex.Controller(new Components.Payment.PayFlex.Models.MPIStatusRequest
            {
                MerchantId = "655500056",
                MerchantPassword = "123456",
                Pan = "4012001037141112",
                ExpiryDate = "1605",
                BrandName = "200",
                PurchaseAmount = "1000",
                VerifyEnrollmentRequestId = "12345678"
            });
            var result = payflex.Is3D("http://sanalpos.innova.com.tr/MPI_V2/Enrollment.aspx");
            return Content(result.ToString() + " : " + Newtonsoft.Json.JsonConvert.SerializeObject(payflex.CheckoutResult));
        }

    }
}