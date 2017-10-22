using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public class PosSettingsListViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = PosSettingsFieldNames.Title)]
        public string Title { get; set; }

        [Display(Name = PosSettingsFieldNames.Status)]
        [UIHint("StatusBadge")]
        public short Status { get; set; }
    }

    public class PosSettingsDetailViewModel
    {
        public int Id { get; set; }

        [Display(Name = PosSettingsFieldNames.Title)]
        [UIHint("ShortString")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        public string Caption { get; set; }

        [UIHint("ShortDropDown")]
        [Display(Name = CatalogBrandFieldNames.Status)]
        public short Status { get; set; }

        public List<KPSerializer.KPModel> KPList { get; set; }
    }
}