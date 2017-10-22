using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartBazaar.Web.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public class CatalogCommentsListViewModel
    {
        public int Id { get; set; }

        [Display(Name = CatalogCommentsFieldNames.UserTitle)]
        public string UserTitle { get; set; }

        [Display(Name = CatalogCommentsFieldNames.Rating)]
        public short Rating { get; set; }

        [Display(Name = CatalogCommentsFieldNames.Status)]
        [UIHint("StatusBadge")]
        public short Status { get; set; }

        [Display(Name = CatalogProductFieldNames.Title)]
        public string ProductTitle { get; set; }

        [Display(Name = CatalogCommentsFieldNames.Created)]
        public DateTime Created { get; set; }
    }

    public class CatalogCommentsDetailViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [Display(Name = CatalogCommentsFieldNames.UserTitle)]
        public string UserTitle { get; set; }

        [Display(Name = CatalogCommentsFieldNames.Rating)]
        public short Rating { get; set; }

        [Display(Name = CatalogCommentsFieldNames.Status)]
        [UIHint("StatusBadge")]
        public short Status { get; set; }

        [Display(Name = CatalogProductFieldNames.Title)]
        public string ProductTitle { get; set; }

        [Display(Name = CatalogCommentsFieldNames.Description)]
        public string Description { get; set; }

        [Display(Name = CatalogCommentsFieldNames.Created)]
        public DateTime Created { get; set; }
    }
}