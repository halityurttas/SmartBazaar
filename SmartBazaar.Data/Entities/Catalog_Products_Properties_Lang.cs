namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Catalog_Products_Properties_Lang
    {
        public int Id { get; set; }

        public int RefId { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [StringLength(250)]
        public string Value { get; set; }

        public virtual Catalog_Products_Properties Catalog_Products_Properties { get; set; }
    }
}
