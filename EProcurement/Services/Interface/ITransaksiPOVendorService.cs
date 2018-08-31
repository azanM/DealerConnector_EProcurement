using System.Collections.Generic;
using EProcurement.Models.ViewModel.Transaksi;
using System.Threading.Tasks;

namespace EProcurement.Services
{
    public interface ITransaksiPOVendorService
    {
        List<ListPOVendorViewModel> GetSearch(string VendorId, string PoNumber);
        List<ListPOVendorViewModel> GetSearch(string VendorId, string PoNumber, string NoRangka);
        List<ListPOVendorViewModel> GetAll(string VendorId);
        List<ListPOVendorViewModel> GetAllDouble(string VendorId);
        DetailAssignmentPOVendorViewModel GetDetailAssignment(string poNumber);
        DetailDeliveryPOVendorViewModel GetDetailDelivery(string poNumber);
        DetailInvoicePOVendorViewModel GetDetailInvoice(string poNumber);
        DetailBPKBPOVendorViewModel GetDetailBPKB(string poNumber);
        DetailBSTKPOVendorViewModel GetDetailBSTK(string poNumber);
        DetailAssignmentPOVendorViewModel SubmitAssignment(DetailAssignmentPOVendorViewModel model);
        DetailDeliveryPOVendorViewModel UpdateDelivery(DetailDeliveryPOVendorViewModel model);
        DetailInvoicePOVendorViewModel UpdateInvoice(DetailInvoicePOVendorViewModel model);
        DetailBPKBPOVendorViewModel UpdateBPKB(DetailBPKBPOVendorViewModel model);
        Task InsertUploadFile(string PONumber, string ket, string fileName);
    }
} 