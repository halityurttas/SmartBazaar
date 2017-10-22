using System.Collections.Generic;
using System.Linq;

namespace SmartBazaar.Web.Models.Internal
{
    public class Statuses
    {
        public static Dictionary<short, string> StatusList = new Dictionary<short, string> {
            { 0, "Pasif"}, {1, "Aktif"}
        };

        public static string GetTitle(short status)
        {
            if (StatusList.Keys.Contains(status))
            {
                return StatusList[status];
            }
            return "Pasif";
        }

    }

}