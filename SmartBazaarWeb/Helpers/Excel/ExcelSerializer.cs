using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using System.IO;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using SmartBazaar.Web.Components.Converters;

namespace SmartBazaar.Web.Helpers.Excel
{
    public class ExcelSerializer
    {
        public static void Serialize<T>(List<T> Rows, ref Stream Output) 
            where T :class
        {
            using (ExcelPackage ep = new ExcelPackage()) {
                var workSheet = ep.Workbook.Worksheets.Add(Rows.FirstOrDefault().GetType().Name);
                workSheet.Cells[1, 1].LoadFromCollection(Rows, true);
                ep.SaveAs(Output);
            }
        }

        public static List<T> Deserialize<T>(Stream Input) 
            where T: class
        {
            ExcelPackage ep = new ExcelPackage();
            ep.Load(Input);
            var workSheet = ep.Workbook.Worksheets.FirstOrDefault();
            List<string> captions = new List<string>();
            int counter = 1;
            while (!string.IsNullOrEmpty(workSheet.Cells[1, counter].GetValue<string>()))
            {
                captions.Add(workSheet.Cells[1, counter].GetValue<string>());
                counter++;
            }
            var tpropList = typeof(T).GetProperties();
            List<T> tlist = new List<T>();
            counter = 2;
            while (!string.IsNullOrEmpty(workSheet.Cells[counter, 1].GetValue<string>()))
            {
                var tinstance = Activator.CreateInstance<T>();
                for (int i = 1; i <= captions.Count; i++)
                {
                    if (tpropList.Any(a => a.Name == captions[i-1]))
                    {
                        var tprop = tpropList.FirstOrDefault(f => f.Name == captions[i-1]);
                        if (workSheet.Cells[counter, i].Value == null) continue;
                        tprop.SetValue(tinstance, DynamicCasting.Cast(tprop.PropertyType, workSheet.Cells[counter, i].Value.ToString()));
                    }
                }
                tlist.Add(tinstance);
                counter++;
            }
            return tlist;
        }
    }
}