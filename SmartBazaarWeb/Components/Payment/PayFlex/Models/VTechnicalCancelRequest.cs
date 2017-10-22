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
    [XmlRoot(ElementName = "VposRequest")]
    public class VTechnicalCancelRequest
    {
        [XmlElement]
        public string MerchantId { get; set; }
        [XmlElement]
        public string Password { get; set; }
        [XmlElement]
        public int BankId { get; set; }
        [XmlElement]
        public string TransactionType { get; set; }
        [XmlElement]
        public string TransactionId { get; set; }
        [XmlElement]
        public string ReferenceTransactionId { get; set; }
    }
}