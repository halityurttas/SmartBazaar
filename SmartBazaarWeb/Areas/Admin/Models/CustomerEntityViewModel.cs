using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    

    public class CustomerEntityListViewModel
    {
        public int Id { get; set; }
        [Display(Name = CustomerEntityFieldNames.Title)]
        public string Title { get; set; }
        [Display(Name = CustomerEntityFieldNames.GroupTitle)]
        public string GroupTitle { get; set; }
        [UIHint("StatusBadge")]
        [Display(Name = CustomerEntityFieldNames.Status)]
        public short Status { get; set; }

    }

    public class CustomerEntityEditViewModel
    {
        public int Id { get; set; }
        [UIHint("ShortDropDown")]
        [Display(Name = CustomerEntityFieldNames.GroupTitle)]
        
        public int? GroupId { get; set; }
        [UIHint("LongString")]
        [Display(Name = CustomerEntityFieldNames.Title)]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(150, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        public string Caption { get; set; }
        [UIHint("ShortString")]
        [Display(Name = CustomerEntityFieldNames.TaxNr)]
        [MaxLength(25, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        public string TaxNr { get; set; }
        [UIHint("ShortString")]
        [Display(Name = CustomerEntityFieldNames.TaxOffice)]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        public string TaxOffice { get; set; }
        [UIHint("ShortDropDown")]
        [Display(Name = CustomerEntityFieldNames.CustomerType)]
        public short CustomerType { get; set; }
        [UIHint("ShortDate")]
        [Display(Name = CustomerEntityFieldNames.BirthDate)]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [UIHint("ShortDropDown")]
        [Display(Name = CustomerEntityFieldNames.Gender)]
        public short? Gender { get; set; }
        [UIHint("LongString")]
        [Display(Name = CustomerEntityFieldNames.Company)]
        [MaxLength(250, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        public string Company { get; set; }
        [UIHint("ShortString")]
        [Display(Name = CustomerEntityFieldNames.ContactPhone)]
        [MaxLength(25, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        public string ContactPhone { get; set; }
        [UIHint("ShortDropDown")]
        [Display(Name = CustomerEntityFieldNames.Status)]
        public short Status { get; set; }
        public string UserId { get; set; }

    }

    public class CustomerEntityDetailViewModel
    {
        public CustomerEntityDetailViewModel()
        {
            this.Addresses = new List<CustomerAddressEditViewModel>();
        }

        public int Id { get; set; }
        public int? GroupId { get; set; }
        [Display(Name=CustomerEntityFieldNames.Title)]
        public string Title { get; set; }
        [Display(Name=CustomerEntityFieldNames.TaxNr)]
        public string TaxNr { get; set; }
        [Display(Name=CustomerEntityFieldNames.TaxOffice)]
        public string TaxOffice { get; set; }
        public short CustomerType { get; set; }
        [Display(Name=CustomerEntityFieldNames.CustomerType)]
        public string CustomerTypeTitle { get; set; }
        [Display(Name=CustomerEntityFieldNames.BirthDate)]
        public DateTime? BirthDate { get; set; }
        public short? Gender { get; set; }
        [Display(Name=CustomerEntityFieldNames.Gender)]
        public string GenderTitle { get; set; }
        [Display(Name=CustomerEntityFieldNames.Company)]
        public string Company { get; set; }
        [Display(Name=CustomerEntityFieldNames.ContactPhone)]
        public string ContactPhone { get; set; }
        [Display(Name=CustomerEntityFieldNames.Email)]
        public string Email { get; set; }
        public short Status { get; set; }
        public string UserId { get; set; }


        // Customer GroupDetail
        [Display(Name = CustomerGroupFieldNames.Title)]
        public string GroupTitle { get; set; }
        [Display(Name = CustomerGroupFieldNames.PriceIndex)]
        public short PriceIndex { get; set; }

        // Customer Address
        public List<CustomerAddressEditViewModel> Addresses { get; set; }
    }
}