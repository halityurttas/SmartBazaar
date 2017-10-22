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
    [Authorize(Roles = "Admins")]
    public class PageManagerController : Controller
    {
        private PageWorker m_pageWorker;

        public PageManagerController()
        {
            m_pageWorker = new PageWorker();
        }

        // GET: Admin/PageManager
        public ActionResult Index(int page = 1, short status = 1)
        {
            this.StatusList(status);
            ViewBag.CurrentPage = page;
            var records = m_pageWorker.GetManagerPageses(status);
            ViewBag.TotalPage = records.Count;
            var model = records.OrderBy(o => o.Title).Skip((page - 1) * AppConfig.PAGE_SIZE).Take(AppConfig.PAGE_SIZE).ToList();
            return View(model);
        }

        // GET: Admin/PageManager/Add
        public ActionResult Create()
        {
            this.StatusList(1);
            var model = new PagesEditViewModel()
            {
                Status = 1,
                IsFixed = false
            };
            return View(model);
        }

        // POST: Admin/PageManager/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(PagesEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_pageWorker.InsertManagerPages(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            this.StatusList(model.Status);
            return View(model);
        }

        //GET: Admin/PageManager/Edit
        public ActionResult Edit(int id)
        {
            try
            {
                var model = m_pageWorker.GetManagerPages(id);
                this.StatusList(model.Status);
                return View(model);
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
                return null;
            }
        }

        // POST: Admin/PageManager/Edit
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(PagesEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_pageWorker.UpdateManagerPages(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            this.StatusList(model.Status);
            return View(model);
        }

        //GET: Admin/PageManager/Delete
        public ActionResult Delete(int id)
        {
            try
            {
                m_pageWorker.DeleteManagerPages(id);
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