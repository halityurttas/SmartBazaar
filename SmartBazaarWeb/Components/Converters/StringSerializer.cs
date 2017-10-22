using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace SmartBazaar.Web.Components.Converters
{
    public class StringSerializer
    {
        public static string Serialize<T>(T Object) where T: class
        {
            XmlSerializer srlz = new XmlSerializer(typeof(T));
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new Utf8StringWriter(sb);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            srlz.Serialize(sw, Object, ns);
            return sb.ToString();
        }

        public static T Deserialize<T>(string Data) where T: class
        {
            XmlSerializer srlz = new XmlSerializer(typeof(T));
            StringReader sr = new StringReader(Data);
            return srlz.Deserialize(sr) as T;
        }


        private class Utf8StringWriter : StringWriter
        {
            public Utf8StringWriter(StringBuilder sb)
                : base(sb)
            {
            }
            public override Encoding Encoding { get { return Encoding.UTF8; } }
        }

    }
}