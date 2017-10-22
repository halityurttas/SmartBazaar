namespace SmartBazaar.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Payment_Entities
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int PaymentTypeId { get; set; }

        public int? InstallmentId { get; set; }

        [Column(TypeName = "money")]
        public decimal OrderPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal PaymentFee { get; set; }

        [Column(TypeName = "money")]
        public decimal InstallmentFee { get; set; }

        [Column(TypeName = "money")]
        public decimal ShipmentFee { get; set; }

        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        public DateTime PaymentDate { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Log { get; set; }

        public short Status { get; set; }

        public virtual Order_Heads Order_Heads { get; set; }

        public virtual Payment_Installment Payment_Installment { get; set; }

        public virtual Payment_Types Payment_Types { get; set; }
    }
}
