using System;

namespace EProcurement.Models.ViewModel.Transaksi
{
    public class ListPOProcViewModel
    {
        public string PRNumber { get; set; }
        public DateTime? TglPO { get; set; }
        public string VendorId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string NoChassis { get; set; }
        public string NoChassisInput { get; set; }
        public string PONumber { get; set; }
        public string VendorName { get; set; }
        public string POStatusId { get; set; }
        public string POStatus { get; set; }
        public string ModifiedBy { get; set; }
        public string NRP { get; set; }
        public string Fullname { get; set; }
        public string Approved { get; set; }
        public string OfficerName { get; set; }
        public string POStatusStream { get; set; }
        public string StrTglPO { get; set; }
        public string StrModifiedDate { get; set; }
    }
}