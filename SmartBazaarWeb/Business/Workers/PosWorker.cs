using SmartBazaar.Data;
using SmartBazaar.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SmartBazaar.Data.Entities;
using SmartBazaar.Web.Models.Site;

namespace SmartBazaar.Web.Business.Workers
{
    public class PosWorker
    {
        private readonly ContentContext m_contentContext;
        public PosWorker()
        {
            m_contentContext = new ContentContext();
        }

        public List<PosSettingsListViewModel> GetManagerPosSettings(short? status)
        {
            var query = from p in m_contentContext.Pos_Settings
                        where !status.HasValue || status.Value == p.Status
                        select p;
            return Mapper.Map<Pos_Settings[], List<PosSettingsListViewModel>>(query.ToArray());
        }

        public PosSettingsDetailViewModel GetManagerPosSettingDetail(int id)
        {
            var query = from p in m_contentContext.Pos_Settings
                        where p.Id == id
                        select p;
            var item = query.FirstOrDefault();
            return Mapper.Map<PosSettingsDetailViewModel>(item);
        }

        public PosSettingViewModel GetSitePosSetting(string posType)
        {
            var query = from p in m_contentContext.Pos_Settings
                        where p.Status == 1 && p.Referance == posType
                        select p;
            var item = query.FirstOrDefault();
            return Mapper.Map<PosSettingViewModel>(item);
        }

        public Dictionary<string, string> GetPosForList()
        {
            var query = from p in m_contentContext.Pos_Settings
                        where p.Status == 1
                        select p;
            var rval = new Dictionary<string, string>();
            foreach (var item in query.AsEnumerable())
            {
                rval.Add(item.Title, item.Referance);
            }
            return rval;
        }

        public void UpdatePosSettings(PosSettingsDetailViewModel model)
        {
            var query = from p in m_contentContext.Pos_Settings
                        where p.Id == model.Id
                        select p;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_contentContext.SaveChanges();
        }
    }
}