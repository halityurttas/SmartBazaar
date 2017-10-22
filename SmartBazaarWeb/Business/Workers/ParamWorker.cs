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
    public class ParamWorker
    {
        private readonly ContentContext m_contentContext;
        public ParamWorker()
        {
            m_contentContext = new ContentContext();
        }

        public List<ParamsGroupsListViewModel> GetParamsGroupsListViewModel()
        {
            var query = from g in m_contentContext.Params_Groups
                        select g;
            return Mapper.Map<Params_Groups[], List<ParamsGroupsListViewModel>>(query.ToArray());
        }

        public List<ParamsListViewModel> GetParamsListViewModel(int groupId)
        {
            var query = from p in m_contentContext.Params
                        where p.GroupId == groupId
                        select p;
            return Mapper.Map<Params[], List<ParamsListViewModel>>(query.ToArray());
        }

        public string GetParamValue(int id)
        {
            var query = from p in m_contentContext.Params
                        where p.Id == id
                        select p;
            var item = query.FirstOrDefault();
            if (item == null)
            {
                return null;
            }
            else
            {
                return item.Value;
            }
        }

        public void Update(int id, string value)
        {
            var query = from p in m_contentContext.Params
                        where p.Id == id
                        select p;
            var item = query.FirstOrDefault();
            item.Value = value;
            m_contentContext.SaveChanges();
        }
    }
}