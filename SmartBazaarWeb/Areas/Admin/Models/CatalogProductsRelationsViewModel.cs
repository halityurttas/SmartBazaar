using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public class CatalogProductsRelationsEditViewModel
    {
        public CatalogProductsRelationsEditViewModel()
        {
            GroupId = Guid.Empty;
        }

        public int Id { get; set; }

        public Guid GroupId { get; set; }

        public int ProductId { get; set; }

        public string ProductTitle { get; set; }
    }

    public class CatalogProductsRelationsJSONModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}