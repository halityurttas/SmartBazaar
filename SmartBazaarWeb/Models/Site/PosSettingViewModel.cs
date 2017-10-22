using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Site
{
    public class PosSettingViewModel
    {
        public int Id { get; set; }
        public string AssemblyData { get; set; }
        public string Referance { get; set; }
        public string Title { get; set; }
        public short Status { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}