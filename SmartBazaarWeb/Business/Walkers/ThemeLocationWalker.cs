using SmartBazaar.Web.Models.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace SmartBazaar.Web.Business.Walkers
{
    public class ThemeLocationWalker
    {
        private List<ThemeLocationModel> m_themeLocations;

        public ThemeLocationWalker(bool isLoad = true)
        {
            if (isLoad)
            {
                load();
            }
        }

        private void load()
        {
            XmlSerializer srlz = new XmlSerializer(typeof(List<ThemeLocationModel>));
            StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/theme_locations.xml"));
            m_themeLocations = srlz.Deserialize(sr) as List<ThemeLocationModel>;
            sr.Close();
        }

        private void save()
        {
            XmlSerializer srlz = new XmlSerializer(typeof(List<ThemeLocationModel>));
            StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath("~/App_Data/theme_locations.xml"));
            srlz.Serialize(sw, m_themeLocations);
            sw.Close();
        }

        

        public List<ThemeLocationModel> ThemeLocations
        {
            get 
            {
                return m_themeLocations; 
            }
            set 
            { 
                m_themeLocations = value;
                save();
            }
        }

    }
}