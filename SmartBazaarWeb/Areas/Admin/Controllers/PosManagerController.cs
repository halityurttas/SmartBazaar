using SmartBazaar.Web.Areas.Admin.Models;
using SmartBazaar.Web.Business.Workers;
using SmartBazaar.Web.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartBazaar.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    public class PosManagerController : Controller
    {
        private readonly PosWorker m_posWorker;

        public PosManagerController()
        {
            m_posWorker = new PosWorker();
        }

        // GET: Admin/PosManager
        public ActionResult Index(int page = 1, short Status = 1)
        {
            this.StatusList(Status);
            ViewBag.CurrentPage = page;
            var result = m_posWorker.GetManagerPosSettings(Status);
            ViewBag.TotalPage = result.Count;
            var model = result.OrderBy(o => o.Id).Skip((page - 1) * AppConfig.PAGE_SIZE).Take(AppConfig.PAGE_SIZE);
            return View(model);
        }

        // GET: Admin/PosManager/Edit
        public ActionResult Edit(int id)
        {
            try
            {
                var model = m_posWorker.GetManagerPosSettingDetail(id);
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
        public ActionResult Edit(PosSettingsDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_posWorker.UpdatePosSettings(model);
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


    }
}