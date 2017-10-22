
using System;
using System.Collections.Generic;
using System.Linq;
using SmartBazaar.Web.Business.Layers;

namespace SmartBazaar.Web.Resources
{

	public static class Langs
	{

		private static LanguageLayer layer;

		public static string anasayfa {
			get 
			{ 
				var items = (layer ?? (layer = new LanguageLayer())).GetLangs(System.Threading.Thread.CurrentThread.CurrentCulture.IetfLanguageTag.Substring(0,2));
				if (items == null) 
				{
					return "anasayfa";
				}
				else
				{
					return items.FirstOrDefault(f => f.Key == "anasayfa").Value;
				}
			}
		}
		public static string hakkimizda {
			get 
			{ 
				var items = (layer ?? (layer = new LanguageLayer())).GetLangs(System.Threading.Thread.CurrentThread.CurrentCulture.IetfLanguageTag.Substring(0,2));
				if (items == null) 
				{
					return "hakkimizda";
				}
				else
				{
					return items.FirstOrDefault(f => f.Key == "hakkimizda").Value;
				}
			}
		}

		public static List<string> GetKeys()
		{
			return new List<string> {
				"anasayfa", "hakkimizda"
			};
		}
	}

}