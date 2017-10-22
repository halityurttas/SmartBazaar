using System.Web.Mvc;
using SmartBazaar.Web.Business.Layers;

namespace SmartBazaar.Web.Controllers
{
    public class BasketController : Controller
    {
        // GET: Basket
        public ActionResult Index()
        {
            var model = BasketLayer.GetInstance();
            var summary = BasketLayer.GetSummary();
            ViewBag.Summary = summary;
            return View(model);
        }

        //GET: UpdateQuantity
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            BasketLayer.UpdateQuantity(id, quantity);
            return RedirectToAction("Index");
        }

        //GET: UpdateRelQuantity
        public ActionResult UpdateRelQuantity(int id, int quantity)
        {
            BasketLayer.UpdateRelQuantity(id, quantity);
            return RedirectToAction("Index");
        }

        //GET: RemoveItem
        public ActionResult RemoveProduct(int id)
        {
            BasketLayer.RemoveBasketItem(id);
            return RedirectToAction("Index");
        }

        //GET: RemoveRelItem
        public ActionResult RemoveRelProduct(int id)
        {
            BasketLayer.RemoveBasketRelItem(id);
            return RedirectToAction("Index");
        }

        //GET: Clear
        public ActionResult ClearBasket()
        {
            BasketLayer.RemoveAll();
            return RedirectToAction("Index");
        }

        
        //Json apies
        public ActionResult AddItem(int ProductId, int Quantity)
        {
            var result = BasketLayer.AddItem(ProductId, Quantity);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddRelatedItem(int ProductId, int RelatedProductId, int Quantity, int CampaignId)
        {
            var result = BasketLayer.AddRelatedItem(ProductId, RelatedProductId, Quantity, CampaignId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBasketDetailApi()
        {
            var result = BasketLayer.GetInstance();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBasketSummaryApi()
        {
            var result = BasketLayer.GetSummary();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveItem(int id)
        {
            var result = BasketLayer.RemoveBasketItem(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveAll()
        {
            var result = BasketLayer.RemoveAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}