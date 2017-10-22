namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Catalog_Product_Comments
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        [StringLength(150)]
        public string UserTitle { get; set; }

        public short Rating { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Description { get; set; }

        public short Status { get; set; }

        public int ProductId { get; set; }

        public DateTime Created { get; set; }

        public virtual Catalog_Products Catalog_Products { get; set; }
    }
}
