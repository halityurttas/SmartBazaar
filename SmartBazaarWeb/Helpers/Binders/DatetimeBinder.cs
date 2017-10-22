using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace SmartBazaar.Web.Helpers.Binders
{
    public class DatetimeBinder: IModelBinder
    {
        [ValidateInput(false)]
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var divalueProvider = bindingContext.ValueProvider as IUnvalidatedValueProvider;

            string data = "";
            if (controllerContext.Controller.ValidateRequest && bindingContext.ModelMetadata.RequestValidationEnabled)
            {
                data = controllerContext.HttpContext.Request.Params[bindingContext.ModelName];
            }
            else
            {
                data = divalueProvider.GetValue(bindingContext.ModelName, true).AttemptedValue;
            }
            
                
            DateTime dt = new DateTime();
            if (DateTime.TryParse(data, CultureInfo.GetCultureInfo("tr-TR"), DateTimeStyles.None, out dt))
            {
                return dt;
            }
            else
            {
                return null;
            }
        }
    }
}