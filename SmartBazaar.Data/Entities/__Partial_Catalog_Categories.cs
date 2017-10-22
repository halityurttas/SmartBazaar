using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBazaar.Data.Entities
{
    public partial class Catalog_Categories
    {
        [NotMapped]
        public int rowNr { get; set; }

        [NotMapped]
        public virtual Catalog_Categories Catalog_Categories_Parent { get; set; }
    }
}
