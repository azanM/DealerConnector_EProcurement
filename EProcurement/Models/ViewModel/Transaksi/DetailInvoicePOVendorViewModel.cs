using System;

namespace EProcurement.Models.ViewModel.Transaksi
{
    public class DetailInvoicePOVendorViewModel
    {
        public string PONumber { get; set; }
        public DateTime? PODate { get; set; }
        public string PODescription { get; set; }
        public string BBN { get; set; }
        public DateTime? PromiseDeliveryDate { get; set; }
        public string PICAdmin { get; set; }
        public DateTime? ExpectedDateDelivered { get; set; }
        public string PurchassingOfficer { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string NoSeriFakturPajak { get; set; }
        public DateTime? PayPlan { get; set; }
        public DateTime? ClearingDate { get; set; }
        public string StatusInvoice { get; set; } 
        public string StatusDeliveryUnit { get; set; }
        public string CBUCKD { get; set; }
        public string ScanBSTK { get; set; }
        public string ScanSTNKSTCK { get; set; }
        public DateTime? ActualReceivedInvoice { get; set; }
        public int? TermOfPayment { get; set; }
        public DateTime? ActualInvoiceReceived { get; set; }
        public string strInvoiceDate { get; set; }
        public string strPoDate { get; set; }
        public string strPromiseDeliveryDate { get; set; }
        public string strExpectedDateDelivered { get; set; }
        public string strPayPlan { get; set; }
        public string strClearingDate { get; set; }
    } 
} 