namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Catalog_Categories
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Catalog_Categories()
        {
            Catalog_Categories_Lang = new HashSet<Catalog_Categories_Lang>();
            Catalog_Products = new HashSet<Catalog_Products>();
        }

        public int Id { get; set; }

        public int? ParentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public int Pos { get; set; }

        public int Level { get; set; }

        public short Status { get; set; }

        [StringLength(100)]
        public string ExternalRef1 { get; set; }

        [StringLength(100)]
        public string ExternalRef2 { get; set; }

        [StringLength(100)]
        public string ExternalRef3 { get; set; }

        public bool IsDisplayInMenu { get; set; }

        [StringLength(250)]
        public string ImageUrl { get; set; }

        public bool IsDisplayInMainPage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog_Categories_Lang> Catalog_Categories_Lang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog_Products> Catalog_Products { get; set; }
    }
}
