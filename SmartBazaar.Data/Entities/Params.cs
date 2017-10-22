namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Params
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        public int GroupId { get; set; }

        [Required]
        [StringLength(250)]
        public string Value { get; set; }

        public virtual Params_Groups Params_Groups { get; set; }
    }
}
