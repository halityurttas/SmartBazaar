namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer_Addresses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer_Addresses()
        {
            Order_Heads = new HashSet<Order_Heads>();
            Order_Heads1 = new HashSet<Order_Heads>();
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string District { get; set; }

        [StringLength(50)]
        public string Town { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public short Status { get; set; }

        public bool IsDefault { get; set; }

        [StringLength(15)]
        public string PostalCode { get; set; }

        public virtual Customer_Entities Customer_Entities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Heads> Order_Heads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Heads> Order_Heads1 { get; set; }
    }
}
