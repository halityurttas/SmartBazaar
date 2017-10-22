namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Catalog_Brands
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Catalog_Brands()
        {
            Catalog_Brands_Lang = new HashSet<Catalog_Brands_Lang>();
            Catalog_Products = new HashSet<Catalog_Products>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public short Status { get; set; }

        [StringLength(100)]
        public string ExternalRef1 { get; set; }

        [StringLength(100)]
        public string ExternalRef2 { get; set; }

        [StringLength(100)]
        public string ExternalRef3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog_Brands_Lang> Catalog_Brands_Lang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog_Products> Catalog_Products { get; set; }
    }
}
