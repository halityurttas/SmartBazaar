namespace SmartBazaar.Web.Models.Site
{
    public class BasketSummaryViewModel
    {
        public int ProductCount { get; set; }
        public int BasketCount { get; set; }
        public decimal BasketPriceTotal { get; set; }
        public decimal BasketTaxTotal { get; set; }
        public decimal BasketTotal { get; set; }
        public double TareTotal { get; set; }
    }
}