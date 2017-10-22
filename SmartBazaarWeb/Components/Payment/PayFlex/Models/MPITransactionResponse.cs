using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Web.Mvc;

namespace SmartBazaar.Web.Components.Payment.PayFlex.Models
{
    public class MPITransactionResponse
    {
        private Dictionary<string, string> m_formCollection;

        public MPITransactionResponse(Dictionary<string, string> formCollection)
        {
            m_formCollection = formCollection;
        }

        public string MerchantID { get { return m_formCollection["MerchantID"]; } }
        public string PAN { get { return m_formCollection["Pan"]; } }
        public string Expiry { get { return m_formCollection["Expiry"]; } }
        public string BrandName { get { return m_formCollection["brand_name"]; } }
        public string PurchaseAmount { get { return m_formCollection["PurchAmount"]; } }
        public string PurchaseCurrency { get { return m_formCollection["PurchCurrency"]; } }
        public string CVV2 { get { return m_formCollection["Cvv2"]; } }
        public string NumberOfInstallment { get { return m_formCollection["NumberOfInstallment"]; } }
        public string CAVV { get { return m_formCollection["Cavv"]; } }
        public string ECI { get { return m_formCollection["Eci"]; } }
        public string XID { get { return m_formCollection["Xid"]; } }
    }

}