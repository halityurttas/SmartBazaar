using SmartBazaar.Web.Business.Workers;
using SmartBazaar.Web.Helpers.Contents;
using SmartBazaar.Web.Models.Internal;
using SmartBazaar.Web.Models.Site;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Business.Layers
{
    public class BasketLayer : List<BasketModel>
    {
        public void Sync()
        {
            HttpContext.Current.Session["Basket"] = this;
        }

        public static BasketLayer GetInstance()
        {
            BasketLayer bl = HttpContext.Current.Session["Basket"] as BasketLayer;
            if (bl == null)
            {
                bl = new BasketLayer();
            }
            return bl;
        }

        public static BasketResultModel AddItem(int id, int quantity) {
            BasketLayer basketLayer = GetInstance();
            CatalogWorker catalogWorker = new CatalogWorker();
            var product = catalogWorker.GetLayerCatalogProductsItem(id);
            if (product == null)
            {
                var result = new BasketResultModel
                {
                    Status = false,
                    Message = "Ürün bulunamadı!"
                };
                return result;
            }
            if (SettingsLayer.SiteSetting.WorkingStock)
            {
                int currentCount = basketLayer.Where(w => w.ProductId == id).Sum(s => s.Quantity);
                if (product.Stock - currentCount < quantity)
                {
                    var result = new BasketResultModel
                    {
                        Status = false,
                        Message = "Stok miktarını aştınız!"
                    };
                    return result;
                }
            }
            if (!product.IsBuyable)
            {
                var result = new BasketResultModel
                {
                    Status = false,
                    Message = "Bu ürün satın alınamaz!"
                };
                return result;
            }
            var price = CatalogHelper.GetPriceWithDetail(product, quantity);
            if (basketLayer.Any(a => a.ProductId == id && a.RelatedId == null))
            {
                var basketItem = basketLayer.FirstOrDefault(f => f.ProductId == id && f.RelatedId == null);
                basketItem.Quantity += quantity;
            }
            else
            {
                var basketItem = new BasketModel();
                if (price.CampaignId.HasValue)
                {
                    basketItem.CampaignId = price.CampaignId.Value;
                }
                basketItem.ProductId = id;
                basketItem.ProductImage = product.ImageUrl;
                basketItem.ProductName = product.Title;
                basketItem.Quantity = quantity;
                basketItem.TaxRate = product.Tax_Products.Rate;
                basketItem.Price = price.Price;
                basketItem.Tare = product.IsShipable && !product.IsFreeShip ? product.Tare : 0;
                basketLayer.Add(basketItem);
            }
            basketLayer.Sync();
            var resultMsg = new BasketResultModel
            {
                Status = true,
                Message = "Ürün sepete eklenmiştir."
            };
            return resultMsg;
        }

        public static BasketResultModel UpdateQuantity(int id, int quantity)
        {
            BasketLayer basketLayer = GetInstance();
            if (basketLayer.Any(a => a.ProductId == id && a.RelatedId == null))
            {
                if (quantity == 0)
                {
                    RemoveBasketItem(id);
                    return new BasketResultModel
                    {
                        Status = true,
                        Message = "Ürün sepetten çıkarıldı"
                    };
                }
                var item = basketLayer.FirstOrDefault(f => f.ProductId == id && f.RelatedId == null);
                item.Quantity = quantity;
                basketLayer.Sync();
                return new BasketResultModel
                {
                    Status = true,
                    Message = "Miktar değiştirildi"
                };
            }
            else
            {
                return new BasketResultModel
                {
                    Status = false,
                    Message = "Ürün sepette bulunamadı"
                };
            }
        }

        public static BasketResultModel UpdateRelQuantity(int id, int quantity)
        {
            BasketLayer basketLayer = GetInstance();
            if (basketLayer.Any(a => a.ProductId == id && basketLayer.Any(a2 => a2.CampaignId == a.CampaignId)))
            {
                if (quantity == 0)
                {
                    
                }
                var item = basketLayer.FirstOrDefault(f => f.ProductId == id && f.RelatedId == 0);
                item.Quantity = quantity;
                var relitem = basketLayer.FirstOrDefault(f => f.RelatedId == item.ProductId && f.CampaignId == item.CampaignId);
                relitem.Quantity = item.Quantity;
                basketLayer.Sync();
            }
            return new BasketResultModel
            {
                Status = true,
                Message = "Miktar değiştirildi"
            };
        }

        public static BasketResultModel AddRelatedItem(int id, int relid, int quantity, int campaignid)
        {
            BasketLayer basketLayer = GetInstance();
            CatalogWorker catalogWorker = new CatalogWorker();
            var product = catalogWorker.GetLayerCatalogProductsItem(id);
            if (product == null)
            {
                var result = new BasketResultModel
                {
                    Status = false,
                    Message = "Ürün bulunamadı!"
                };
                return result;
            }
            var relproduct = catalogWorker.GetLayerCatalogProductsItem(relid);
            if (relproduct == null)
            {
                var result = new BasketResultModel
                {
                    Status = false,
                    Message = "Ürün bulunamadı!"
                };
                return result;
            }
            if (
                product.Catalog_Campaigns_Sources.FirstOrDefault(f => f.CampaignId == campaignid).Catalog_Campaigns.MaxUsage > 0
                && product.Order_Lines.Where(w => w.CampaignId == campaignid && w.Order_Heads.CustomerId == CustomerLayer.Customer.Id).Sum(s => s.Quantity) + quantity > product.Catalog_Campaigns_Sources.FirstOrDefault(f => f.CampaignId == campaignid).Catalog_Campaigns.MaxUsage
                )
            {
                var result = new BasketResultModel
                {
                    Status = false,
                    Message = "Maksimum kampanya kullanım limitini aştınız!"
                };
                return result;
            }
            if (basketLayer.Any(a => a.ProductId == id && a.CampaignId == campaignid))
            {
                var item = basketLayer.FirstOrDefault(f => f.ProductId == id && f.CampaignId == campaignid);
                item.Quantity += quantity;
                var itemRel = basketLayer.FirstOrDefault(f => f.ProductId == relid && f.RelatedId == id && f.CampaignId == campaignid);
                itemRel.Quantity = item.Quantity;
            }
            else
            {
                var item = new BasketModel();
                item.CampaignId = campaignid;
                var price = CatalogHelper.GetPriceWithDetail(product, quantity);
                item.Price = price.Price;
                item.Quantity = quantity;
                item.TaxRate = product.Tax_Products.Rate;
                item.ProductId = product.Id;
                item.ProductName = product.Title;
                item.ProductImage = product.ImageUrl;
                item.RelatedId = 0;
                basketLayer.Add(item);

                var relprice = CatalogHelper.GetRelatedPriceWithDetail(relproduct, id);
                var relitem = new BasketModel();
                relitem.CampaignId = campaignid;
                relitem.Price = relprice.Price;
                relitem.ProductId = relproduct.Id;
                relitem.ProductImage = relproduct.ImageUrl;
                relitem.ProductName = relproduct.Title;
                relitem.Quantity = quantity;
                relitem.RelatedId = id;
                relitem.TaxRate = relprice.TaxRate;
                basketLayer.Add(relitem);
            }
            basketLayer.Sync();
            var resultMsg = new BasketResultModel
            {
                Status = true,
                Message = "Ürünler sepete eklendi"
            };
            return resultMsg;
        }

        public static BasketSummaryViewModel GetSummary()
        {
            var summary = new BasketSummaryViewModel();
            var basket = GetInstance();
            summary.BasketCount = basket.Count;
            summary.BasketPriceTotal = basket.Sum(s => s.TotalPrice);
            summary.BasketTaxTotal = basket.Sum(s => s.TotalTax);
            summary.BasketTotal = basket.Sum(s => s.Total);
            summary.ProductCount = basket.Sum(s => s.Quantity);
            summary.TareTotal = basket.Sum(s => s.Tare);
            return summary;
        }

        public static BasketResultModel RemoveBasketItem(int id)
        {
            var basket = GetInstance();
            var item = basket.FirstOrDefault(f => f.ProductId == id && f.RelatedId == null);
            if (item == null)
            {
                return new BasketResultModel
                {
                    Status = false,
                    Message = "Sepette bu ürün yok!"
                };
            }
            basket.Remove(item);
            return new BasketResultModel
            {
                Status = true,
                Message = "Ürün sepetten çıkarıldı"
            };
        }

        public static BasketResultModel RemoveBasketRelItem(int id)
        {
            var basket = GetInstance();
            var item = basket.FirstOrDefault(f => f.ProductId == id && f.RelatedId != null);
            if (item == null)
            {
                return new BasketResultModel
                {
                    Status = false,
                    Message = "Sepette bu ürün yok!"
                };
            }
            var relitem = basket.FirstOrDefault(f => f.RelatedId == item.ProductId && f.CampaignId == item.CampaignId);
            basket.Remove(item);
            basket.Remove(relitem);
            basket.Sync();
            return new BasketResultModel
            {
                Status = true,
                Message = "Ürün sepetten çıkarıldı"
            };
        }

        public static BasketResultModel RemoveAll()
        {
            var basket = GetInstance();
            basket.Clear();
            basket.Sync();
            return new BasketResultModel
            {
                Status = true,
                Message = "Sepet temizlendi"
            };
        }

    }
}