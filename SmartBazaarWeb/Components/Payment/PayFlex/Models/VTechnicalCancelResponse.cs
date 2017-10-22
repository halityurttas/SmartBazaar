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
    [XmlRoot(ElementName = "VposResponse")]
    public class VTechnicalCancelResponse
    {
        [XmlElement]
        public string MerchantId { get; set; }
        [XmlElement]
        public string TransactionType { get; set; }
        [XmlElement]
        public string TransactionId { get; set; }
        [XmlElement]
        public string ReferenceTransactionId { get; set; }
        [XmlElement]
        public string ResultCode { get; set; }
    }
}