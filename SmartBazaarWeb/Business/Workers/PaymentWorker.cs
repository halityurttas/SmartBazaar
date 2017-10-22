using AutoMapper;
using SmartBazaar.Data;
using SmartBazaar.Data.Entities;
using SmartBazaar.Web.Areas.Admin.Models;
using SmartBazaar.Web.Models.Site;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace SmartBazaar.Web.Business.Workers
{
    public class PaymentWorker
    {
        private readonly ContentContext m_ContentContext;
        public PaymentWorker()
        {
            m_ContentContext = new ContentContext();
        }

        #region Payment Types

        public List<PaymentTypesListViewModel> GetManagerPaymentTypesList(short status)
        {
            var query = from p in m_ContentContext.Payment_Types
                        where p.Status == status
                        select p;
            return Mapper.Map<Payment_Types[], List<PaymentTypesListViewModel>>(query.ToArray());
        }
        
        public PaymentTypesEditViewModel GetManagerPaymentTypesEdit(int id)
        {
            var query = from p in m_ContentContext.Payment_Types
                        where p.Id == id
                        select p;
            var item = query.FirstOrDefault();
            return Mapper.Map<PaymentTypesEditViewModel>(item);
        }

        public PaymentTypesLangViewModel GetManagerPaymentTypesLang(int id, string code)
        {
            var query = from p in m_ContentContext.Payment_Types
                        where p.Id == id
                        select p;
            var item = query.FirstOrDefault();
            return item.Payment_Types_Lang.Any(a => a.Code == code) ? Mapper.Map<PaymentTypesLangViewModel>(item.Payment_Types_Lang.FirstOrDefault(f => f.Code == code)) : Mapper.Map<PaymentTypesLangViewModel>(item);
        }

        public void InsertManagerPaymentTypesEdit(PaymentTypesEditViewModel model)
        {
            var item = Mapper.Map<Payment_Types>(model);
            m_ContentContext.Payment_Types.Add(item);
            m_ContentContext.SaveChanges();
            foreach (var modelInst in model.PaymentInstallments)
            {
                var itemInst = Mapper.Map<Payment_Installment>(modelInst);
                itemInst.PaymentId = item.Id;
                m_ContentContext.Payment_Installment.Add(itemInst);
            }
            m_ContentContext.SaveChanges();
        }

        public void InsertManagerPaymentTypesLang(PaymentTypesLangViewModel model)
        {
            var item = Mapper.Map<Payment_Types_Lang>(model);
            m_ContentContext.Payment_Types_Lang.Add(item);
            m_ContentContext.SaveChanges();
        }

        public void UpdateManagerPaymentTypesEdit(PaymentTypesEditViewModel model)
        {
            var query = from p in m_ContentContext.Payment_Types
                        where p.Id == model.Id
                        select p;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            if (model.PaymentInstallments == null)
            {
                foreach (var itemPayment in item.Payment_Installment)
                {
                    m_ContentContext.Payment_Installment.Remove(itemPayment);
                }
            }
            else
            {
                foreach (var modelInst in model.PaymentInstallments)
                {
                    var itemInst = item.Payment_Installment.FirstOrDefault(f => f.Id == modelInst.Id);
                    if (itemInst == null)
                    {
                        itemInst = Mapper.Map<Payment_Installment>(modelInst);
                        itemInst.PaymentId = item.Id;
                        m_ContentContext.Payment_Installment.Add(itemInst);
                    }
                    else
                    {
                        Mapper.Map(modelInst, itemInst);
                    }
                }
                var deleteList = item.Payment_Installment.Where(w => !model.PaymentInstallments.Select(s => s.Id).Contains(w.Id)).ToList();
                int deleteListCount = deleteList.Count;
                for (int i = 0; i < deleteListCount; i++)
                {
                    m_ContentContext.Payment_Installment.Remove(deleteList[0]);
                }
            }
            m_ContentContext.SaveChanges();
        }

        public void UpdateManagerPaymentTypesLang(PaymentTypesLangViewModel model)
        {
            var query = from l in m_ContentContext.Payment_Types_Lang
                        where l.Id == model.Id
                        select l;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_ContentContext.SaveChanges();
        }

        public void DeleteManagerPaymentTypesDelete(int id)
        {
            var query = from p in m_ContentContext.Payment_Types
                        where p.Id == id
                        select p;
            var item = query.FirstOrDefault();
            item.Status = -1;
            m_ContentContext.SaveChanges();
        }

        public List<Models.Site.PaymentTypeViewModel> GetSitePaymentTypes()
        {
            var query = from p in m_ContentContext.Payment_Types
                        where p.Status == 1
                        select p;
            return Mapper.Map<Payment_Types[], List<Models.Site.PaymentTypeViewModel>>(query.ToArray());
        }

        public Models.Site.PaymentTypeViewModel GetSitePaymentType(int id)
        {
            var query = from p in m_ContentContext.Payment_Types
                        where p.Id == id
                        select p;
            return Mapper.Map<Models.Site.PaymentTypeViewModel>(query.FirstOrDefault());
        }

        #endregion

        #region Payment Entities

        public List<PaymentEntitiesListViewModel> GetManagerPaymentEntitiesList(short status)
        {
            var query = from p in m_ContentContext.Payment_Entities
                        where p.Status == status
                        select p;
            return Mapper.Map<Payment_Entities[], List<PaymentEntitiesListViewModel>>(query.ToArray());
        }

        public PaymentEntitiesEditViewModel GetManagerPaymentEntitesEdit(int id)
        {
            var query = from p in m_ContentContext.Payment_Entities
                        where p.Id == id
                        select p;
            var item = query.FirstOrDefault();
            return Mapper.Map<PaymentEntitiesEditViewModel>(item);
        }

        public PaymentEntitiesEditViewModel GetManagerPaymentEntitiesByOrder(int id)
        {
            var query = from p in m_ContentContext.Payment_Entities
                        where p.OrderId == id
                        select p;
            var item = query.FirstOrDefault();
            return Mapper.Map<PaymentEntitiesEditViewModel>(item);
        }

        public List<Payment_Entities> GetManagerPaymentsByOrderId(int id)
        {
            var query = from p in m_ContentContext.Payment_Entities
                        where p.OrderId == id
                        select p;
            return query.ToList();
        }

        public void UpdatePaymentStatus(int id, short status, string log)
        {
            var query = from p in m_ContentContext.Payment_Entities
                        where p.Id == id
                        select p;
            var item = query.FirstOrDefault();
            item.Status = status;
            if (!string.IsNullOrEmpty(log)) item.Log = log;
            m_ContentContext.SaveChanges();
        }

        public int InsertSitePaymentEntity(PaymentEntityViewModel model)
        {
            var item = Mapper.Map<Payment_Entities>(model);
            m_ContentContext.Payment_Entities.Add(item);
            try
            {
                m_ContentContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            
            return item.Id;
        }

        #endregion

    }
}