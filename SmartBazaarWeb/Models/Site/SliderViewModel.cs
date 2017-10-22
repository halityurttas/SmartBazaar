using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Site
{
    public class SliderSliderViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }

    public class SliderDetailViewModel
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Detail { get; set; }
    }
}