using SmartBazaar.Web.Business.Workers;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;

namespace SmartBazaar.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly CatalogWorker m_catalogWorker;
        public ProductController()
        {
            m_catalogWorker = new CatalogWorker();
        }

        // GET: Product
        public ActionResult Index(string q, int? id, string url, int page = 1)
        {
            ViewBag.q = q;
            ViewBag.id = id;
            ViewBag.url = url;
            ViewBag.CurrentPage = page;
            var records = m_catalogWorker.Search(q, category: id);
            ViewBag.TotalPage = records.Count;
            var model = records.OrderBy(o => o.Id).Skip((page - 1) * AppConfig.PAGE_SIZE).Take(AppConfig.PAGE_SIZE).ToList();
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var paymentWorker = new PaymentWorker();

            var model = m_catalogWorker.GetSiteProductDetail(id);
            model.PaymentTypes = paymentWorker.GetSitePaymentTypes();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddComment(Models.Site.CommentsPostViewModel model)
        {
            m_catalogWorker.InsertSiteComments(model, User.Identity.GetUserId());
            return Content("Ok");
        }

        public ActionResult JSearch(string q)
        {
            var records = m_catalogWorker.Search(q);
            ViewBag.TotalPage = records.Count;
            var model = records.OrderBy(o => o.Id).Take(AppConfig.JSON_SEARCH_RESULT_COUNT).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult PartialCategoryDetail(int? id, string viewpage = "PartialCategoryDetail")
        {
            if (id.HasValue)
            {
                var model = m_catalogWorker.GetSiteCatalogCategoryDetail(id.Value);
                return PartialView(viewpage, model);
            }
            return null;
        }

        [ChildActionOnly]
        public ActionResult Featured(string search = null, int? category = null, int take = 4, string view = "Featured")
        {
            var catalogWorker = new CatalogWorker();
            if (category.HasValue)
            {
                var categoryDetail = catalogWorker.GetSiteCatalogCategoryDetail(category.Value);
                if (categoryDetail != null)
                {
                    ControllerContext.ParentActionViewContext.ViewData["Featured_Category_" + category.Value.ToString()] = categoryDetail.ImageUrl;
                }
            }
            var model = catalogWorker.Search(search, isfeatured: true, category: category).Take(take).ToList();
            return PartialView(view, model);
        }

        [ChildActionOnly]
        public ActionResult NewProducts(string search = null, int? category = null, int take = 4, string view = "NewProducts")
        {
            var catalogWorker = new CatalogWorker();
            if (category.HasValue)
            {
                var categoryDetail = catalogWorker.GetSiteCatalogCategoryDetail(category.Value);
                ControllerContext.ParentActionViewContext.ViewData["NewProducts_Category_" + category.Value.ToString()] = categoryDetail.ImageUrl;
            }
            var model = catalogWorker.Search(search, isnew: true, category: category).Take(take).ToList();
            return PartialView(view, model);
        }

        [ChildActionOnly]
        public ActionResult HomepageProducts(string search = null, int? category = null, int take = 4, string view = "HomepageProducts")
        {
            var catalogWorker = new CatalogWorker();
            var model = catalogWorker.Search(search, ismain: true, category: category).Take(take).ToList();
            return PartialView(view, model);
        }

        [ChildActionOnly]
        public ActionResult CategoryList(int? parent, int depth, bool usedisplay = false, string view = "CategoryList")
        {
            var catalogWorker = new CatalogWorker();
            var model = catalogWorker.GetMenus(parent, depth, usedisplay);
            return PartialView(view, model);
        }

        [ChildActionOnly]
        public ActionResult HomeSingleProduct(string view = "HomeSingleProduct")
        {
            var paramWorker = new ParamWorker();
            var catalogWorker = new CatalogWorker();
            string param = paramWorker.GetParamValue(int.Parse(ConfigurationManager.AppSettings["HomeSingleProduct"]));
            var model = catalogWorker.Search(param);
            return PartialView(view, model);
        }
    }
}