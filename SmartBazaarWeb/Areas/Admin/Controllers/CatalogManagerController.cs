using Newtonsoft.Json;
using SmartBazaar.Web.Areas.Admin.Models;
using SmartBazaar.Web.Business.Workers;
using SmartBazaar.Web.Helpers.Excel;
using SmartBazaar.Web.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartBazaar.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    public class CatalogManagerController : Controller
    {
        private readonly CatalogWorker m_catalogWorker;
        public CatalogManagerController()
        {
            m_catalogWorker = new CatalogWorker();
        }

        private TaxWorker GetTaxWorkerInstance()
        {
            return new TaxWorker();
        }

        #region Products

        // GET: Admin/CatalogManager
        
        public ActionResult Index(int page = 1, short status = 1, string ProductCode = null, string Caption = null, int? Category = null, int? Brand = null, int StockMin = int.MinValue, int StockMax = int.MaxValue, string export = null)
        {
            if (export == "excel")
            {
                return ExportExcel(status, ProductCode, Caption, Category, Brand, StockMin, StockMax);
            }
            ProductCode = string.IsNullOrWhiteSpace(ProductCode) ? null : ProductCode;
            Caption = string.IsNullOrWhiteSpace(Caption) ? null : Caption;

            ViewBag.Caption = Caption;
            ViewBag.ProductCode = ProductCode;
            ViewBag.StockMin = StockMin == int.MinValue ? (int?)null : StockMin;
            ViewBag.StockMax = StockMax == int.MaxValue ? (int?)null : StockMax;

            this.StatusList(status);
            ViewBag.Category = m_catalogWorker.GetCategorySelectLists();
            
            var brandList = m_catalogWorker.GetManagerCatalogBrandList(1);
            var brandSelectLists = brandList.Select(item => new SelectListItem { Text = item.Title, Value = item.Id.ToString()}).ToList();
            ViewBag.Brand = brandSelectLists;

            ViewBag.CurrentPage = page;

            int[] catTree = { };
            if (Category.HasValue)
            {
                catTree = m_catalogWorker.GetCategorySubsTree(Category.Value);
            }

            var records = m_catalogWorker.GetManagerCatalogProductList( p => 
                    p.Status == status
                    && (ProductCode == null || p.ProductCode == ProductCode)
                    && (Caption == null || p.Title.Contains(Caption))
                    && (!Category.HasValue || catTree.Contains(p.CategoryId))
                    && (!Brand.HasValue || p.BrandId == Brand.Value)
                    && (p.Stock > StockMin && p.Stock < StockMax)
                );
            ViewBag.TotalPage = records.Count;
            var model = records.OrderBy(o => o.Title).Skip((page - 1) * AppConfig.PAGE_SIZE).Take(AppConfig.PAGE_SIZE).ToList();
            return View(model);
        }

        //GET: Admin/CatalogManager/Create
        public ActionResult Create()
        {
            var brandList = m_catalogWorker.GetManagerCatalogBrandList(1);
            var brandSelectLists = brandList.Select(item => new SelectListItem { Text = item.Title, Value = item.Id.ToString() }).ToList();
            brandSelectLists.Insert(0, new SelectListItem { Text = "-", Value = "" });
            ViewBag.BrandId = brandSelectLists;
            var taxList = GetTaxWorkerInstance().GetManagerProductTaxList(1);
            ViewBag.TaxGroup = taxList.Select(item => new SelectListItem { Text = item.Title, Value = item.Id.ToString() }).ToList();
            ViewBag.CategoryTree = m_catalogWorker.GetCategoryTree();
            this.StatusList(1);
            var model = new CatalogProductEditViewModel { 
                Status = 1,
                IsShipable = true,
                IsBuyable = true
            };
            ViewBag.Relations = JsonConvert.SerializeObject(model.Relations);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CatalogProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_catalogWorker.InsertManagerCatalogProduct(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Generic", ex.Message);
                }
            }
            var brandList = m_catalogWorker.GetManagerCatalogBrandList(1);
            var brandSelectLists = brandList.Select(item => new SelectListItem { Text = item.Title, Value = item.Id.ToString(), Selected = item.Id == model.BrandId }).ToList();
            brandSelectLists.Insert(0, new SelectListItem { Text = "-", Value = "" });
            ViewBag.BrandId = brandSelectLists;
            var taxList = GetTaxWorkerInstance().GetManagerProductTaxList(1);
            ViewBag.TaxGroup = taxList.Select(item => new SelectListItem { Text = item.Title, Value = item.Id.ToString(), Selected = item.Id == model.TaxGroup }).ToList();
            ViewBag.CategoryTree = m_catalogWorker.GetCategoryTree();
            this.StatusList(model.Status);
            ViewBag.PropList = Newtonsoft.Json.JsonConvert.SerializeObject(model.Properties);
            ViewBag.Relations = JsonConvert.SerializeObject(model.Relations);
            return View(model);
        }

        //GET: Admin/CatalogManager/Edit
        public ActionResult Edit(int id)
        {
            try
            {
                var model = m_catalogWorker.GetManagerCatalogProductEdit(id);
                var brandList = m_catalogWorker.GetManagerCatalogBrandList(1);
                var brandSelectLists = brandList.Select(item => new SelectListItem { Text = item.Title, Value = item.Id.ToString(), Selected = item.Id == model.BrandId }).ToList();
                brandSelectLists.Insert(0, new SelectListItem { Value = "", Text = "-" });
                ViewBag.BrandId = brandSelectLists;
                var taxList = GetTaxWorkerInstance().GetManagerProductTaxList(1);
                ViewBag.TaxGroup = taxList.Select(item => new SelectListItem { Text = item.Title, Value = item.Id.ToString(), Selected = item.Id == model.TaxGroup }).ToList();
                ViewBag.CategoryTree = m_catalogWorker.GetCategoryTree();
                ViewBag.PropList = Newtonsoft.Json.JsonConvert.SerializeObject(model.Properties);
                ViewBag.ImgList = Newtonsoft.Json.JsonConvert.SerializeObject(model.Images);
                ViewBag.Relations = JsonConvert.SerializeObject(AutoMapper.Mapper.Map<List<Models.CatalogProductsRelationsEditViewModel>, List<Models.CatalogProductsRelationsJSONModel>>(model.Relations));
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
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CatalogProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_catalogWorker.UpdateManagerCatalogProductEdit(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            var brandList = m_catalogWorker.GetManagerCatalogBrandList(1);
            var brandSelectLists = brandList.Select(item => new SelectListItem { Text = item.Title, Value = item.Id.ToString(), Selected = item.Id == model.BrandId }).ToList();
            brandSelectLists.Insert(0, new SelectListItem { Text = "-" });
            ViewBag.BrandId = brandSelectLists;
            var taxList = GetTaxWorkerInstance().GetManagerProductTaxList(1);
            ViewBag.TaxGroup = taxList.Select(item => new SelectListItem { Text = item.Title, Value = item.Id.ToString(), Selected = item.Id == model.TaxGroup }).ToList();
            ViewBag.CategoryTree = m_catalogWorker.GetCategoryTree();
            ViewBag.PropList = Newtonsoft.Json.JsonConvert.SerializeObject(model.Properties);
            ViewBag.Relations = JsonConvert.SerializeObject(AutoMapper.Mapper.Map<List<Models.CatalogProductsRelationsEditViewModel>, List<Models.CatalogProductsRelationsJSONModel>>(model.Relations));
            this.StatusList(model.Status);
            return View(model);
        }

        //GET: Admin/CatalogManager/Delete
        public ActionResult Delete(int id)
        {
            try
            {
                m_catalogWorker.DeleteManagerCatalogProductEdit(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
                return null;
            }
        }

        public ActionResult MultiProcess()
        {
            var categoryList = m_catalogWorker.GetCategorySelectLists();
            categoryList.Insert(0, new SelectListItem { Text = "Tümü", Value = "" });
            ViewBag.category = categoryList;
            var brandList = m_catalogWorker.GetManagerCatalogBrandList(1);
            var brandSelectLists = brandList.Select(item => new SelectListItem { Text = item.Title, Value = item.Id.ToString() }).ToList();
            brandSelectLists.Insert(0, new SelectListItem { Text = "Tümü", Value = "" });
            ViewBag.brand = brandSelectLists;

            return View();
        }

        [HttpPost]
        public ActionResult MultiProcess(FormCollection frm)
        {
            short? status = string.IsNullOrWhiteSpace(frm["status"]) ? (short?)null : short.Parse(frm["status"]);
            string title = frm["title"];
            int? category = string.IsNullOrWhiteSpace(frm["category"]) ? (int?)null : int.Parse(frm["category"]);
            int? brand = string.IsNullOrWhiteSpace(frm["brand"]) ? (int?)null : int.Parse(frm["brand"]);
            int minstock = string.IsNullOrWhiteSpace(frm["minstock"]) ? int.MinValue : int.Parse(frm["minstock"]);
            int maxstock = string.IsNullOrWhiteSpace(frm["maxstock"]) ? int.MaxValue : int.Parse(frm["maxstock"]);
            decimal? price1rate = string.IsNullOrWhiteSpace(frm["price1rate"]) ? (decimal?)null : decimal.Parse(frm["price1rate"]);
            decimal? price2rate = string.IsNullOrWhiteSpace(frm["price2rate"]) ? (decimal?)null : decimal.Parse(frm["price2rate"]);
            decimal? price3rate = string.IsNullOrWhiteSpace(frm["price3rate"]) ? (decimal?)null : decimal.Parse(frm["price3rate"]);
            decimal? price4rate = string.IsNullOrWhiteSpace(frm["price4rate"]) ? (decimal?)null : decimal.Parse(frm["price4rate"]);
            decimal? price5rate = string.IsNullOrWhiteSpace(frm["price5rate"]) ? (decimal?)null : decimal.Parse(frm["price5rate"]);
            int? stock = string.IsNullOrWhiteSpace(frm["stock"]) ? (int?)null : int.Parse(frm["stock"]);
            m_catalogWorker.MultiProductUpdate(status, title, category, brand, minstock, maxstock, price1rate, price2rate, price3rate, price4rate, price5rate, stock);
            return RedirectToAction("Index");
        }

        public ActionResult Comments(int page = 1, short status = 0)
        {
            this.StatusList(status);
            ViewBag.CurrentPage = page;
            var records = m_catalogWorker.GetManagerComments(status);
            ViewBag.TotalPage = records.Count;
            var model = records.OrderByDescending(o => o.Created).Skip((page - 1) * AppConfig.PAGE_SIZE).Take(AppConfig.PAGE_SIZE).ToList();
            return View(model);
        }

        public ActionResult CommentDetail(int id)
        {
            var model = m_catalogWorker.GetManagerCommentDetail(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CommentDetail(FormCollection frm)
        {
            if (frm["op"] == "Onayla")
            {
                m_catalogWorker.ApproveManagerCommentDetail(int.Parse(frm["id"]));
            }
            else if (frm["op"] == "Sil")
            {
                m_catalogWorker.DeleteManagerCommentDetail(int.Parse(frm["id"]));
            }
            return RedirectToAction("Comments");
        }

        #endregion

        #region Brand

        //GET: Admin/CatalogManager/Brands
        public ActionResult Brands(int page = 1, short status = 1)
        {
            this.StatusList(status);
            ViewBag.CurrentPage = page;
            var records = m_catalogWorker.GetManagerCatalogBrandList(status);
            ViewBag.TotalPage = records.Count;
            var model = records.OrderBy(o => o.Title).Skip((page - 1) * AppConfig.PAGE_SIZE).Take(AppConfig.PAGE_SIZE).ToList();
            return View(model);
        }

        //GET: Admin/CatalogManager/CreateBrand
        public ActionResult CreateBrand()
        {
            var model = new CatalogBrandEditViewModel { Status = 1 };
            this.StatusList(1);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBrand(CatalogBrandEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_catalogWorker.InsertManagerCatalogBrandEdit(model);
                    return RedirectToAction("Brands");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            this.StatusList(model.Status);
            return View(model);
        }

        //GET: Admin/CatagoryManager/EditBrand
        public ActionResult EditBrand(int id)
        {
            var model = m_catalogWorker.GetManagerCatalogBrandEdit(id);
            this.StatusList(model.Status);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBrand(CatalogBrandEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_catalogWorker.UpdateManagerCatalogBrandEdit(model);
                    return RedirectToAction("Brands");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            this.StatusList(model.Status);
            return View(model);
        }

        //GET: Admin/CatalogManager/DeleteBrand
        public ActionResult DeleteBrand(int id)
        {
            try
            {
                m_catalogWorker.DeleteManagerCatalogBrandEdit(id);
                return RedirectToAction("Brands");
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
                return null;
            }
        }

        #endregion

        #region Categories

        //GET: Admin/CatalogManager/Categories
        public ActionResult Categories(int page = 1, short status = 1)
        {
            this.StatusList(status);
            ViewBag.CurrentPage = page;
            var records = m_catalogWorker.GetManagerCatalogCategoryList(status);
            ViewBag.TotalPage = records.Count;
            var model = records.OrderBy(o => o.Caption).Skip((page - 1) * AppConfig.PAGE_SIZE).Take(AppConfig.PAGE_SIZE).ToList();
            return View(model);
        }

        //GET: Admin/CatalogManager/CreateCategory
        public ActionResult CreateCategory()
        {
            this.StatusList(1);
            var model = new CatalogCategoryEditViewModel { Status = 1, Pos = 1 };
            ViewBag.CategoryTree = m_catalogWorker.GetCategoryTree();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CatalogCategoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_catalogWorker.InsertManagerCatalogCategoryEdit(model);
                    return RedirectToAction("Categories");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            this.StatusList(1);
            ViewBag.CategoryTree = m_catalogWorker.GetCategoryTree();
            return View(model);
        }

        //GET: Admin/CatalogManager/EditCategory
        public ActionResult EditCategory(int id)
        {
            try
            {
                var model = m_catalogWorker.GetManagerCatalogCategoryEdit(id);
                this.StatusList(model.Status);
                ViewBag.CategoryTree = m_catalogWorker.GetCategoryTree();
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
        public ActionResult EditCategory(CatalogCategoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_catalogWorker.UpdateManagerCatalogCategoryEdit(model);
                    return RedirectToAction("Categories");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            this.StatusList(model.Status);
            ViewBag.CategoryTree = m_catalogWorker.GetCategoryTree();
            return View(model);
        }

        //GET: Admin/CatalogManager/DeleteCategory
        public ActionResult DeleteCategory(int id)
        {
            try
            {
                m_catalogWorker.DeleteManagerCatalogCategory(id);
                return RedirectToAction("Categories");
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
                return null;
            }
        }

        #endregion

        //JSON: Admin/CatalogManager/JIndex
        public ActionResult JIndex(string q)
        {
            var model = m_catalogWorker.FindManagerCatalogProductList(q);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportExcel(short status = 1, string ProductCode = null, string Caption = null, int? Category = null, int? Brand = null, int StockMin = int.MinValue, int StockMax = int.MaxValue)
        {
            List<CatalogProductsExcelModel> model = m_catalogWorker.GetExcelData(status, ProductCode, Caption, Category, Brand, StockMin, StockMax);
            try
            {
                MemoryStream ms = new MemoryStream();
                Stream output = (Stream)ms;
                ExcelSerializer.Serialize<CatalogProductsExcelModel>(model, ref output);
                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Urunler.xlsx");
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
                return null;
            }
        }

        public ActionResult ImportExcel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportExcel(HttpPostedFileBase excel)
        {
            try
            {
                var model = ExcelSerializer.Deserialize<CatalogProductsExcelModel>(excel.InputStream);
                var result = m_catalogWorker.SaveExcelData(model);
                if (result.Count > 0)
	            {
                    MemoryStream ms = new MemoryStream();
                    Stream output = (Stream)ms;
                    ExcelSerializer.Serialize<CatalogProductsExcelModel>(result, ref output);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BasarisizAktarimlar.xlsx");
	            }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }
    }
}