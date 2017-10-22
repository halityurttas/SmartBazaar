using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Internal
{
    public class GenderListProvider
    {
        public static Dictionary<short, string> GenderTitles()
        {
            return new Dictionary<short, string>
            {
                {1, "Bay" },
                {2, "Bayan" }
            };
        }
    }
}