namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_Heads
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order_Heads()
        {
            Order_Lines = new HashSet<Order_Lines>();
            Payment_Entities = new HashSet<Payment_Entities>();
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int? ShipAddressId { get; set; }

        public int? InvoiceAddressId { get; set; }

        public DateTime OrderDate { get; set; }

        [Column(TypeName = "money")]
        public decimal OrderTotal { get; set; }

        [Column(TypeName = "money")]
        public decimal TaxTotal { get; set; }

        public int ShipmentTypeId { get; set; }

        [Column(TypeName = "money")]
        public decimal ShipCost { get; set; }

        [Column(TypeName = "money")]
        public decimal GrandTotal { get; set; }

        public short Status { get; set; }

        [Column(TypeName = "money")]
        public decimal PaymentFee { get; set; }

        [Column(TypeName = "money")]
        public decimal InstallmentFee { get; set; }

        [Column(TypeName = "ntext")]
        public string Note { get; set; }

        public virtual Customer_Addresses Customer_Addresses { get; set; }

        public virtual Customer_Addresses Customer_Addresses1 { get; set; }

        public virtual Customer_Entities Customer_Entities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Lines> Order_Lines { get; set; }

        public virtual Shipment_Types Shipment_Types { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment_Entities> Payment_Entities { get; set; }
    }
}
