using SmartBazaar.Web.Areas.Admin.Models;
using SmartBazaar.Web.Business.Workers;
using SmartBazaar.Web.Helpers.Extensions;
using SmartBazaar.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SmartBazaar.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    public class CustomerManagerController : Controller
    {
        private readonly CustomerWorker m_customerWorker;
        public CustomerManagerController()
        {
            m_customerWorker = new CustomerWorker();
        }

        #region Customers

        // GET: Admin/CustomerManager
        public ActionResult Index(int page = 1, short Status = 1, string CustTitle = null)
        {
            ViewBag.CurrentPage = page;
            ViewBag.CustTitle = CustTitle;
            this.StatusList(Status);
            var records = m_customerWorker.GetManagerCustomerEntityList(Status, CustTitle);
            ViewBag.TotalPage = records.Count;
            var model = records.OrderBy(o => o.Title).Skip((page - 1) * AppConfig.PAGE_SIZE).Take(AppConfig.PAGE_SIZE);
            return View(model);
        }

        //GET: Admin/CustomerManager/Detail
        public ActionResult Detail(int id)
        {
            try
            {
                var model = m_customerWorker.GetManagerCustomerEntityDetail(id);

                var orderWorker = new OrderWorker();
                ViewBag.Orders = orderWorker.GetManagerOrderList(id).Take(10).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
                return null;
            }
        }

        //GET: Admin/CustomerManager/Edit
        public ActionResult Edit(int id)
        {
            try
            {
                var model = m_customerWorker.GetManagerCustomerEntityEdit(id);
                this.StatusList(model.Status);
                ViewBag.Gender = CustomerEntityListProvider.GetGenderList().Select(item => new SelectListItem { Text = item.Value, Value = item.Key.ToString(), Selected = item.Key == model.Gender }).ToList();
                this.Pair2List<CustomerEntityEditViewModel, short, short>(CustomerEntityListProvider.GetCustomerTypeList(), p => p.CustomerType, model.CustomerType.ToString());
                ViewBag.GroupId = m_customerWorker.GetCustomerGroupList(1).Select(item => new SelectListItem { Text = item.Title, Value = item.Id.ToString(), Selected = item.Id == model.GroupId }).ToList();
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
        public ActionResult Edit(CustomerEntityEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_customerWorker.UpdateManagerCustomerEntityEdit(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            this.StatusList(model.Status);
            ViewBag.Gender = CustomerEntityListProvider.GetGenderList().Select(item => new SelectListItem { Text = item.Value, Value = item.Key.ToString(), Selected = item.Key == model.Gender }).ToList();
            this.Pair2List<CustomerEntityEditViewModel, short, short>(CustomerEntityListProvider.GetCustomerTypeList(), p => p.CustomerType, model.CustomerType.ToString());
            ViewBag.GroupId = m_customerWorker.GetCustomerGroupList(1).Select(item => new SelectListItem { Text = item.Title, Value = item.Id.ToString(), Selected = item.Id == model.GroupId }).ToList();
            return View(model);
        }


        #endregion

        #region Customer Groups

        //GET: Admin/CustomerManager/Groups
        public ActionResult Groups(int page = 1, short status = 1)
        {
            ViewBag.CurrentPage = page;
            this.StatusList(status);
            var records = m_customerWorker.GetCustomerGroupList(status);
            ViewBag.TotalPage = records.Count;
            var model = records.OrderBy(o => o.Title).Skip((page - 1) * AppConfig.PAGE_SIZE).Take(AppConfig.PAGE_SIZE);
            return View(model);
        }

        //GET: Admin/CustomerManager/CreateGroup
        public ActionResult CreateGroup()
        {
            List<SelectListItem> priceIndexes = new List<SelectListItem>();
            for (int i = 1; i <= 5; i++)
            {
                priceIndexes.Add(new SelectListItem { Text = i.ToString() + ". Fiyat", Value = i.ToString() });
            }
            ViewBag.PriceIndex = priceIndexes;
            var model = new CustomerGroupEditViewModel { Status = 1 };
            this.StatusList(model.Status);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(CustomerGroupEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_customerWorker.InsertCustomerGroupEdit(model);
                    return RedirectToAction("Groups");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            List<SelectListItem> priceIndexes = new List<SelectListItem>();
            for (int i = 1; i <= 5; i++)
            {
                priceIndexes.Add(new SelectListItem { Text = i.ToString() + ". Fiyat", Value = i.ToString() });
            }
            ViewBag.PriceIndex = priceIndexes;
            this.StatusList(model.Status);
            return View(model);
        }

        //GET: Admin/CustomerManager/EditGroup
        public ActionResult EditGroup(int id)
        {
            try
            {
                var model = m_customerWorker.GetManagerCustomerGroupEdit(id);
                List<SelectListItem> priceIndexes = new List<SelectListItem>();
                for (int i = 1; i <= 5; i++)
                {
                    priceIndexes.Add(new SelectListItem { Text = i.ToString() + ". Fiyat", Value = i.ToString(), Selected = model.PriceIndex == i });
                }
                ViewBag.PriceIndex = priceIndexes;
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
        public ActionResult EditGroup(CustomerGroupEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_customerWorker.UpdateManagerCustomerGroupEdit(model);
                    return RedirectToAction("Groups");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            List<SelectListItem> priceIndexes = new List<SelectListItem>();
            for (int i = 1; i <= 5; i++)
            {
                priceIndexes.Add(new SelectListItem { Text = i.ToString() + ". Fiyat", Value = i.ToString() });
            }
            ViewBag.PriceIndex = priceIndexes;
            this.StatusList(model.Status);
            return View(model);
        }

        //GET: Admin/CustomerManager/DeleteGroup
        public ActionResult DeleteGroup(int id)
        {
            try
            {
                m_customerWorker.DeleteManagerCustomerGroupEdit(id);
                return RedirectToAction("Groups");
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
                return null;
            }

        }

        #endregion


    }
}