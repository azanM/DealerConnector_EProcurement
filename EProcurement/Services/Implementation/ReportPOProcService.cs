using System.Collections.Generic;
using System.Linq;
using EProcurement.Models;
using EProcurement.Models.ViewModel.Reporting;
using EProcurement.Services.Interface;
using System;

namespace EProcurement.Services.Implementation
{
    public class ReportPOProcService : IReportPOProcService
    {
        public List<ListReportProcViewModel> GetAll()
        {
            var dc = new eprocdbDataContext();
            //var sCUSTOM_S02002 = dc.CUSTOM_S02002s
            //                      .GroupBy(v => v.PONumber)
            //                      .Select(x => x.OrderByDescending(pv => pv.DataVersion).First());
            //var sCUSTOM_S02006 = dc.CUSTOM_S02006s
            //                      .GroupBy(v => v.PONumberSERA)
            //                      .Select(x => x.OrderByDescending(pv => pv.VersionPOSERA).First());

            var sCUSTOM_S02002 = from t in dc.CUSTOM_S02002s
                                 group t by t.PONumber
                                into g
                                 select new
                                 {
                                     PONumber = g.Key,
                                     MaxVer = (from t2 in g select t2.DataVersion).Max(),
                                     SO = (from t2 in g select t2.SalesOrderNo).Max()
                                 };

            var sCUSTOM_S02006 = from t in dc.CUSTOM_S02006s
                                 group t by t.PONumberSERA
                                into g
                                 select new
                                 {
                                     PONumberSERA = g.Key,
                                     MaxVer = (from t2 in g select t2.VersionPOSERA).Max(),
                                     BN = (from t2 in g select t2.BillingNo).Max()
                                 };

            var model = (from custPO in dc.CUSTOMPOs
                         join custS02002 in sCUSTOM_S02002 on custPO.PONUMBER equals custS02002.PONumber into c2
                         from custS02002 in c2.DefaultIfEmpty()
                         join custGR in dc.CUSTOMGRs on custPO.PONUMBER equals custGR.PONUMBER
                         join custIR in dc.CUSTOMIRs on custPO.PONUMBER equals custIR.PONUMBER
                         join custS02006 in sCUSTOM_S02006 on custPO.PONUMBER equals custS02006.PONumberSERA into c6
                         from custS02006 in c6.DefaultIfEmpty()
                         orderby custPO.TGLPO
                         select new ListReportProcViewModel
                         {
                             //StrTglPO = a.TglPO == null ? "" : a.TglPO.Value.ToString("dd/MM/yyyy"),
                             TGLPO = string.Format("{0:dd/MM/yyyy}", custPO.TGLPO),
                             //custPO.TGLPO == null ? "" : custPO.TGLPO.Value.ToString("dd/MM/yyyy"),
                             PONUMBER = custPO.PONUMBER,
                             MAINTYPEUNIT = custPO.MAINTYPEUNIT,
                             POQTY = custPO.POQTY,
                             WARNA = custPO.COLOR,
                             VENDORNAME = custPO.VENDORNAME,
                             CITY = custPO.CITY,
                             DELIVERYDATE = string.Format("{0:dd/MM/yyyy}", custPO.PROMISEDLVDATEPO),
                             SALESORDERNO = custS02002.SO,
                             BILLINGNO = custS02006.BN,
                             DONUMBER = custPO.DONUMBER,
                             ACTUALDATEDELIVEREDUNIT = string.Format("{0:dd/MM/yyyy}", custPO.ACTUALDATEDELIVEREDUNIT),
                             NOCHASIS_INPUT = custGR.NOCHASIS_INPUT,
                             NOENGINE_INPUT = custGR.NOENGINE_INPUT,
                             INVNO = custIR.INVNO,
                             ACTUALRECEIVEDINV = string.Format("{0:dd/MM/yyyy}", custIR.ACTUALRECEIVEDINV),
                             CODEGROUP = custPO.CODEGROUP,
                             TGLPEMBAYARAN = string.Format("{0:dd/MM/yyyy}", custIR.TGLPEMBAYARAN)
                         }).ToList();

            return model;
        }

        public List<ListReportProcViewModel> GetSearch(string PoNumber)
        {
            var dc = new eprocdbDataContext();
            var model = new List<ListReportProcViewModel>();
            if (PoNumber != "")
            {
                var sCUSTOM_S02002 = from t in dc.CUSTOM_S02002s
                                     group t by t.PONumber
                                into g
                                     select new
                                     {
                                         PONumber = g.Key,
                                         MaxVer = (from t2 in g select t2.DataVersion).Max(),
                                         SO = (from t2 in g select t2.SalesOrderNo).Max()
                                     };

                var sCUSTOM_S02006 = from t in dc.CUSTOM_S02006s
                                     group t by t.PONumberSERA
                                    into g
                                     select new
                                     {
                                         PONumberSERA = g.Key,
                                         MaxVer = (from t2 in g select t2.VersionPOSERA).Max(),
                                         BN = (from t2 in g select t2.BillingNo).Max()
                                     };

                model = (from custPO in dc.CUSTOMPOs
                         join custS02002 in sCUSTOM_S02002 on custPO.PONUMBER equals custS02002.PONumber into c2
                         from custS02002 in c2.DefaultIfEmpty()
                         join custGR in dc.CUSTOMGRs on custPO.PONUMBER equals custGR.PONUMBER
                         join custIR in dc.CUSTOMIRs on custPO.PONUMBER equals custIR.PONUMBER
                         join custS02006 in sCUSTOM_S02006 on custPO.PONUMBER equals custS02006.PONumberSERA into c6
                         from custS02006 in c6.DefaultIfEmpty()
                         where custPO.PONUMBER.Contains(PoNumber)
                         orderby custPO.TGLPO
                         select new ListReportProcViewModel
                         {
                             TGLPO = string.Format("{0:dd/MM/yyyy}", custPO.TGLPO),
                             PONUMBER = custPO.PONUMBER,
                             MAINTYPEUNIT = custPO.MAINTYPEUNIT,
                             POQTY = custPO.POQTY,
                             WARNA = custPO.COLOR,
                             VENDORNAME = custPO.VENDORNAME,
                             CITY = custPO.CITY,
                             DELIVERYDATE = string.Format("{0:dd/MM/yyyy}", custPO.PROMISEDLVDATEPO),// == null ? "" : custPO.PROMISEDLVDATEPO.Value.ToString(""),
                             SALESORDERNO = custS02002.SO,
                             BILLINGNO = custS02006.BN,
                             DONUMBER = custPO.DONUMBER,
                             ACTUALDATEDELIVEREDUNIT = string.Format("{0:dd/MM/yyyy}", custPO.ACTUALDATEDELIVEREDUNIT),// == null ? "" : custPO.ACTUALDATEDELIVEREDUNIT.Value.ToString(""),
                             NOCHASIS_INPUT = custGR.NOCHASIS_INPUT,
                             NOENGINE_INPUT = custGR.NOENGINE_INPUT,
                             INVNO = custIR.INVNO,
                             ACTUALRECEIVEDINV = string.Format("{0:dd/MM/yyyy}", custIR.ACTUALRECEIVEDINV),// == null ? "" : custIR.ACTUALRECEIVEDINV.Value.ToString(""),
                             CODEGROUP = custPO.CODEGROUP,
                             TGLPEMBAYARAN = string.Format("{0:dd/MM/yyyy}", custIR.TGLPEMBAYARAN),// == null ? "" : custIR.TGLPEMBAYARAN.Value.ToString("")
                         }).ToList();
            }
            else
            {
                var sCUSTOM_S02002 = from t in dc.CUSTOM_S02002s
                                     group t by t.PONumber
                                into g
                                     select new
                                     {
                                         PONumber = g.Key,
                                         MaxVer = (from t2 in g select t2.DataVersion).Max(),
                                         SO = (from t2 in g select t2.SalesOrderNo).Max()
                                     };

                var sCUSTOM_S02006 = from t in dc.CUSTOM_S02006s
                                     group t by t.PONumberSERA
                                    into g
                                     select new
                                     {
                                         PONumberSERA = g.Key,
                                         MaxVer = (from t2 in g select t2.VersionPOSERA).Max(),
                                         BN = (from t2 in g select t2.BillingNo).Max()
                                     };

                model = (from custPO in dc.CUSTOMPOs
                         join custS02002 in sCUSTOM_S02002 on custPO.PONUMBER equals custS02002.PONumber into c2
                         from custS02002 in c2.DefaultIfEmpty()
                         join custGR in dc.CUSTOMGRs on custPO.PONUMBER equals custGR.PONUMBER
                         join custIR in dc.CUSTOMIRs on custPO.PONUMBER equals custIR.PONUMBER
                         join custS02006 in sCUSTOM_S02006 on custPO.PONUMBER equals custS02006.PONumberSERA into c6
                         from custS02006 in c6.DefaultIfEmpty()
                         orderby custPO.TGLPO
                         select new ListReportProcViewModel
                         {
                             TGLPO = string.Format("{0:dd/MM/yyyy}", custPO.TGLPO),// == null ? "" : custPO.TGLPO.Value.ToString("dd/MM/yyyy"),
                             PONUMBER = custPO.PONUMBER,
                             MAINTYPEUNIT = custPO.MAINTYPEUNIT,
                             POQTY = custPO.POQTY,
                             WARNA = custPO.COLOR,
                             VENDORNAME = custPO.VENDORNAME,
                             CITY = custPO.CITY,
                             DELIVERYDATE = string.Format("{0:dd/MM/yyyy}", custPO.PROMISEDLVDATEPO),// == null ? "" : custPO.PROMISEDLVDATEPO.Value.ToString(),
                             SALESORDERNO = custS02002.SO,
                             BILLINGNO = custS02006.BN,
                             DONUMBER = custPO.DONUMBER,
                             ACTUALDATEDELIVEREDUNIT = string.Format("{0:dd/MM/yyyy}", custPO.ACTUALDATEDELIVEREDUNIT),// == null ? "" : custPO.ACTUALDATEDELIVEREDUNIT.Value.ToString(""),
                             NOCHASIS_INPUT = custGR.NOCHASIS_INPUT,
                             NOENGINE_INPUT = custGR.NOENGINE_INPUT,
                             INVNO = custIR.INVNO,
                             ACTUALRECEIVEDINV = string.Format("{0:dd/MM/yyyy}", custIR.ACTUALRECEIVEDINV),// == null ? "" : custIR.ACTUALRECEIVEDINV.Value.ToString(),
                             CODEGROUP = custPO.CODEGROUP,
                             TGLPEMBAYARAN = string.Format("{0:dd/MM/yyyy}", custIR.TGLPEMBAYARAN)// == null ? "" : custIR.TGLPEMBAYARAN.Value.ToString()
                         }).ToList();

            }
            return model;
        }
    }
}