using SmartBazaar.Web.Business.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartBazaar.Web.Helpers.Extensions;
using SmartBazaar.Web.Areas.Admin.Models;
using SmartBazaar.Web.Business.Walkers;
using SmartBazaar.Web.Models.Internal;

namespace SmartBazaar.Web.Areas.Admin.Controllers
{
    public class HtmlBlocksManagerController : Controller
    {
        private readonly HtmlBlocksWorker m_htmlBlocksWorker;
        public HtmlBlocksManagerController()
        {
            m_htmlBlocksWorker = new HtmlBlocksWorker();
        }

        private List<ThemeLocationModel> GetLocationWalkerList()
        {
            return new ThemeLocationWalker().ThemeLocations;
        }

        // GET: Admin/HtmlBlocksManager
        public ActionResult Index(int page = 1, short status = 1)
        {
            this.StatusList(status);
            ViewBag.CurrentPage = page;
            var records = m_htmlBlocksWorker.GetManagerHtmlBlocksList(status);
            ViewBag.TotalPage = records.Count;
            var model = records.OrderBy(o => o.Title).Skip((page - 1) * AppConfig.PAGE_SIZE).Take(AppConfig.PAGE_SIZE).ToList();
            return View(model);
        }

        //GET: Admin/HtmlBlocksManager/Create
        public ActionResult Create()
        {
            this.StatusList(1);
            ViewBag.Location = GetLocationWalkerList().Select(s => new SelectListItem { Text = s.Title, Value = s.Id.ToString() }).ToList();
            var model = new HtmlBlocksEditViewModel
            {
                Status = 1
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(HtmlBlocksEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_htmlBlocksWorker.InsertManagerHtmlBlocks(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Generic", ex.Message);
                }
            }
            this.StatusList(model.Status);
            ViewBag.Location = GetLocationWalkerList().Select(s => new SelectListItem { Text = s.Title, Value = s.Id.ToString(), Selected = s.Id == model.Location }).ToList();
            return View(model);
        }

        //GET: Admin/HtmlBlocksManager/Edit
        public ActionResult Edit(int id)
        {
            try
            {
                var model = m_htmlBlocksWorker.GetManagerHtmlBlocksEdit(id);
                this.StatusList(model.Status);
                ViewBag.Location = GetLocationWalkerList().Select(s => new SelectListItem { Text = s.Title, Value = s.Id.ToString(), Selected = s.Id == model.Location }).ToList();
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
        public ActionResult Edit(HtmlBlocksEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_htmlBlocksWorker.UpdateMangerHtmlBlocks(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Generic", ex.Message);
                }
            }
            this.StatusList(model.Status);
            ViewBag.Location = GetLocationWalkerList().Select(s => new SelectListItem { Text = s.Title, Value = s.Id.ToString(), Selected = s.Id == model.Location }).ToList();
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                m_htmlBlocksWorker.DeleteManagerHtmlBlocks(id);
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