using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public class ParamsListViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Tanım")]
        public string Title { get; set; }
        [Display(Name = "Değer")]
        public string Value { get; set; }
    }

    public class ParamsGroupsListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}