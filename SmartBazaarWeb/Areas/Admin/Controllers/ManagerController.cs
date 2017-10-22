using SmartBazaar.Web.Business.Workers;
using SmartBazaar.Web.Models.Internal;
using System.Web.Mvc;
using System.Linq;
using System.Text;

namespace SmartBazaar.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    public class ManagerController : Controller
    {
        // GET: Admin/Manage
        public ActionResult Index()
        {
            var orderWorker = new OrderWorker();
            ViewBag.DashboardSummary = orderWorker.GetDashboardSummary();
            //ViewBag.Sums = orderWorker.GetDashboardSums().Select()

            var sums = orderWorker.GetDashboardSums();
            var sumsSb = new StringBuilder();
            foreach (var kp in sums)
            {
                sumsSb.AppendFormat("[\"{0}\", {1}],", kp.Key, kp.Value);
            }
            if (sums.Count > 0)
            {
                ViewBag.Sums = "[" + sumsSb.ToString().Substring(0, sumsSb.Length - 1) + "]";
            }
            else
            {
                ViewBag.Sums = "[]";
            }
            

            var cnts = orderWorker.GetDashboardCounts();
            var cntsSb = new StringBuilder();
            foreach (var kp in cnts)
            {
                cntsSb.AppendFormat("[\"{0}\", {1}],", kp.Key, kp.Value);
            }
            if (cnts.Count > 0)
            {
                ViewBag.Cnts = "[" + cntsSb.ToString().Substring(0, cntsSb.Length - 1) + "]";
            }
            else
            {
                ViewBag.Cnts = "[]";
            }
            

            return View();
        }

        public ActionResult Error()
        {
            ErrorModel error = TempData[Constants.OperationErrorKey] as ErrorModel;
            return View(error);
        }

        /* partial actions */

        [ChildActionOnly]
        public ActionResult Notific()
        {
            var orderWorker = new OrderWorker();
            var model = orderWorker.GetNotificList();
            return PartialView("_MenuNotification", model);
        }

    }
}