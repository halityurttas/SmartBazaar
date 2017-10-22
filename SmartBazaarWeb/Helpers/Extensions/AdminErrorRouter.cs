using SmartBazaar.Web.Models.Internal;
using System;
using System.Web.Mvc;

namespace SmartBazaar.Web.Helpers.Extensions
{
    public static class AdminErrorRouter
    {
        public static void ShowError(this Controller controller, Exception ex)
        {
            controller.TempData[Constants.OperationErrorKey] = new ErrorModel { Error = ex, ReturnPage = controller.Request.UrlReferrer.PathAndQuery };
            controller.Response.Redirect("/Admin/Manager/Error", true);
        }
    }
}