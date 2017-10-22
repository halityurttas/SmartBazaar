using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SmartBazaar.Web.Components.Payment.PayFlex.Models
{
    [Serializable]
    [XmlRoot(ElementName = "IPaySecure")]
    public class MPIStatusResponse
    {
        [XmlElement(ElementName = "Message")]
        public MPIStatusMessage Message { get; set; }
        [XmlElement(ElementName = "VERes")]
        public MPIStatusVERes VERes { get; set; }
    }

    [Serializable]
    
    public class MPIStatusMessage
    {
        [XmlAttribute(AttributeName = "ID")]
        public string ID { get; set; }
    }

    [Serializable]
    public class MPIStatusVERes
    {
        [XmlElement]
        public string Version { get; set; }
        [XmlElement]
        public string Status { get; set; }
        [XmlElement]
        public string PAReq { get; set; }
        [XmlElement]
        public string ACSUrl { get; set; }
        [XmlElement]
        public string TermUrl { get; set; }
        [XmlElement]
        public string MD { get; set; }
        [XmlElement]
        public string ACTUALBRAND { get; set; }
    }
}