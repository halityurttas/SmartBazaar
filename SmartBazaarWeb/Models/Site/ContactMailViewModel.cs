using SmartBazaar.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Site
{
    public class ContactMailViewModel
    {
        [Display(Name = "Adınız Soyadınız")]
        [Required(ErrorMessage = "", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        public string Name { get; set; }

        [Display(Name = "E-Posta Adresiniz")]
        [Required(ErrorMessage = "", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [EmailAddress(ErrorMessage = "", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldEmail")]
        public string Email { get; set; }

        [Display(Name = "Telefonunuz")]
        public string Phone { get; set; }

        [Display(Name = "Mesajınız")]
        [Required(ErrorMessage = "", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        public string Comment { get; set; }
    }
}