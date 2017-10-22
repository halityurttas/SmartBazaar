namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HtmlBlocks_Lang
    {
        public int Id { get; set; }

        public int RefId { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public virtual HtmlBlocks HtmlBlocks { get; set; }
    }
}
