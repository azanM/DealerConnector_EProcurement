using EProcurement.Controllers;
using EProcurement.Models;
using EProcurement.Services.Interface;
using System;
using System.Linq;

namespace EProcurement.Services.Implementation
{
    public class UploadVehicleService : IUploadVehicleService
    {
        HomeController general = new HomeController();
        public CUSTOMFILEUPLOAD Add(CUSTOMFILEUPLOAD model)
        {
            var dc = new eprocdbDataContext();
            dc.CUSTOMFILEUPLOADs.InsertOnSubmit(model);
            dc.SubmitChanges();
            return model;
        }

        public static string GenerateID()
        {
            string uniqueID = "";

            using (eprocdbDataContext db = new eprocdbDataContext())
            {
                int lastID = (from a in db.CUSTOMFILEUPLOADs
                              select a).Count();

                int seqID = lastID + 1;

                uniqueID = seqID.ToString();

            }

            return uniqueID;
        }
        public CUSTOMIR UpdateCustomIR(string poNumber, CUSTOMIR model)
        {
            try
            {

                var dc = new eprocdbDataContext();

                var md = (from c in dc.CUSTOMIRs
                          where c.PONUMBER == poNumber.Trim()
                          select c).SingleOrDefault();
                md.NOAP = model.NOAP;
                //md.INVDATE = model.INVDATE;
                //md.INVNO = model.INVNO;

                dc.SubmitChanges();
                UpdateStatus(poNumber);
                return model;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public CUSTOMPO UpdateDeliveryCustPO(string poNumber, CUSTOMPO model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.CUSTOMPOs
                      where c.PONUMBER == poNumber.Trim()
                      select c).SingleOrDefault();

            md.PURCHASESTATUS = model.PURCHASESTATUS;
            md.CARROSERIEVENDORNAME = model.CARROSERIEVENDORNAME;
            md.ACCESORIESADDRESS = model.ACCESORIESADDRESS;
            md.REMARKSCARROSSERIE = model.REMARKSCARROSSERIE;
            md.PICKAROSERI = model.PICKAROSERI;
            md.NOTELEPONKAROSERI = model.NOTELEPONKAROSERI;
            md.BENTUKKAROSERI = model.BENTUKKAROSERI;
            md.PROMISEDLVDATEPO = model.PROMISEDLVDATEPO;
            md.TGLSELESAIKAROSERI = model.TGLSELESAIKAROSERI;
            md.REMARKS = model.REMARKS;
            md.MODIFIED_BY = model.MODIFIED_BY;
            md.MODIFIED_DATE = model.MODIFIED_DATE;
            dc.SubmitChanges();
            UpdateStatus(poNumber);
            return model;
        }

        public CUSTOMPO UpdateInvoiceCustPO(string poNumber, CUSTOMPO model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.CUSTOMPOs
                      where c.PONUMBER == poNumber.Trim()
                      select c).SingleOrDefault();

            md.TGLMASUKKAROSERI = model.TGLMASUKKAROSERI;
            md.DONUMBER = model.DONUMBER;
            md.DODATE = model.DODATE;
            md.REASONREVISEDBYVENDOR = model.REASONREVISEDBYVENDOR;
            md.ACTUALDATEDELIVEREDUNIT = model.ACTUALDATEDELIVEREDUNIT;
            md.MODIFIED_BY = model.MODIFIED_BY;
            md.MODIFIED_DATE = model.MODIFIED_DATE;
            dc.SubmitChanges();
            UpdateStatus(poNumber);
            return model;
        }

        public CUSTOMBPKB UpdateInvoiceCustBpkb(string poNumber, CUSTOMBPKB model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.CUSTOMBPKBs
                      where c.PONUMBER == poNumber.Trim()
                      select c).SingleOrDefault();

            md.NOFAKTUR = model.NOFAKTUR;
            md.MODIFIED_BY = model.MODIFIED_BY;
            md.MODIFIED_DATE = model.MODIFIED_DATE;
            dc.SubmitChanges();
            UpdateStatus(poNumber);
            return model;
        }

        public CUSTOMGR UpdateInvoiceCustGR(string poNumber, CUSTOMGR model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.CUSTOMGRs
                      where c.PONUMBER == poNumber.Trim()
                      select c).SingleOrDefault();

            md.NOCHASIS_INPUT = model.NOCHASIS_INPUT;
            md.NOENGINE_INPUT = model.NOENGINE_INPUT;
            md.ACTUALRECEIVEDUNITINBDEL = model.ACTUALRECEIVEDUNITINBDEL;
            md.MODIFIED_BY = model.MODIFIED_BY;
            md.MODIFIED_DATE = model.MODIFIED_DATE;
            dc.SubmitChanges();
            UpdateStatus(poNumber);
            return model;
        }

        public CUSTOMIR UpdateInvoiceCustIR(string poNumber, CUSTOMIR model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.CUSTOMIRs
                      where c.PONUMBER == poNumber.Trim()
                      select c).SingleOrDefault();

            md.NOFAKTURPAJAK = model.NOFAKTURPAJAK;
            md.TERMOFPAYMENT = model.TERMOFPAYMENT;
            md.TGLSERAHTAGIHAN = model.TGLSERAHTAGIHAN;
            md.KETBAYAR = model.KETBAYAR;
            md.KETTAGIHAN = model.KETTAGIHAN;
            md.MODIFIED_BY = model.MODIFIED_BY;
            md.MODIFIED_DATE = model.MODIFIED_DATE;
            dc.SubmitChanges();
            UpdateStatus(poNumber);
            return model;
        }
        public CUSTOMBPKB UpdateBpkbCustBPKB(string poNumber, CUSTOMBPKB model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.CUSTOMBPKBs
                      where c.PONUMBER == poNumber.Trim()
                      select c).SingleOrDefault();

            md.ACTUALRECEIVEDBPKBDIHO = model.ACTUALRECEIVEDBPKBDIHO;
            md.ACTUALRECEIVEDBPKBDICAB = model.ACTUALRECEIVEDBPKBDICAB;
            md.DETAILPROBLEM = model.DETAILPROBLEM;
            md.REASONREVISEBPKB = model.REASONREVISEBPKB;
            md.TGLSERAHBPKB = model.TGLSERAHBPKB;
            md.ACTUALDELIVERYBPKBTOFINANCE = model.ACTUALDELIVERYBPKBTOFINANCE;
            md.ACTUALRECEIVEDBPKBHOBACK = model.ACTUALRECEIVEDBPKBHOBACK;
            md.DATEDELIVERYTOBRANCHORVENDOR = model.DATEDELIVERYTOBRANCHORVENDOR;
            md.NOBPKB = model.NOBPKB;
            md.NOFAKTUR = model.NOFAKTUR;
            md.NOSERTIFIKAT = model.NOSERTIFIKAT;
            md.NOFORMULIRA = model.NOFORMULIRA;
            md.KETSURATUBAHBENTUK = model.KETSURATUBAHBENTUK;
            md.NOSURATUBAHBENTUK = model.NOSURATUBAHBENTUK;
            md.TGLSURATUBAHBENTUK = model.TGLSURATUBAHBENTUK;
            md.KETERANGANSURATRUBAHWARNA = model.KETERANGANSURATRUBAHWARNA;
            md.NOSURATRUBAHWARNA = model.NOSURATRUBAHWARNA;
            md.TANGGALSURATRUBAHWARNA = model.TANGGALSURATRUBAHWARNA;
            md.NOSERTIFIKATREGUJITIPE = model.NOSERTIFIKATREGUJITIPE;
            md.REMARKSDETAILPROBLEM = model.REMARKSDETAILPROBLEM;
            md.KETBPKB = model.KETBPKB;
            md.MODIFIED_BY = model.MODIFIED_BY;
            md.MODIFIED_DATE = model.MODIFIED_DATE;
            dc.SubmitChanges();
            UpdateStatus(poNumber);
            return model;
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
    }
}