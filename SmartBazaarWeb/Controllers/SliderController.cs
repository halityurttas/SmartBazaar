using SmartBazaar.Web.Business.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartBazaar.Web.Controllers
{
    public class SliderController : Controller
    {
        private readonly SliderWorker m_sliderWorker;
        public SliderController()
        {
            m_sliderWorker = new SliderWorker();
        }

        // GET: Slider
        public ActionResult Index(int id)
        {
            var model = m_sliderWorker.GetSiteSliderDetail(id);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult Sliders(string view = "Sliders")
        {
            var model = m_sliderWorker.GetSiteSliders();
            return PartialView(view, model);
        }
    }
}