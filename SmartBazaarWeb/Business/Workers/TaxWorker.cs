using SmartBazaar.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SmartBazaar.Data;
using SmartBazaar.Data.Entities;

namespace SmartBazaar.Web.Business.Workers
{
    public class TaxWorker
    {
        private readonly ContentContext m_ContentContext;
        public TaxWorker()
        {
            m_ContentContext = new ContentContext();
        }

        public List<ProductTaxListViewModel> GetManagerProductTaxList(short status)
        {
            var query = from t in m_ContentContext.Tax_Products
                        where t.Status == status
                        select t;
            return Mapper.Map<Tax_Products[], List<ProductTaxListViewModel>>(query.ToArray());
        }

        public void InsertManagerProductTaxEdit(ProductTaxEditViewModel model)
        {
            var item = Mapper.Map<Tax_Products>(model);
            m_ContentContext.Tax_Products.Add(item);
            m_ContentContext.SaveChanges();
        }

        public ProductTaxEditViewModel GetManagerProductTaxEdit(int id)
        {
            var query = from t in m_ContentContext.Tax_Products
                        where t.Id == id
                        select t;
            var item = query.FirstOrDefault();

            return Mapper.Map<ProductTaxEditViewModel>(item);

        }

        public void UpdateManagerProductTaxEdit(ProductTaxEditViewModel model)
        {
            var query = from t in m_ContentContext.Tax_Products
                        where t.Id == model.Id
                        select t;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_ContentContext.SaveChanges();
        }

        public void DeleteProductTax(int id)
        {
            var query = from t in m_ContentContext.Tax_Products
                        where t.Id == id
                        select t;
            var item = query.FirstOrDefault();
            if (item.Catalog_Products.Count > 0)
            {
                throw new Exception("Bu vergi oranında ürünler var! Lütfen ürünlerden vergi oranını kaldırın.");
            }
            item.Status = -1;
            m_ContentContext.SaveChanges();
        }
    }
}