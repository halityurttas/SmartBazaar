using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartBazaar.Web.Controllers
{
    public class SettingsController : Controller
    {
        
        [OutputCache(Duration=60*60)]
        [ChildActionOnly]
        public ActionResult FacebookApp()
        {
            if (Business.Layers.SettingsLayer.SiteSetting.UseFacebookComments)
            {
                var paramWorker = new Business.Workers.ParamWorker();
                string model = paramWorker.GetParamValue(AppConfig.PARAM_FACEBOOK_APPID_ID);
                return PartialView("FacebookApp", model);
            }
            else
            {
                return null;
            }
        }
    }
}