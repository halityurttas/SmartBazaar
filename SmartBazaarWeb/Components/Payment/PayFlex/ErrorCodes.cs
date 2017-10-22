using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Components.Payment.PayFlex
{
    public class ErrorCodes
    {
        //TODO: payflex hata kodları girilecek
        private readonly Dictionary<string, string> Codes = new Dictionary<string,string> {
            {"1001", "Hata"}
        };

        public string this[string index]
        {
            get
            {
                return Codes[index];
            }
        }
    }
}