using System.Collections.Generic;
using EProcurement.Models.ViewModel.Transaksi;
using EProcurement.Models;

namespace EProcurement.Services
{
    public interface ITransaksiPOProcService
    {
        List<ListPOProcViewModel> GetSearch(string poNumber, string NoRangka);
        List<ListPOProcViewModel> GetAll();
        List<ListPOProcViewModel> GetAllDouble();
        DetailAssignmentPOProcViewModel GetDetailAssignment(string poNumber);
        DetailDeliveryPOProcViewModel GetDetailDelivery(string poNumber);
        DetailInvoicePOProcViewModel GetDetailInvoice(string poNumber);
        DetailBPKBPOProcViewModel GetDetailBPKB(string poNumber);
        DetailDeliveryPOProcViewModel UpdateDelivery(DetailDeliveryPOProcViewModel model);
        DetailInvoicePOProcViewModel UpdateInvoice(DetailInvoicePOProcViewModel model);
        DetailBPKBPOProcViewModel UpdateBPKB(DetailBPKBPOProcViewModel model);
        CUSTOMIR ReviseDetailInvoice(string PONumber, string ReasonForRevise);
        CUSTOMBPKB ReviseDetailBPKB(string PONumber, string ReasonForRevise);
        DetailAssignmentPOProcViewModel UpdateAssignment(DetailAssignmentPOProcViewModel model);

    }
}