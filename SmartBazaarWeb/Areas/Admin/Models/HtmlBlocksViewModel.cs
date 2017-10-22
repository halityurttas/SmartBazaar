using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public class HtmlBlocksListViewModel
    {
        public int Id { get; set; }

        [Display(Name = HtmlBlocksFieldNames.Title)]
        public string Title { get; set; }

        [UIHint("StatusBadge")]
        [Display(Name = HtmlBlocksFieldNames.Status)]
        public short Status { get; set; }
    }

    public class HtmlBlocksEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = HtmlBlocksFieldNames.Title)]
        [MaxLength(150, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [UIHint("LongString")]
        public string Caption { get; set; }

        [Display(Name = HtmlBlocksFieldNames.Detail)]
        [UIHint("SummernoteSingle")]
        public string Detail { get; set; }

        [Display(Name = HtmlBlocksFieldNames.Location)]
        [UIHint("ShortDropDown")]
        public int Location { get; set; }

        [Display(Name = HtmlBlocksFieldNames.Status)]
        [UIHint("ShortDropDown")]
        public short Status { get; set; }
    }

    public class HtmlBlocksLangViewModel
    {
        public int Id { get; set; }

        public int RefId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [StringLength(10)]
        public string Code { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = HtmlBlocksFieldNames.Title)]
        [MaxLength(150, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [UIHint("LongString")]
        public string Caption { get; set; }

        [Display(Name = HtmlBlocksFieldNames.Detail)]
        [UIHint("SummernoteSingle")]
        public string Detail { get; set; }
    }
}