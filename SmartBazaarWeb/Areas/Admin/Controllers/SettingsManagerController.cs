using SmartBazaar.Web.Areas.Admin.Models;
using SmartBazaar.Web.Business.Workers;
using SmartBazaar.Web.Helpers.Extensions;
using SmartBazaar.Web.Models.Common;
using SmartBazaar.Web.Models.Internal;
using System;
using System.Web.Mvc;

namespace SmartBazaar.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    public class SettingsManagerController : Controller
    {
        private readonly SettingsWorker m_settingsWorker;
        public SettingsManagerController()
        {
            m_settingsWorker = new SettingsWorker();
        }

        // GET: Admin/Settings
        public ActionResult Index()
        {
            ViewBag.ColSizeLabel = "col-sm-8 col-md-9";
            ViewBag.ColSizeShort = "col-md-4 col-md-3";

            var model = m_settingsWorker.GetManagerSettingsEdit();
            this.Pair2List<SettingsEditViewModel, string, string>(SettingValues.GetYesNo(), m => m.WorkingStock, model.WorkingStock);
            this.Pair2List<SettingsEditViewModel, string, string>(SettingValues.GetYesNo(), m => m.ShowUnstockItem, model.ShowUnstockItem);
            this.Pair2List<SettingsEditViewModel, string, string>(SettingValues.GetYesNo(), m => m.PriceIncludeTax, model.PriceIncludeTax);
            this.Pair2List<SettingsEditViewModel, string, string>(SettingValues.GetYesNo(), m => m.ShowComments, model.ShowComments);
            this.Pair2List<SettingsEditViewModel, string, string>(SettingValues.GetYesNo(), m => m.UseFacebookComments, model.UseFacebookComments);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SettingsEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    m_settingsWorker.UpdateManagerSettingsEdit(model);
                    Business.Layers.SettingsLayer.RegisterAppSettings();
                    return RedirectToAction("Index", "Manager");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            this.Pair2List<SettingsEditViewModel, string, string>(SettingValues.GetYesNo(), m => m.WorkingStock, model.WorkingStock);
            this.Pair2List<SettingsEditViewModel, string, string>(SettingValues.GetYesNo(), m => m.ShowUnstockItem, model.ShowUnstockItem);
            this.Pair2List<SettingsEditViewModel, string, string>(SettingValues.GetYesNo(), m => m.PriceIncludeTax, model.PriceIncludeTax);
            this.Pair2List<SettingsEditViewModel, string, string>(SettingValues.GetYesNo(), m => m.ShowComments, model.ShowComments);
            this.Pair2List<SettingsEditViewModel, string, string>(SettingValues.GetYesNo(), m => m.UseFacebookComments, model.UseFacebookComments);
            return View(model);
        }

        //GET Settings/Contact
        public ActionResult Contact()
        {
            var model = AutoMapper.Mapper.Map<ContactViewModel>(Business.Walkers.ContactWalker.Load());
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = AutoMapper.Mapper.Map<ContactModel>(model);
                    Business.Walkers.ContactWalker.Save(item);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("General", ex.Message);
                }
            }
            return View(model);
        }
    }
}