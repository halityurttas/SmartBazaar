namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Payment_Types
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Payment_Types()
        {
            Payment_Entities = new HashSet<Payment_Entities>();
            Payment_Installment = new HashSet<Payment_Installment>();
            Payment_Types_Lang = new HashSet<Payment_Types_Lang>();
        }

        public int Id { get; set; }

        public short Method { get; set; }

        public double CommissionRate { get; set; }

        [Column(TypeName = "money")]
        public decimal ProcessFee { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public short Status { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [StringLength(50)]
        public string PosType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment_Entities> Payment_Entities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment_Installment> Payment_Installment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment_Types_Lang> Payment_Types_Lang { get; set; }
    }
}
