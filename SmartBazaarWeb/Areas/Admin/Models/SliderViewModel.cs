using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public class SliderListViewModel
    {
        public int Id { get; set; }
        [Display(Name = SliderFieldNames.Title)]
        public string Title { get; set; }
        [UIHint("StatusBadge")]
        public short Status { get; set; }
    }

    public class SliderEditViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = SliderFieldNames.Title)]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(250, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [UIHint("LongString")]
        public string Caption { get; set; }

        [Display(Name = SliderFieldNames.ImageUrl)]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        public string ImageUrl { get; set; }

        [Display(Name = SliderFieldNames.Detail)]
        [UIHint("SummernoteSingle")]
        public string Detail { get; set; }

        [Display(Name = SliderFieldNames.Status)]
        [UIHint("ShortDropDown")]
        public short Status { get; set; }
    }
}