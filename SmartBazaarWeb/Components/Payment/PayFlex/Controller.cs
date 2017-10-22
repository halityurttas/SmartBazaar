using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Web.Mvc;
using SmartBazaar.Web.Components.Payment.PayFlex.Models;
using SmartBazaar.Web.Components.Converters;

namespace SmartBazaar.Web.Components.Payment.PayFlex
{

    public class Controller
    {
        public SmartBazaar.Web.Models.Internal.PaymentModel PaymentModel { get; set; }
        public MPIStatusResponse MPIStatus { get; set; }
        public MPITransactionResponse MPITransaction { get; set; }
        public MPIStatusRequest MPIQuery { get; set; } 
        public dynamic CheckoutResult { get; set; }

        public Controller(MPIStatusRequest model) 
        {
            MPIQuery = model;
        }

        public Controller()
        {

        }

        public bool Is3D(string MPIControlUrl)
        {
            string modelData = MPIQuery.ToString();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(MPIControlUrl);
            request.Method = "POST";
            request.Timeout = 59000;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = modelData.Length;
            using(StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.Write(modelData);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseData = "";
            using(StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                responseData = sr.ReadToEnd();
                sr.Close();
            }
            response.Close();
            MPIStatus = StringSerializer.Deserialize<MPIStatusResponse>(responseData);
            return MPIStatus.VERes.Status == "Y";
        }

        public void MPITransactionResult(Dictionary<string, string> frm)
        {
            MPITransaction = new MPITransactionResponse(frm);
        }
        
        public void MPIChekout(string CheckoutUrl)
        {
            
            if (string.IsNullOrEmpty(MPIQuery.InstallmentCount)) 
            {
                
                CheckoutResult = XOperation<MPIPrepaidRequest, VPrepaidResponse>(CheckoutUrl, new MPIPrepaidRequest {
                    MerchantId = MPITransaction.MerchantID,
                    Password = MPIQuery.MerchantPassword,
                    BankId = MPIQuery.BankId,
                    TransactionType = "Sale",
                    TransactionId = Guid.NewGuid().ToString(),
                    CurrencyAmount = MPITransaction.PurchaseAmount,
                    CurrencyCode = MPITransaction.PurchaseCurrency,
                    Pan = MPITransaction.PAN,
                    Cvv = MPITransaction.CVV2,
                    Expiry = MPITransaction.Expiry,
                    Eci = MPITransaction.ECI,
                    Cavv = MPITransaction.CAVV,
                    Xid = MPITransaction.XID
                });
            }
            else
            {
                CheckoutResult = XOperation<MPIInstallmentRequest, VInstallmentResponse>(CheckoutUrl, new MPIInstallmentRequest {
                    MerchantId = MPITransaction.MerchantID,
                    Password = MPIQuery.MerchantPassword,
                    BankId = MPIQuery.BankId,
                    TransactionType = "Sale",
                    TransactionId = Guid.NewGuid().ToString(),
                    CurrencyAmount = MPITransaction.PurchaseAmount,
                    CurrencyCode = MPITransaction.PurchaseCurrency,
                    Pan = MPITransaction.PAN,
                    Cvv = MPITransaction.CVV2,
                    Expiry = MPITransaction.Expiry,
                    Eci = MPITransaction.ECI,
                    Cavv = MPITransaction.CAVV,
                    Xid = MPITransaction.XID,
                    InstallmentCount = MPITransaction.NumberOfInstallment
                });
            }
        }

        public void VPrepaidCheckout(string CheckoutUrl, VPrepaidRequest model)
        {
            CheckoutResult = XOperation<VPrepaidRequest, VPrepaidResponse>(CheckoutUrl, model);
        }

        public void VInstallmentCheckout(string CheckoutUrl, VInstallmentRequest model)
        {
            CheckoutResult = XOperation<VInstallmentRequest, VInstallmentResponse>(CheckoutUrl, model);
        }

        public void VCancel(string CheckoutUrl, VCancelRequest model)
        {
            CheckoutResult = XOperation<VCancelRequest, VCancelResponse>(CheckoutUrl, model);
        }

        public void VRefund(string CheckoutUrl, VRefundRequest model)
        {
            CheckoutResult = XOperation<VRefundRequest, VRefundResponse>(CheckoutUrl, model);
        }

        public void VTechnicalCancel(string CheckoutUrl, VTechnicalCancelRequest model)
        {
            CheckoutResult = XOperation<VTechnicalCancelRequest, VTechnicalCancelResponse>(CheckoutUrl, model);
        }

        private O XOperation<I, O>(string Url, I model, string prmstr = "prmstr=") 
            where I: class
            where O: class
        {
            string modelData = StringSerializer.Serialize<I>(model);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.Timeout = 59000;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = modelData.Length;
            using(StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.Write(prmstr + modelData);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseData = "";
            using(StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                responseData = sr.ReadToEnd();
                sr.Close();
            }
            response.Close();
            return StringSerializer.Deserialize<O>(responseData);
        }
    }

}