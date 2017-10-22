using SmartBazaar.Web.Business.Workers;
using SmartBazaar.Web.Models.Layer;
using System.Web;
using Microsoft.AspNet.Identity;

namespace SmartBazaar.Web.Business.Layers
{
    public class CustomerLayer
    {
        public static CustomerStorageModel Customer
        {
            get
            {
                var model = HttpContext.Current.Session["Customer"] as CustomerStorageModel;
                if (model == null)
                {
                    var user = HttpContext.Current.GetOwinContext().Authentication.User.Identity;
                    if (user == null || !user.IsAuthenticated)
                    {
                        model = new CustomerStorageModel
                        {
                            PriceIndex = 1
                        };
                        return model;
                    }
                    else
                    {
                        var customerWorker = new CustomerWorker();
                        try
                        {
                            HttpContext.Current.Session["Customer"] = customerWorker.GetCustomerStorage(user.GetUserId<string>());
                        }
                        catch (System.Exception)
                        {
                            model = new CustomerStorageModel
                            {
                                PriceIndex = 1
                            };
                            return model;
                        }
                        return Customer;
                    }
                }
                else
                {
                    return model;
                }
            }
        }
    }
}