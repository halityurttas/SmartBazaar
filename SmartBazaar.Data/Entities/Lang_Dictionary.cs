namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lang_Dictionary
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        [Required]
        [StringLength(250)]
        public string Key { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Value { get; set; }

        public virtual Lang_Books Lang_Books { get; set; }
    }
}
