namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_Lines
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public double TaxRate { get; set; }

        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        public int? CampaignId { get; set; }

        public virtual Catalog_Campaigns Catalog_Campaigns { get; set; }

        public virtual Catalog_Products Catalog_Products { get; set; }

        public virtual Order_Heads Order_Heads { get; set; }
    }
}
