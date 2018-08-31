using System;
using System.ComponentModel.DataAnnotations;

namespace EProcurement.Models.ViewModel.Transaksi
{
    public class DetailAssignmentPOProcViewModel
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
        public string PRKaroseri { get; set; }
        public string Accessories { get; set; }
        public string AssignVendorTo { get; set; }
        public string BBN { get; set; }
        public string Color { get; set; }
        public string TujuanDeliveryUnit { get; set; }        
    }
}