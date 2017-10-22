using SmartBazaar.Web.Business.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartBazaar.Web.Controllers
{
    public class HtmlBlocksController : Controller
    {
        private readonly HtmlBlocksWorker m_htmlBlocksWorker;
        public HtmlBlocksController()
        {
            m_htmlBlocksWorker = new HtmlBlocksWorker();
        }

        [ChildActionOnly]
        public ActionResult LocationBlocks(int id, string viewpage = null)
        {
            var model = m_htmlBlocksWorker.GetSiteLocationBlocks(id);
            if (!model.Any())
            {
                return null;
            }
            if (string.IsNullOrEmpty(viewpage))
            {
                return PartialView(model);
            }
            else
            {
                return PartialView(viewpage, model);
            }
        }
    }
}