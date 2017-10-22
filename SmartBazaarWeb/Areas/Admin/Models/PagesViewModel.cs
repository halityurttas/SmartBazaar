using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public class PagesListViewModel
    {
        public int Id { get; set; }

        [Display(Name = PagesModelFieldNames.Title)]
        public string Title { get; set; }

        [UIHint("StatusBadge")]
        [Display(Name = PagesModelFieldNames.Status)]
        public short Status { get; set; }
        
        public bool IsFixed { get; set; }
    }

    public class PagesEditViewModel
    {
        public int Id { get; set; }

        [UIHint("LongString")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(250, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = PagesModelFieldNames.Title)]
        public string Caption { get; set; }

        [UIHint("SummernoteSingle")]
        [Display(Name = PagesModelFieldNames.Detail)]
        public string Detail { get; set; }

        [UIHint("ShortDropDown")]
        [Display(Name = PagesModelFieldNames.Status)]
        public short Status { get; set; }

        public bool IsFixed { get; set; }
    }

    public class PagesLangViewModel
    {
        public int Id { get; set; }

        public int RefId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [StringLength(10)]
        public string Code { get; set; }

        [UIHint("LongString")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(250, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = PagesModelFieldNames.Title)]
        public string Caption { get; set; }

        [UIHint("SummernoteSingle")]
        [Display(Name = PagesModelFieldNames.Detail)]
        public string Detail { get; set; }
    }
}