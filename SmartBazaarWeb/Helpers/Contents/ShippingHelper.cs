using SmartBazaar.Web.Business.Layers;
using SmartBazaar.Web.Models.Site;

namespace SmartBazaar.Web.Helpers.Contents
{
    public class ShippingHelper
    {
        public static decimal ShippingPrice(ShipmentTypeViewModel model)
        {
            if (model.PricingMethod == 2)
            {
                return BasketLayer.GetSummary().BasketTotal * (decimal)(model.PricingValue / 100);
            }
            else if (model.PricingMethod == 3)
            {
                return BasketLayer.GetSummary().ProductCount * (decimal)model.PricingValue;
            }
            else if (model.PricingMethod == 4)
            {
                return (decimal)BasketLayer.GetSummary().TareTotal * (decimal)model.PricingValue; 
            }
            else
            {
                return (decimal)model.PricingValue;
            }
        }
    }
}