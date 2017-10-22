namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Catalog_Campaigns_Sources
    {
        public int Id { get; set; }

        public int CampaignId { get; set; }

        public int ProductId { get; set; }

        public virtual Catalog_Campaigns Catalog_Campaigns { get; set; }

        public virtual Catalog_Products Catalog_Products { get; set; }
    }
}
