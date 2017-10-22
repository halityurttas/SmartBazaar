using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Helpers.SEO
{
    public static class SeoFriendlyUrl
    {
        public static string Convert(string data)
        {
            return data.ToLower(CultureInfo.GetCultureInfo("en-US")).Replace(" ", "_");
        }
    }
}