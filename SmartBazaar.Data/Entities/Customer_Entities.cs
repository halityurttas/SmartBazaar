namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer_Entities
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer_Entities()
        {
            Customer_Addresses = new HashSet<Customer_Addresses>();
            Order_Heads = new HashSet<Order_Heads>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public int? GroupId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(25)]
        public string TaxNr { get; set; }

        [StringLength(100)]
        public string TaxOffice { get; set; }

        public short CustomerType { get; set; }

        public DateTime? BirthDate { get; set; }

        public short? Gender { get; set; }

        [StringLength(250)]
        public string Company { get; set; }

        [StringLength(25)]
        public string ContactPhone { get; set; }

        public short Status { get; set; }

        public DateTime Created { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer_Addresses> Customer_Addresses { get; set; }

        public virtual Customer_Groups Customer_Groups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Heads> Order_Heads { get; set; }
    }
}
