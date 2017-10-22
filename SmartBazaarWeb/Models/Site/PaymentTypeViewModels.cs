using System.Collections.Generic;

namespace SmartBazaar.Web.Models.Site
{
    

    public class PaymentTypeViewModel
    {
        public PaymentTypeViewModel()
        {
            Installments = new List<PaymentInstallmentViewModel>();
        }

        public int Id { get; set; }
        public short Method { get; set; }
        public double CommissionRate { get; set; }
        public decimal ProcessFee { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PosType { get; set; }

        public List<PaymentInstallmentViewModel> Installments { get; set; }
    }
}