using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SmartBazaar.Data.Entities
{
    public partial class Catalog_Products
    {
        [NotMapped]
        public virtual ICollection<Catalog_Products_Relations> Related_Catalog_Products_Relations { get; set; }

        [NotMapped]
        public virtual ICollection<Catalog_Products> Related_Catalog_Products { get; set; }
    }
}
