using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    

    public class CatalogCampaignsListViewModel
    {
        public int Id { get; set; }
        [Display(Name = CatalogCampaignFieldNames.CampaignMethod)]
        public string CampaignMethod { get; set; }
        [Display(Name = CatalogCampaignFieldNames.Title)]
        public string Title { get; set; }
        [Display(Name = CatalogCampaignFieldNames.StartDate)]
        [UIHint("ShortDate")]
        public DateTime StartDate { get; set; }
        [Display(Name = CatalogCampaignFieldNames.EndDate)]
        [UIHint("ShortDate")]
        public DateTime EndDate { get; set; }
        [Display(Name = CatalogCampaignFieldNames.Status)]
        [UIHint("StatusBadge")]
        public short Status { get; set; }

    }


    public class CatalogCampaignsSourceTransformViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class CatalogCampaignsSourcesViewModel
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

    }


    public class CatalogCampaignsDestinationsViewModel
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

    }

    public class CatalogCampaignsEditViewModel
    {
        public CatalogCampaignsEditViewModel()
        {
            Sources = new List<CatalogCampaignsSourcesViewModel>();
            Destinations = new List<CatalogCampaignsDestinationsViewModel>();
        }
        public int Id { get; set; }
        [UIHint("ShortDropDown")]
        [Display(Name = CatalogCampaignFieldNames.CampaignMethod)]
        public short CampaignMethod { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = CatalogCampaignFieldNames.StartDate)]
        [UIHint("ShortDate")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = CatalogCampaignFieldNames.EndDate)]
        [UIHint("ShortDate")]
        public DateTime EndDate { get; set; }
        [Display(Name = CatalogCampaignFieldNames.DiscountMethod)]
        [UIHint("ShortDropDown")]
        public short DiscountMethod { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = CatalogCampaignFieldNames.DiscountValue)]
        [UIHint("ShortDouble")]
        public double DiscountValue { get; set; }
        [UIHint("LongString")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(250, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogCampaignFieldNames.Title)]
        public string Caption { get; set; }
        [UIHint("Summernote")]
        [Display(Name = CatalogCampaignFieldNames.Description)]
        public string Description { get; set; }
        [UIHint("ShortDropDown")]
        [Display(Name = CatalogCampaignFieldNames.Status)]
        public short Status { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [UIHint("ShortInt")]
        [Display(Name = CatalogCampaignFieldNames.MaxUsage)]
        public int MaxUsage { get; set; }

        [Display(Name = CatalogCampaignFieldNames.SlideUrl)]
        public string SlideUrl { get; set; }

        public List<CatalogCampaignsSourcesViewModel> Sources { get; set; }
        public List<CatalogCampaignsDestinationsViewModel> Destinations { get; set; }

    }
}