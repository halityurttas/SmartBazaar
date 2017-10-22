using AutoMapper;
using SmartBazaar.Data;
using SmartBazaar.Data.Entities;
using SmartBazaar.Web.Areas.Admin.Models;
using SmartBazaar.Web.Business.Layers;
using SmartBazaar.Web.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace SmartBazaar.Web.Business.Workers
{
    public class CatalogWorker
    {
        private readonly ContentContext m_ContentContext;
        public CatalogWorker()
        {
            m_ContentContext = new ContentContext();
        }

        #region Products

        public List<CatalogProductListViewModel> GetManagerCatalogProductList(Expression<Func<Catalog_Products, bool>> predicate)
        {
            var query = m_ContentContext.Catalog_Products.Where(predicate);
            return Mapper.Map<Catalog_Products[], List<CatalogProductListViewModel>>(query.ToArray());
        }

        public List<CatalogProductListViewModel> FindManagerCatalogProductList(string SearchKey)
        {
            var query = from p in m_ContentContext.Catalog_Products
                        where p.Status == 1 && (p.SearchText.Contains(SearchKey) || p.Title.Contains(SearchKey) || p.ProductCode.Contains(SearchKey))
                        select p;
            return Mapper.Map<Catalog_Products[], List<CatalogProductListViewModel>>(query.ToArray());
        }

        public CatalogProductEditViewModel GetManagerCatalogProductEdit(int id)
        {
            var query = from p in m_ContentContext.Catalog_Products
                        where p.Id == id
                        select p;
            var item = query.FirstOrDefault();
            Guid[] groupids = item.Catalog_Products_Relations.Select(s => s.GroupId).ToArray();
            var qrels = from rl in m_ContentContext.Catalog_Products_Relations
                        join rr in m_ContentContext.Catalog_Products_Relations on rl.GroupId equals rr.GroupId
                        where rr.Id != rl.Id && rl.ProductId == item.Id
                        select rr;

            item.Related_Catalog_Products_Relations = qrels.ToList();
            return Mapper.Map<CatalogProductEditViewModel>(item);
        }

        public CatalogProductLangViewModel GetManagerCatalogProductLang(int id, string code)
        {
            var query = from l in m_ContentContext.Catalog_Products
                        where l.Id == id
                        select l;
            var item = query.FirstOrDefault();
            if (item.Catalog_Products_Lang.Any(a => a.Code == code))
            {
                var lang = Mapper.Map<CatalogProductLangViewModel>(item.Catalog_Products_Lang.FirstOrDefault(f => f.Code == code));
                lang.Properties = Mapper.Map<List<CatalogProductPropertiesLangViewModel>>(item.Catalog_Products_Properties.ToArray());
                return lang;
            }
            else
            {
                return Mapper.Map<CatalogProductLangViewModel>(item);
            }
        }

        public void InsertManagerCatalogProduct(CatalogProductEditViewModel model)
        {
            var exported = Mapper.Map<Catalog_Products>(model);
            m_ContentContext.Catalog_Products.Add(exported);
            m_ContentContext.SaveChanges();
            foreach (var propModel in model.Properties)
            {
                var propItem = Mapper.Map<Catalog_Products_Properties>(propModel);
                propItem.ProductId = exported.Id;
                m_ContentContext.Catalog_Products_Properties.Add(propItem);
            }
            foreach (var imgModel in model.Images)
            {
                var imgItem = Mapper.Map<Catalog_Product_Images>(imgModel);
                imgItem.ProductId = exported.Id;
                m_ContentContext.Catalog_Product_Images.Add(imgItem);
            }
            foreach (var relModel in model.Relations)
            {
                var relItem = Mapper.Map<Catalog_Products_Relations>(relModel);
                relItem.GroupId = Guid.NewGuid();
                var thisItem = new Catalog_Products_Relations
                {
                    GroupId = relItem.GroupId,
                    ProductId = exported.Id
                };
                m_ContentContext.Catalog_Products_Relations.Add(relItem);
                m_ContentContext.Catalog_Products_Relations.Add(thisItem);
            }
            m_ContentContext.SaveChanges();
        }

        public void InsertManagerCatalogProductLang(CatalogProductLangViewModel model)
        {
            var item = Mapper.Map<Catalog_Products_Lang>(model);
            m_ContentContext.Catalog_Products_Lang.Add(item);
            foreach (var langModel in model.Properties)
            {
                var lang = Mapper.Map<Catalog_Products_Properties_Lang>(langModel);
                m_ContentContext.Catalog_Products_Properties_Lang.Add(lang);
            }
            m_ContentContext.SaveChanges();
        }
        
        public void UpdateManagerCatalogProductEdit(CatalogProductEditViewModel model)
        {
            var query = from p in m_ContentContext.Catalog_Products
                        where p.Id == model.Id
                        select p;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);

            for (int i = item.Catalog_Products_Properties.Count - 1; i >= 0; i--)
            {
                if (!model.Properties.Any(a => a.Id == item.Catalog_Products_Properties.ToList()[i].Id))
                {
                    var itemProp = item.Catalog_Products_Properties.ToList()[i];
                    for (int k = itemProp.Catalog_Products_Properties_Lang.Count - 1; k >= 0; k--)
                    {
                        var langProp = itemProp.Catalog_Products_Properties_Lang.ToList()[k];
                        m_ContentContext.Catalog_Products_Properties_Lang.Remove(langProp);
                    }
                    m_ContentContext.Catalog_Products_Properties.Remove(itemProp);
                }
                else
                {
                    var itemProp = item.Catalog_Products_Properties.ToList()[i];
                    var modelProp = model.Properties.FirstOrDefault(f => f.Id == itemProp.Id);
                    Mapper.Map(modelProp, itemProp);
                }
            }
            foreach (var propModel in model.Properties)
            {
                if (!item.Catalog_Products_Properties.Any(f => f.Id == propModel.Id && propModel.Id != 0))
                {
                    var propItem = Mapper.Map<Catalog_Products_Properties>(propModel);
                    propItem.ProductId = model.Id;
                    m_ContentContext.Catalog_Products_Properties.Add(propItem);
                }
            }
            
            for (int i = item.Catalog_Product_Images.Count - 1; i >= 0; i--)
            {
                if (model.Images == null || !model.Images.Any(a => a.Id == item.Catalog_Product_Images.ToList()[i].Id))
                {
                    var itemImg = item.Catalog_Product_Images.ToList()[i];
                    m_ContentContext.Catalog_Product_Images.Remove(itemImg);
                }
            }
            foreach (var imgModel in model.Images ?? new List<CatalogProductImagesEditViewModel>())
            {
                if (item.Catalog_Product_Images.Any(f => f.Id == imgModel.Id && imgModel.Id != 0))
                {
                    var imgItem = item.Catalog_Product_Images.FirstOrDefault(f => f.Id == imgModel.Id);
                    Mapper.Map(imgModel, imgItem);
                }
                else
                {
                    var imgItem = Mapper.Map<Catalog_Product_Images>(imgModel);
                    imgItem.ProductId = model.Id;
                    m_ContentContext.Catalog_Product_Images.Add(imgItem);
                }
            }
            var relids = item.Catalog_Products_Relations.Select(s => s.Id).ToArray();
            m_ContentContext.Catalog_Products_Relations.RemoveRange(m_ContentContext.Catalog_Products_Relations.Where(w => relids.Contains(w.Id)).AsEnumerable());
            foreach (var relModel in model.Relations)
            {
                var relItem = Mapper.Map<Catalog_Products_Relations>(relModel);
                relItem.GroupId = Guid.NewGuid();
                var thisItem = new Catalog_Products_Relations
                {
                    GroupId = relItem.GroupId,
                    ProductId = item.Id
                };
                m_ContentContext.Catalog_Products_Relations.Add(relItem);
                m_ContentContext.Catalog_Products_Relations.Add(thisItem);
            }
            m_ContentContext.SaveChanges();
        }

        public void UpdateManagerCatalogProductLang(CatalogProductLangViewModel model)
        {
            var query = from p in m_ContentContext.Catalog_Products
                        where p.Id == model.Id
                        select p;

            var item = query.FirstOrDefault();
            var langItem = item.Catalog_Products_Lang.FirstOrDefault(f => f.Id == model.Id);
            Mapper.Map(model, langItem);
            foreach (var langModel in model.Properties)
            {
                var litem = item.Catalog_Products_Properties.FirstOrDefault(f => f.Id == langModel.RefId).Catalog_Products_Properties_Lang.FirstOrDefault(f => f.Id == langModel.Id);
                Mapper.Map(langModel, litem);
            }
            m_ContentContext.SaveChanges();
        }

        public void DeleteManagerCatalogProductEdit(int id)
        {
            var query = from p in m_ContentContext.Catalog_Products
                        where p.Id == id
                        select p;
            var item = query.FirstOrDefault();
            item.Status = -1;
            m_ContentContext.SaveChanges();
        }
        
        public Catalog_Products GetLayerCatalogProductsItem(int id)
        {
            var query = from p in m_ContentContext.Catalog_Products
                        where p.Id == id
                        select p;
            return query.FirstOrDefault();
        }

        public ProductDetailViewModel GetSiteProductDetail(int id)
        {
            var query = from p in m_ContentContext.Catalog_Products
                        where p.Id == id && p.Status == 1
                        select p;
            var item = query.FirstOrDefault();
            Guid[] groupids = item.Catalog_Products_Relations.Select(s => s.GroupId).ToArray();
            var qrels = from rl in m_ContentContext.Catalog_Products_Relations
                        join rr in m_ContentContext.Catalog_Products_Relations on rl.GroupId equals rr.GroupId
                        join pp in m_ContentContext.Catalog_Products on rr.ProductId equals pp.Id
                        where rr.Id != rl.Id && rl.ProductId == item.Id
                        select pp;
            item.Related_Catalog_Products = qrels.ToList();
            return Mapper.Map<ProductDetailViewModel>(item);
        }

        public int GetLayerMaxInstallment(int[] productIds)
        {
            var query = from p in m_ContentContext.Catalog_Products
                        where productIds.Contains(p.Id)
                        select (int?)p.MaxInstallment;
            return query.Where(w => w > 0).Min<int?>(m => m) ?? 0;
        }

        public List<CatalogProductsExcelModel> GetExcelData(short status, string ProductCode, string Caption, int? Category, int? Brand, int StockMi, int StockMax)
        {
            var query = from p in m_ContentContext.Catalog_Products
                        where (p.Status == status)
                            && (ProductCode == null || ProductCode == "" || ProductCode == p.ProductCode)
                            && (Caption == null || Caption == "" || Caption == p.Title)
                            && (!Category.HasValue || Category.Value == p.CategoryId)
                            && (!Brand.HasValue || Brand.Value == p.BrandId)
                            && (StockMi <= p.Stock)
                            && (StockMax >= p.Stock)
                        select p;
            return Mapper.Map<Catalog_Products[], List<CatalogProductsExcelModel>>(query.ToArray());
        }

        public List<CatalogProductsExcelModel> SaveExcelData(List<CatalogProductsExcelModel> models)
        {
            var lofProducts = m_ContentContext.Catalog_Products.ToList();
            var lofCategories = m_ContentContext.Catalog_Categories.ToList();
            var lofBrands = m_ContentContext.Catalog_Brands.ToList();
            var lofTaxes = m_ContentContext.Tax_Products.ToList();
            List<CatalogProductsExcelModel> retList = new List<CatalogProductsExcelModel>();
            foreach (var model in models)
            {
                if (lofProducts.Any(a => a.ProductCode == model.UrunKodu))
                {
                    var item = lofProducts.FirstOrDefault(f => f.ProductCode == model.UrunKodu);
                    Mapper.Map(model, item);
                    if (model.Kategori != item.Catalog_Categories.Title)
                    {
                        if (lofCategories.Any(a => a.Title == model.Kategori))
                        {
                            item.CategoryId = lofCategories.FirstOrDefault(f => f.Title == model.Kategori).Id;
                        }
                        else
                        {
                            retList.Add(model);
                            continue;
                        }
                    }
                    if (model.Marka != item.Catalog_Brands.Title)
                    {
                        if (!string.IsNullOrEmpty(model.Marka) && lofBrands.Any(a => a.Title == model.Marka))
                        {
                            item.BrandId = lofBrands.FirstOrDefault(f => f.Title == model.Marka).Id;
                        }
                        else if (!string.IsNullOrEmpty(model.Marka))
                        {
                            retList.Add(model);
                        }
                    }
                    if (model.VergiGrubu != item.Tax_Products.Title)
                    {
                        if (lofTaxes.Any(a => a.Title == model.VergiGrubu))
                        {
                            item.TaxGroup = lofTaxes.FirstOrDefault(f => f.Title == model.VergiGrubu).Id;
                        }
                        else
                        {
                            retList.Add(model);
                            continue;
                        }
                    }
                }
                else
                {
                    var item = Mapper.Map<Catalog_Products>(model);
                    if (lofCategories.Any(a => a.Title == model.Kategori))
                    {
                        item.CategoryId = lofCategories.FirstOrDefault(f => f.Title == model.Kategori).Id;
                    }
                    else
                    {
                        retList.Add(model);
                        continue;
                    }
                    if (!string.IsNullOrEmpty(model.Marka) && lofBrands.Any(a => a.Title == model.Marka))
                    {
                        item.BrandId = lofBrands.FirstOrDefault(f => f.Title == model.Marka).Id;
                    }
                    else if (!string.IsNullOrEmpty(model.Marka))
                    {
                        retList.Add(model);
                    }
                    if (lofTaxes.Any(a => a.Title == model.VergiGrubu))
                    {
                        item.TaxGroup = lofTaxes.FirstOrDefault(f => f.Title == model.VergiGrubu).Id;
                    }
                    else
                    {
                        retList.Add(model);
                        continue;
                    }
                    m_ContentContext.Catalog_Products.Add(item);
                }
            }
            m_ContentContext.SaveChanges();
            return retList;
        }

        public List<Areas.Admin.Models.CatalogCommentsListViewModel> GetManagerComments(short status)
        {
            var query = from c in m_ContentContext.Catalog_Product_Comments
                        where c.Status == status
                        select c;
            return Mapper.Map<Catalog_Product_Comments[], List<Areas.Admin.Models.CatalogCommentsListViewModel>>(query.ToArray());
        }

        public Areas.Admin.Models.CatalogCommentsDetailViewModel GetManagerCommentDetail(int id)
        {
            var query = from c in m_ContentContext.Catalog_Product_Comments
                        where c.Id == id
                        select c;
            var item = query.FirstOrDefault();
            return Mapper.Map<Areas.Admin.Models.CatalogCommentsDetailViewModel>(item);
        }

        public void ApproveManagerCommentDetail(int id)
        {
            var query = from c in m_ContentContext.Catalog_Product_Comments
                        where c.Id == id
                        select c;
            var item = query.FirstOrDefault();
            item.Status = 1;
            m_ContentContext.SaveChanges();
        }

        public void DeleteManagerCommentDetail(int id)
        {
            var query = from c in m_ContentContext.Catalog_Product_Comments
                        where c.Id == id
                        select c;
            var item = query.FirstOrDefault();
            m_ContentContext.Catalog_Product_Comments.Remove(item);
            m_ContentContext.SaveChanges();
        }

        public void InsertSiteComments(Models.Site.CommentsPostViewModel model, string userId)
        {
            var item = new Catalog_Product_Comments
            {
                Created = DateTime.Now,
                Description = model.Description,
                ProductId = model.ProductId,
                Rating = model.Rating,
                Status = 0,
                UserId = userId,
                UserTitle = Layers.CustomerLayer.Customer.Title
            };
            m_ContentContext.Catalog_Product_Comments.Add(item);
            m_ContentContext.SaveChanges();
        }

        #endregion

        #region Catalog Brands

        public List<CatalogBrandListViewModel> GetManagerCatalogBrandList(short status)
        {
            var query = from b in m_ContentContext.Catalog_Brands
                        where b.Status == status
                        select b;
            return Mapper.Map<Catalog_Brands[], List<CatalogBrandListViewModel>>(query.ToArray());
        }
        
        public CatalogBrandEditViewModel GetManagerCatalogBrandEdit(int id)
        {
            var query = from b in m_ContentContext.Catalog_Brands
                        where b.Id == id
                        select b;
            var item = query.FirstOrDefault();
            return Mapper.Map<CatalogBrandEditViewModel>(item);
        }

        public CatalogBrandLangViewModel GetManagerCatalogBrandLang(int id, string code)
        {
            var query = from l in m_ContentContext.Catalog_Brands
                        where l.Id == id
                        select l;
            var item = query.FirstOrDefault();
            var lang = item.Catalog_Brands_Lang.Any(a => a.Code == code) ? Mapper.Map<CatalogBrandLangViewModel>(item.Catalog_Brands_Lang.FirstOrDefault(f => f.Code == code)) : Mapper.Map<CatalogBrandLangViewModel>(item);
            lang.Code = code;
            return lang;
        }

        public void InsertManagerCatalogBrandEdit(CatalogBrandEditViewModel model)
        {
            var item = Mapper.Map<Catalog_Brands>(model);
            m_ContentContext.Catalog_Brands.Add(item);
            m_ContentContext.SaveChanges();

        }

        public void InsertManagerCatalogBrandLang(CatalogBrandLangViewModel model)
        {
            var item = Mapper.Map<Catalog_Brands_Lang>(model);
            m_ContentContext.Catalog_Brands_Lang.Add(item);
            m_ContentContext.SaveChanges();
        }

        public void UpdateManagerCatalogBrandEdit(CatalogBrandEditViewModel model)
        {
            var query = from b in m_ContentContext.Catalog_Brands
                        where b.Id == model.Id
                        select b;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_ContentContext.SaveChanges();
        }

        public void UpdateManagerCatalogBrandLang(CatalogBrandLangViewModel model)
        {
            var query = from l in m_ContentContext.Catalog_Brands_Lang
                        where l.Id == model.Id
                        select l;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_ContentContext.SaveChanges();
        }

        public void DeleteManagerCatalogBrandEdit(int id)
        {
            var query = from b in m_ContentContext.Catalog_Brands
                        where b.Id == id
                        select b;
            var item = query.FirstOrDefault();
            if (item.Catalog_Products.Count > 0)
            {
                throw new Exception("Bu markaya ait ürünler var! Öncelikle onlar silinmelidir.");
            }
            item.Status = -1;
            m_ContentContext.SaveChanges();
        }

        #endregion

        #region Catalog Categories

        public List<CatalogCategoryListViewModel> GetManagerCatalogCategoryList(int status)
        {
            var query = from c in m_ContentContext.Catalog_Categories
                        where c.Status == status
                        orderby c.Level, c.Pos, c.ParentId
                        select c;
            var list = query.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                int? parentId = list[i].ParentId;
                list[i].Catalog_Categories_Parent = query.FirstOrDefault(f => f.Id == parentId);
                list[i].rowNr = i;
            }
            return Mapper.Map<Catalog_Categories[], List<CatalogCategoryListViewModel>>(list.ToArray());
        }

        public CatalogCategoryEditViewModel GetManagerCatalogCategoryEdit(int id)
        {
            var query = from c in m_ContentContext.Catalog_Categories
                        where c.Id == id
                        select c;
            var item = query.FirstOrDefault();
            var parent = from c in m_ContentContext.Catalog_Categories
                         where c.Id == item.ParentId
                         select c;
            var parentItem = parent.FirstOrDefault();
            item.Catalog_Categories_Parent = parentItem;
            return Mapper.Map<CatalogCategoryEditViewModel>(item);

        }

        public CatalogCategoryLangViewModel GetManagerCatalogCategoryLang(int id, string code)
        {
            var query = from l in m_ContentContext.Catalog_Categories
                        where l.Id == id
                        select l;
            var item = query.FirstOrDefault();
            var lang = item.Catalog_Categories_Lang.Any(a => a.Code == code) ? Mapper.Map<CatalogCategoryLangViewModel>(item.Catalog_Categories_Lang.FirstOrDefault(f => f.Code == code)) : Mapper.Map<CatalogCategoryLangViewModel>(item);
            lang.Code = code;
            return lang;
        }

        public void InsertManagerCatalogCategoryEdit(CatalogCategoryEditViewModel model)
        {
            var item = Mapper.Map<Catalog_Categories>(model);
            m_ContentContext.Catalog_Categories.Add(item);
            m_ContentContext.SaveChanges();
        }

        public void InsertManagerCatalogCategoryLang(CatalogCategoryLangViewModel model)
        {
            var item = Mapper.Map<Catalog_Categories_Lang>(model);
            m_ContentContext.Catalog_Categories_Lang.Add(item);
            m_ContentContext.SaveChanges();
        }

        public void UpdateManagerCatalogCategoryEdit(CatalogCategoryEditViewModel model)
        {
            var query = from c in m_ContentContext.Catalog_Categories
                        where c.Id == model.Id
                        select c;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_ContentContext.SaveChanges();
        }

        public void UpdateManagerCatalogCategoryLang(CatalogCategoryLangViewModel model)
        {
            var query = from l in m_ContentContext.Catalog_Categories_Lang
                        where l.Id == model.Id
                        select l;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_ContentContext.SaveChanges();
        }

        public void DeleteManagerCatalogCategory(int id)
        {
            var query = from c in m_ContentContext.Catalog_Categories
                        where c.Id == id
                        select c;
            var item = query.FirstOrDefault();
            var childs = from c in m_ContentContext.Catalog_Categories
                         where c.ParentId == id && c.Status != -1
                         select c;
            if (childs.Any())
            {
                throw new Exception("Bu kategoriye ait alt kategoriler var! Lütfen alt kategorileri taşıyın veya silin");
            }
            var prods = from p in m_ContentContext.Catalog_Products
                        where p.CategoryId == id && p.Status != -1
                        select p;
            if (prods.Any())
            {
                throw new Exception("Bu kategoriye ait ürünler var! Lütfen ürünleri farklı kategoriye taşıyın veya silin");
            }
            item.Status = -1;
            m_ContentContext.SaveChanges();
        }

        public Models.Site.CatalogCategoryDetialViewModel GetSiteCatalogCategoryDetail(int id)
        {
            var query = from c in m_ContentContext.Catalog_Categories
                        where c.Id == id
                        select c;
            var item = query.FirstOrDefault();
            return Mapper.Map<Models.Site.CatalogCategoryDetialViewModel>(item);
        }

        public List<CatalogCategoryHomeListViewModel> GetSiteHomeList()
        {
            var query = from c in m_ContentContext.Catalog_Categories
                        where c.Status == 1 && c.IsDisplayInMainPage == true
                        select c;
            return Mapper.Map<Catalog_Categories[], List<CatalogCategoryHomeListViewModel>>(query.ToArray());
        }

        public CatalogCategoryTreeModel GetCategoryTree()
        {
            var query = from c in m_ContentContext.Catalog_Categories
                        where c.Status == 1
                        select c;
            var tree = new CatalogCategoryTreeModel();
            tree.Imports(query.ToList());
            return tree;
        }

        public List<SelectListItem> GetCategorySelectLists()
        {
            var query = from c in m_ContentContext.Catalog_Categories
                        where c.Status == 1
                        select c;
            var result = query.ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in result.Where(w => w.ParentId == null))
            {
                list.Add(new SelectListItem { Text = item.Title, Value = item.Id.ToString() });
                recursiveCategorySelectList(list, item.Id, result);
            }
            return list;
        }

        private void recursiveCategorySelectList(List<SelectListItem> list, int parent, List<Catalog_Categories> cats)
        {
            foreach (var item in cats.Where(w => w.ParentId == parent))
            {
                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = new String('-', item.Level) + item.Title });
                recursiveCategorySelectList(list, item.Id, cats);
            }
        }

        public int[] GetCategorySubsTree(int categoryId)
        {
            var cats = m_ContentContext.Catalog_Categories.ToList();
            List<int> rval = new List<int> { categoryId };
            categorySubs(cats, rval, categoryId);
            return rval.ToArray();
        }

        private void categorySubs(List<Catalog_Categories> list, List<int> tree, int parentId)
        {
            foreach (var item in list.Where(p => p.ParentId == parentId))
            {
                tree.Add(item.Id);
                categorySubs(list, tree, item.Id);
            }
        }

        public List<MenuViewModel> GetMenus(int? parentId, int depth, bool useDisplay)
        {
            var query = from c in m_ContentContext.Catalog_Categories
                        where c.Status == 1
                        select c;
            var cats = query.ToList();
            List<MenuViewModel> list = new List<MenuViewModel>();

            var current = cats.Where(w => w.ParentId == parentId);
            if (!current.Any())
            {
                current = cats.Where(w => w.ParentId == (cats.FirstOrDefault(f => f.Id == parentId).ParentId));
            }

            foreach (var cat in current.Where(w => w.IsDisplayInMenu == true || !useDisplay).ToList())
            {
                var item = new MenuViewModel
                {
                    Id = cat.Id,
                    Title = cat.Title
                };
                if (depth > 0)
                {
                    parseMenu(ref item, cats, depth - 1);
                }
                list.Add(item);
            }
            return list;
        }

        private void parseMenu(ref MenuViewModel menu, List<Catalog_Categories> cats, int depth)
        {
            int menuid = menu.Id;
            foreach (var cat in cats.Where(w => w.ParentId == menuid).ToList())
            {
                var item = new MenuViewModel
                {
                    Id = cat.Id,
                    Title = cat.Title
                };
                if (depth > 0)
                {
                    parseMenu(ref item, cats, depth - 1);
                }
                menu.SubList.Add(item);
            }
        }

        #endregion

        #region Campaigns

        public List<CatalogCampaignsListViewModel> GetManagerCatalogCampaignList(short status)
        {
            var query = from c in m_ContentContext.Catalog_Campaigns
                        where c.Status == status
                        select c;
            return Mapper.Map<Catalog_Campaigns[], List<CatalogCampaignsListViewModel>>(query.ToArray());
        }

        public void InsertManagerCatalogCampaign(CatalogCampaignsEditViewModel model)
        {
            var item = Mapper.Map<Catalog_Campaigns>(model);
            m_ContentContext.Catalog_Campaigns.Add(item);
            m_ContentContext.SaveChanges();
            foreach (var modelSourceProds in model.Sources)
            {
                var itemSource = Mapper.Map<Catalog_Campaigns_Sources>(modelSourceProds);
                itemSource.CampaignId = item.Id;
                m_ContentContext.Catalog_Campaigns_Sources.Add(itemSource);
            }
            if (model.CampaignMethod == 2)
            {
                foreach (var modelDestinationProds in model.Destinations)
                {
                    var itemDestination = Mapper.Map<Catalog_Campaigns_Destinations>(modelDestinationProds);
                    itemDestination.CampaignId = item.Id;
                    m_ContentContext.Catalog_Campaigns_Destinations.Add(itemDestination);
                }
            }
            m_ContentContext.SaveChanges();
        }

        public CatalogCampaignsEditViewModel GetManagerCatalogCampaignsEdit(int id)
        {
            var query = from c in m_ContentContext.Catalog_Campaigns
                        where c.Id == id
                        select c;
            var item = query.FirstOrDefault();
            return Mapper.Map<CatalogCampaignsEditViewModel>(item);
        }

        public void UpdateManagerCatalogCampaignsEdit(CatalogCampaignsEditViewModel model)
        {
            var query = from c in m_ContentContext.Catalog_Campaigns
                        where c.Id == model.Id
                        select c;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            int sourcesCount = item.Catalog_Campaigns_Sources.Count;
            for (int i = 0; i < sourcesCount; i++)
            {
                var itemSource = item.Catalog_Campaigns_Sources.ToList()[0];
                m_ContentContext.Catalog_Campaigns_Sources.Remove(itemSource);
            }
            int destCount = item.Catalog_Campaigns_Destinations.Count;
            for (int i = 0; i < destCount; i++)
            {
                var itemDest = item.Catalog_Campaigns_Destinations.ToList()[0];
                m_ContentContext.Catalog_Campaigns_Destinations.Remove(itemDest);
            }
            foreach (var modelSource in model.Sources)
            {
                var sourceItem = Mapper.Map<Catalog_Campaigns_Sources>(modelSource);
                sourceItem.CampaignId = model.Id;
                m_ContentContext.Catalog_Campaigns_Sources.Add(sourceItem);
            }
            foreach (var modelDest in model.Destinations)
            {
                var destItem = Mapper.Map<Catalog_Campaigns_Destinations>(modelDest);
                destItem.CampaignId = model.Id;
                m_ContentContext.Catalog_Campaigns_Destinations.Add(destItem);
            }
            m_ContentContext.SaveChanges();
        }

        public void DeleteManagerCatalogCampaigns(int id)
        {
            var query = from c in m_ContentContext.Catalog_Campaigns
                        where c.Id == id
                        select c;
            var item = query.FirstOrDefault();
            item.Status = -1;
            m_ContentContext.SaveChanges();
        }

        

        public CampaignDetailViewModel GetSiteCampaignDetail(int id)
        {
            var query = from c in m_ContentContext.Catalog_Campaigns
                        where c.Id == id
                        select c;
            return Mapper.Map<CampaignDetailViewModel>(query.FirstOrDefault());
        }

        public List<CampaignSliderViewModel> GetSiteCampaignSlider()
        {
            var query = from c in m_ContentContext.Catalog_Campaigns
                        where c.Status == 1
                            && c.StartDate <= DateTime.Now
                            && c.EndDate >= DateTime.Now
                            && c.SliderUrl != null
                        select c;
            return Mapper.Map<Catalog_Campaigns[], List<CampaignSliderViewModel>>(query.ToArray());
        }

        #endregion

        #region Process

        public void MultiProductUpdate(short? status, string title, int? category, int? brand, int minStock, int maxStock, decimal? price1rate, decimal? price2rate, decimal? price3rate, decimal? price4rate, decimal? price5rate, int? stock)
        {
            var query = from o in m_ContentContext.Catalog_Products
                        where (status == null || status.Value == o.Status)
                            && (title == null || o.Title.Contains(title))
                            && (category == null || category.Value == o.CategoryId)
                            && (brand == null || brand.Value == o.BrandId)
                            && (minStock < o.Stock) && (maxStock > o.Stock)
                        select o;
            foreach (var item in query.AsEnumerable())
            {
                if (price1rate.HasValue) item.Price1 *= (price1rate.Value + 100) / 100;
                if (price2rate.HasValue && item.Price2.HasValue) item.Price2 *= (price2rate + 100) / 100;
                if (price3rate.HasValue && item.Price3.HasValue) item.Price3 *= (price3rate + 100) / 100;
                if (price4rate.HasValue && item.Price4.HasValue) item.Price4 *= (price4rate + 100) / 100;
                if (price5rate.HasValue && item.Price5.HasValue) item.Price5 *= (price5rate + 100) / 100;
                if (stock.HasValue) item.Stock = stock.Value;
            }
            m_ContentContext.SaveChanges();
        }

        #endregion

        #region Search etc.

        public List<ProductListDisplayModel> Search(string searchText, bool? isfeatured = null, bool? isnew = null, bool? ismain = null, int? category = null)
        {
            searchText = string.IsNullOrWhiteSpace(searchText) ? null : searchText;
            bool workingStock = SettingsLayer.SiteSetting.WorkingStock;
            bool showUnstockItem = SettingsLayer.SiteSetting.ShowUnstockItem;

            int[] ids = null;
            bool idsEscape = true;
            if (category.HasValue)
            {
                var lofids = new List<int>();
                lofids.Add(category.Value);

                var lofCats = (from c in m_ContentContext.Catalog_Categories
                              select c).ToList();
                GetCategoryIds(ref lofids, lofCats, category.Value);
                ids = lofids.ToArray();
                idsEscape = false;
            }
            else
            {
                ids = new int[0];
            }

            var query = from p in m_ContentContext.Catalog_Products
                        where (p.SearchText.Contains(searchText) || p.ProductCode == searchText || searchText == null)
                            && (!workingStock || showUnstockItem || p.Stock > 0)
                            && (p.Status == 1)
                            && (p.IsFeatured == isfeatured || isfeatured == null)
                            && (p.IsNew == isnew || isnew == null)
                            && (p.IsHomepage == ismain || ismain == null)
                            && (idsEscape || ids.Contains(p.CategoryId))
                        select p;

            return Mapper.Map<Catalog_Products[], List<ProductListDisplayModel>>(query.ToArray());

        }

        private void GetCategoryIds(ref List<int> ids, List<Catalog_Categories> cats, int categoryId)
        {
            var items = cats.Where(w => w.ParentId == categoryId).ToList();
            foreach (var item in items)
            {
                ids.Add(item.Id);
                GetCategoryIds(ref ids, cats, item.Id);
            }
        }

        #endregion

    }
}