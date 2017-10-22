namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Catalog_Products_Lang
    {
        public int Id { get; set; }

        public int RefId { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [StringLength(100)]
        public string MetaTitle { get; set; }

        [StringLength(150)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescription { get; set; }

        [Column(TypeName = "ntext")]
        public string ShortDescription { get; set; }

        [StringLength(250)]
        public string MetaUrl { get; set; }

        public virtual Catalog_Products Catalog_Products { get; set; }
    }
}
