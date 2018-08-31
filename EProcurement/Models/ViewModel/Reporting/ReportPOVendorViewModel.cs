using System;

namespace EProcurement.Models.ViewModel.Reporting
{
    public class ReportPOVendorViewModel
    {
        public string Officer { get; set; }
        public string Plant { get; set; }
        public int PeriodePO { get; set; }
        public DateTime? TglPO { get; set; }
        public string PONumber { get; set; }
        public string TypeUnit { get; set; }
        public string Warna { get; set; }
        public string BBN { get; set; }
        public string StatusBeli { get; set; }
        public string YEAR { get; set; }
        public string Merk { get; set; }
        public string Varian { get; set; }
        public string NoPolisi { get; set; }
        public string NoEngine { get; set; }
        public DateTime? TglSTNK { get; set; }
        public string NoPolisiVendor { get; set; }
        public string NoChasisVendor { get; set; }
        public string NoEngineVendor { get; set; }
        public DateTime? TglSTNKVendor { get; set; }
        public string KeteranganKaroseri { get; set; }
        public DateTime? TglMasukKaroseri { get; set; }
        public DateTime? TglSelesaiKaroseri { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public double? OTRPrice { get; set; }
        public string OTRVendor { get; set; }
        public double? Disc { get; set; }
        public double? NetPrice { get; set; }
        public double? HargaDPP { get; set; }
        public double? HargaPPNUnit { get; set; }
        public double? HargaBBNVendor { get; set; }
        public double? HargaDPPVendor { get; set; }
        public double? HargaPPNUnitVendor { get; set; }
        public string NoFakturPajak { get; set; }
        public string NoAP { get; set; }
        public DateTime? ActualReceivedInvoice { get; set; }
        public DateTime? EstimatedPaymentDate { get; set; }
        public DateTime? TglPembayaran { get; set; }
        public string InvoiceStatus { get; set; }
        public string NamaPemilik { get; set; }
        public string NoBPKB { get; set; }
        public DateTime? ActualReceivedBPKBCabang { get; set; }
        public string KeteranganBPKB { get; set; }
        public string PosisiBPKB { get; set; }
        public string StatusBPKB { get; set; }
        public string DetailProblem { get; set; }
        public string RemarksDetailProblem { get; set; }
        public string AgingOutstandingUnit { get; set; }
        public string AgingOverdueOutstandongUnit { get; set; }
        public string AgingOutstandingUnitBPKB { get; set; }
        public string AgingOverdueOutstandingUnitBPKB { get; set; }
        public DateTime? TglPromiseDeliveryBPKB { get; set; }
        public string NamaPemilikVendor { get; set; }
        public string POStatus { get; set; }
        public string NoChasis { get; set; }
        public string MerkVendor { get; set; }
        public string VarianVendor { get; set; }
    }
}