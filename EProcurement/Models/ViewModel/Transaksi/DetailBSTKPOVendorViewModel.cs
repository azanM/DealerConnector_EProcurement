using System;

namespace EProcurement.Models.ViewModel.Transaksi
{
    public class DetailBSTKPOVendorViewModel
    {
        public string PONumber { get; set; }
        public DateTime? PODate { get; set; }
        public string PODescription { get; set; }
        public string ScanBSTK { get; set; }
        public string ScanSTNKSTCK { get; set; }
    }
}