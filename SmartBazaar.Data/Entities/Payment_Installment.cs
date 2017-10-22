namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Payment_Installment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Payment_Installment()
        {
            Payment_Entities = new HashSet<Payment_Entities>();
        }

        public int Id { get; set; }

        public int PaymentId { get; set; }

        public int NumberOf { get; set; }

        public double Rate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment_Entities> Payment_Entities { get; set; }

        public virtual Payment_Types Payment_Types { get; set; }
    }
}
