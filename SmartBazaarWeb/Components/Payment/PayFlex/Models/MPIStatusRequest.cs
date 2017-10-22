using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Components.Payment.PayFlex.Models
{
    public class MPIStatusRequest
    {
        public MPIStatusRequest()
        {
            Currency = "792";
        }
        public string MerchantId { get; set; }
        public string MerchantPassword { get; set; }
        public string VerifyEnrollmentRequestId { get; set; }
        public string Pan { get; set; }
        public string ExpiryDate { get; set; }
        public string PurchaseAmount { get; set; }
        public string Currency { get; set; }
        public string BrandName { get; set; }
        public string AcquirerBinPassword { get; set; }
        public string SessionInfo { get; set; }
        public string SuccessUrl { get; set; }
        public string FailureUrl { get; set; }
        public string InstallmentCount { get; set; }
        public string BankId { get; set; }

        public override string ToString()
        {
            return "MerchantId=" + MerchantId
                + "&MerchantPassword=" + MerchantPassword
                + "&VerifyEnrollmentRequestId=" + VerifyEnrollmentRequestId
                + "&Pan=" + Pan
                + "&ExpiryDate=" + ExpiryDate
                + "&PurchaseAmount=" + PurchaseAmount
                + "&Currency=" + Currency
                + "&BrandName=" + BrandName
                + (string.IsNullOrEmpty(AcquirerBinPassword) ? "" : "&AcquirerBinPassword=" + AcquirerBinPassword)
                + (string.IsNullOrEmpty(SessionInfo) ? "" : "&SessionInfo=" + SessionInfo)
                + (string.IsNullOrEmpty(SuccessUrl) ? "" : "&SuccessUrl=" + SuccessUrl)
                + (string.IsNullOrEmpty(FailureUrl) ? "" : "&FailureUrl=" + FailureUrl)
                + (string.IsNullOrEmpty(InstallmentCount) ? "" : "&InstallmentCount=" + InstallmentCount)
                + (string.IsNullOrEmpty(BankId) ? "" : "&BankId=" + BankId);
        }
    }
}