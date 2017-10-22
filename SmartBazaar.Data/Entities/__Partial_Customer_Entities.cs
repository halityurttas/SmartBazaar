using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SmartBazaar.Data.Entities
{
    public partial class Customer_Entities
    {
        [NotMapped]
        public string Email { get; set; }
    }
}
