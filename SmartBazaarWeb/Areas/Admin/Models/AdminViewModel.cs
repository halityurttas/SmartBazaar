using SmartBazaar.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }

    public class AdminRememberViewModel
    {
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "E-Posta")]
        [EmailAddress(ErrorMessageResourceName = "FieldEmail", ErrorMessageResourceType = typeof(Messages))]
        public string Email { get; set; }
    }
}