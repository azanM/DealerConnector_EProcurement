using System;

namespace EProcurement.Models.ViewModel.Transaksi
{
    public class DetailInvoicePOProcViewModel
    {
        public string PONumber { get; set; }
        public DateTime? PODate { get; set; }
        public string PODescription { get; set; }
        public double? OnTheRoadPrice { get; set; }
        public double? Discount { get; set; }
        public double? NetPrice { get; set; }
        public string PriceFormated { get; set; }
        public string DiscountFormated { get; set; }
        public string NetFormated { get; set; }
        public DateTime? ExpectedDateDelivered { get; set; }
        public DateTime? PromiseDeliveryDate { get; set; }
        public string OfficerName { get; set; }
        public string PICAdmin { get; set; }
        public DateTime? DateEntryCarrosserieAccessories { get; set; }
        public string NoSeriFakturPajak { get; set; }
        public string FakturDONumber { get; set; }
        public DateTime? FakturDODate { get; set; }
        public string ChassisNumberSAP { get; set; }
        public string MachineNumberSAP { get; set; }
        public string ChassisNumber { get; set; }
        public string MachineNumber { get; set; }
        public string DownloadPOVendor { get; set; }
        public double? DPPByVendor { get; set; }
        public string DPPFormated { get; set; }
        public double? PPNByVendor { get; set; }
        public string PPNFormated { get; set; }
        public double? BBNPriceByVendor { get; set; }
        public string BBNFormated { get; set; }
        public DateTime? TanggalBSTB { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceStatus { get; set; }
        public string ReasonForRevise { get; set; }
        public DateTime? ActualReceivedUnitFromGR { get; set; }
        public string BPHNumber { get; set; }
        public DateTime? ClearingDate { get; set; }
        public int? TermOfPayment { get; set; }
        public DateTime? ActualInvoiceReceived { get; set; }
        public DateTime? DateDeliveredInvoiceToFinance { get; set; }
        public DateTime? PayPlan { get; set; }
        public string ScanBSTK { get; set; }
        public string ScanSTNKSTCK { get; set; }
        public string KeteranganBayar { get; set; }
        public string KeteranganTagihan { get; set; }
        public string StatusPO { get; set; }
    }
}