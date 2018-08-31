using System;

namespace EProcurement.Models.ViewModel.Transaksi
{
    public class ListPOVendorViewModel
    {
        public string PONumber { get; set; }
        public DateTime? PODate { get; set; }
        public string OfficerName { get; set; }
        public string StatusId { get; set; }
        public string StatusPO { get; set; }
        public string ChasisNumber { get; set; }
        public string VendorID { get; set; }
        public string StrPoDate { get; set; }
    }
} 