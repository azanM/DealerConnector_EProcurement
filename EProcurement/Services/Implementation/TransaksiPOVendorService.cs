using System.Collections.Generic;
using System.Linq;
using EProcurement.Models;
using EProcurement.Models.ViewModel.Transaksi;
using System.Threading.Tasks;
using System;
using EProcurement.Controllers;

namespace EProcurement.Services
{
    public class TransaksiPOVendorService : ITransaksiPOVendorService
    {
        HomeController general = new HomeController();
        public List<ListPOVendorViewModel> GetAllDouble(string VendorId)
        {
            var dc = new eprocdbDataContext();
            var part1 = GetAll(VendorId);
            //var part2 = GetAll(VendorId).Where(p => p.StatusPO != "PO Cancel" && p.StatusPO != "PO Complete" && p.StatusPO != "Rejected");

            //foreach (var p in part2)
            //{
            //    p.StatusPO = "BPKB Outstanding";
            //}
            var model = part1.ToList();
            return model;
        }
        public List<ListPOVendorViewModel> GetAll(string VendorId)
        {
            var dc = new eprocdbDataContext();
            //  from custgr in dc.CUSTOMGRs.Where(mapping => mapping.PONUMBER == custpo.PONUMBER).DefaultIfEmpty()
            var model = (from a in dc.CUSTOMPOs
                         join d in dc.CUSTOMGRs on a.PONUMBER equals d.PONUMBER
                         join e in dc.CUSTOMSTATUS on a.POSTATUSID equals e.ID
                         join b in dc.STREAMLINERs on a.PONUMBER equals b.PONUMBER //from b in dc.STREAMLINERs.Where(mapping => mapping.PONUMBER == a.PONUMBER).DefaultIfEmpty() //on a.PONUMBER equals b.PONUMBER//join b in dc.STREAMLINERs on a.PONUMBER equals b.PONUMBER
                         join custMISC in dc.CUSTOMMISCs on a.PONUMBER equals custMISC.PONumber
                         join custBPKB in dc.CUSTOMBPKBs on a.PONUMBER equals custBPKB.PONUMBER
                         //where a.VENDORID == VendorId
                         //from d in dc.CUSTOMGRs.Where(mapping => mapping.PONUMBER == a.PONUMBER).DefaultIfEmpty() //on a.PONUMBER equals d.PONUMBER
                         //from e in dc.CUSTOMSTATUS.Where(mapping => mapping.ID == a.POSTATUSID).DefaultIfEmpty() //on a.POSTATUSID equals e.ID
                         //from b in dc.STREAMLINERs.Where(mapping => mapping.PONUMBER == a.PONUMBER).DefaultIfEmpty() //on a.PONUMBER equals b.PONUMBER
                         //from custMISC in dc.CUSTOMMISCs.Where(mapping => mapping.PONumber == a.PONUMBER).DefaultIfEmpty() //on a.PONUMBER equals custMISC.PONumber
                         //from custBPKB in dc.CUSTOMBPKBs.Where(mapping => mapping.PONUMBER == a.PONUMBER).DefaultIfEmpty() //on a.PONUMBER equals custBPKB.PONUMBER
                         //where a.VENDORID == VendorId
                         select new ListPOVendorViewModel
                         {
                             VendorID = a.VENDORID,
                             PONumber = a.PONUMBER,
                             PODate = a.TGLPO,
                             StatusId = a.POSTATUSID,
                             StatusPO = b.POStatus == "Rejected" ? "Rejected" : e.STATUS, //c.POStatus == "Rejected" ? "Rejected" : d.STATUS,
                             ChasisNumber = d.NOCHASIS,
                             OfficerName = b.OfficerName
                         }).Distinct().ToList();

            if (VendorId != "") {
                model = (from a in model where a.VendorID == VendorId && a.StatusPO != "PO Cancel" orderby a.PODate ascending select a).ToList();
                model = (from a in model
                         select new ListPOVendorViewModel {
                             StrPoDate = a.PODate.Value.ToString("dd/MM/yyyy"),
                                VendorID = a.VendorID,
                                PONumber = a.PONumber,
                             StatusId = a.StatusId,
                             StatusPO = a.StatusPO,
                             ChasisNumber = a.ChasisNumber,
                             OfficerName = a.OfficerName
                         }).Distinct().ToList();
           }

            else {
                model = (from a in model where a.StatusPO != "PO Cancel" orderby a.PODate ascending select a).ToList();
                model = (from a in model
                         select new ListPOVendorViewModel
                         {
                             StrPoDate = a.PODate.Value.ToString("dd/MM/yyyy"),
                             VendorID = a.VendorID,
                             PONumber = a.PONumber,
                             StatusId = a.StatusId,
                             StatusPO = a.StatusPO,
                             ChasisNumber = a.ChasisNumber,
                             OfficerName = a.OfficerName
                         }).Distinct().ToList();
            }
            return model;
        }
        public List<ListPOVendorViewModel> GetSearch(string VendorId,string PoNumber)
        {
            List<ListPOVendorViewModel> model = new List<ListPOVendorViewModel>();


                var dc = new eprocdbDataContext();
            //  from custgr in dc.CUSTOMGRs.Where(mapping => mapping.PONUMBER == custpo.PONUMBER).DefaultIfEmpty()
            if (PoNumber != "" && PoNumber != null)
            {
                model = (from a in dc.CUSTOMPOs
                         join d in dc.CUSTOMGRs on a.PONUMBER equals d.PONUMBER
                         join e in dc.CUSTOMSTATUS on a.POSTATUSID equals e.ID
                         join b in dc.STREAMLINERs on a.PONUMBER equals b.PONUMBER //from b in dc.STREAMLINERs.Where(mapping => mapping.PONUMBER == a.PONUMBER).DefaultIfEmpty() //on a.PONUMBER equals b.PONUMBER//join b in dc.STREAMLINERs on a.PONUMBER equals b.PONUMBER
                         join custMISC in dc.CUSTOMMISCs on a.PONUMBER equals custMISC.PONumber
                         join custBPKB in dc.CUSTOMBPKBs on a.PONUMBER equals custBPKB.PONUMBER
                         where a.PONUMBER.Contains(PoNumber)
                         //from d in dc.CUSTOMGRs.Where(mapping => mapping.PONUMBER == a.PONUMBER).DefaultIfEmpty() //on a.PONUMBER equals d.PONUMBER
                         //from e in dc.CUSTOMSTATUS.Where(mapping => mapping.ID == a.POSTATUSID).DefaultIfEmpty() //on a.POSTATUSID equals e.ID
                         //from b in dc.STREAMLINERs.Where(mapping => mapping.PONUMBER == a.PONUMBER).DefaultIfEmpty() //on a.PONUMBER equals b.PONUMBER
                         //from custMISC in dc.CUSTOMMISCs.Where(mapping => mapping.PONumber == a.PONUMBER).DefaultIfEmpty() //on a.PONUMBER equals custMISC.PONumber
                         //from custBPKB in dc.CUSTOMBPKBs.Where(mapping => mapping.PONUMBER == a.PONUMBER).DefaultIfEmpty() //on a.PONUMBER equals custBPKB.PONUMBER
                         //where a.VENDORID == VendorId
                         select new ListPOVendorViewModel
                         {
                            VendorID = a.VENDORID,
                            PONumber = a.PONUMBER,
                            PODate = a.TGLPO,
                            StatusId = a.POSTATUSID,
                            StatusPO = b.POStatus == "Rejected" ? "Rejected" : e.STATUS, //c.POStatus == "Rejected" ? "Rejected" : d.STATUS,
                            ChasisNumber = d.NOCHASIS,
                            OfficerName = b.OfficerName
                        }).Distinct().ToList();

                if (VendorId != "")
                {
                    model = (from a in model where a.VendorID == VendorId && a.StatusPO != "PO Cancel" orderby a.PODate ascending select a).ToList();
                    model = (from a in model
                             select new ListPOVendorViewModel
                             {
                                 StrPoDate = a.PODate.Value.ToString("dd/MM/yyyy"),
                                 VendorID = a.VendorID,
                                 PONumber = a.PONumber,
                                 StatusId = a.StatusId,
                                 StatusPO = a.StatusPO,
                                 ChasisNumber = a.ChasisNumber,
                                 OfficerName = a.OfficerName
                             }).Distinct().ToList();
                }
                else
                {
                    model = (from a in model where a.StatusPO != "PO Cancel" orderby a.PODate ascending select a).ToList();
                    model = (from a in model
                             select new ListPOVendorViewModel
                             {
                                 StrPoDate = a.PODate.Value.ToString("dd/MM/yyyy"),
                                 VendorID = a.VendorID,
                                 PONumber = a.PONumber,
                                 StatusId = a.StatusId,
                                 StatusPO = a.StatusPO,
                                 ChasisNumber = a.ChasisNumber,
                                 OfficerName = a.OfficerName
                             }).Distinct().ToList();
                }
            }else
            {
                model = GetAll(VendorId);
            }
            return model;
        }
        public DetailAssignmentPOVendorViewModel GetDetailAssignment(string poNumber)
        {
            var dc = new eprocdbDataContext();
            var model = (from custpo in dc.CUSTOMPOs
                         from custgr in dc.CUSTOMGRs.Where(mapping => mapping.PONUMBER == custpo.PONUMBER).DefaultIfEmpty()  //on custpo.PONUMBER equals custgr.PONUMBER
                         from custbpkb in dc.CUSTOMBPKBs.Where(mapping => mapping.PONUMBER == custgr.PONUMBER).DefaultIfEmpty()  //on custgr.PONUMBER equals custbpkb.PONUMBER
                         from stream in dc.STREAMLINERs.Where(mapping => mapping.PONUMBER == custpo.PONUMBER).DefaultIfEmpty()  //on custpo.PONUMBER equals stream.PONUMBER
                         where custpo.PONUMBER == poNumber
                         select new DetailAssignmentPOVendorViewModel
                         {
                             PONumber = custpo.PONUMBER,
                             PODate = custpo.TGLPO,
                             PODescription = custpo.DESCPO,
                             PlantCabang = custpo.CABANGTRAC,
                             OnTheRoadPrice = custpo.ONTHEROADPRICE,
                             Discount = custpo.DISC,
                             NetPrice = (custpo.ONTHEROADPRICE + custpo.DISC),
                             Color = custpo.COLOR,
                             PromiseDeliveryDate = custpo.PROMISEDLVDATEPO.Value.Date,
                             OfficerName = stream.OfficerName,
                             PurchaseStatus = custpo.PURCHASESTATUS,
                             UnitDeliveryAddress = custpo.UNITDELIVERYADDRESS,
                             ProsesKeVendorKaroseri = stream.PRKaroseri,
                             ProsesKeVendorAksesoris = stream.PRAccessories,
                             POStatus = custpo.POSTATUSID,
                             BBN = stream.BBN,
                             TujuanDeliveryUnit = custgr.STATUSDELIVERYUNIT
                         }).SingleOrDefault();
            return model;
        }
        public DetailDeliveryPOVendorViewModel GetDetailDelivery(string poNumber)
        {
            var dc = new eprocdbDataContext();
            var model = (from custpo in dc.CUSTOMPOs
                         from custgr in dc.CUSTOMGRs.Where(mapping => mapping.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() //on custpo.PONUMBER equals custgr.PONUMBER
                         from custbpkb in dc.CUSTOMBPKBs.Where(mappingCust => mappingCust.PONUMBER == custgr.PONUMBER).DefaultIfEmpty()  //on custgr.PONUMBER equals custbpkb.PONUMBER
                         from stream in dc.STREAMLINERs.Where(mappingStream => mappingStream.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() //on custpo.PONUMBER equals stream.PONUMBER
                         from upload in dc.UPLOAD_FILEs.Where(mappingUpload => mappingUpload.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() //on custpo.PONUMBER equals upload.PONUMBER
                         where custpo.PONUMBER == poNumber
                         select new DetailDeliveryPOVendorViewModel
                         {
                             PONumber = custpo.PONUMBER,
                             PODate = custpo.TGLPO,
                             PODescription = custpo.DESCPO,
                             ExpectedDateDelivered = custpo.EXPECTEDDATEDELIVERED.Value.Date,
                             OfficerName = stream.OfficerName,
                             BBN = stream.BBN,
                             DPP = custpo.HARGADPP,
                             PPN = custpo.HARGAPPNUNIT,
                             BBNPrice = custpo.HARGABBN,
                             DPPByVendor = custpo.HARGADPP_INPUT,
                             PPNByVendor = custpo.HARGAPPNUNIT_INPUT,
                             BBNPriceByVendor = custpo.HARGABBN_INPUT,
                             FakturDODate = custpo.DODATE,
                             FakturDONumber = custpo.DONUMBER,
                             LicensePlate = custbpkb.NOPOLISI,
                             ChassisNumber = custgr.NOCHASIS,
                             MachineNumber = custgr.NOENGINE,
                             STNKDate = custbpkb.TGLSTNK,
                             LicensePlateByVendor = custbpkb.NOPOLISI_INPUT,
                             ChassisNumberByVendor = custgr.NOCHASIS_INPUT,
                             MachineNumberByVendor = custgr.NOENGINE_INPUT,
                             STNKDateByVendor = custbpkb.TGLSTNK_INPUT,
                             STCKDate = custbpkb.STCKDATE,
                             TanggalBSTB = custpo.ACTUALDATEDELIVEREDUNIT,
                             CarrosserieAccessoriesVendorName = custpo.CARROSERIEVENDORNAME,
                             CarrosserieAccessoriesAddress = custpo.ACCESORIESADDRESS,
                             RemarksCarrosserieAccessories = custpo.REMARKSCARROSSERIE,
                             DateEntryCarrosserieAccessories = custpo.TGLMASUKKAROSERI,
                             DateFinishCarrosserieAccessories = custpo.TGLSELESAIKAROSERI,
                             RemarksPO = custpo.REMARKS,
                             UploadScanCopyPO = upload.FILE
                         }).SingleOrDefault();
            return model;
        }
        public DetailInvoicePOVendorViewModel GetDetailInvoice(string poNumber)
        {
            var dc = new eprocdbDataContext();
            var model = (from custpo in dc.CUSTOMPOs
                         from custgr in dc.CUSTOMGRs.Where(mapping => mapping.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() //on custpo.PONUMBER equals custgr.PONUMBER
                         from custir in dc.CUSTOMIRs.Where(mapping2 => mapping2.PONUMBER == custpo.PONUMBER).DefaultIfEmpty()  // on custpo.PONUMBER equals custir.PONUMBER
                         from stream in dc.STREAMLINERs.Where(mapping3 => mapping3.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() //on custpo.PONUMBER equals stream.PONUMBER
                         where custpo.PONUMBER == poNumber
                         select new DetailInvoicePOVendorViewModel
                         {
                             TermOfPayment = custir.TERMOFPAYMENT,
                             ActualReceivedInvoice = custir.ACTUALRECEIVEDINV,
                             PONumber = custpo.PONUMBER,
                             PODate = custpo.TGLPO,
                             PODescription = custpo.DESCPO,
                             BBN = stream.BBN,
                             PromiseDeliveryDate = custpo.PROMISEDLVDATEPO.Value.Date,
                             ExpectedDateDelivered = custpo.EXPECTEDDATEDELIVERED.Value.Date,
                             PurchassingOfficer = stream.OfficerName,
                             InvoiceNo = custir.INVNO,
                             InvoiceDate = custir.INVDATE,
                             NoSeriFakturPajak = custir.NOFAKTURPAJAK,
                             PayPlan = custir.ESTIMATEDPAYMENTDATE,
                             ClearingDate = custir.TGLPEMBAYARAN,
                             StatusInvoice = custir.INVOICESTATUS,
                             StatusDeliveryUnit = custgr.STATUSDELIVERYUNIT,
                             ScanBSTK = string.Empty,
                             ActualInvoiceReceived = custir.ACTUALRECEIVEDINV,
                             ScanSTNKSTCK = string.Empty
                         }).SingleOrDefault();

            if (model.ActualReceivedInvoice != null)
            {
                model.PayPlan = model.ActualReceivedInvoice.Value.AddDays(Convert.ToDouble(model.TermOfPayment == null ? 0 : model.TermOfPayment));
            }
            else
            {

            }
            //if (model.StatusInvoice == "")
            //{
            //    model.StatusInvoice = "";
            //}
            //else if (model.StatusInvoice == "Receipt")
            //{
            //    model.StatusInvoice = "Complete";
            //}
            //else
            //{
            //    model.StatusInvoice = "In Progress";
            //}

            return model;
        }
        public DetailBPKBPOVendorViewModel GetDetailBPKB(string poNumber)
        {
            var dc = new eprocdbDataContext();
            var model = (from custpo in dc.CUSTOMPOs
                         from custgr in dc.CUSTOMGRs.Where(mapping => mapping.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() //on custpo.PONUMBER equals custgr.PONUMBER
                         from custbpkb in dc.CUSTOMBPKBs.Where(mapping1 => mapping1.PONUMBER == custgr.PONUMBER).DefaultIfEmpty() //on custgr.PONUMBER equals custbpkb.PONUMBER
                         from custir in dc.CUSTOMIRs.Where(mapping2 => mapping2.PONUMBER == custgr.PONUMBER).DefaultIfEmpty() //on custpo.PONUMBER equals custir.PONUMBER
                         from custpr in dc.CUSTOMPRs.Where(mapping3 => mapping3.PRSAP == custpo.PRSAP).DefaultIfEmpty() //on custpo.PRNUMBERSAP equals custpr.PRSAP
                         from stream in dc.STREAMLINERs.Where(mapping4 => mapping4.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() //on custpo.PONUMBER equals stream.PONUMBER
                         where custpo.PONUMBER == poNumber
                         select new DetailBPKBPOVendorViewModel
                         {
                             PONumber = custpo.PONUMBER,
                             PODate = custpo.TGLPO,
                             PODescription = custpo.DESCPO,
                             BBNPrice = custpo.HARGABBN,
                             OfficerName = stream.OfficerName,
                             LicensePlate = custbpkb.NOPOLISI,
                             ChassisNumber = custgr.NOCHASIS,
                             MachineNumber = custgr.NOENGINE,
                             ClearingDate = custir.TGLPEMBAYARAN,
                             ActualReceivedUnit = custgr.ACTUALRECEIVEDUNIT,
                             StatusBPKBDeliveredVendor = custbpkb.STATUSBPKB,
                             ActualDeliveryBPKBDateHO = custbpkb.ACTUALRECEIVEDBPKBDIHO,
                             ActualDeliveredBPKBDateCabang = custbpkb.ACTUALRECEIVEDBPKBDICAB,
                             BPKBPosition = custbpkb.POSISIBPKB,
                             RemarksBPKB = custbpkb.KETBPKB,
                             OwnerName = custpo.COMPANYNAME,
                             Year = custpr.YEAR,
                             BPKBNumber = custbpkb.NOBPKB,
                             KeteranganSuratUbahBentuk = custbpkb.KETSURATUBAHBENTUK,
                             KeteranganSuratUbahWarna = custbpkb.KETERANGANSURATRUBAHWARNA,
                             NoFakturKendaraan = custbpkb.NOFAKTUR,
                             TanggalFakturKendaraan = custbpkb.TGLFAKTUR,
                             NoSertifikat = custbpkb.NOSERTIFIKAT,
                             TanggalSertifikat = custbpkb.TGLSERTIFIKAT,
                             NoFormulirA = custbpkb.NOFORMULIRA,
                             TanggalFormulirA = custbpkb.TGLFORMULIRA,
                             NoSuratUbahBentuk = custbpkb.NOSURATUBAHBENTUK,
                             TanggalSuratUbahBentuk = custbpkb.TGLSURATUBAHBENTUK,
                             NoSuratUbahWarna = custbpkb.NOSURATRUBAHWARNA,
                             TanggalSuratUbahWarna = custbpkb.TANGGALSURATRUBAHWARNA,
                             NoSertifikatRegistrasiUjiTipe = custbpkb.NOSERTIFIKATREGUJITIPE,
                             ActualReceivedBPKBHOFromBranch = custbpkb.ACTUALRECEIVEDBPKBDIHO,
                             DetailProblem = custbpkb.DETAILPROBLEM,
                             StatusBPKB = custbpkb.STATUSBPKB,
                             ActualDeliveredDateBPKBToFinance = custbpkb.ACTUALDELIVERYBPKBTOFINANCE,
                             BPKBDateSentBack = custbpkb.TGLBPKBDIKIRIMKEMBALI,
                             ActualReceivedBPKBHOBack = custbpkb.ACTUALRECEIVEDBPKBHOBACK,
                             DateDeliveryToBranchVendor = custbpkb.DATEDELIVERYTOBRANCHORVENDOR,
                             RemarksDetailProblem = custbpkb.REMARKSDETAILPROBLEM,
                             BuktiSerahTerimaBPKB = string.Empty
                         }).SingleOrDefault();
            if (model.ActualDeliveryBPKBDateHO == null && model.ActualDeliveredBPKBDateCabang == null && model.ActualDeliveredDateBPKBToFinance == null)
            {
                model.BPKBPosition = "Dealer";
            }
            else if (model.ActualDeliveredBPKBDateCabang != null && model.ActualDeliveryBPKBDateHO == null && model.ActualDeliveredDateBPKBToFinance == null)
            {
                model.BPKBPosition = "Cabang";
            }
            else if (model.ActualDeliveryBPKBDateHO != null && model.ActualDeliveredDateBPKBToFinance == null)
            {
                model.BPKBPosition = "Head Office";
            }
            else if (model.ActualDeliveredDateBPKBToFinance != null)
            {
                model.BPKBPosition = "Finance";
            }

            return model;
        }
        public DetailBSTKPOVendorViewModel GetDetailBSTK(string poNumber)
        {
            var dc = new eprocdbDataContext();
            var model = (from custpo in dc.CUSTOMPOs
                         from custgr in dc.CUSTOMGRs.Where(mapping => mapping.PONUMBER == custpo.PONUMBER).DefaultIfEmpty() //on custpo.PONUMBER equals custgr.PONUMBER
                         from custbpkb in dc.CUSTOMBPKBs.Where(mapping => mapping.PONUMBER == custgr.PONUMBER).DefaultIfEmpty() //on custgr.PONUMBER equals custbpkb.PONUMBER
                         where custpo.PONUMBER == poNumber
                         select new DetailBSTKPOVendorViewModel
                         {
                             PONumber = custpo.PONUMBER,
                             PODate = custpo.TGLPO,
                             PODescription = custpo.DESCPO,
                             ScanBSTK = string.Empty,
                             ScanSTNKSTCK = string.Empty
                         }).SingleOrDefault();
            return model;
        }
        public DetailAssignmentPOVendorViewModel SubmitAssignment(DetailAssignmentPOVendorViewModel model)
        {
            var dc = new eprocdbDataContext();
            var result = (from custpo in dc.CUSTOMPOs where custpo.PONUMBER == model.PONumber select custpo).SingleOrDefault();
            result.POSTATUSID = model.POStatus;
            result.MODIFIED_DATE = DateTime.Now;
            result.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();

            var result2 = (from stream in dc.STREAMLINERs where stream.PONUMBER == model.PONumber select stream).SingleOrDefault();
            result2.BBN = model.BBN;
            dc.SubmitChanges();
            UpdateStatus(model.PONumber);
            UpdateInvoiceStatus(model.PONumber);
            return model;
        }
        public DetailDeliveryPOVendorViewModel UpdateDelivery(DetailDeliveryPOVendorViewModel model)
        {
            var dc = new eprocdbDataContext();
            var result = (from custpo in dc.CUSTOMPOs where custpo.PONUMBER == model.PONumber select custpo).SingleOrDefault();
            result.HARGADPP_INPUT = model.DPPByVendor;
            result.HARGAPPNUNIT_INPUT = model.PPNByVendor;
            result.HARGABBN_INPUT = model.BBNPriceByVendor;
            result.DODATE = model.FakturDODate;
            result.DONUMBER = model.FakturDONumber;
            result.CARROSERIEVENDORNAME = model.CarrosserieAccessoriesVendorName;
            result.ACCESORIESADDRESS = model.CarrosserieAccessoriesAddress;
            result.REMARKSCARROSSERIE = model.RemarksCarrosserieAccessories;
            result.TGLMASUKKAROSERI = model.DateEntryCarrosserieAccessories;
            result.TGLSELESAIKAROSERI = model.DateFinishCarrosserieAccessories;
            result.ACTUALDATEDELIVEREDUNIT = model.TanggalBSTB;
            result.REMARKS = model.RemarksPO;
            result.MODIFIED_DATE = DateTime.Now;
            result.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();

            var result2 = (from custbpkb in dc.CUSTOMBPKBs where custbpkb.PONUMBER == model.PONumber select custbpkb).SingleOrDefault();
            result2.NOPOLISI_INPUT = model.LicensePlateByVendor;
            result2.TGLSTNK_INPUT = model.STNKDateByVendor;
            result2.STCKDATE = model.STCKDate;
            result2.TGLSERAHBPKB = model.TanggalBSTB;
            result.MODIFIED_DATE = DateTime.Now;
            result.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();

            var result3 = (from custgr in dc.CUSTOMGRs where custgr.PONUMBER == model.PONumber select custgr).SingleOrDefault();
            result3.NOCHASIS_INPUT = model.ChassisNumberByVendor;
            result3.NOENGINE_INPUT = model.MachineNumberByVendor;
            result.MODIFIED_DATE = DateTime.Now;
            result.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();

            dc.SubmitChanges();
            UpdateStatus(model.PONumber);
            UpdateInvoiceStatus(model.PONumber);
            return model;
        }
        public DetailInvoicePOVendorViewModel UpdateInvoice(DetailInvoicePOVendorViewModel model)
        {
            var dc = new eprocdbDataContext();
            var result = (from custpo in dc.CUSTOMPOs where custpo.PONUMBER == model.PONumber select custpo).SingleOrDefault();
            if (result != null)
            {
                result.TGLGI = model.ClearingDate;
                result.MODIFIED_DATE = DateTime.Now;
                result.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();

                var result2 = (from custbpkb in dc.CUSTOMBPKBs where custbpkb.PONUMBER == model.PONumber select custbpkb).SingleOrDefault();
                if (result2 != null)
                {
                    result2.NOFAKTUR = model.NoSeriFakturPajak == null ? "" : model.NoSeriFakturPajak;
                    result2.MODIFIED_DATE = DateTime.Now;
                    result2.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();
                }
                var result3 = (from custgr in dc.CUSTOMGRs where custgr.PONUMBER == model.PONumber select custgr).SingleOrDefault();
                if (result3 != null)
                {
                    result3.STATUSDELIVERYUNIT = model.StatusDeliveryUnit;
                    result3.MODIFIED_DATE = DateTime.Now;
                    result3.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();
                }

                var result4 = (from custir in dc.CUSTOMIRs where custir.PONUMBER == model.PONumber select custir).SingleOrDefault();
                if (result4 != null)
                {
                    result4.NOFAKTURPAJAK = model.NoSeriFakturPajak;
                    result4.INVNO = model.InvoiceNo;
                    result4.INVDATE = model.InvoiceDate;
                    result4.MODIFIED_DATE = DateTime.Now;
                    result4.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();
                }
                dc.SubmitChanges();
                UpdateStatus(model.PONumber);
                UpdateInvoiceStatus(model.PONumber);
            }
            return model;
        }
        public DetailBPKBPOVendorViewModel UpdateBPKB(DetailBPKBPOVendorViewModel model)
        {
            var dc = new eprocdbDataContext();
            var prnumber = (from custpo in dc.CUSTOMPOs where custpo.PONUMBER == model.PONumber select custpo).SingleOrDefault();
            prnumber.ACTUALRECEIVEDBPKBHOFROMBRANCH = model.ActualReceivedBPKBHOFromBranch;
            string strPrNumber = prnumber.PRNUMBERSAP;
            var result = (from custbpkb in dc.CUSTOMBPKBs where custbpkb.PONUMBER == model.PONumber select custbpkb).SingleOrDefault();
            result.ACTUALDELIVERYBPKBTOFINANCE = model.ActualDeliveryBPKBDateHO;
            result.ACTUALRECEIVEDBPKBDICAB = model.ActualDeliveredBPKBDateCabang;
            result.KETBPKB = model.RemarksBPKB;
            result.NOBPKB = model.BPKBNumber;
            result.KETSURATUBAHBENTUK = model.KeteranganSuratUbahBentuk;
            result.KETERANGANSURATRUBAHWARNA = model.KeteranganSuratUbahWarna;
            result.NOFAKTUR = model.NoFakturKendaraan;
            result.TGLFAKTUR = model.TanggalFakturKendaraan;
            result.NOSERTIFIKAT = model.NoSertifikat;
            result.TGLSERTIFIKAT = model.TanggalSertifikat;
            result.NOFORMULIRA = model.NoFormulirA;
            result.TGLFORMULIRA = model.TanggalFormulirA;
            result.NOSURATUBAHBENTUK = model.NoSuratUbahBentuk;
            result.TGLSURATUBAHBENTUK = model.TanggalSuratUbahBentuk;
            result.NOSURATRUBAHWARNA = model.NoSuratUbahWarna;
            result.TANGGALSURATRUBAHWARNA = model.TanggalSuratUbahWarna;
            result.NOSERTIFIKATREGUJITIPE = model.NoSertifikatRegistrasiUjiTipe;
            result.ACTUALRECEIVEDBPKBDIHO = model.ActualDeliveryBPKBDateHO;
            result.ACTUALDELIVERYBPKBTOFINANCE = model.ActualDeliveredDateBPKBToFinance;
            result.ACTUALRECEIVEDBPKBHOBACK = model.BPKBDateSentBack;
            result.ACTUALRECEIVEDBPKBHOBACK = model.ActualReceivedBPKBHOBack;
            result.DATEDELIVERYTOBRANCHORVENDOR = model.DateDeliveryToBranchVendor;
            result.REMARKSDETAILPROBLEM = model.RemarksDetailProblem;
            result.POSISIBPKB = model.BPKBPosition;
            result.MODIFIED_DATE = DateTime.Now;
            result.MODIFIED_BY = System.Web.HttpContext.Current.Session["UserID"].ToString();
            var result2 = (from custpr in dc.CUSTOMPRs where custpr.PRSAP == strPrNumber select custpr.YEAR).FirstOrDefault();
            result2 = model.Year;


            dc.SubmitChanges();
            UpdateStatus(model.PONumber);
            UpdateInvoiceStatus(model.PONumber);

            return model;
        }
        public Task InsertUploadFile(string PONumber, string ket, string fileName)
        {
            var dc = new eprocdbDataContext();
            var data = (from a in dc.UPLOAD_FILEs where a.PONUMBER == PONumber select a).FirstOrDefault();
            if (data == null)
            {
                UPLOAD_FILE model = new UPLOAD_FILE();
                model.PONUMBER = PONumber;
                model.KET = ket;
                model.FILE = fileName;
                dc.UPLOAD_FILEs.InsertOnSubmit(model);
                dc.SubmitChanges();
            }else
            {
                data.PONUMBER = PONumber;
                data.FILE = fileName;
                data.KET = ket;
                dc.SubmitChanges();
            }
            return null;
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
            if (rs != null)
            {
                general.AddLogError("Update Status (awal)", rs.PONUMBER, rs.POSTATUSID);
                if (Convert.ToInt16(rs.POSTATUSID) >= 3 && rs.POSTATUSID != "10")
                {
                    general.AddLogError("Update Status (5)", rs.PONUMBER, rs.ACTUALDATEDELIVEREDUNIT + "//" + rs.TGLGRSAP + "//" + CustomS02002);
                    if (rs.ACTUALDATEDELIVEREDUNIT != null && rs.TGLGRSAP != null && CustomS02002 != null)
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
                    }
                    else
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


        public List<ListPOVendorViewModel> GetSearch(string VendorId, string PoNumber, string NoRangka)
        {
            List<ListPOVendorViewModel> model = new List<ListPOVendorViewModel>();
            var dc = new eprocdbDataContext();

            model = (from a in dc.CUSTOMPOs
                     join d in dc.CUSTOMGRs on a.PONUMBER equals d.PONUMBER
                     join e in dc.CUSTOMSTATUS on a.POSTATUSID equals e.ID
                     join b in dc.STREAMLINERs on a.PONUMBER equals b.PONUMBER
                     join custMISC in dc.CUSTOMMISCs on a.PONUMBER equals custMISC.PONumber
                     join custBPKB in dc.CUSTOMBPKBs on a.PONUMBER equals custBPKB.PONUMBER
                     select new ListPOVendorViewModel
                     {
                         VendorID = a.VENDORID,
                         PONumber = a.PONUMBER,
                         PODate = a.TGLPO,
                         StatusId = a.POSTATUSID,
                         StatusPO = b.POStatus == "Rejected" ? "Rejected" : e.STATUS,
                         ChasisNumber = d.NOCHASIS,
                         OfficerName = b.OfficerName
                     }).Distinct().ToList();

            if (VendorId != "")
            {
                model = (from a in model where a.VendorID == VendorId && a.StatusPO != "PO Cancel" orderby a.PODate ascending select a).ToList();
            }
            if (PoNumber != "")
            {
                model = model.Where(p => p.PONumber != null && p.PONumber.Contains(PoNumber)).ToList();
            }
            if (NoRangka != "")
            {
                model = (from a in model where (a.ChasisNumber != null && a.ChasisNumber.Contains(NoRangka)) && a.StatusPO != "PO Cancel" orderby a.PODate ascending select a).ToList();
            }

            return model;
        }

    }
}