using SmartBazaar.Web.Areas.Admin.Models;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartBazaar.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    public class FileManagerController : Controller
    {
        // GET: Admin/FileManager
        public ActionResult Index()
        {
            var dirs = Directory.GetFiles(Server.MapPath("~/WebFiles/"));
            var model = dirs.Select(item => FileListModel.Import(Path.GetFileName(item), Path.GetExtension(item))).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase mainfile)
        {
            if (mainfile != null && mainfile.ContentLength > 0)
            {
                if (System.IO.File.Exists(Server.MapPath("~/WebFiles/") + mainfile.FileName))
                {
                    string existsfilename = Path.GetFileNameWithoutExtension(mainfile.FileName) + "_1" + Path.GetExtension(mainfile.FileName);
                    mainfile.SaveAs(Server.MapPath("~/WebFiles/") + existsfilename);
                }
                else
                {
                    mainfile.SaveAs(Server.MapPath("~/WebFiles/") + mainfile.FileName);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string file)
        {
            if (System.IO.File.Exists(Server.MapPath("~/WebFiles/") + file))
            {
                System.IO.File.Delete(Server.MapPath("~/WebFiles/") + file);
            }
            return RedirectToAction("Index");
        }
    }
}