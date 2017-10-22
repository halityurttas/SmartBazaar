namespace SmartBazaar.Web.Models.Site
{
    public class ShipmentTypeViewModel
    {
        public int Id { get; set; }
        public short PricingMethod { get; set; }
        public double PricingValue { get; set; }
        public string Title { get; set; }
        public short Status { get; set; }
        public decimal Price { get; set; }
    }
}