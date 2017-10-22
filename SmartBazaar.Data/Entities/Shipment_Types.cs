namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Shipment_Types
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shipment_Types()
        {
            Order_Heads = new HashSet<Order_Heads>();
            Shipment_Types_Lang = new HashSet<Shipment_Types_Lang>();
        }

        public int Id { get; set; }

        public short PricingMethod { get; set; }

        public double PricingValue { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        public short Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Heads> Order_Heads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shipment_Types_Lang> Shipment_Types_Lang { get; set; }
    }
}
