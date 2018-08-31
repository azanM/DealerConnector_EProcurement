using System.Collections.Generic;
using System.Linq;
using EProcurement.Models;
using EProcurement.Models.ViewModel.Reporting;
using EProcurement.Services.Interface;

namespace EProcurement.Services.Implementation
{
    public class ReportPOVendorService : IReportPOVendorService
    {
        public List<ReportPOVendorViewModel> GetAll()
        {
            string VendorID = System.Web.HttpContext.Current.Session["VendorID"] == null ? "" : System.Web.HttpContext.Current.Session["VendorID"].ToString();
            var dc = new eprocdbDataContext();
            var model = (from custPO in dc.CUSTOMPOs
                         join custIR in dc.CUSTOMIRs on custPO.PONUMBER equals custIR.PONUMBER
                         join custGR in dc.CUSTOMGRs on custPO.PONUMBER equals custGR.PONUMBER
                         join custBPKB in dc.CUSTOMBPKBs on custPO.PONUMBER equals custBPKB.PONUMBER
                         join custPR in dc.CUSTOMPRs on custPO.PRSAP equals custPR.PRSAP
                         join custMISC in dc.CUSTOMMISCs on custPO.PONUMBER equals custMISC.PONumber
                         join stream in dc.STREAMLINERs on custPO.PONUMBER equals stream.PONUMBER
                         join custSTAT in dc.CUSTOMSTATUS on custPO.POSTATUSID equals custSTAT.ID
                         where custPO.TGLPO.Value.Year > (System.DateTime.Now.Year - 3)
                         && custPO.VENDORID == VendorID
                         orderby custPO.TGLPO
                         select new ReportPOVendorViewModel
                         {
                             Officer = stream.OfficerName,
                             Plant = stream.Plant,
                             PeriodePO = custPO.TGLPO.Value.Month,
                             TglPO = custPO.TGLPO,
                             PONumber = custPO.PONUMBER,
                             TypeUnit = custPO.DESCPO,
                             Warna = custPO.COLOR, //
                             BBN = stream.BBN,
                             StatusBeli = custPO.PURCHASESTATUS,
                             YEAR = custPR.YEAR,
                             Merk = custPO.MERK,
                             Varian = custPO.TYPEUNIT,
                             NoPolisi = custBPKB.NOPOLISI,
                             VarianVendor = custPO.VARIAN_INPUT,
                             MerkVendor = custPO.MERK_INPUT,
                             NoChasis = custGR.NOCHASIS,
                             NoEngine = custGR.NOENGINE,
                             TglSTNK = custBPKB.TGLSTNK,
                             NoPolisiVendor = custBPKB.NOPOLISI_INPUT,
                             NoChasisVendor = custGR.NOCHASIS_INPUT,
                             NoEngineVendor = custGR.NOENGINE_INPUT,
                             TglSTNKVendor = custBPKB.TGLSTNK_INPUT,
                             KeteranganKaroseri = custBPKB.KETKAROSERI,
                             TglMasukKaroseri = custPO.TGLMASUKKAROSERI,
                             TglSelesaiKaroseri = custPO.TGLSELESAIKAROSERI,
                             InvoiceDate = custIR.INVDATE,
                             InvoiceNumber = custIR.INVNO,
                             OTRPrice = custPO.ONTHEROADPRICE,
                             Disc = custPO.DISC,
                             NetPrice = custPO.ONTHEROADPRICE + custPO.DISC,
                             HargaDPP = custPO.HARGADPP,
                             HargaPPNUnit = custPO.HARGAPPNUNIT,
                             HargaBBNVendor = custPO.HARGABBN_INPUT,
                             HargaDPPVendor = custPO.HARGADPP_INPUT,
                             HargaPPNUnitVendor = custPO.HARGAPPNUNIT_INPUT,
                             NoFakturPajak = custIR.NOFAKTURPAJAK,
                             NoAP = custIR.NOAP,
                             ActualReceivedInvoice = custIR.ACTUALRECEIVEDINV,
                             EstimatedPaymentDate = custIR.ESTIMATEDPAYMENTDATE,
                             TglPembayaran = custIR.TGLPEMBAYARAN,
                             InvoiceStatus = custIR.INVOICESTATUS,
                             NamaPemilik = custPO.COMPANYNAME,
                             NoBPKB = custBPKB.NOBPKB,
                             ActualReceivedBPKBCabang = custBPKB.ACTUALRECEIVEDBPKBDICAB,
                             KeteranganBPKB = custBPKB.KETBPKB,
                             PosisiBPKB = custBPKB.POSISIBPKB,
                             StatusBPKB = custBPKB.STATUSBPKB,
                             DetailProblem = custBPKB.DETAILPROBLEM,
                             RemarksDetailProblem = custBPKB.REMARKSDETAILPROBLEM,
                             AgingOutstandingUnit = (custPO.ACTUALDATEDELIVEREDUNIT == null ? (System.DateTime.Now - custPO.TGLPO).Value.Days :
                                                    (custPO.ACTUALDATEDELIVEREDUNIT - custPO.TGLPO).Value.Days).ToString(),
                             AgingOverdueOutstandongUnit = (custPO.ACTUALDATEDELIVEREDUNIT == null ? (System.DateTime.Now - custPO.PROMISEDLVDATEPO).Value.Days :
                                                        (custPO.PROMISEDLVDATEPO - custPO.ACTUALDATEDELIVEREDUNIT).Value.Days).ToString(),
                             AgingOverdueOutstandingUnitBPKB = (custBPKB.TGLGRBPKB == null ? (System.DateTime.Now - custPO.ACTUALDATEDELIVEREDUNIT).Value.Days :
                                                                (custBPKB.TGLGRBPKB - custPO.ACTUALDATEDELIVEREDUNIT).Value.Days).ToString(),
                             TglPromiseDeliveryBPKB = custMISC.ItemDelvDate,
                             //NamaPemilikVendor = custGR.NAMAPEMILIK_INPUT,
                             POStatus = custSTAT.STATUS,
                             //POStatus = (
                             //               (custPO.POSTATUSID == null || custPO.POSTATUSID == "2") &&
                             //                   custMISC.MatDoc == null &&
                             //                   custMISC.FlagDelete == null ? "PO Assignment" :

                             //               custMISC.FlagDelete == "L" ? "PO Cancel" :

                             //               (custMISC.AccDocNumber == null &&
                             //                   (custGR.TGLGRSAP != null ||
                             //                       custGR.NOCHASIS != null) ||
                             //                       custPO.ACTUALDATEDELIVEREDUNIT != null) ? "PO Unit Delivery" :

                             //               custMISC.AccDocNumber != null &&
                             //                   custMISC.ClearingDocNumber != null ? "AP In process" :

                             //               custMISC.AccDocNumber != null &&
                             //                   custMISC.ClearingDocNumber != null &&
                             //                   custMISC.MatDoc == null &&
                             //                   custMISC.ItemDelvDate == null &&
                             //                   custBPKB.TGLGRBPKB == null ? "AP Complete" :

                             //               custMISC.AccDocNumber != null &&
                             //                   custMISC.ClearingDocNumber != null &&
                             //                   custMISC.MatDoc != null &&
                             //                   custBPKB.TGLGRBPKB != null &&
                             //                   (custBPKB.TGLGRBPKB == null && custMISC.ItemDelvDate == null) ? "BPKB Outstanding" :

                             //               custMISC.MatDoc != null &&
                             //                   custMISC.ItemDelvDate != null &&
                             //                   custMISC.ClearingDocNumber != null &&
                             //                   custMISC.AccDocNumber != null &&
                             //                   custBPKB.TGLGRBPKB != null ? "PO Complete" :

                             //               (
                             //                   custPO.POSTATUSID == "2" ? "PO assignment" :
                             //                   custPO.POSTATUSID == "3" ? "PO Accept Vendor" :
                             //                   custPO.POSTATUSID == "4" ? "PO Reject Vendor" :
                             //                   custPO.POSTATUSID == "5" ? "PO Unit Delivery" :
                             //                   custPO.POSTATUSID == "6" ? "PO Invoice Delivery" :
                             //                   custPO.POSTATUSID == "7" ? "AP In process" :
                             //                   custPO.POSTATUSID == "8" ? "AP Complete" :
                             //                   custPO.POSTATUSID == "9" ? "AP Reject" :
                             //                   custPO.POSTATUSID == "10" ? "PO Cancel" :
                             //                   custPO.POSTATUSID == "11" ? "BPKB Outstanding" :
                             //                   custPO.POSTATUSID == "12" ? "PO Complete" :
                             //                   custPO.POSTATUSID == "13" ? "Approval" :
                             //                   "PO Revise Vendor"
                             //               )
                             //           )
                         }).ToList();

            return model;
        }
    }
}