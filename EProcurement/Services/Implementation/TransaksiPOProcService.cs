using System.Collections.Generic;
using System.Linq;
using EProcurement.Models;
using EProcurement.Models.ViewModel.Transaksi;
using System;
using EProcurement.Controllers;
using System.Text.RegularExpressions;

namespace EProcurement.Services
{
    public class TransaksiPOProcService : ITransaksiPOProcService
    {
        HomeController general = new HomeController();
        public static string newLineBreak(string input)
        {
            Regex regex = new Regex(@"[\\\r]+");
            return regex.Replace(input, "<br />");
        }
        public List<ListPOProcViewModel> GetAllDouble()
        {
            var dc = new eprocdbDataContext();
            var part1 = GetAll();
            //var part2 = GetAll().Where(p => p.POStatus != "PO Cancel" && p.POStatus != "PO Complete" && p.POStatus != "Rejected");
            //foreach (var p in part2)
            //{
            //    p.POStatus = "BPKB Outstanding";
            //}
            //part1.Union(part2).OrderBy(p => p.PONumber).ToList();
            var model = part1.OrderBy(p => p.PONumber).ToList();
            //part2.OrderBy(p => p.PONumber).ToList();
            return model;
        }
        public List<ListPOProcViewModel> GetAll()
        {
            var dc = new eprocdbDataContext();
            var model = (from a in dc.CUSTOMPOs
                         join d in dc.CUSTOMSTATUS on a.POSTATUSID equals d.ID
                         join b in dc.CUSTOMGRs on a.PONUMBER equals b.PONUMBER
                         join custMISC in dc.CUSTOMMISCs on a.PONUMBER equals custMISC.PONumber
                         //from c in dc.STREAMLINERs.Where(mapping1 => mapping1.PONUMBER == a.PONUMBER).DefaultIfEmpty() // on a.PONUMBER equals c.PONUMBER
                         join c in dc.STREAMLINERs on a.PONUMBER equals c.PONUMBER                                                                                              //join c in dc.STREAMLINERs on a.PONUMBER equals c.PONUMBER
                         join custBPKB in dc.CUSTOMBPKBs on a.PONUMBER equals custBPKB.PONUMBER
                         join status in dc.CUSTOMSTATUS on a.POSTATUSID equals status.ID
                         // from d in dc.CUSTOMSTATUS.Where(mapping1 => mapping1.ID == a.POSTATUSID).DefaultIfEmpty()  //on a.POSTATUSID equals d.ID
                         // from b in dc.CUSTOMGRs.Where(mapping1 => mapping1.PONUMBER == a.PONUMBER).DefaultIfEmpty() //on a.PONUMBER equals b.PONUMBER
                         //from custMISC in dc.CUSTOMMISCs.Where(mapping1 => mapping1.PONumber == a.PONUMBER).DefaultIfEmpty() //on a.PONUMBER equals custMISC.PONumber
                         //from c in dc.STREAMLINERs.Where(mapping1 => mapping1.PONUMBER == a.PONUMBER).DefaultIfEmpty() // on a.PONUMBER equals c.PONUMBER
                         //from custBPKB in dc.CUSTOMBPKBs.Where(mapping1 => mapping1.PONUMBER == a.PONUMBER).DefaultIfEmpty()  //on a.PONUMBER equals custBPKB.PONUMBER
                         //from status in dc.CUSTOMSTATUS.Where(mapping => mapping.ID == a.POSTATUSID).DefaultIfEmpty()
                         select new ListPOProcViewModel
                         {
                             TglPO = a.TGLPO,
                             NoChassis = b.NOCHASIS,
                             OfficerName = c.OfficerName,
                             VendorId = a.VENDORID,
                             ModifiedDate = a.MODIFIED_DATE,
                             PONumber = a.PONUMBER,
                             VendorName = a.VENDORNAME,
                             POStatusId = a.POSTATUSID,
                             POStatusStream = c.POStatus,
                             POStatus = c.POStatus == "Rejected" ? "Rejected" : d.STATUS,
                             ModifiedBy = a.MODIFIED_BY
                         }).Distinct().ToList();
            model = (from a in model
                     orderby a.TglPO descending
                     select new ListPOProcViewModel
                     {
                         TglPO = a.TglPO,
                         NoChassis = a.NoChassis,
                         OfficerName = a.OfficerName,
                         VendorId = a.VendorId,
                         ModifiedDate = a.ModifiedDate,
                         PONumber = a.PONumber,
                         VendorName = a.VendorName,
                         POStatusId = a.POStatusId,
                         POStatusStream = a.POStatusStream,
                         POStatus = a.POStatus,
                         ModifiedBy = a.ModifiedBy,
                         StrModifiedDate = a.ModifiedDate.ToString(),
                         StrTglPO = a.TglPO == null ? "" : a.TglPO.Value.ToString("dd/MM/yyyy"),
                     }).OrderByDescending(p => p.TglPO).ToList();
            return model;
        }
        public List<ListPOProcViewModel> GetSearch(string PoNumber, string NoRangka)
        {
            var dc = new eprocdbDataContext();
            var model = new List<ListPOProcViewModel>();

            model = (from a in dc.CUSTOMPOs
                     join d in dc.CUSTOMSTATUS on a.POSTATUSID equals d.ID
                     join b in dc.CUSTOMGRs on a.PONUMBER equals b.PONUMBER
                     join custMISC in dc.CUSTOMMISCs on a.PONUMBER equals custMISC.PONumber
                     join c in dc.STREAMLINERs on a.PONUMBER equals c.PONUMBER // on a.PONUMBER equals c.PONUMBER//join c in dc.STREAMLINERs on a.PONUMBER equals c.PONUMBER
                     join custBPKB in dc.CUSTOMBPKBs on a.PONUMBER equals custBPKB.PONUMBER
                     join status in dc.CUSTOMSTATUS on a.POSTATUSID equals status.ID
                     where a.PONUMBER.Contains(PoNumber)
                     select new ListPOProcViewModel
                     {
                         TglPO = a.TGLPO,
                         NoChassis = b.NOCHASIS,
                         NoChassisInput = b.NOCHASIS_INPUT,
                         OfficerName = c.OfficerName,
                         VendorId = a.VENDORID,
                         ModifiedDate = a.MODIFIED_DATE,
                         PONumber = a.PONUMBER,
                         VendorName = a.VENDORNAME,
                         POStatusId = a.POSTATUSID,
                         POStatusStream = c.POStatus,
                         POStatus = c.POStatus == "Rejected" ? "Rejected" : d.STATUS,
                         StrTglPO = a.TGLPO.ToString(),
                         StrModifiedDate = a.MODIFIED_DATE.ToString(),
                         ModifiedBy = a.MODIFIED_BY
                     }).Distinct().ToList();

            model = (from a in model
                     orderby a.TglPO descending
                     select new ListPOProcViewModel
                     {
                         TglPO = a.TglPO,
                         NoChassis = a.NoChassis,
                         OfficerName = a.OfficerName,
                         VendorId = a.VendorId,
                         ModifiedDate = a.ModifiedDate,
                         PONumber = a.PONumber,
                         VendorName = a.VendorName,
                         POStatusId = a.POStatusId,
                         POStatusStream = a.POStatusStream,
                         POStatus = a.POStatus,
                         ModifiedBy = a.ModifiedBy,
                         StrModifiedDate = a.ModifiedDate.ToString(),
                         StrTglPO = a.TglPO == null ? "" : a.TglPO.Value.ToString("dd/MM/yyyy"),
                     }).OrderByDescending(p => p.TglPO).ToList();

            if (PoNumber != "")
                model = model.Where(p => p.PONumber.Contains(PoNumber)).ToList();

            if (NoRangka != "")
                model = model.Where(p => (p.NoChassis != null && p.NoChassis.Contains(NoRangka)) ||
                (p.NoChassisInput != null && p.NoChassisInput.Contains(NoRangka))).ToList();

            return model;
        }
        public DetailAssignmentPOProcViewModel GetDetailAssignment(string poNumber)
        {
            var dc = new eprocdbDataContext();
            var model = (from custpo in dc.CUSTOMPOs
                         from stream in dc.STREAMLINERs.Where(mapping1 => mapping1.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() // on custpo.PONUMBER equals stream.PONUMBER
                         from custgr in dc.CUSTOMGRs.Where(mapping1 => mapping1.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() //on custpo.PONUMBER equals custgr.PONUMBER
                         where custpo.PONUMBER == poNumber
                         select new DetailAssignmentPOProcViewModel
                         {
                             TujuanDeliveryUnit = custgr.STATUSDELIVERYUNIT,
                             PONumber = custpo.PONUMBER,
                             PODate = custpo.TGLPO,
                             PODescription = custpo.DESCPO,
                             OnTheRoadPrice = custpo.ONTHEROADPRICE,
                             Discount = custpo.DISC,
                             NetPrice = (custpo.ONTHEROADPRICE + custpo.DISC),
                             ExpectedDateDelivered = custpo.EXPECTEDDATEDELIVERED,
                             PromiseDeliveryDate = custpo.PROMISEDLVDATEPO,
                             OfficerName = stream.OfficerName,
                             PRKaroseri = stream.PRKaroseri,
                             Accessories = stream.PRAccessories,
                             AssignVendorTo = custpo.VENDORNAME,
                             Color = custpo.COLOR,
                             BBN = stream.BBN
                         }).SingleOrDefault();
            return model;
        }
        public DetailDeliveryPOProcViewModel GetDetailDelivery(string poNumber)
        {
            var dc = new eprocdbDataContext();
            var model = (from custpo in dc.CUSTOMPOs
                         from custgr in dc.CUSTOMGRs.Where(mapping1 => mapping1.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() //on custpo.PONUMBER equals custgr.PONUMBER
                         from custbpkb in dc.CUSTOMBPKBs.Where(mapping1 => mapping1.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() //on custgr.PONUMBER equals custbpkb.PONUMBER
                         from stream in dc.STREAMLINERs.Where(mapping1 => mapping1.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() //on custpo.PONUMBER equals stream.PONUMBER
                                                                                                                                 //join upload in dc.UPLOAD_FILEs.Where(mappingUpload => mappingUpload.PONUMBER == custpo.PONUMBER).FirstOrDefault() //on custpo.PONUMBER equals upload.PONUMBER
                         where custpo.PONUMBER == poNumber
                         select new DetailDeliveryPOProcViewModel
                         {
                             PONumber = custpo.PONUMBER,
                             PODate = custpo.TGLPO,
                             PlantBranch = custpo.CABANGTRAC,
                             PODescription = custpo.DESCPO,
                             OnTheRoadPrice = custpo.ONTHEROADPRICE,
                             Discount = custpo.DISC,
                             NetPrice = (custpo.ONTHEROADPRICE + custpo.DISC),
                             ExpectedDateDelivered = custpo.EXPECTEDDATEDELIVERED.Value.Date,
                             PromiseDeliveryDate = custpo.PROMISEDLVDATEPO.Value.Date,
                             OfficerName = stream.OfficerName,
                             LicensePlateNumberSAP = custbpkb.NOPOLISI,
                             ChassisNumberSAP = custgr.NOCHASIS,
                             MachineNumberSAP = custgr.NOENGINE,
                             LicensePlateByVendor = custbpkb.NOPOLISI_INPUT,
                             ChassisNumberByVendor = custgr.NOCHASIS_INPUT,
                             MachineNumberByVendor = custgr.NOENGINE_INPUT,
                             PurchaseStatus = custpo.PURCHASESTATUS,
                             CarrosserieAccessoriesVendorName = custpo.CARROSERIEVENDORNAME,
                             CarrosserieAccessoriesAddress = custpo.ACCESORIESADDRESS,
                             RemarksCarrosserieAccessories = custpo.REMARKSCARROSSERIE,
                             PICCarrosserieAccessories = custpo.PICKAROSERI,
                             PhoneCarrosserieAccessories = custpo.NOTELEPONKAROSERI,
                             BentukCarrosserieAccessories = custpo.BENTUKKAROSERI,
                             InfoPromiseDeliveryAccessories = custpo.PROMISEDLVDATEPO,
                             DateEntryCarrosserieAccessories = custpo.TGLMASUKKAROSERI,
                             DateFinishCarrosserieAccessories = custpo.TGLSELESAIKAROSERI,
                             RemarksPO = custpo.REMARKS
                         }).SingleOrDefault();

            var file = dc.UPLOAD_FILEs.Where(mappingUpload => mappingUpload.PONUMBER == model.PONumber).FirstOrDefault();
            model.UploadScanCopyPO = file == null ? "" : file.FILE;
            return model;
        }
        public DetailInvoicePOProcViewModel GetDetailInvoice(string poNumber)
        {
            var dc = new eprocdbDataContext();
            var model = (from custpo in dc.CUSTOMPOs
                         from custgr in dc.CUSTOMGRs.Where(mapping1 => mapping1.PONUMBER == custpo.PONUMBER).DefaultIfEmpty()  //on custpo.PONUMBER equals custgr.PONUMBER
                         from custbpkb in dc.CUSTOMBPKBs.Where(mapping1 => mapping1.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() //on custgr.PONUMBER equals custbpkb.PONUMBER
                         from custir in dc.CUSTOMIRs.Where(mapping1 => mapping1.PONUMBER == custpo.PONUMBER).DefaultIfEmpty()  //on custpo.PONUMBER equals custir.PONUMBER
                         from custmisc in dc.CUSTOMMISCs.Where(mapping1 => mapping1.PONumber == custpo.PONUMBER).DefaultIfEmpty()  //on custpo.PONUMBER equals custmisc.PONumber
                         from stream in dc.STREAMLINERs.Where(mapping1 => mapping1.PONUMBER == custpo.PONUMBER).DefaultIfEmpty()
                             //on custpo.PONUMBER equals stream.PONUMBER
                             //join upload in dc.UPLOAD_FILEs on custpo.PONUMBER equals upload.PONUMBER
                             //into obj
                             //from upload in dc.UPLOAD_FILEs.DefaultIfEmpty()
                         where custpo.PONUMBER == poNumber
                         select new DetailInvoicePOProcViewModel
                         {
                             PONumber = custpo.PONUMBER,
                             PODate = custpo.TGLPO,
                             PODescription = custpo.DESCPO,
                             OnTheRoadPrice = custpo.ONTHEROADPRICE,
                             Discount = custpo.DISC,
                             NetPrice = (custpo.ONTHEROADPRICE + custpo.DISC),
                             ExpectedDateDelivered = custpo.EXPECTEDDATEDELIVERED.Value.Date,
                             PromiseDeliveryDate = custpo.PROMISEDLVDATEPO.Value.Date,
                             OfficerName = stream.OfficerName,
                             DateEntryCarrosserieAccessories = custpo.TGLMASUKKAROSERI,
                             NoSeriFakturPajak = custir.NOFAKTURPAJAK,
                             FakturDONumber = custpo.DONUMBER,
                             FakturDODate = custpo.DODATE,
                             ChassisNumberSAP = custgr.NOCHASIS,
                             MachineNumberSAP = custgr.NOENGINE,
                             ChassisNumber = custgr.NOCHASIS_INPUT,
                             MachineNumber = custgr.NOENGINE_INPUT,
                             StatusPO = custpo.POSTATUSID,
                             //DownloadPOVendor = upload.FILE,
                             DPPByVendor = custpo.HARGADPP_INPUT,
                             PPNByVendor = custpo.HARGAPPNUNIT_INPUT,
                             BBNPriceByVendor = custpo.HARGABBN_INPUT,
                             TanggalBSTB = custpo.ACTUALDATEDELIVEREDUNIT,//custgr.ACTUALRECEIVEDUNIT,
                             InvoiceNo = custir.INVNO,
                             InvoiceDate = custir.INVDATE,
                             InvoiceStatus = custir.INVOICESTATUS,
                             ReasonForRevise = custir.REASONREJECTIONINVOICE,
                             ActualReceivedUnitFromGR = custgr.TGLGRSAP,
                             BPHNumber = custmisc.AccDocNumber,
                             ClearingDate = custir.TGLPEMBAYARAN,
                             TermOfPayment = custir.TERMOFPAYMENT,
                             ActualInvoiceReceived = custir.ACTUALRECEIVEDINV,
                             DateDeliveredInvoiceToFinance = custir.TGLSERAHTAGIHAN,
                             PayPlan = custir.ESTIMATEDPAYMENTDATE,
                             ScanBSTK = string.Empty,
                             ScanSTNKSTCK = string.Empty,
                             KeteranganBayar = custir.KETBAYAR,
                             KeteranganTagihan = custir.KETTAGIHAN
                         }).FirstOrDefault();


            var file = dc.UPLOAD_FILEs.Where(mappingUpload => mappingUpload.PONUMBER == model.PONumber).FirstOrDefault();
            model.DownloadPOVendor = file == null ? "" : file.FILE;

            if (model.ActualInvoiceReceived != null)//perubahan Andri Dari InvoiceDate 
            {
                model.PayPlan = model.ActualInvoiceReceived.Value.AddDays(Convert.ToDouble(model.TermOfPayment == null ? 0 : model.TermOfPayment));
            }
            else
            {
                //  model.PayPlan = "";
            }

            return model;
        }
        public DetailBPKBPOProcViewModel GetDetailBPKB(string poNumber)
        {
            var dc = new eprocdbDataContext();
            var model = (from custpo in dc.CUSTOMPOs
                         from custgr in dc.CUSTOMGRs.Where(mapping1 => mapping1.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() //on custpo.PONUMBER equals custgr.PONUMBER
                         from custbpkb in dc.CUSTOMBPKBs.Where(mapping1 => mapping1.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() //on custgr.PONUMBER equals custbpkb.PONUMBER
                         from custir in dc.CUSTOMIRs.Where(mapping1 => mapping1.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() //on custpo.PONUMBER equals custir.PONUMBER
                         where custpo.PONUMBER == poNumber
                         select new DetailBPKBPOProcViewModel
                         {
                             PONumber = custpo.PONUMBER,
                             PODate = custpo.TGLPO,
                             PODescription = custpo.DESCPO,
                             BPKBGRDate = custbpkb.TGLGRBPKB,
                             ActualDeliveredDateBPKBFromVendor = custbpkb.ACTUALDELIVERYBPKBTOFINANCE,
                             ActualReceivedDateBPKBdiHO = custbpkb.ACTUALRECEIVEDBPKBDIHO,
                             ActualReceivedDateBPKBdiCabang = custbpkb.ACTUALRECEIVEDBPKBDICAB,
                             ActualReceivedDateBPKBHOFromBranch = custpo.ACTUALRECEIVEDBPKBHOFROMBRANCH,
                             DetailProblem = custbpkb.DETAILPROBLEM,
                             ReasonForReviseBPKB = custbpkb.REASONREVISEBPKB,
                             TanggalBSTB = custpo.ACTUALDATEDELIVEREDUNIT,
                             ActualDeliveredDateBPKBToFinance = custbpkb.ACTUALDELIVERYBPKBTOFINANCE,
                             BPKBDateSentBack = custbpkb.TGLBPKBDIKIRIMKEMBALI,
                             ActualReceivedBPKBHOBack = custbpkb.ACTUALRECEIVEDBPKBHOBACK,
                             DateDeliveryToBranchVendor = custbpkb.DATEDELIVERYTOBRANCHORVENDOR,
                             ClearingDate = custir.TGLPEMBAYARAN,
                             BPKBNumber = custbpkb.NOBPKB,
                             NoFakturKendaraan = custbpkb.NOFAKTUR,
                             NoSertifikatNIK = custbpkb.NOSERTIFIKAT,
                             NoFormulirA = custbpkb.NOFORMULIRA,
                             NoRangka = custgr.NOCHASIS,
                             NoMesin = custgr.NOENGINE,
                             StatusPO = custpo.POSTATUSID,
                             KeteranganSuratUbahBentuk = custbpkb.KETSURATUBAHBENTUK,
                             NoSuratUbahBentuk = custbpkb.NOSURATUBAHBENTUK,
                             TanggalSuratUbahBentuk = custbpkb.TGLSURATUBAHBENTUK,
                             KeteranganSuratUbahWarna = custbpkb.KETERANGANSURATRUBAHWARNA,
                             NoSuratUbahWarna = custbpkb.NOSURATRUBAHWARNA,
                             TanggalSuratUbahWarna = custbpkb.TANGGALSURATRUBAHWARNA,
                             NoSertifikatRegistrasiUjiTipe = custbpkb.NOSERTIFIKATREGUJITIPE,
                             RemarksDetailProblem = custbpkb.REMARKSDETAILPROBLEM,
                             RemarksBPKB = custbpkb.KETBPKB
                         }).SingleOrDefault();
            if (model.BPKBGRDate != null)
            {
                model.StatusBPKB = "Completed";
            }
            else if (model.ActualReceivedDateBPKBdiCabang != null)
            {
                model.StatusBPKB = "In Process";
            }
            else
            {
                model.StatusBPKB = "";
            }
            return model;
        }

        public DetailAssignmentPOProcViewModel UpdateAssignment(DetailAssignmentPOProcViewModel model)
        {
            var dc = new eprocdbDataContext();
            var result = (from custgr in dc.CUSTOMGRs where custgr.PONUMBER == model.PONumber select custgr).SingleOrDefault();
            result.STATUSDELIVERYUNIT = model.TujuanDeliveryUnit;
            result.MODIFIED_DATE = DateTime.Now;
            result.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();
            var result2 = (from custpo in dc.CUSTOMPOs where custpo.PONUMBER == model.PONumber select custpo).FirstOrDefault();
            result2.MODIFIED_DATE = DateTime.Now;
            result2.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();
            dc.SubmitChanges();
            UpdateStatus(model.PONumber);
            UpdateInvoiceStatus(model.PONumber);
            return model;
        }
        public DetailDeliveryPOProcViewModel UpdateDelivery(DetailDeliveryPOProcViewModel model)
        {
            var dc = new eprocdbDataContext();
            var result = (from custpo in dc.CUSTOMPOs where custpo.PONUMBER == model.PONumber select custpo).SingleOrDefault();
            result.PURCHASESTATUS = model.PurchaseStatus;
            result.CARROSERIEVENDORNAME = model.CarrosserieAccessoriesVendorName;
            result.ACCESORIESADDRESS = model.CarrosserieAccessoriesAddress;
            result.REMARKSCARROSSERIE = model.RemarksCarrosserieAccessories;
            result.PICKAROSERI = model.PICCarrosserieAccessories;
            result.NOTELEPONKAROSERI = model.PhoneCarrosserieAccessories;
            result.BENTUKKAROSERI = model.BentukCarrosserieAccessories;
            result.TGLSELESAIKAROSERI = model.DateFinishCarrosserieAccessories;
            result.REMARKS = model.RemarksPO;
            result.MODIFIED_DATE = DateTime.Now;
            result.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();
            dc.SubmitChanges();
            UpdateStatus(model.PONumber);
            UpdateInvoiceStatus(model.PONumber);
            return model;
        }
        public DetailInvoicePOProcViewModel UpdateInvoice(DetailInvoicePOProcViewModel model)
        {
            var dc = new eprocdbDataContext();
            var result = (from custpo in dc.CUSTOMPOs where custpo.PONUMBER == model.PONumber select custpo).SingleOrDefault();
            result.TGLMASUKKAROSERI = model.DateEntryCarrosserieAccessories;
            result.DONUMBER = model.FakturDONumber;
            result.DODATE = model.FakturDODate;
            result.REASONREVISEDBYVENDOR = model.ReasonForRevise;
            result.ACTUALDATEDELIVEREDUNIT = model.TanggalBSTB;
            result.MODIFIED_DATE = DateTime.Now;
            result.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();

            var result2 = (from custbpkb in dc.CUSTOMBPKBs where custbpkb.PONUMBER == model.PONumber select custbpkb).SingleOrDefault();
            result2.NOFAKTUR = model.NoSeriFakturPajak;
            result2.MODIFIED_DATE = DateTime.Now;
            result2.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();

            var result3 = (from custgr in dc.CUSTOMGRs where custgr.PONUMBER == model.PONumber select custgr).SingleOrDefault();
            result3.NOCHASIS_INPUT = model.ChassisNumber;
            result3.NOENGINE_INPUT = model.MachineNumber;
            //result3.ACTUALRECEIVEDUNIT = model.TanggalBSTB;
            result3.MODIFIED_DATE = DateTime.Now;
            result3.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();

            var result4 = (from custir in dc.CUSTOMIRs where custir.PONUMBER == model.PONumber select custir).SingleOrDefault();
            result4.NOFAKTURPAJAK = model.NoSeriFakturPajak;
            result4.ACTUALRECEIVEDINV = model.ActualInvoiceReceived;
            result4.TGLSERAHTAGIHAN = model.DateDeliveredInvoiceToFinance;
            result4.KETBAYAR = model.KeteranganBayar;
            result4.KETTAGIHAN = model.KeteranganTagihan;
            result4.MODIFIED_DATE = DateTime.Now;
            result4.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();
            dc.SubmitChanges();
            UpdateStatus(model.PONumber);
            UpdateInvoiceStatus(model.PONumber);
            return model;
        }
        public DetailBPKBPOProcViewModel UpdateBPKB(DetailBPKBPOProcViewModel model)
        {
            var dc = new eprocdbDataContext();

            var resultpo = (from custpo in dc.CUSTOMPOs where custpo.PONUMBER == model.PONumber select custpo).SingleOrDefault();
            resultpo.ACTUALRECEIVEDBPKBHOFROMBRANCH = model.ActualReceivedDateBPKBHOFromBranch;
            resultpo.MODIFIED_DATE = DateTime.Now;
            resultpo.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();
            resultpo.ACTUALDATEDELIVEREDUNIT = model.TanggalBSTB;
            var result = (from custbpkb in dc.CUSTOMBPKBs where custbpkb.PONUMBER == model.PONumber select custbpkb).SingleOrDefault();
            result.ACTUALRECEIVEDBPKBDIHO = model.ActualReceivedDateBPKBdiHO;
            result.ACTUALRECEIVEDBPKBDICAB = model.ActualReceivedDateBPKBdiCabang;
            result.DETAILPROBLEM = model.DetailProblem;
            result.REASONREVISEBPKB = model.ReasonForReviseBPKB;
            result.TGLSERAHBPKB = model.BPKBGRDate;

            result.ACTUALDELIVERYBPKBTOFINANCE = model.ActualDeliveredDateBPKBToFinance;
            result.TGLBPKBDIKIRIMKEMBALI = model.BPKBDateSentBack;
            result.ACTUALRECEIVEDBPKBHOBACK = model.ActualReceivedBPKBHOBack;
            result.DATEDELIVERYTOBRANCHORVENDOR = model.DateDeliveryToBranchVendor;
            result.NOBPKB = model.BPKBNumber;
            result.NOFAKTUR = model.NoFakturKendaraan;
            result.NOSERTIFIKAT = model.NoSertifikatNIK;
            result.NOFORMULIRA = model.NoFormulirA;
            result.KETSURATUBAHBENTUK = model.KeteranganSuratUbahBentuk;
            result.NOSURATUBAHBENTUK = model.NoSuratUbahBentuk;
            result.TGLSURATUBAHBENTUK = model.TanggalSuratUbahBentuk;
            result.KETERANGANSURATRUBAHWARNA = model.KeteranganSuratUbahWarna;
            result.NOSURATRUBAHWARNA = model.NoSuratUbahWarna;
            result.TANGGALSURATRUBAHWARNA = model.TanggalSuratUbahWarna;
            result.NOSERTIFIKATREGUJITIPE = model.NoSertifikatRegistrasiUjiTipe;
            result.REMARKSDETAILPROBLEM = model.RemarksDetailProblem;
            result.KETBPKB = model.RemarksBPKB;
            result.STATUSBPKB = model.StatusBPKB;
            result.MODIFIED_DATE = DateTime.Now;
            result.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();

            dc.SubmitChanges();
            UpdateStatus(model.PONumber);
            UpdateInvoiceStatus(model.PONumber);
            return model;
        }

        public CUSTOMIR ReviseDetailInvoice(string PONumber, string ReasonForRevise)
        {
            var dc = new eprocdbDataContext();
            var result = (from custir in dc.CUSTOMIRs where custir.PONUMBER == PONumber select custir).SingleOrDefault();
            result.isRevised = true;
            result.REASONREJECTIONINVOICE = ReasonForRevise;
            result.MODIFIED_DATE = DateTime.Now;
            result.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();
            dc.SubmitChanges();
            return result;
        }
        public CUSTOMBPKB ReviseDetailBPKB(string PONumber, string ReasonForRevise)
        {
            var dc = new eprocdbDataContext();
            var result = (from custbpkb in dc.CUSTOMBPKBs where custbpkb.PONUMBER == PONumber select custbpkb).SingleOrDefault();
            result.isRevised = true;
            result.REASONREVISEBPKB = ReasonForRevise;
            result.MODIFIED_DATE = DateTime.Now;
            result.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();
            dc.SubmitChanges();
            return result;
        }
        public void UpdateInvoiceStatus(string ponumber)
        {
            var dc = new eprocdbDataContext();
            //string status = "";
            var rs = (from a in dc.CUSTOMIRs where a.PONUMBER == ponumber select a).FirstOrDefault();
            if (rs != null)
            {
                if (rs.TGLPEMBAYARAN != null && rs.TGLPEMBAYARAN.ToString() != "")
                {
                    rs.INVOICESTATUS = "Receipt";
                    dc.SubmitChanges();
                }
            }
        }
        public void UpdateStatus(string PONumber)
        {
            var dc = new eprocdbDataContext();
            string status = "";
            var rs = (from a in dc.CUSTOMPOs
                      join b in dc.CUSTOMGRs on a.PONUMBER equals b.PONUMBER
                      join c in dc.CUSTOMIRs on a.PONUMBER equals c.PONUMBER
                      join d in dc.CUSTOMBPKBs on a.PONUMBER equals d.PONUMBER
                      where a.PONUMBER == PONumber
                      select new
                      {
                          a.PONUMBER,
                          a.POSTATUSID,
                          a.ACTUALDATEDELIVEREDUNIT,
                          b.ACTUALRECEIVEDUNIT,
                          c.ACTUALRECEIVEDINV,
                          c.NOAP,
                          c.TGLPEMBAYARAN,
                          d.TGLGRBPKB,
                          b.TGLGRSAP,
                          d.ACTUALRECEIVEDBPKBDIHO,
                          d.ACTUALRECEIVEDBPKBDICAB,
                          d.ACTUALDELIVERYBPKBTOFINANCE,
                      }
                      ).FirstOrDefault();
            var CustomS02002 = (from a in dc.CUSTOM_S02002s where a.PONumber == PONumber select a.ActualDateDeliveryUnit).FirstOrDefault();

            general.AddLogError("Update Status (awal)", rs.PONUMBER, rs.POSTATUSID);
            if (Convert.ToInt16(rs.POSTATUSID) >= 3 && rs.POSTATUSID != "10")
            {
                general.AddLogError("Update Status (5)", rs.PONUMBER, rs.ACTUALDATEDELIVEREDUNIT + "//" + rs.TGLGRSAP + "//" + CustomS02002);
                if (rs.ACTUALDATEDELIVEREDUNIT != null || CustomS02002 != null && rs.TGLGRSAP != null)// perubahan andri jika tglbstb atau customs02002 !=null
                {
                    status = "5"; // PO Unit Delivery
                    general.AddLogError("Update Status (6)", rs.PONUMBER, rs.ACTUALRECEIVEDINV + "");

                    if (rs.ACTUALRECEIVEDINV != null)
                    {
                        status = "6"; // PO Invoice Delivery
                        general.AddLogError("Update Status (7)", rs.PONUMBER, rs.NOAP + "");
                        if (rs.NOAP != null && rs.NOAP != "")
                        {
                            status = "7"; //AP In Process
                            general.AddLogError("Update Status (6)", rs.PONUMBER, rs.TGLPEMBAYARAN + "");
                            if (rs.TGLPEMBAYARAN != null)
                            {
                                status = "8"; //AP Complete
                                general.AddLogError("Update Status (6)", rs.PONUMBER, rs.TGLGRBPKB + "//" + rs.ACTUALRECEIVEDBPKBDIHO + "//" + rs.ACTUALRECEIVEDBPKBDICAB + "//" + rs.ACTUALDELIVERYBPKBTOFINANCE);
                                if (rs.TGLGRBPKB != null && rs.ACTUALRECEIVEDBPKBDIHO != null && rs.ACTUALRECEIVEDBPKBDICAB != null && rs.ACTUALDELIVERYBPKBTOFINANCE != null)
                                {
                                    status = "12";//PO Complete
                                }
                            }
                        }
                    }
                }else
                {
                    if (rs.ACTUALRECEIVEDINV != null)
                    {
                        status = "6"; // PO Invoice Delivery
                        general.AddLogError("Update Status (7)", rs.PONUMBER, rs.NOAP + "");
                        if (rs.NOAP != null && rs.NOAP != "")
                        {
                            status = "7"; //AP In Process
                            general.AddLogError("Update Status (6)", rs.PONUMBER, rs.TGLPEMBAYARAN + "");
                            if (rs.TGLPEMBAYARAN != null)
                            {
                                status = "8"; //AP Complete
                                general.AddLogError("Update Status (6)", rs.PONUMBER, rs.TGLGRBPKB + "//" + rs.ACTUALRECEIVEDBPKBDIHO + "//" + rs.ACTUALRECEIVEDBPKBDICAB + "//" + rs.ACTUALDELIVERYBPKBTOFINANCE);
                                if (rs.TGLGRBPKB != null && rs.ACTUALRECEIVEDBPKBDIHO != null && rs.ACTUALRECEIVEDBPKBDICAB != null && rs.ACTUALDELIVERYBPKBTOFINANCE != null)
                                {
                                    status = "12";//PO Complete
                                }
                            }
                        }
                    }
                }
            }
            if (status != "")
            {
                var result = (from custpo in dc.CUSTOMPOs where custpo.PONUMBER == PONumber select custpo).SingleOrDefault();
                result.POSTATUSID = status;
            }
            dc.SubmitChanges();
        }
    }
}