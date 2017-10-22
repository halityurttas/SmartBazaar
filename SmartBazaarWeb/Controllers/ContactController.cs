using Khaled.SmtpClient;
using SmartBazaar.Web.Business.Walkers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartBazaar.Web.Helpers.Extensions;
using SmartBazaar.Web.Models.Site;

namespace SmartBazaar.Web.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = ContactWalker.Load();
            ViewBag.ContactData = model;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactForm(FormCollection frm)
        {
            ContactMailViewModel model = new ContactMailViewModel {
                Name = frm["name"],
                Email = frm["email"],
                Phone = frm["phone"],
                Comment = frm["comment"]
            };
            SmtpMailClient mail = new SmtpMailClient();
            string toEmail = ConfigurationManager.AppSettings["AdminEmail"];
            string content = this.RenderRazorView("Mails/Contact", model);
            mail.PostMail(toEmail, "Iletisim Formu", content);
            return View();
        }

        [ChildActionOnly]
        public ActionResult Partial()
        {
            var model = ContactWalker.Load();
            return PartialView(model);
        }
    }
}