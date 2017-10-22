using AutoMapper;
using SmartBazaar.Data;
using SmartBazaar.Data.Entities;
using SmartBazaar.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Business.Workers
{
    public class PageWorker
    {
        private ContentContext m_contentContext;
        public PageWorker()
        {
            m_contentContext = new ContentContext();
        }

        public List<PagesListViewModel> GetManagerPageses(short status = 1)
        {
            var query = from p in m_contentContext.Pages
                        where p.Status == status
                        select p;
            return Mapper.Map<Pages[], List<PagesListViewModel>>(query.ToArray());
        }

        public PagesEditViewModel GetManagerPages(int id)
        {
            var query = from p in m_contentContext.Pages
                        where p.Id == id
                        select p;
            return Mapper.Map<PagesEditViewModel>(query.FirstOrDefault());
        }

        public PagesLangViewModel GetManagerPageLang(int id, string code)
        {
            var query = from l in m_contentContext.Pages
                        where l.Id == id
                        select l;
            var item = query.FirstOrDefault();
            return item.Pages_Lang.Any(a => a.Code == code) ? Mapper.Map<PagesLangViewModel>(item.Pages_Lang.FirstOrDefault(f => f.Code == code)) : Mapper.Map<PagesLangViewModel>(item);
        }

        public void InsertManagerPages(PagesEditViewModel model)
        {
            var item = Mapper.Map<Pages>(model);
            
            var qryNr = m_contentContext.Pages.Select(s => (int?)s.Id).Max(m => m);
            
            item.Id = (qryNr ?? 0) + 1;
            m_contentContext.Pages.Add(item);
            m_contentContext.SaveChanges();
        }

        public void InsertManagerPageLang(PagesLangViewModel model)
        {
            var item = Mapper.Map<Pages_Lang>(model);
            m_contentContext.Pages_Lang.Add(item);
            m_contentContext.SaveChanges();
        }

        public void UpdateManagerPages(PagesEditViewModel model)
        {
            var query = from p in m_contentContext.Pages
                        where p.Id == model.Id
                        select p;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_contentContext.SaveChanges();
        }

        public void UpdateManagerPageLang(PagesLangViewModel model)
        {
            var query = from l in m_contentContext.Pages_Lang
                        where l.Id == model.Id
                        select l;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_contentContext.SaveChanges();
        }

        public void DeleteManagerPages(int id)
        {
            var query = from p in m_contentContext.Pages
                        where p.Id == id
                        select p;
            var item = query.FirstOrDefault();
            if (item.IsFixed)
            {
                return;
            }
            for (int i = item.Pages_Lang.Count - 1; i >= 0; i--)
            {
                var lang = item.Pages_Lang.ToArray()[i];
                m_contentContext.Pages_Lang.Remove(lang);
            }
            m_contentContext.Pages.Remove(item);
            m_contentContext.SaveChanges();
        }

        public Models.Site.PageDetailViewModel GetSitePage(int id)
        {
            var query = from p in m_contentContext.Pages
                        where p.Id == id
                        select p;
            var item = query.FirstOrDefault();
            return Mapper.Map<Models.Site.PageDetailViewModel>(item);
        }

        public List<Models.Site.PageMenuViewModel> GetPageMenus()
        {
            var query = from p in m_contentContext.Pages
                        where !p.IsFixed
                        select p;
            return Mapper.Map<Pages[], List<Models.Site.PageMenuViewModel>>(query.ToArray());
        }

    }
}