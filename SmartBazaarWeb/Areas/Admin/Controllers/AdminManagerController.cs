using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SmartBazaar.Web.Areas.Admin.Models;
using SmartBazaar.Web.Helpers.Extensions;
using SmartBazaar.Web.Models.Ident;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartBazaar.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    public class AdminManagerController : Controller
    {
        // GET: Admin/AdminManager
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var signManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            var result = signManager.PasswordSignIn(model.UserName, model.Password, false, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Manager");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Geçersiz Kullanıcı Adı/Parola");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Remember()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Remember(AdminRememberViewModel model)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindByEmail(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return View("ForgotPasswordConfirmation");
            }

            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            // Send an email with this link
            string code = userManager.GeneratePasswordResetToken(user.Id);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code, area = string.Empty }, protocol: Request.Url.Scheme);
            string mailContent = this.RenderRazorView("Mail/ForgotPassword", callbackUrl);
            userManager.SendEmail(user.Id, "Parola Sıfırlama", mailContent);
            return RedirectToAction("ForgotPasswordConfirmation");
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var result = userManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = userManager.FindById(User.Identity.GetUserId());
                if (user != null)
                {
                    var signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                    signInManager.SignIn(user, false, false);
                }
                return RedirectToAction("Index", "Manager", new { Message = true });
            }
            AddErrors(result);
            return View(model);
        }





        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}