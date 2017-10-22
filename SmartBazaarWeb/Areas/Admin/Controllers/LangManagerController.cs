using SmartBazaar.Web.Business.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartBazaar.Web.Helpers.Extensions;

namespace SmartBazaar.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    public class LangManagerController : Controller
    {
        private readonly LangWorker m_langWorker;

        public LangManagerController()
        {
            m_langWorker = new LangWorker();
        }

        // GET: Admin/LangManager
        public ActionResult Index(short status = 1)
        {
            var model = m_langWorker.GetManagerLangBooksList(status);
            this.StatusList(status);
            return View(model);
        }

        // Get: Admin/LangManager/Create
        public ActionResult Create()
        {
            var model = new Models.LangBooksEditViewModel();
            this.StatusList(1);
            return View(model);
        }

        // Post: Admin/LangManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.LangBooksEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_langWorker.InsertManagerLangBook(model);
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

        // Get: Admin/LangManager/Edit
        public ActionResult Edit(int id)
        {
            var model = m_langWorker.GetManagerLangBook(id);
            this.StatusList(model.Status);
            return View(model);
        }

        // Post: Admin/LangManager/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.LangBooksEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(model.Code)) { model.Code = ""; }
                    m_langWorker.UpdateManagerLangBook(model);
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

    }
}