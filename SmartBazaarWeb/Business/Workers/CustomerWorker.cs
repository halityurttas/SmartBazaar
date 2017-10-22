using AutoMapper;
using SmartBazaar.Data;
using SmartBazaar.Data.Entities;
using SmartBazaar.Web.Areas.Admin.Models;
using SmartBazaar.Web.Business.Layers;
using SmartBazaar.Web.Models.Layer;
using SmartBazaar.Web.Models.Site;
using System.Collections.Generic;
using System.Linq;

namespace SmartBazaar.Web.Business.Workers
{
    public class CustomerWorker
    {
        private readonly ContentContext m_ContentContext;
        public CustomerWorker()
        {
            m_ContentContext = new ContentContext();
        }

        #region Customer Entities

        public List<CustomerEntityListViewModel> GetManagerCustomerEntityList(short status, string title = null)
        {
            title = title == "" ? null : title;
            var query = from e in m_ContentContext.Customer_Entities
                        where e.Status == status && (e.Title == title || title == null)
                        select e;
            return Mapper.Map<Customer_Entities[], List<CustomerEntityListViewModel>>(query.ToArray());
        }

        public CustomerEntityEditViewModel GetManagerCustomerEntityEdit(int id)
        {
            var query = from e in m_ContentContext.Customer_Entities
                        where e.Id == id
                        select e;
            var item = query.FirstOrDefault();
            return Mapper.Map<CustomerEntityEditViewModel>(item);
        }

        public void UpdateManagerCustomerEntityEdit(CustomerEntityEditViewModel model)
        {
            var query = from e in m_ContentContext.Customer_Entities
                        where e.Id == model.Id
                        select e;
            var item = query.FirstOrDefault();
            item.BirthDate = model.BirthDate;
            item.Title = model.Caption;
            item.Company = model.Company;
            item.ContactPhone = model.ContactPhone;
            item.CustomerType = model.CustomerType;
            item.Gender = model.Gender;
            item.GroupId = model.GroupId;
            item.Status = model.Status;
            item.TaxNr = model.TaxNr;
            item.TaxOffice = model.TaxOffice;
            m_ContentContext.SaveChanges();
        }

        public CustomerEntityDetailViewModel GetManagerCustomerEntityDetail(int id)
        {
            var query = from e in m_ContentContext.Customer_Entities
                        where e.Id == id
                        select e;
            var model = query.FirstOrDefault();
            model.Email = GetCustomerUser(model.UserId).Email;
            return Mapper.Map<CustomerEntityDetailViewModel>(model);
        }

        #endregion

        #region Customer Groups

        public List<CustomerGroupListViewModel> GetCustomerGroupList(short status)
        {
            var query = from g in m_ContentContext.Customer_Groups
                        where g.Status == status
                        select g;
            return Mapper.Map<Customer_Groups[], List<CustomerGroupListViewModel>>(query.ToArray());
        }

        public void InsertCustomerGroupEdit(CustomerGroupEditViewModel model)
        {
            var item = Mapper.Map<Customer_Groups>(model);
            m_ContentContext.Customer_Groups.Add(item);
            m_ContentContext.SaveChanges();
        }

        public CustomerGroupEditViewModel GetManagerCustomerGroupEdit(int id)
        {
            var query = from g in m_ContentContext.Customer_Groups
                        where g.Id == id
                        select g;
            var item = query.FirstOrDefault();
            return Mapper.Map<CustomerGroupEditViewModel>(item);
        }

        public void UpdateManagerCustomerGroupEdit(CustomerGroupEditViewModel model)
        {
            var query = from g in m_ContentContext.Customer_Groups
                        where g.Id == model.Id
                        select g;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_ContentContext.SaveChanges();
        }

        public void DeleteManagerCustomerGroupEdit(int id)
        {
            var query = from g in m_ContentContext.Customer_Groups
                        where g.Id == id
                        select g;
            var item = query.FirstOrDefault();
            item.Status = -1;
            m_ContentContext.SaveChanges();
        }

        #endregion

        #region Customer Address

        public List<CustomerAddressListViewModel> GetManagerCustomerAddresses(int customerId, short status)
        {
            var query = from a in m_ContentContext.Customer_Addresses
                        where a.CustomerId == customerId && a.Status == status
                        select a;
            return Mapper.Map<Customer_Addresses[], List<CustomerAddressListViewModel>>(query.ToArray());
        }

        public CustomerAddressEditViewModel GetManagerCustomerAddress(int id)
        {
            var query = from a in m_ContentContext.Customer_Addresses
                        where a.Id == id
                        select a;
            var item = query.FirstOrDefault();
            return Mapper.Map<CustomerAddressEditViewModel>(item);
        }

        public void InsertManagerCustomerAddress(CustomerAddressEditViewModel model)
        {
            var item = Mapper.Map<Customer_Addresses>(model);
            m_ContentContext.Customer_Addresses.Add(item);
            m_ContentContext.SaveChanges();
        }

        public void UpdateManagerCustomerAddress(CustomerAddressEditViewModel model)
        {
            var query = from a in m_ContentContext.Customer_Addresses
                        where a.Id == model.Id
                        select a;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_ContentContext.SaveChanges();
        }

        public void DeleteCustomerAddress(int id)
        {
            var query = from a in m_ContentContext.Customer_Addresses
                        where a.Id == id
                        select a;
            var item = query.FirstOrDefault();
            item.Status = -1;
            m_ContentContext.SaveChanges();
        }

        public List<CustomerAddressViewModel> GetSiteCustomerAddresses(int customerId, short status)
        {
            var query = from a in m_ContentContext.Customer_Addresses
                        where a.CustomerId == customerId && a.Status == status
                        select a;
            return Mapper.Map<Customer_Addresses[], List<CustomerAddressViewModel>>(query.ToArray());
        }

        public CustomerAddressViewModel GetSiteCustomerAddress(int id)
        {
            var query = from a in m_ContentContext.Customer_Addresses
                        where a.Id == id && a.CustomerId == CustomerLayer.Customer.Id
                        select a;
            return Mapper.Map<CustomerAddressViewModel>(query.FirstOrDefault());
        }

        public void UpdateSiteCustomerAddress(CustomerAddressViewModel model)
        {
            var item = m_ContentContext.Customer_Addresses.FirstOrDefault(f => f.Id == model.Id);
            Mapper.Map(model, item);
            m_ContentContext.SaveChanges();
        }

        public void InsertSiteCustomerAddress(CustomerAddressViewModel model)
        {
            var item = Mapper.Map<Customer_Addresses>(model);
            m_ContentContext.Customer_Addresses.Add(item);
            m_ContentContext.SaveChanges();
        }

        #endregion

        #region Account

        public Models.Ident.ApplicationUser GetCustomerUser(string id)
        {
            var userCtx = new Models.Ident.ApplicationDbContext();
            var query = from u in userCtx.Users
                        where u.Id == id
                        select u;
            return query.FirstOrDefault();
        }

        public CustomerStorageModel GetCustomerStorage(string userId)
        {
            var query = from c in m_ContentContext.Customer_Entities
                        where c.UserId == userId
                        select c;
            var item = query.FirstOrDefault();
            short priceIndex = 1;
            if (item.Customer_Groups != null)
            {
                priceIndex = item.Customer_Groups.PriceIndex;
            }
            return new CustomerStorageModel
            {
                Id = item.Id,
                GroupId = item.GroupId,
                Title = item.Title,
                PriceIndex = priceIndex
            };
        }

        public void CustomerRegister(CustomerRegisterViewModel model)
        {
            var item = Mapper.Map<Customer_Entities>(model);
            item.Status = 1;
            m_ContentContext.Customer_Entities.Add(item);
            m_ContentContext.SaveChanges();
        }

        #endregion

    }
}