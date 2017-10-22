using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SmartBazaar.Web.Components.Payment.PayFlex.Models
{
    [Serializable]
    [XmlRoot(ElementName = "VposRequest")]
    public class MPIInstallmentRequest
    {
        [XmlElement]
        public string MerchantId { get; set; }
        [XmlElement]
        public string Password { get; set; }
        [XmlElement]
        public string BankId { get; set; }
        [XmlElement]
        public string TransactionType { get; set; }
        [XmlElement]
        public string TransactionId { get; set; }
        [XmlElement]
        public string CurrencyAmount { get; set; }
        [XmlElement]
        public string CurrencyCode { get; set; }
        [XmlElement]
        public string InstallmentCount { get; set; }
        [XmlElement]
        public string Pan { get; set; }
        [XmlElement]
        public string Cvv { get; set; }
        [XmlElement]
        public string Expiry { get; set; }
        [XmlElement]
        public string Eci { get; set; }
        [XmlElement]
        public string Cavv { get; set; }
        [XmlElement]
        public string Xid { get; set; }
    }
}