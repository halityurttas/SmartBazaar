using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Site
{
    public class CatalogCategoryHomeListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public List<ProductListDisplayModel> Products { get; set; }
    }
}