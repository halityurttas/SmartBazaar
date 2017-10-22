using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Site
{
    public class ProductPivotsViewModel
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public short Side { get; set; }
        public SmartBazaar.Web.Helpers.Contents.CatalogHelper.PriceDetail Price { get; set; }
        public SmartBazaar.Web.Helpers.Contents.CatalogHelper.PriceDetail RelPrice { get; set; }
    }
}