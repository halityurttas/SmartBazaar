namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pos_Settings
    {
        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Data { get; set; }

        [Required]
        [StringLength(100)]
        public string AssemblyData { get; set; }

        [Required]
        [StringLength(100)]
        public string Referance { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        public short Status { get; set; }
    }
}
