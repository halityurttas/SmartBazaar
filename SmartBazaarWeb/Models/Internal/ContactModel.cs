using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Internal
{
    [Serializable]
    public class ContactModel
    {
        public string Address { get; set; }
        public string Phone1Title { get; set; }
        public string Phone1 { get; set; }
        public string Phone2Title { get; set; }
        public string Phone2 { get; set; }
        public string Email1Title { get; set; }
        public string Email1 { get; set; }
        public string Email2Title { get; set; }
        public string Email2 { get; set; }
        public string GMapData { get; set; }
    }
}