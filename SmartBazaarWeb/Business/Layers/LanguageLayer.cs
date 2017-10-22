using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Business.Layers
{
    public class LanguageLayer
    {
        private static Dictionary<string, List<Models.Layer.LanguagesModel>> langs;

        public LanguageLayer()
        {
            if (langs == null)
            {
                langs = new Dictionary<string, List<Models.Layer.LanguagesModel>>();
            }
        }

        public List<Models.Layer.LanguagesModel> GetLangs(string code)
        {
            
            if (langs.ContainsKey(code))
            {
                return langs[code];
            }
            else if (langs.ContainsKey(System.Configuration.ConfigurationManager.AppSettings["DefaultLanguage"]))
            {
                return langs[System.Configuration.ConfigurationManager.AppSettings["DefaultLanguage"]];
            }
            else
            {
                return null;
            }
        }

        public void SetLangs(string code, List<Models.Layer.LanguagesModel> list)
        {
            if (langs.ContainsKey(code))
            {
                langs[code] = list;
            }
            else
            {
                langs.Add(code, list);
            }
        }
        
    }
}