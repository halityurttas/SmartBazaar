using AutoMapper;
using SmartBazaar.Data;
using SmartBazaar.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Business.Workers
{
    public class HtmlBlocksWorker
    {
        private readonly ContentContext m_contentContext;
        public HtmlBlocksWorker()
        {
            m_contentContext = new ContentContext();
        }

        public List<Areas.Admin.Models.HtmlBlocksListViewModel> GetManagerHtmlBlocksList(short status)
        {
            var model = from b in m_contentContext.HtmlBlocks
                        where b.Status == status
                        select b;
            return Mapper.Map<HtmlBlocks[], List<Areas.Admin.Models.HtmlBlocksListViewModel>>(model.ToArray());
        }

        public Areas.Admin.Models.HtmlBlocksEditViewModel GetManagerHtmlBlocksEdit(int id)
        {
            var model = from b in m_contentContext.HtmlBlocks
                        where b.Id == id
                        select b;
            return Mapper.Map<Areas.Admin.Models.HtmlBlocksEditViewModel>(model.FirstOrDefault());
        }

        public Areas.Admin.Models.HtmlBlocksLangViewModel GetManagerHtmlBlocksLang(int id, string code)
        {
            var model = from l in m_contentContext.HtmlBlocks
                        where l.Id == id
                        select l;
            var item = model.FirstOrDefault();
            return item.HtmlBlocks_Lang.Any(a => a.Code == code) ? Mapper.Map<Areas.Admin.Models.HtmlBlocksLangViewModel>(item.HtmlBlocks_Lang.FirstOrDefault(f => f.Code == code)) : Mapper.Map<Areas.Admin.Models.HtmlBlocksLangViewModel>(item);
        }

        public void InsertManagerHtmlBlocks(Areas.Admin.Models.HtmlBlocksEditViewModel model)
        {
            var item = Mapper.Map<HtmlBlocks>(model);
            m_contentContext.HtmlBlocks.Add(item);
            m_contentContext.SaveChanges();
        }

        public void InsertManagerHtmlBlocksLang(Areas.Admin.Models.HtmlBlocksLangViewModel model)
        {
            var item = Mapper.Map<HtmlBlocks_Lang>(model);
            m_contentContext.HtmlBlocks_Lang.Add(item);
            m_contentContext.SaveChanges();
        }

        public void UpdateMangerHtmlBlocks(Areas.Admin.Models.HtmlBlocksEditViewModel model)
        {
            var query = from b in m_contentContext.HtmlBlocks
                        where b.Id == model.Id
                        select b;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_contentContext.SaveChanges();
        }

        public void UpdateManagerHtmlBlocksLang(Areas.Admin.Models.HtmlBlocksLangViewModel model)
        {
            var query = from l in m_contentContext.HtmlBlocks_Lang
                        where l.Id == model.Id
                        select l;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_contentContext.SaveChanges();
        }

        public void DeleteManagerHtmlBlocks(int id)
        {
            var query = from b in m_contentContext.HtmlBlocks
                        where b.Id == id
                        select b;
            var item = query.FirstOrDefault();
            for (int i = item.HtmlBlocks_Lang.Count - 1; i >= 0; i--)
            {
                var lang = item.HtmlBlocks_Lang.ToArray()[i];
                m_contentContext.HtmlBlocks_Lang.Remove(lang);
            }
            m_contentContext.HtmlBlocks.Remove(item);
            m_contentContext.SaveChanges();
        }

        public List<Models.Site.HtmlBlocksViewModel> GetSiteLocationBlocks(int id)
        {
            var query = from b in m_contentContext.HtmlBlocks
                        where b.Location == id && b.Status == 1
                        select b;
            return Mapper.Map<HtmlBlocks[], List<Models.Site.HtmlBlocksViewModel>>(query.ToArray());
        }
    }
}