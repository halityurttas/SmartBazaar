using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public class PosCardsListViewModel
    {
        public int Id { get; set; }

        public int SettingId { get; set; }
        
        [Display(Name = PosCardsFieldNames.Title)]
        public string Title { get; set; }
        
        [Display(Name = PosCardsFieldNames.Referance)]
        public string PosTitle { get; set; }

        [Display(Name = PosCardsFieldNames.Status)]
        [UIHint("StatusBadge")]
        public short Status { get; set; }
    }

    public class PosCardsEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = PosCardsFieldNames.Referance)]
        [UIHint("ShortDropDown")]
        public int SettingId { get; set; }

        [Display(Name = PosCardsFieldNames.Title)]
        [UIHint("ShortString")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        public string Caption { get; set; }

        [Display(Name = PosCardsFieldNames.Brand)]
        [UIHint("ShortDropDown")]
        public string Brand { get; set; }

        [UIHint("ShortDropDown")]
        [Display(Name = PosCardsFieldNames.Status)]
        public short Status { get; set; }
    }
}