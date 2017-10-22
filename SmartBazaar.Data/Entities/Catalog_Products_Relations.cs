namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Catalog_Products_Relations
    {
        public int Id { get; set; }

        public Guid GroupId { get; set; }

        public int ProductId { get; set; }

        public virtual Catalog_Products Catalog_Products { get; set; }
    }
}
