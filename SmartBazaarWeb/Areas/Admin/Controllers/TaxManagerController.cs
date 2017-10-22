using SmartBazaar.Web.Helpers.Extensions;
using SmartBazaar.Web.Business.Workers;
using System;
using System.Linq;
using System.Web.Mvc;
using SmartBazaar.Web.Areas.Admin.Models;

namespace SmartBazaar.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    public class TaxManagerController : Controller
    {
        public readonly TaxWorker m_taxWorker;
        public TaxManagerController()
        {
            m_taxWorker = new TaxWorker();
        }

        // GET: Admin/TaxManager
        public ActionResult Index(int page = 1, short Status = 1)
        {
            this.StatusList(Status);
            ViewBag.CurrentPage = page;
            var records = m_taxWorker.GetManagerProductTaxList(Status);
            ViewBag.TotalPage = records.Count;
            var model = records.OrderBy(o => o.Id).Skip((page - 1) * AppConfig.PAGE_SIZE).Take(AppConfig.PAGE_SIZE);
            return View(model);
        }

        //GET: Admin/TaxManager/Create
        public ActionResult Create()
        {
            this.StatusList(1);
            var model = new ProductTaxEditViewModel { Status = 1 };
            return View(model);
        }

        //POST: Admin/TaxManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductTaxEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_taxWorker.InsertManagerProductTaxEdit(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Generic", ex.Message);
                }
            }
            this.StatusList(model.Status);
            return View(model);
        }

        //GET: Admin/TaxManager/Edit
        public ActionResult Edit(int id)
        {
            try
            {
                this.StatusList(1);
                var model = m_taxWorker.GetManagerProductTaxEdit(id);
                return View(model);
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
                return null;
            }

        }

        //POST: Admin/TaxManager/Edit
        [HttpPost]
        public ActionResult Edit(ProductTaxEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_taxWorker.UpdateManagerProductTaxEdit(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Generic", ex.Message);
                }
            }
            this.StatusList(model.Status);
            return View(model);
        }

        //GET: Admin/TaxManager/Delete
        public ActionResult Delete(int id)
        {
            try
            {
                m_taxWorker.DeleteProductTax(id);
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