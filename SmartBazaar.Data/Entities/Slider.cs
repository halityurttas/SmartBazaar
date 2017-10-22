namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slider")]
    public partial class Slider
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        [Required]
        [StringLength(100)]
        public string ImageUrl { get; set; }

        public short Status { get; set; }
    }
}
