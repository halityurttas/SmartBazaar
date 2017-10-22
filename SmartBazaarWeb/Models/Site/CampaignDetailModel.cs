using System;

namespace SmartBazaar.Web.Models.Site
{
    public class CampaignDetailViewModel
    {
        public int Id { get; set; }
        public short CampaignMethod { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public short DiscountMethod { get; set; }
        public double DiscountValue { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SlideUrl { get; set; }
        public int MaxUsage { get; set; }
    }
}