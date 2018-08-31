using System;
using System.ComponentModel.DataAnnotations;

namespace EProcurement.Models.ViewModel.Transaksi
{
    public class DetailDeliveryPOProcViewModel
    {
        public string PONumber { get; set; }
        public DateTime? PODate { get; set; }
        public string PlantBranch { get; set; }
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
        public string LicensePlateNumberSAP { get; set; }
        public string ChassisNumberSAP { get; set; }
        public string MachineNumberSAP { get; set; }
        public string ChassisNumberByVendor { get; set; }
        public string MachineNumberByVendor { get; set; }
        public string LicensePlateByVendor { get; set; }
        public string PurchaseStatus { get; set; }
        public string CarrosserieAccessoriesVendorName { get; set; }
        public string CarrosserieAccessoriesAddress { get; set; }
        public string RemarksCarrosserieAccessories { get; set; }
        public string PICCarrosserieAccessories { get; set; }
        public string PhoneCarrosserieAccessories { get; set; }
        public string BentukCarrosserieAccessories { get; set; }
        public DateTime? InfoPromiseDeliveryAccessories { get; set; }
        public DateTime? DateEntryCarrosserieAccessories { get; set; }
        public DateTime? DateFinishCarrosserieAccessories { get; set; }
        public string RemarksPO { get; set; }
        public string RemarksRejectionPOByVendor { get; set; }
        public string UploadScanCopyPO { get; set; }
    }
}