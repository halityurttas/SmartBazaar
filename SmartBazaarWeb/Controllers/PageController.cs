using SmartBazaar.Web.Business.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartBazaar.Web.Controllers
{
    public class PageController : Controller
    {
        private readonly PageWorker pageWorker;
        public PageController()
        {
            pageWorker = new PageWorker();
        }

        // GET: Page
        public ActionResult Index(int id)
        {
            var model = pageWorker.GetSitePage(id);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult MenuItems(string viewpage = "MenuItems")
        {
            var model = pageWorker.GetPageMenus();
            return PartialView(viewpage, model);
        }
    }
}