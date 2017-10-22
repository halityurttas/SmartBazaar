using AutoMapper;
using SmartBazaar.Data;
using SmartBazaar.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Business.Workers
{
    public class LangWorker
    {
        private readonly ContentContext m_contentContext;

        public LangWorker()
        {
            m_contentContext = new ContentContext();
        }

        public List<Areas.Admin.Models.LangBooksListViewModel> GetManagerLangBooksList(short status) 
        {
            var query = from b in m_contentContext.Lang_Books
                        where b.Status == status
                        select b;
            return Mapper.Map<Lang_Books[], List<Areas.Admin.Models.LangBooksListViewModel>>(query.ToArray());
        }

        public Areas.Admin.Models.LangBooksEditViewModel GetManagerLangBook(int id)
        {
            var query = from b in m_contentContext.Lang_Books
                        where b.Id == id
                        select b;
            return Mapper.Map<Areas.Admin.Models.LangBooksEditViewModel>(query.FirstOrDefault());
        }

        public void InsertManagerLangBook(Areas.Admin.Models.LangBooksEditViewModel model)
        {
            var item = Mapper.Map<Lang_Books>(model);
            m_contentContext.Lang_Books.Add(item);
            m_contentContext.SaveChanges();
        }

        public void UpdateManagerLangBook(Areas.Admin.Models.LangBooksEditViewModel model)
        {
            var query = from b in m_contentContext.Lang_Books
                        where b.Id == model.Id
                        select b;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_contentContext.SaveChanges();
        }

        public List<Areas.Admin.Models.LangDictionaryListViewModel> GetManagerLangDictionaryList(int bookId)
        {
            var query = from d in m_contentContext.Lang_Dictionary
                        where d.BookId == bookId
                        select d;
            return Mapper.Map<Lang_Dictionary[], List<Areas.Admin.Models.LangDictionaryListViewModel>>(query.ToArray());
        }

        public void InsertOrUpdateManagerLangDictionary(List<Areas.Admin.Models.LangDictionaryListViewModel> models)
        {
            int bookId = models.FirstOrDefault().BookId;
            var query = from d in m_contentContext.Lang_Dictionary
                        where d.BookId == bookId
                        select d;
            foreach (var model in models)
            {
                var item = query.FirstOrDefault(f => f.Id == model.Id);
                if (item == null)
                {
                    item = Mapper.Map<Lang_Dictionary>(model);
                    m_contentContext.Lang_Dictionary.Add(item);
                }
                else
                {
                    Mapper.Map(model, item);
                }
            }
            m_contentContext.SaveChanges();
        }
        
    }
}