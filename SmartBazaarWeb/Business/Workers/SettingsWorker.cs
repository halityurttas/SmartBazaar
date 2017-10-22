using SmartBazaar.Data;
using SmartBazaar.Web.Areas.Admin.Models;
using System.Linq;

namespace SmartBazaar.Web.Business.Workers
{
    public class SettingsWorker
    {
        private readonly ContentContext m_ContentContext;
        public SettingsWorker()
        {
            m_ContentContext = new ContentContext();
        }

        public SettingsEditViewModel GetManagerSettingsEdit()
        {
            var query = from s in m_ContentContext.Settings
                        select s;
            var model = new SettingsEditViewModel();
            if (query.Any(f => f.Id == model.WorkingStockId))
            {
                model.WorkingStock = query.FirstOrDefault(f => f.Id == model.WorkingStockId).Value;
            }
            else
            {
                model.WorkingStock = "2";
            }

            if (query.Any(f => f.Id == model.ShowUnstockItemId))
            {
                model.ShowUnstockItem = query.FirstOrDefault(f => f.Id == model.ShowUnstockItemId).Value;
            }
            else
            {
                model.ShowUnstockItem = "2";
            }

            if (query.Any(f => f.Id == model.PriceIncludeTaxId))
            {
                model.PriceIncludeTax = query.FirstOrDefault(f => f.Id == model.PriceIncludeTaxId).Value;
            }
            else
            {
                model.PriceIncludeTax = "2";
            }

            if (query.Any(f => f.Id == model.ShowCommentsId))
            {
                model.ShowComments = query.FirstOrDefault(f => f.Id == model.ShowCommentsId).Value;
            }
            else
            {
                model.ShowComments = "2";
            }

            if (query.Any(f => f.Id == model.UseFacebookCommentsId))
            {
                model.UseFacebookComments = query.FirstOrDefault(f => f.Id == model.UseFacebookCommentsId).Value;
            }
            else
            {
                model.UseFacebookComments = "2";
            }
            
            return model;
        }

        public void UpdateManagerSettingsEdit(SettingsEditViewModel model)
        {
            var query = from s in m_ContentContext.Settings
                        select s;
            var itemWorkingStock = query.FirstOrDefault(f => f.Id == model.WorkingStockId);
            if (itemWorkingStock!= null)
            {
                itemWorkingStock.Value = model.WorkingStock;
            }
            else
            {
                m_ContentContext.Settings.Add(new Data.Entities.Settings { Id = model.WorkingStockId, Value = model.WorkingStock });
            }

            var itemShowUnstockItem = query.FirstOrDefault(f => f.Id == model.ShowUnstockItemId);
            if (itemShowUnstockItem != null)
            {
                itemShowUnstockItem.Value = model.ShowUnstockItem;
            }
            else
            {
                m_ContentContext.Settings.Add(new Data.Entities.Settings { Id = model.ShowUnstockItemId, Value = model.ShowUnstockItem });
            }
            
            var itemPriceIncludeTax = query.FirstOrDefault(f => f.Id == model.PriceIncludeTaxId);
            if (itemPriceIncludeTax != null)
            {
                itemPriceIncludeTax.Value = model.PriceIncludeTax;
            }
            else
            {
                m_ContentContext.Settings.Add(new Data.Entities.Settings { Id = model.PriceIncludeTaxId, Value = model.PriceIncludeTax });
            }
            
            var itemShowComments = query.FirstOrDefault(f => f.Id == model.ShowCommentsId);
            if (itemShowComments != null)
            {
                itemShowComments.Value = model.ShowComments;
            }
            else
            {
                m_ContentContext.Settings.Add(new Data.Entities.Settings { Id = model.ShowCommentsId, Value = model.ShowComments });
            }
            
            var itemUseFacebookComments = query.FirstOrDefault(f => f.Id == model.UseFacebookCommentsId);
            if (itemUseFacebookComments != null)
            {
                itemUseFacebookComments.Value = model.UseFacebookComments;
            }
            else
            {
                m_ContentContext.Settings.Add(new Data.Entities.Settings { Id = model.UseFacebookCommentsId, Value = model.UseFacebookComments });
            }
            
            m_ContentContext.SaveChanges();
        }
    }
}