using System.Collections.Generic;
using System.Linq;
using EProcurement.Models;
using EProcurement.Services.Implementation;
using EProcurement.Services.Interface;
using EProcurement.Controllers;
using System;

namespace EProcurement.Services.Implementation
{
    public class UploadVendorService : IUploadVendorService
    {
        HomeController general = new HomeController();
        public bool IfExist(string poNumber)
        {
            var dc = new eprocdbDataContext();
            var md = (from c in dc.CUSTOMPOs
                      where c.PONUMBER == poNumber.Trim()
                      select c).Count();
            return md > 0 ? true : false;
        }
        public CUSTOMPO UpdateDeliveryCustPO(string poNumber, CUSTOMPO model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.CUSTOMPOs
                      where c.PONUMBER == poNumber.Trim()
                      select c).SingleOrDefault();
            if (md != null)
            {
                md.HARGADPP_INPUT = model.HARGADPP_INPUT;
                md.HARGAPPNUNIT_INPUT = model.HARGAPPNUNIT_INPUT;
                md.HARGABBN_INPUT = model.HARGABBN_INPUT;
                md.DODATE = model.DODATE;
                md.DONUMBER = model.DONUMBER;
                md.CARROSERIEVENDORNAME = model.CARROSERIEVENDORNAME;
                md.ACCESORIESADDRESS = model.ACCESORIESADDRESS;
                md.REMARKSCARROSSERIE = model.REMARKSCARROSSERIE;
                md.TGLMASUKKAROSERI = model.TGLMASUKKAROSERI;
                md.TGLSELESAIKAROSERI = model.TGLSELESAIKAROSERI;
                md.REMARKS = model.REMARKS;
                md.MODIFIED_BY = model.MODIFIED_BY;
                md.MODIFIED_DATE = model.MODIFIED_DATE;
                dc.SubmitChanges();
                UpdateStatus(poNumber);
            }
            return model;
        }

        public CUSTOMBPKB UpdateDeliveryCustBPKB(string poNumber, CUSTOMBPKB model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.CUSTOMBPKBs
                      where c.PONUMBER == poNumber.Trim()
                      select c).SingleOrDefault();
            if (md != null)
            {
                md.NOPOLISI_INPUT = model.NOPOLISI_INPUT;
                md.TGLSTNK_INPUT = model.TGLSTNK_INPUT;
                md.STCKDATE = model.STCKDATE;
                md.TGLSERAHBPKB = model.TGLSERAHBPKB;
                md.MODIFIED_BY = model.MODIFIED_BY;
                md.MODIFIED_DATE = model.MODIFIED_DATE;
                dc.SubmitChanges();
                UpdateStatus(poNumber);
            }
            return model;
        }

        public CUSTOMGR UpdateDeliveryCustGR(string poNumber, CUSTOMGR model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.CUSTOMGRs
                      where c.PONUMBER == poNumber.Trim()
                      select c).SingleOrDefault();
            if (md != null)
            {
                md.NOCHASIS_INPUT = model.NOCHASIS_INPUT;
                md.NOENGINE_INPUT = model.NOENGINE_INPUT;
                md.MODIFIED_BY = model.MODIFIED_BY;
                md.MODIFIED_DATE = model.MODIFIED_DATE;
                dc.SubmitChanges();
                UpdateStatus(poNumber);
            }
            return model;
        }
        public bool CheckAutheicatePoNumberByVendorID(string poNumber,string VendorId)
        {
            bool x = false;
            try
            {
                var dc = new eprocdbDataContext();
                var md = (from a in dc.CUSTOMPOs where a.PONUMBER == poNumber.Trim() && a.VENDORID == VendorId select a).ToList();
                x = md.Count > 0 ? true : false;
            }
            catch (System.Exception)
            {

                throw;
            }
            return x;
        }
        public CUSTOMPO UpdateInvoiceCustPO(string poNumber, CUSTOMPO model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.CUSTOMPOs
                      where c.PONUMBER == poNumber.Trim()
                      select c).SingleOrDefault();
            if (md != null)
            {
                md.TGLGI = model.TGLGI;
                md.MODIFIED_BY = model.MODIFIED_BY;
                md.MODIFIED_DATE = model.MODIFIED_DATE;
                dc.SubmitChanges();
                UpdateStatus(poNumber);
            }
            return model;
        }

        public CUSTOMBPKB UpdateInvoiceCustBpkb(string poNumber, CUSTOMBPKB model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.CUSTOMBPKBs
                      where c.PONUMBER == poNumber.Trim()
                      select c).SingleOrDefault();
            if (md != null)
            {
                md.NOFAKTUR = model.NOFAKTUR;
                md.MODIFIED_BY = model.MODIFIED_BY;
                md.MODIFIED_DATE = model.MODIFIED_DATE;
                dc.SubmitChanges();
                UpdateStatus(poNumber);
            }
            return model;
        }

        public CUSTOMGR UpdateInvoiceCustGR(string poNumber, CUSTOMGR model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.CUSTOMGRs
                      where c.PONUMBER == poNumber.Trim()
                      select c).SingleOrDefault();
            if (md != null)
            {
                md.STATUSDELIVERYUNIT = model.STATUSDELIVERYUNIT;
                md.MODIFIED_BY = model.MODIFIED_BY;
                md.MODIFIED_DATE = model.MODIFIED_DATE;
                dc.SubmitChanges();
                UpdateStatus(poNumber);
            }
            return model;
        }

        public CUSTOMIR UpdateInvoiceCustIR(string poNumber, CUSTOMIR model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.CUSTOMIRs
                      where c.PONUMBER == poNumber.Trim()
                      select c).SingleOrDefault();
            if (md != null)
            {
                md.INVNO = model.INVNO;
                md.INVDATE = model.INVDATE;
                md.MODIFIED_BY = model.MODIFIED_BY;
                md.MODIFIED_DATE = model.MODIFIED_DATE;
                dc.SubmitChanges();
                UpdateStatus(poNumber);
            }
            return model;
        }

        public CUSTOMBPKB UpdateBpkbCustBPKB(string poNumber, CUSTOMBPKB model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.CUSTOMBPKBs
                      where c.PONUMBER == poNumber.Trim()
                      select c).SingleOrDefault();
            if (md != null)
            {
                md.ACTUALDELIVERYBPKBTOFINANCE = model.ACTUALDELIVERYBPKBTOFINANCE;
                md.ACTUALRECEIVEDBPKBDICAB = model.ACTUALRECEIVEDBPKBDICAB;
                md.KETBPKB = model.KETBPKB;
                md.NOBPKB = model.NOBPKB;
                md.KETSURATUBAHBENTUK = model.KETSURATUBAHBENTUK;
                md.KETERANGANSURATRUBAHWARNA = model.KETERANGANSURATRUBAHWARNA;
                md.NOFAKTUR = model.NOFAKTUR;
                md.TGLFAKTUR = model.TGLFAKTUR;
                md.NOSERTIFIKAT = model.NOSERTIFIKAT;
                md.TGLSERTIFIKAT = model.TGLSERTIFIKAT;
                md.NOFORMULIRA = model.NOFORMULIRA;
                md.TGLFORMULIRA = model.TGLFORMULIRA;
                md.NOSURATUBAHBENTUK = model.NOSURATUBAHBENTUK;
                md.TGLSURATUBAHBENTUK = model.TGLSURATUBAHBENTUK;
                md.NOSURATRUBAHWARNA = model.NOSURATRUBAHWARNA;
                md.TANGGALSURATRUBAHWARNA = model.TANGGALSURATRUBAHWARNA;
                md.NOSERTIFIKATREGUJITIPE = model.NOSERTIFIKATREGUJITIPE;
                md.ACTUALRECEIVEDBPKBDIHO = model.ACTUALRECEIVEDBPKBDIHO;
                md.ACTUALDELIVERYBPKBTOFINANCE = model.ACTUALDELIVERYBPKBTOFINANCE;
                md.ACTUALRECEIVEDBPKBHOBACK = model.ACTUALRECEIVEDBPKBHOBACK;
                md.DATEDELIVERYTOBRANCHORVENDOR = model.DATEDELIVERYTOBRANCHORVENDOR;
                md.REMARKSDETAILPROBLEM = model.REMARKSDETAILPROBLEM;
                md.MODIFIED_BY = model.MODIFIED_BY;
                md.MODIFIED_DATE = model.MODIFIED_DATE;
                dc.SubmitChanges();
                UpdateStatus(poNumber);
            }
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