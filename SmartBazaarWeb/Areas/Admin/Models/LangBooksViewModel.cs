using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public class LangBooksListViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = LangBooksFieldNames.Title)]
        public string Title { get; set; }

        [UIHint("StatusBadge")]
        [Display(Name = LangBooksFieldNames.Status)]
        public short Status { get; set; }
    }

    public class LangBooksEditViewModel
    {
        public LangBooksEditViewModel()
        {
            Code = System.Configuration.ConfigurationManager.AppSettings["DefaultLanguage"];
        }

        public int Id { get; set; }

        [UIHint("ShortString")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(10, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = LangBooksFieldNames.Code)]
        public string Code { get; set; }

        [UIHint("LongString")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(25, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = LangBooksFieldNames.Title)]
        public string Caption { get; set; }

        [UIHint("ShortDropDown")]
        [Display(Name = LangBooksFieldNames.Status)]
        public short Status { get; set; }
    }
}