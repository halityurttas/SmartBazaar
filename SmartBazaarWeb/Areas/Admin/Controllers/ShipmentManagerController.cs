using SmartBazaar.Web.Areas.Admin.Models;
using SmartBazaar.Web.Business.Workers;
using SmartBazaar.Web.Helpers.Extensions;
using SmartBazaar.Web.Models.Common;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SmartBazaar.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    public class ShipmentManagerController : Controller
    {
        private readonly ShipmentWorker m_shipmentWorker;
        public ShipmentManagerController()
        {
            m_shipmentWorker = new ShipmentWorker();
        }

        // GET: Admin/ShipmentManager
        public ActionResult Index(int page = 1, short Status = 1)
        {
            this.StatusList(Status);
            ViewBag.CurrentPage = page;
            var records = m_shipmentWorker.GetManagerShipmentTypesList(Status);
            ViewBag.TotalPage = records.Count;
            var model = records.OrderBy(o => o.Id).Skip((page - 1) * AppConfig.PAGE_SIZE).Take(AppConfig.PAGE_SIZE);
            return View(model);
        }

        //GET: Admin/ShipmentManager/Create
        public ActionResult Create()
        {
            var model = new ShipmentTypesEditViewModel
            {
                Status = 1
            };
            this.StatusList(1);
            this.Pair2List<ShipmentTypesEditViewModel, short, short>(ShipmentListProvider.GetMethodList(), m => m.PricingMethod, null);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShipmentTypesEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_shipmentWorker.InsertManagerShipmentTypesEdit(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            this.StatusList(model.Status);
            this.Pair2List<ShipmentTypesEditViewModel, short, short>(ShipmentListProvider.GetMethodList(), m => m.PricingMethod, model.PricingMethod.ToString());
            return View(model);
        }

        //GET: Admin/ShipmentManager/Edit
        public ActionResult Edit(int id)
        {
            try
            {
                var model = m_shipmentWorker.GetManagerShipmentTypesEdit(id);
                this.StatusList(model.Status);
                this.Pair2List<ShipmentTypesEditViewModel, short, short>(ShipmentListProvider.GetMethodList(), m => m.PricingMethod, model.PricingMethod.ToString());
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
        public ActionResult Edit(ShipmentTypesEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_shipmentWorker.UpdateManagerShipmentTypesEdit(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            this.StatusList(model.Status);
            this.Pair2List<ShipmentTypesEditViewModel, short, short>(ShipmentListProvider.GetMethodList(), m => m.PricingMethod, model.PricingMethod.ToString());
            return View(model);
        }

        //GET: Admin/ShipmentManager/Delete
        public ActionResult Delete(int id)
        {
            try
            {
                m_shipmentWorker.DeleteManagerShipmentTypes(id);
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