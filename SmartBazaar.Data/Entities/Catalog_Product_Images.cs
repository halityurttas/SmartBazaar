namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Catalog_Product_Images
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required]
        [StringLength(250)]
        public string ImageUrl { get; set; }

        public int Sort { get; set; }

        public virtual Catalog_Products Catalog_Products { get; set; }
    }
}
