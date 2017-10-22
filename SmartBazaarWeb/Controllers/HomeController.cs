using System.Linq;
using System.Web.Mvc;

namespace SmartBazaar.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var catalogWorker = new Business.Workers.CatalogWorker();
            var model = catalogWorker.GetSiteHomeList();
            return View(model);
        }

        public ActionResult Contact()
        {
            return null;
        }

        /* Partial action */
        [ChildActionOnly]
        public ActionResult Slider()
        {
            var catalogWorker = new Business.Workers.CatalogWorker();
            var model = catalogWorker.GetSiteCampaignSlider();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult ParamValue(int id)
        {
            var paramWorker = new Business.Workers.ParamWorker();
            return Content(paramWorker.GetParamValue(id));
        }

    }
}