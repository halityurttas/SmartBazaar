namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Catalog_Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Catalog_Products()
        {
            Catalog_Campaigns_Destinations = new HashSet<Catalog_Campaigns_Destinations>();
            Catalog_Campaigns_Sources = new HashSet<Catalog_Campaigns_Sources>();
            Catalog_Product_Comments = new HashSet<Catalog_Product_Comments>();
            Catalog_Product_Images = new HashSet<Catalog_Product_Images>();
            Catalog_Products_Lang = new HashSet<Catalog_Products_Lang>();
            Catalog_Products_Relations = new HashSet<Catalog_Products_Relations>();
            Order_Lines = new HashSet<Order_Lines>();
            Catalog_Products_Properties = new HashSet<Catalog_Products_Properties>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductCode { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public int? BrandId { get; set; }

        [StringLength(250)]
        public string ImageUrl { get; set; }

        public int Stock { get; set; }

        [StringLength(255)]
        public string SearchText { get; set; }

        [Column(TypeName = "money")]
        public decimal Price1 { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price2 { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price3 { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price4 { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price5 { get; set; }

        public int TaxGroup { get; set; }

        public short Status { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsNew { get; set; }

        [StringLength(100)]
        public string MetaTitle { get; set; }

        [StringLength(150)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescription { get; set; }

        public int MaxInstallment { get; set; }

        [Column(TypeName = "ntext")]
        public string ShortDescription { get; set; }

        [Column(TypeName = "ntext")]
        public string ManagerNotes { get; set; }

        public bool IsHomepage { get; set; }

        [StringLength(25)]
        public string SKU { get; set; }

        [StringLength(25)]
        public string MPN { get; set; }

        [StringLength(250)]
        public string Barcode { get; set; }

        public bool IsBuyable { get; set; }

        public bool IsShipable { get; set; }

        public bool IsFreeShip { get; set; }

        public double Tare { get; set; }

        [StringLength(250)]
        public string ExternalRef1 { get; set; }

        [StringLength(250)]
        public string ExternalRef2 { get; set; }

        [StringLength(250)]
        public string ExternalRef3 { get; set; }

        [StringLength(250)]
        public string MetaUrl { get; set; }

        public virtual Catalog_Brands Catalog_Brands { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog_Campaigns_Destinations> Catalog_Campaigns_Destinations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog_Campaigns_Sources> Catalog_Campaigns_Sources { get; set; }

        public virtual Catalog_Categories Catalog_Categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog_Product_Comments> Catalog_Product_Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog_Product_Images> Catalog_Product_Images { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog_Products_Lang> Catalog_Products_Lang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog_Products_Relations> Catalog_Products_Relations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Lines> Order_Lines { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog_Products_Properties> Catalog_Products_Properties { get; set; }

        public virtual Tax_Products Tax_Products { get; set; }
    }
}
