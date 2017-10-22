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
    public class CampaignManagerController : Controller
    {
        private readonly CatalogWorker m_catalogWorker;
        public CampaignManagerController()
        {
            m_catalogWorker = new CatalogWorker();
        }

        // GET: Admin/CampaignManager
        public ActionResult Index(int page = 1, short status = 1)
        {
            this.StatusList(status);
            ViewBag.CurrentPage = page;
            var records = m_catalogWorker.GetManagerCatalogCampaignList(status);
            ViewBag.TotalPage = records.Count;
            var model = records.OrderBy(o => o.Title).Skip((page - 1) * AppConfig.PAGE_SIZE).Take(AppConfig.PAGE_SIZE).ToList();

            return View(model);
        }

        //GET: Admin/CampaignManager/Create
        public ActionResult Create()
        {
            this.StatusList(1);
            this.Pair2List<CatalogCampaignsEditViewModel, short, short>(CatalogCampaignsListProvider.GetCapaignMethodList(), m => m.CampaignMethod, null);
            this.Pair2List<CatalogCampaignsEditViewModel, short, short>(CatalogCampaignsListProvider.GetDiscountMethodList(), m => m.DiscountMethod, null);
            var model = new CatalogCampaignsEditViewModel { Status = 1, StartDate = DateTime.Today, EndDate = DateTime.Today };
            model.Sources = new List<CatalogCampaignsSourcesViewModel>();
            model.Destinations = new List<CatalogCampaignsDestinationsViewModel>();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CatalogCampaignsEditViewModel model, FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_catalogWorker.InsertManagerCatalogCampaign(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            this.StatusList(model.Status);
            this.Pair2List<CatalogCampaignsEditViewModel, short, short>(CatalogCampaignsListProvider.GetCapaignMethodList(), m => m.CampaignMethod, model.CampaignMethod.ToString());
            this.Pair2List<CatalogCampaignsEditViewModel, short, short>(CatalogCampaignsListProvider.GetDiscountMethodList(), m => m.DiscountMethod, model.DiscountMethod.ToString());
            var sources = model.Sources.Select(s => new CatalogCampaignsSourceTransformViewModel { Id = s.ProductId, Text = s.ProductName }).ToList();
            var destinations = model.Destinations.Select(s => new CatalogCampaignsSourceTransformViewModel { Id = s.ProductId, Text = s.ProductName }).ToList();
            ViewBag.SourceProds = Newtonsoft.Json.JsonConvert.SerializeObject(sources);
            ViewBag.DestinationsProds = Newtonsoft.Json.JsonConvert.SerializeObject(destinations);
            return View(model);
        }

        //GET: Admin/CampaignManager/Edit
        public ActionResult Edit(int id)
        {
            var model = m_catalogWorker.GetManagerCatalogCampaignsEdit(id);
            this.StatusList(model.Status);
            this.Pair2List<CatalogCampaignsEditViewModel, short, short>(CatalogCampaignsListProvider.GetCapaignMethodList(), m => m.CampaignMethod, model.CampaignMethod.ToString());
            this.Pair2List<CatalogCampaignsEditViewModel, short, short>(CatalogCampaignsListProvider.GetDiscountMethodList(), m => m.DiscountMethod, model.DiscountMethod.ToString());
            var sources = model.Sources.Select(s => new CatalogCampaignsSourceTransformViewModel { Id = s.ProductId, Text = s.ProductName }).ToList();
            int[] sourceIds = sources.Select(s => s.Id).ToArray();
            var destinations = model.Destinations.Select(s => new CatalogCampaignsSourceTransformViewModel { Id = s.ProductId, Text = s.ProductName }).ToList();
            int[] destIds = destinations.Select(s => s.Id).ToArray();
            ViewBag.SourceProds = Newtonsoft.Json.JsonConvert.SerializeObject(sources);
            ViewBag.DestinationsProds = Newtonsoft.Json.JsonConvert.SerializeObject(destinations);
            ViewBag.SourceIds = Newtonsoft.Json.JsonConvert.SerializeObject(sourceIds);
            ViewBag.DestinationIds = Newtonsoft.Json.JsonConvert.SerializeObject(destIds);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CatalogCampaignsEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_catalogWorker.UpdateManagerCatalogCampaignsEdit(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            this.StatusList(model.Status);
            this.Pair2List<CatalogCampaignsEditViewModel, short, short>(CatalogCampaignsListProvider.GetCapaignMethodList(), m => m.CampaignMethod, model.CampaignMethod.ToString());
            this.Pair2List<CatalogCampaignsEditViewModel, short, short>(CatalogCampaignsListProvider.GetDiscountMethodList(), m => m.DiscountMethod, model.DiscountMethod.ToString());
            var sources = model.Sources.Select(s => new CatalogCampaignsSourceTransformViewModel { Id = s.ProductId, Text = s.ProductName }).ToList();
            int[] sourceIds = sources.Select(s => s.Id).ToArray();
            var destinations = model.Destinations.Select(s => new CatalogCampaignsSourceTransformViewModel { Id = s.ProductId, Text = s.ProductName }).ToList();
            int[] destIds = destinations.Select(s => s.Id).ToArray();
            ViewBag.SourceProds = Newtonsoft.Json.JsonConvert.SerializeObject(sources);
            ViewBag.DestinationsProds = Newtonsoft.Json.JsonConvert.SerializeObject(destinations);
            ViewBag.SourceIds = Newtonsoft.Json.JsonConvert.SerializeObject(sourceIds);
            ViewBag.DestinationIds = Newtonsoft.Json.JsonConvert.SerializeObject(destIds);
            return View(model);
        }

        //GET: Admin/CampaignManager/Delete
        public ActionResult Delete(int id)
        {
            try
            {
                m_catalogWorker.DeleteManagerCatalogCampaigns(id);
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