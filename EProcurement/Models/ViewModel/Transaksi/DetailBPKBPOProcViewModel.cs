using System;

namespace EProcurement.Models.ViewModel.Transaksi
{
    public class DetailBPKBPOProcViewModel
    {
        public string PONumber { get; set; }
        public DateTime? PODate { get; set; }
        public string PODescription { get; set; }
        public DateTime? BPKBGRDate { get; set; }
        public DateTime? ActualDeliveredDateBPKBFromVendor { get; set; }
        public DateTime? ActualReceivedDateBPKBdiHO { get; set; }
        public DateTime? ActualReceivedDateBPKBdiCabang { get; set; }
        public DateTime? ActualReceivedDateBPKBHOFromBranch { get; set; }
        public string DetailProblem { get; set; }
        public string StatusBPKB { get; set; }
        public string ReasonForReviseBPKB { get; set; }
        public DateTime? TanggalBSTB { get; set; }
        public DateTime? ActualDeliveredDateBPKBToFinance { get; set; }
        public DateTime? BPKBDateSentBack { get; set; }
        public DateTime? ActualReceivedBPKBHOBack { get; set; }
        public DateTime? DateDeliveryToBranchVendor { get; set; }
        public DateTime? ClearingDate { get; set; }
        public string BPKBNumber { get; set; }
        public string NoFakturKendaraan { get; set; }
        public string NoSertifikatNIK { get; set; }
        public string NoFormulirA { get; set; }
        public string NoRangka { get; set; }
        public string NoMesin { get; set; }
        public string KeteranganSuratUbahBentuk { get; set; }
        public string NoSuratUbahBentuk { get; set; }
        public DateTime? TanggalSuratUbahBentuk { get; set; }
        public string KeteranganSuratUbahWarna { get; set; }
        public string NoSuratUbahWarna { get; set; }
        public DateTime? TanggalSuratUbahWarna { get; set; }
        public string NoSertifikatRegistrasiUjiTipe { get; set; }
        public string RemarksDetailProblem { get; set; }
        public string RemarksBPKB { get; set; }
        public string StatusPO { get; set; }
    }
}