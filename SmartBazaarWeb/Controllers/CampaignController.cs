using SmartBazaar.Web.Business.Workers;
using System.Web.Mvc;

namespace SmartBazaar.Web.Controllers
{
    public class CampaignController : Controller
    {
        private readonly CatalogWorker m_catalogWorker;

        public CampaignController()
        {
            m_catalogWorker = new CatalogWorker();
        }

        [NonAction]
        // GET: Campaign
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
            var model = m_catalogWorker.GetSiteCampaignDetail(id);
            return View(model);
        }
    }
}