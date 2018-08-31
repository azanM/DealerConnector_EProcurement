using System;

namespace EProcurement.Models.ViewModel.Transaksi
{
    public class DetailAssignmentPOVendorViewModel
    {
        public string PONumber { get; set; }
        public DateTime? PODate { get; set; }
        public string PODescription { get; set; }
        public string PlantCabang { get; set; }
        public double? OnTheRoadPrice { get; set; }
        public double? Discount { get; set; }
        public double? NetPrice { get; set; }
        public string Color { get; set; }
        public DateTime? PromiseDeliveryDate { get; set; }
        public string OfficerName { get; set; }
        public string PurchaseStatus { get; set; } 
        public string UnitDeliveryAddress { get; set; }
        public string ProsesKeVendorKaroseri { get; set; }
        public string ProsesKeVendorAksesoris { get; set; }
        public string ReasonForRejection { get; set; }
        public string POStatus { get; set; }
        public string BBN { get; set; }
        public string TujuanDeliveryUnit { get; set; }
        public string strOTR { get; set; }
        public string strDiscount { get; set; } 
        public string strNetPrice { get; set; }
        public string strPODate { get; set; }
        public string strPromiseDeliveryDate { get; set; } 
    }
}