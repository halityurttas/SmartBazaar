using SmartBazaar.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public class ContactViewModel
    {
        [Display(Name=Contact.Address)]
        [UIHint("ShortText")]
        public string Address { get; set; }
        [Display(Name=Contact.Phone1Title)]
        [UIHint("ShortString")]
        public string Phone1Title { get; set; }
        [Display(Name = Contact.Phone1)]
        [UIHint("ShortString")]
        public string Phone1 { get; set; }
        [Display(Name = Contact.Phone2Title)]
        [UIHint("ShortString")]
        public string Phone2Title { get; set; }
        [Display(Name = Contact.Phone2)]
        [UIHint("ShortString")]
        public string Phone2 { get; set; }
        [Display(Name = Contact.Email1Title)]
        [UIHint("ShortString")]
        public string Email1Title { get; set; }
        [Display(Name = Contact.Email1)]
        [UIHint("ShortString")]
        public string Email1 { get; set; }
        [Display(Name = Contact.Email2Title)]
        [UIHint("ShortString")]
        public string Email2Title { get; set; }
        [Display(Name = Contact.Email2)]
        [UIHint("ShortString")]
        public string Email2 { get; set; }
        [Display(Name = Contact.GMapData)]
        [UIHint("ShortText")]
        public string GMapData { get; set; }
    }
}