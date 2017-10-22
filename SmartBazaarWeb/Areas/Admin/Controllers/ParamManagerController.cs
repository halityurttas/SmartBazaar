using SmartBazaar.Web.Areas.Admin.Models;
using SmartBazaar.Web.Business.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartBazaar.Web.Areas.Admin.Controllers
{
    public class ParamManagerController : Controller
    {
        private readonly ParamWorker m_paramWorker;

        public ParamManagerController()
        {
            m_paramWorker = new ParamWorker();
        }

        // GET: Admin/ParamManager
        public ActionResult Index(int? group)
        {
            var paramGroups = m_paramWorker.GetParamsGroupsListViewModel();
            int selectedGroup = group ?? paramGroups.FirstOrDefault().Id;
            ViewBag.group = paramGroups.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Title, Selected = selectedGroup == s.Id }).ToList();
            var model = m_paramWorker.GetParamsListViewModel(selectedGroup);
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(FormCollection frm)
        {
            try
            {
                m_paramWorker.Update(int.Parse(frm["item.Id"]), frm["item.Value"]);
                return Content(true.ToString());
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}