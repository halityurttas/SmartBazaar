using AutoMapper;
using SmartBazaar.Data;
using SmartBazaar.Data.Entities;
using SmartBazaar.Web.Areas.Admin.Models;
using SmartBazaar.Web.Models.Site;
using System.Collections.Generic;
using System.Linq;

namespace SmartBazaar.Web.Business.Workers
{
    public class ShipmentWorker
    {
        private readonly ContentContext m_ConentContext;
        public ShipmentWorker()
        {
            m_ConentContext = new ContentContext();
        }

        public List<ShipmentTypesListViewModel> GetManagerShipmentTypesList(short status)
        {
            var query = from s in m_ConentContext.Shipment_Types
                        where s.Status == status
                        select s;
            return Mapper.Map<Shipment_Types[], List<ShipmentTypesListViewModel>>(query.ToArray());
        }

        public ShipmentTypesEditViewModel GetManagerShipmentTypesEdit(int id)
        {
            var query = from s in m_ConentContext.Shipment_Types
                        where s.Id == id
                        select s;
            var item = query.FirstOrDefault();
            return Mapper.Map<ShipmentTypesEditViewModel>(item);
        }

        public ShipmentTypesLangViewModel GetManagerShipmentTypesLang(int id, string code)
        {
            var query = from s in m_ConentContext.Shipment_Types
                        where s.Id == id
                        select s;
            var item = query.FirstOrDefault();
            return item.Shipment_Types_Lang.Any(a => a.Code == code) ? Mapper.Map<ShipmentTypesLangViewModel>(item.Shipment_Types_Lang.FirstOrDefault(f => f.Code == code)) : Mapper.Map<ShipmentTypesLangViewModel>(item);
        }

        public void InsertManagerShipmentTypesEdit(ShipmentTypesEditViewModel model)
        {
            var item = Mapper.Map<Shipment_Types>(model);
            m_ConentContext.Shipment_Types.Add(item);
            m_ConentContext.SaveChanges();
        }

        public void InsertManagerShipmentTypesLang(ShipmentTypesLangViewModel model)
        {
            var item = Mapper.Map<Shipment_Types_Lang>(model);
            m_ConentContext.Shipment_Types_Lang.Add(item);
            m_ConentContext.SaveChanges();
        }

        public void UpdateManagerShipmentTypesEdit(ShipmentTypesEditViewModel model)
        {
            var query = from s in m_ConentContext.Shipment_Types
                        where s.Id == model.Id
                        select s;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_ConentContext.SaveChanges();
        }

        public void UpdateManagerShipmentTypesLang(ShipmentTypesLangViewModel model)
        {
            var query = from l in m_ConentContext.Shipment_Types_Lang
                        where l.Id == model.Id
                        select l;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_ConentContext.SaveChanges();
        }

        public void DeleteManagerShipmentTypes(int id)
        {
            var query = from s in m_ConentContext.Shipment_Types
                        where s.Id == id
                        select s;
            var item = query.FirstOrDefault();
            item.Status = -1;
            m_ConentContext.SaveChanges();
        }

        public List<ShipmentTypeViewModel> GetSiteShipmentTypes()
        {
            var query = from s in m_ConentContext.Shipment_Types
                        where s.Status == 1
                        select s;
            return Mapper.Map<Shipment_Types[], List<ShipmentTypeViewModel>>(query.ToArray());
        }

        public ShipmentTypeViewModel GetSiteShipmentType(int id)
        {
            var query = from s in m_ConentContext.Shipment_Types
                        where s.Id == id
                        select s;
            return Mapper.Map<ShipmentTypeViewModel>(query.FirstOrDefault());
        }
    }
}