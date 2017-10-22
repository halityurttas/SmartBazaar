using SmartBazaar.Web.Business.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartBazaar.Web.Helpers.Extensions;
using SmartBazaar.Web.Areas.Admin.Models;

namespace SmartBazaar.Web.Areas.Admin.Controllers
{
    public class SliderManagerController : Controller
    {
        private readonly SliderWorker m_sliderWorker;
        public SliderManagerController()
        {
            m_sliderWorker = new SliderWorker();
        }

        // GET: Admin/SliderManager
        public ActionResult Index(int page = 1, short status = 1)
        {
            this.StatusList(status);
            ViewBag.CurrentPage = page;
            var records = m_sliderWorker.GetManagerSliders(status);
            ViewBag.TotalPage = records.Count;
            var model = records.OrderBy(o => o.Title).Skip((page - 1) * AppConfig.PAGE_SIZE).Take(AppConfig.PAGE_SIZE).ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            this.StatusList(1);
            var model = new SliderEditViewModel
            {
                Status = 1
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(SliderEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_sliderWorker.SaveManagerSlider(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            this.StatusList(model.Status);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var model = m_sliderWorker.GetManagerSlider(id);
                this.StatusList(model.Status);
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
        public ActionResult Edit(SliderEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_sliderWorker.UpdateManagerSlider(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            this.StatusList(model.Status);
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                m_sliderWorker.DeleteManagerSlider(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
                return null;
            }
        }

    }
}