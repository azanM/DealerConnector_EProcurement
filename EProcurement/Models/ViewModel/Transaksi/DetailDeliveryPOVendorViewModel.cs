using System;

namespace EProcurement.Models.ViewModel.Transaksi
{
    public class DetailDeliveryPOVendorViewModel
    {
        public string PONumber { get; set; }
        public DateTime? PODate { get; set; }
        public string PODescription { get; set; }
        public DateTime? ExpectedDateDelivered { get; set; }
        public string OfficerName { get; set; }
        public string BBN { get; set; }
        public double? DPP { get; set; }
        public double? PPN { get; set; }
        public double? BBNPrice { get; set; }
        public double? DPPByVendor { get; set; }
        public double? PPNByVendor { get; set; }
        public double? BBNPriceByVendor { get; set; }
        public DateTime? FakturDODate { get; set; } 
        public string FakturDONumber { get; set; }
        public string LicensePlate { get; set; }
        public string ChassisNumber { get; set; }
        public string MachineNumber { get; set; }
        public DateTime? STNKDate { get; set; }
        public string LicensePlateByVendor { get; set; }
        public string ChassisNumberByVendor { get; set; }
        public string MachineNumberByVendor { get; set; }
        public DateTime? STNKDateByVendor { get; set; }
        public DateTime? STCKDate { get; set; }
        public DateTime? TanggalBSTB { get; set; }
        public string PICAdmin { get; set; }
        public string CarrosserieAccessoriesVendorName { get; set; }
        public string CarrosserieAccessoriesAddress { get; set; }
        public string RemarksCarrosserieAccessories { get; set; }
        public DateTime? DateEntryCarrosserieAccessories { get; set; }
        public DateTime? DateFinishCarrosserieAccessories { get; set; }
        public string RemarksPO { get; set; }
        public string UploadScanCopyPO { get; set; }
        public string PoDateString { get; set; }
        public string strTanggalBSTB { get; set; }
        public string strDateFinishCarrosserieAccessories { get; set; }
        public string strDateEntryCarrosserieAccessories { get; set; }
        public string strDoDate { get; set; }
        public string strSTNKDateByVendor { get; set; }
    } 
} 