using SmartBazaar.Web.Models.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml.Serialization;
using System.Web.Caching;

namespace SmartBazaar.Web.Business.Walkers
{
    public class LocationWalker
    {
        public static List<CityModel> GetCities()
        {
            if (HttpContext.Current.Cache.Get("cities") == null)
            {
                XmlSerializer srlz = new XmlSerializer(typeof(List<CityModel>));
                StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/Cities.xml"));
                List<CityModel> cities = srlz.Deserialize(sr) as List<CityModel>;
                sr.Close();
                sr.Dispose();
                HttpContext.Current.Cache.Add("cities", cities, null, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                return cities;
            }
            else
            {
                return HttpContext.Current.Cache.Get("cities") as List<CityModel>;
            }
        }

        public static List<DistrictModel> GetDistrict(int CityId)
        {
            List<DistrictModel> districts = null;
            if (HttpContext.Current.Cache.Get("districts") == null)
            {
                XmlSerializer srlz = new XmlSerializer(typeof(List<DistrictModel>));
                StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/Districts.xml"));
                districts = srlz.Deserialize(sr) as List<DistrictModel>;
                sr.Close();
                sr.Dispose();
                HttpContext.Current.Cache.Add("districts", districts, null, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            else
            {
                districts = HttpContext.Current.Cache.Get("districts") as List<DistrictModel>;
            }
            return districts.Where(w => w.CityId == CityId).ToList();
        }
    }
}