using SmartBazaar.Web.Models.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Xml.Serialization;

namespace SmartBazaar.Web.Business.Walkers
{
    public class ContactWalker
    {
        public static ContactModel Load()
        {
            ContactModel contactData = HttpContext.Current.Cache.Get("contact") as ContactModel;
            if (contactData == null)
            {
                XmlSerializer srlz = new XmlSerializer(typeof(ContactModel));
                StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/Contact.xml"));
                contactData = srlz.Deserialize(sr) as ContactModel;
                HttpContext.Current.Cache.Add("contact", contactData, null, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                sr.Close();
                return contactData;
            }
            else
            {
                return contactData;
            }
        }

        public static void Save(ContactModel model)
        {
            HttpContext.Current.Cache.Remove("contact");
            HttpContext.Current.Cache.Add("contact", model, null, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            XmlSerializer srlz = new XmlSerializer(typeof(ContactModel));
            StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath("~/App_Data/Contact.xml"));
            srlz.Serialize(sw, model);
            sw.Close();
        }
    }
}