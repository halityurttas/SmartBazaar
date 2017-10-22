namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Catalog_Campaigns
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Catalog_Campaigns()
        {
            Catalog_Campaigns_Destinations = new HashSet<Catalog_Campaigns_Destinations>();
            Order_Lines = new HashSet<Order_Lines>();
            Catalog_Campaigns_Sources = new HashSet<Catalog_Campaigns_Sources>();
        }

        public int Id { get; set; }

        public short CampaignMethod { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public short DiscountMethod { get; set; }

        public double DiscountValue { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [StringLength(250)]
        public string SliderUrl { get; set; }

        public short Status { get; set; }

        public int MaxUsage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog_Campaigns_Destinations> Catalog_Campaigns_Destinations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Lines> Order_Lines { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog_Campaigns_Sources> Catalog_Campaigns_Sources { get; set; }
    }
}
