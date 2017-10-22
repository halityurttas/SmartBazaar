using SmartBazaar.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SmartBazaar.Data.Entities;

namespace SmartBazaar.Web.Business.Workers
{
    public class SliderWorker
    {
        private readonly ContentContext m_contentContext;

        public SliderWorker()
        {
            m_contentContext = new ContentContext();
        }

        public List<Areas.Admin.Models.SliderListViewModel> GetManagerSliders(short? status)
        {
            var query = from s in m_contentContext.Slider
                        where status.HasValue && status.Value == s.Status
                        select s;
            return Mapper.Map<Slider[], List<Areas.Admin.Models.SliderListViewModel>>(query.ToArray());
        }

        public Areas.Admin.Models.SliderEditViewModel GetManagerSlider(int id)
        {
            var query = from s in m_contentContext.Slider
                        where s.Id == id
                        select s;
            var item = query.FirstOrDefault();
            return Mapper.Map<Areas.Admin.Models.SliderEditViewModel>(item);
        }

        public void SaveManagerSlider(Areas.Admin.Models.SliderEditViewModel model)
        {
            var item = Mapper.Map<Slider>(model);
            m_contentContext.Slider.Add(item);
            m_contentContext.SaveChanges();
        }

        public void UpdateManagerSlider(Areas.Admin.Models.SliderEditViewModel model)
        {
            var query = from s in m_contentContext.Slider
                        where s.Id == model.Id
                        select s;
            var item = query.FirstOrDefault();
            Mapper.Map(model, item);
            m_contentContext.SaveChanges();
        }

        public void DeleteManagerSlider(int id)
        {
            var query = from s in m_contentContext.Slider
                        where s.Id == id
                        select s;
            var item = query.FirstOrDefault();
            m_contentContext.Slider.Remove(item);
            m_contentContext.SaveChanges();
        }

        public List<Models.Site.SliderSliderViewModel> GetSiteSliders()
        {
            var query = from s in m_contentContext.Slider
                        where s.Status == 1
                        select s;
            return Mapper.Map<Slider[], List<Models.Site.SliderSliderViewModel>>(query.ToArray());
        }

        public Models.Site.SliderDetailViewModel GetSiteSliderDetail(int id)
        {
            var query = from s in m_contentContext.Slider
                        where s.Id == id
                        select s;
            var item = query.FirstOrDefault();
            return Mapper.Map<Models.Site.SliderDetailViewModel>(item);
        }

    }
}