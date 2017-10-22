using SmartBazaar.Data.Entities;
using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SmartBazaar.Web.Areas.Admin.Models
{

    public class CatalogCategoryListViewModel
    {
        public int Id { get; set; }
        public int RowNr { get; set; }
        [Display(Name = CatalogCategoryFieldNames.ParentId)]
        public string Parent { get; set; }
        [Display(Name = CatalogCategoryFieldNames.Title)]
        public string Caption { get; set; }
        [UIHint("StatusBadge")]
        [Display(Name = CatalogCategoryFieldNames.Status)]
        public short Status { get; set; }

    }

    public class CatalogCategoryEditViewModel
    {

        public int Id { get; set; }
        public int Level { get; set; }
        [Display(Name = CatalogCategoryFieldNames.ParentId)]
        public int? ParentId { get; set; }
        [Display(Name = CatalogCategoryFieldNames.ParentId)]
        public string ParentTitle { get; set; }
        [UIHint("LongString")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogCategoryFieldNames.Title)]
        public string Caption { get; set; }
        [UIHint("LongText")]
        [Display(Name = CatalogCategoryFieldNames.Description)]
        public string Description { get; set; }
        [UIHint("ShortInt")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [Display(Name = CatalogCategoryFieldNames.Pos)]
        public int Pos { get; set; }
        [UIHint("ShortDropDown")]
        [Display(Name = CatalogCategoryFieldNames.Status)]
        public short Status { get; set; }
        [UIHint("ShortString")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogCategoryFieldNames.ExternalRef1)]
        public string ExternalRef1 { get; set; }
        [UIHint("ShortString")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogCategoryFieldNames.ExternalRef2)]
        public string ExternalRef2 { get; set; }
        [UIHint("ShortString")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogCategoryFieldNames.ExternalRef3)]
        public string ExternalRef3 { get; set; }
        [UIHint("ShortCheck")]
        [Display(Name = CatalogCategoryFieldNames.IsDisplayInMenu)]
        public bool IsDisplayInMenu { get; set; }
        [Display(Name = CatalogCategoryFieldNames.ImageUrl)]
        [MaxLength(250, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        public string ImageUrl { get; set; }
        [UIHint("ShortCheck")]
        [Display(Name = CatalogCategoryFieldNames.IsDisplayInMainPage)]
        public bool IsDisplayInMainPage { get; set; }

    }

    public class CatalogCategoryLangViewModel
    {
        public int Id { get; set; }
        public int RefId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [StringLength(10)]
        public string Code { get; set; }
        [UIHint("LongString")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = CatalogCategoryFieldNames.Title)]
        public string Caption { get; set; }
        [UIHint("LongText")]
        [Display(Name = CatalogCategoryFieldNames.Description)]
        public string Description { get; set; }
    }

    public class CatalogCategoryTreeItemModel
    {
        public CatalogCategoryTreeItemModel()
        {
            Items = new List<CatalogCategoryTreeItemModel>();
        }
        public int Id { get; set; }
        public int Level { get; set; }
        public string Title { get; set; }

        public List<CatalogCategoryTreeItemModel> Items { get; set; }
    }

    public class CatalogCategoryTreeModel
    {
        public CatalogCategoryTreeModel()
        {
            Items = new List<CatalogCategoryTreeItemModel>();
        }

        public List<CatalogCategoryTreeItemModel> Items { get; set; }

        private void recursiveParser(ICollection<Catalog_Categories> items, CatalogCategoryTreeItemModel item)
        {
            var query = from f in items
                        where f.ParentId == item.Id
                        select f;
            foreach (var q in query)
            {
                var nitem = new CatalogCategoryTreeItemModel
                {
                    Id = q.Id,
                    Level = q.Level,
                    Title = q.Title
                };
                item.Items.Add(nitem);
                recursiveParser(items, nitem);
            }
        }

        public void Imports(ICollection<Catalog_Categories> items)
        {
            var query = from c in items
                        where c.ParentId == null
                        select c;
            foreach (var q in query.OrderBy(o => o.Pos))
            {
                var item = new CatalogCategoryTreeItemModel
                {
                    Id = q.Id,
                    Level = q.Level,
                    Title = q.Title
                };
                Items.Add(item);
                recursiveParser(items, item);
            }
        }

    }

}