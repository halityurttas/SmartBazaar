namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Catalog_Products_Properties
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Catalog_Products_Properties()
        {
            Catalog_Products_Properties_Lang = new HashSet<Catalog_Products_Properties_Lang>();
        }

        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [StringLength(250)]
        public string Value { get; set; }

        public virtual Catalog_Products Catalog_Products { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog_Products_Properties_Lang> Catalog_Products_Properties_Lang { get; set; }
    }
}
