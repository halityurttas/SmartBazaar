using SmartBazaar.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmartBazaar.Web.Models.Common;

namespace SmartBazaar.Web.Models.Site
{
    
    public class CustomerRegisterViewModel
    {
        public string UserId { get; set; }

        [UIHint("LongString")]
        [Display(Name = CustomerEntityFieldNames.Title)]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [StringLength(150, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "StringLength", MinimumLength = 5)]
        public string Caption { get; set; }

        [UIHint("ShortDropDown")]
        [Display(Name = CustomerEntityFieldNames.CustomerType)]
        public short CustomerType { get; set; }

        [UIHint("ShortString")]
        [MaxLength(25, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = CustomerEntityFieldNames.TaxNr)]
        public string TaxNr { get; set; }

        [UIHint("ShortString")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CustomerEntityFieldNames.TaxOffice)]
        public string TaxOffice { get; set; }

        [UIHint("ShortDate")]
        [Display(Name = CustomerEntityFieldNames.BirthDate)]
        public DateTime? BirthDate { get; set; }

        [UIHint("ShortDropDown")]
        [Display(Name = CustomerEntityFieldNames.Gender)]
        public short? Gender { get; set; }

        [UIHint("LongString")]
        [MaxLength(250, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CustomerEntityFieldNames.Company)]
        public string Company { get; set; }

        [UIHint("ShortString")]
        [Display(Name = CustomerEntityFieldNames.ContactPhone)]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [StringLength(25, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "StringLength", MinimumLength = 7)]
        public string ContactPhone { get; set; }

    }
}