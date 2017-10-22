using SmartBazaar.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartBazaar.Web.Helpers.Contents
{
    public class CatalogHelper
    {
        public struct PriceDetail
        {
            public decimal OriginalPrice;
            public decimal Price;
            public double TaxRate;
            public int? CampaignId;
            public decimal OriginalPriceCalculated;
            public decimal PriceCalculated;
        }

        public static decimal GetPrice(Catalog_Products model)
        {
            short priceGroup = Business.Layers.CustomerLayer.Customer.PriceIndex;
            bool taxInclude = Business.Layers.SettingsLayer.SiteSetting.PriceIndcludeTax;

            decimal price = 0;
            switch (priceGroup)
            {
                case 2:
                    price = model.Price2 ?? model.Price1;
                    break;
                case 3:
                    price = model.Price3 ?? model.Price1;
                    break;
                case 4:
                    price = model.Price4 ?? model.Price1;
                    break;
                case 5:
                    price = model.Price5 ?? model.Price1;
                    break;
                default:
                    price = model.Price1;
                    break;
            }
            if (model.Catalog_Campaigns_Sources.Any())
            {

                var activeCampSources = model.Catalog_Campaigns_Sources.Where(w => w.Catalog_Campaigns.Status == 1 && w.Catalog_Campaigns.StartDate <= DateTime.Now && w.Catalog_Campaigns.EndDate > DateTime.Now).OrderByDescending(o => o.Catalog_Campaigns.StartDate);

                List<decimal> prices = new List<decimal>();

                foreach (var activeCampSource in activeCampSources)
                {
                    var camp = activeCampSource.Catalog_Campaigns;
                    if (camp.CampaignMethod == 1)
                    {
                        if (camp.DiscountMethod == 1)
                        {
                            prices.Add(price - (price * (decimal)(camp.DiscountValue / 100)));
                        }
                        else if (camp.DiscountMethod == 2)
                        {
                            prices.Add(price - (decimal)camp.DiscountValue);
                        }
                    }
                }
                prices.Sort();
                price = prices.FirstOrDefault();
            }
            if (taxInclude)
            {
                price += price * (decimal)(model.Tax_Products.Rate / 100);
            }
            return price;
        }

        public static PriceDetail GetPriceWithDetail(Catalog_Products model, int quantity)
        {
            short priceGroup = Business.Layers.CustomerLayer.Customer.PriceIndex;
            bool taxInclude = Business.Layers.SettingsLayer.SiteSetting.PriceIndcludeTax;

            decimal price = 0;
            int? campId = null;

            switch (priceGroup)
            {
                case 2:
                    price = model.Price2 ?? model.Price1;
                    break;
                case 3:
                    price = model.Price3 ?? model.Price1;
                    break;
                case 4:
                    price = model.Price4 ?? model.Price1;
                    break;
                case 5:
                    price = model.Price5 ?? model.Price1;
                    break;
                default:
                    price = model.Price1;
                    break;
            }
            decimal originalPrice = price;
            if (model.Catalog_Campaigns_Sources.Any())
            {

                var activeCampSources = model.Catalog_Campaigns_Sources.Where(w => w.Catalog_Campaigns.Status == 1 && w.Catalog_Campaigns.StartDate <= DateTime.Now && w.Catalog_Campaigns.EndDate > DateTime.Now).OrderByDescending(o => o.Catalog_Campaigns.StartDate);

                List<decimal> prices = new List<decimal>();
                Dictionary<decimal, int> pricePairs = new Dictionary<decimal, int>();

                foreach (var activeCampSource in activeCampSources)
                {
                    var camp = activeCampSource.Catalog_Campaigns;
                    if (camp.MaxUsage > 0 && model.Order_Lines.Where(w => w.Order_Heads.CustomerId == Business.Layers.CustomerLayer.Customer.Id && w.CampaignId == camp.Id).Sum(s => s.Quantity) + quantity > camp.MaxUsage)
                    {
                        continue;
                    }
                    if (camp.CampaignMethod == 1)
                    {
                        decimal tmpPrice = 0;
                        if (camp.DiscountMethod == 1)
                        {
                            tmpPrice = price - (price * (decimal)(camp.DiscountValue / 100));
                            prices.Add(tmpPrice);
                            pricePairs.Add(tmpPrice, camp.Id);
                        }
                        else if (camp.DiscountMethod == 2)
                        {
                            tmpPrice = price - (decimal)camp.DiscountValue;
                            prices.Add(tmpPrice);
                            pricePairs.Add(tmpPrice, camp.Id);
                        }
                    }
                }
                if (prices.Any())
                {
                    prices.Sort();
                    price = prices.FirstOrDefault();
                }
                if (pricePairs.Keys.Contains(price))
                {
                    campId = pricePairs[price];
                }
            }
            var priceDetail = new PriceDetail
            {
                CampaignId = campId,
                Price = price,
                TaxRate = model.Tax_Products.Rate,
                OriginalPrice = originalPrice,
                PriceCalculated = taxInclude ? price * (decimal)((model.Tax_Products.Rate+100)/100) : price,
                OriginalPriceCalculated = taxInclude ? originalPrice * (decimal)((model.Tax_Products.Rate + 100) / 100) : originalPrice
            };
            return priceDetail;
        }

        public static PriceDetail GetPriceWithoutCampaign(Catalog_Products model)
        {
            short priceGroup = Business.Layers.CustomerLayer.Customer.PriceIndex;
            bool taxInclude = Business.Layers.SettingsLayer.SiteSetting.PriceIndcludeTax;

            decimal price = 0;

            switch (priceGroup)
            {
                case 2:
                    price = model.Price2 ?? model.Price1;
                    break;
                case 3:
                    price = model.Price3 ?? model.Price1;
                    break;
                case 4:
                    price = model.Price4 ?? model.Price1;
                    break;
                case 5:
                    price = model.Price5 ?? model.Price1;
                    break;
                default:
                    price = model.Price1;
                    break;
            }
            return new PriceDetail
            {
                OriginalPrice = price,
                OriginalPriceCalculated = taxInclude ? price * (decimal)((model.Tax_Products.Rate / 100) + 1) : price,
                Price = price,
                PriceCalculated = taxInclude ? price * (decimal)((model.Tax_Products.Rate / 100) + 1) : price,
                TaxRate = model.Tax_Products.Rate
            };
        }

        public static PriceDetail GetRelatedPriceWithDetail(Catalog_Products model, int sourceItemId)
        {
            short priceGroup = Business.Layers.CustomerLayer.Customer.PriceIndex;
            bool taxInclude = Business.Layers.SettingsLayer.SiteSetting.PriceIndcludeTax;

            decimal price = 0;
            int? campId = null;

            switch (priceGroup)
            {
                case 2:
                    price = model.Price2 ?? model.Price1;
                    break;
                case 3:
                    price = model.Price3 ?? model.Price1;
                    break;
                case 4:
                    price = model.Price4 ?? model.Price1;
                    break;
                case 5:
                    price = model.Price5 ?? model.Price1;
                    break;
                default:
                    price = model.Price1;
                    break;
            }
            decimal originalPrice = price;
            List<decimal> prices = new List<decimal>();
            Dictionary<decimal, int> pricePairs = new Dictionary<decimal, int>();

            var activeCampSources = model.Catalog_Campaigns_Destinations.Where(w => w.Catalog_Campaigns.Catalog_Campaigns_Sources.Any(a => a.ProductId == sourceItemId));

            foreach (var activeCampSource in activeCampSources)
            {
                var camp = activeCampSource.Catalog_Campaigns;
                if (camp.CampaignMethod == 2)
                {
                    decimal tmpPrice = 0;
                    if (camp.DiscountMethod == 1)
                    {
                        tmpPrice = price - (price * (decimal)(camp.DiscountValue / 100));
                        prices.Add(tmpPrice);
                        pricePairs.Add(tmpPrice, camp.Id);
                    }
                    else if (camp.DiscountMethod == 2)
                    {
                        tmpPrice = price - (decimal)camp.DiscountValue;
                        prices.Add(tmpPrice);
                        pricePairs.Add(tmpPrice, camp.Id);
                    }
                }
            }

            if (prices.Any())
            {
                prices.Sort();
                price = prices.FirstOrDefault();
            }
            if (pricePairs.Keys.Contains(price))
            {
                campId = pricePairs[price];
            }
            var priceDetail = new PriceDetail
            {
                CampaignId = campId,
                Price = price,
                TaxRate = model.Tax_Products.Rate,
                OriginalPrice = originalPrice,
                PriceCalculated = taxInclude ? price * (decimal)((model.Tax_Products.Rate + 100) / 100) : price,
                OriginalPriceCalculated = taxInclude ? originalPrice * (decimal)((model.Tax_Products.Rate + 100) / 100) : originalPrice
            };
            return priceDetail;

        }

        public static PriceDetail GetRelatedPriceWithDetailByCampaign(Catalog_Products model, int campignId)
        {
            short priceGroup = Business.Layers.CustomerLayer.Customer.PriceIndex;
            bool taxInclude = Business.Layers.SettingsLayer.SiteSetting.PriceIndcludeTax;

            decimal price = 0;
            int? campId = campignId;

            switch (priceGroup)
            {
                case 2:
                    price = model.Price2 ?? model.Price1;
                    break;
                case 3:
                    price = model.Price3 ?? model.Price1;
                    break;
                case 4:
                    price = model.Price4 ?? model.Price1;
                    break;
                case 5:
                    price = model.Price5 ?? model.Price1;
                    break;
                default:
                    price = model.Price1;
                    break;
            }
            decimal originalPrice = price;
            List<decimal> prices = new List<decimal>();
            Dictionary<decimal, int> pricePairs = new Dictionary<decimal, int>();

            var activeCampSources = model.Catalog_Campaigns_Destinations.Where(w => w.Catalog_Campaigns.Id == campignId);

            foreach (var activeCampSource in activeCampSources)
            {
                var camp = activeCampSource.Catalog_Campaigns;
                if (camp.CampaignMethod == 2)
                {
                    decimal tmpPrice = 0;
                    if (camp.DiscountMethod == 1)
                    {
                        tmpPrice = price - (price * (decimal)(camp.DiscountValue / 100));
                        prices.Add(tmpPrice);
                        pricePairs.Add(tmpPrice, camp.Id);
                    }
                    else if (camp.DiscountMethod == 2)
                    {
                        tmpPrice = price - (decimal)camp.DiscountValue;
                        prices.Add(tmpPrice);
                        pricePairs.Add(tmpPrice, camp.Id);
                    }
                }
            }

            if (prices.Any())
            {
                prices.Sort();
                price = prices.FirstOrDefault();
            }
            if (pricePairs.Keys.Contains(price))
            {
                campId = pricePairs[price];
            }
            var priceDetail = new PriceDetail
            {
                CampaignId = campId,
                Price = price,
                TaxRate = model.Tax_Products.Rate,
                OriginalPrice = originalPrice,
                PriceCalculated = taxInclude ? price * (decimal)((model.Tax_Products.Rate + 100) / 100) : price,
                OriginalPriceCalculated = taxInclude ? originalPrice * (decimal)((model.Tax_Products.Rate + 100) / 100) : originalPrice
            };
            return priceDetail;

        }

    }
}