using System.Collections.Generic;
using System.Linq;
using EProcurement.Models;
using EProcurement.Models.ViewModel.Reporting;
using EProcurement.Services.Interface;

namespace EProcurement.Services.Implementation
{
    public class ReportPOCabangService : IReportPOCabangService
    {
        public List<ListPOCabangViewModel> GetAll()
        {
            var dc = new eprocdbDataContext();
            string PlantID = System.Web.HttpContext.Current.Session["VendorID"] == null ? "" : System.Web.HttpContext.Current.Session["VendorID"].ToString();
            var model = (from custPO in dc.CUSTOMPOs
                         join custIR in dc.CUSTOMIRs on custPO.PONUMBER equals custIR.PONUMBER
                         join custGR in dc.CUSTOMGRs on custPO.PONUMBER equals custGR.PONUMBER
                         join custBPKB in dc.CUSTOMBPKBs on custPO.PONUMBER equals custBPKB.PONUMBER
                         join custPR in dc.CUSTOMPRs on custPO.PRSAP equals custPR.PRSAP
                         join custMISC in dc.CUSTOMMISCs on custPO.PONUMBER equals custMISC.PONumber
                         join stream in dc.STREAMLINERs on custPO.PONUMBER equals stream.PONUMBER
                         join custSTAT in dc.CUSTOMSTATUS on custPO.POSTATUSID equals custSTAT.ID
                         join plant in dc.MSPLANTs on stream.Plant equals plant.PLANTID
                         where custPO.TGLPO.Value.Year > (System.DateTime.Now.Year - 2)
                         && stream.Plant == PlantID
                         select new ListPOCabangViewModel
                         {
                             OFFICER = stream.OfficerName,
                             POSTATUS = custSTAT.STATUS,
                             PLANT = plant.NAME,
                             REQUESTER = stream.RequesterName,
                             REQUESTDELIVERYDATE = stream.REQUESTDELIVERYDATE,
                             PRNUMBERDATE = custPR.TGLPRSAP,
                             PRNUMBER = custPR.PRSAP,
                             PRDATESAP = custPR.TGLPRSAP,
                             PRNUMBERSAP = custPR.PRSAP,
                             PERIODEPO = custPO.TGLPO.Value.Month,
                             TGLPO = custPO.TGLPO,
                             PONUMBER = custPO.PONUMBER,
                             TYPEUNIT = custPO.DESCPO,
                             COLOR = custPO.COLOR,
                             BBN = stream.BBN,
                             PURCHASESTATUS = custPO.PURCHASESTATUS,
                             ASTRA = custPO.VENDORNAME == "AI-" ? "ASTRA" :
                                    custPO.VENDORNAME == null ? "" :
                                    "NONASTRA",
                             OVERDUEDLV = (custPO.PROMISEDLVDATEPO - System.DateTime.Now).Value.Days,
                             ACTUALRECEIVEDUNIT = custPO.ACTUALDATEDELIVEREDUNIT,
                             KETPO = custPO.REMARKS,
                             NOCHASIS_INPUT = custGR.NOCHASIS_INPUT,
                             NOENGINE_INPUT = custGR.NOENGINE_INPUT,
                             NOPOLISI_INPUT = custBPKB.NOPOLISI_INPUT,
                             TGLSTNK_INPUT = custBPKB.TGLSTNK_INPUT,
                             NOCHASIS = custGR.NOCHASIS,
                             NOENGINE = custGR.NOENGINE,
                             NOPOLISI = custBPKB.NOPOLISI,
                             TGLSTNK = custBPKB.TGLSTNK,
                             TGLGRSAP = custGR.TGLGRSAP,
                             YEAR = custPR.YEAR,
                             TGLMASUKKAROSERI = custPO.TGLMASUKKAROSERI,
                             TGLSELESAIKAROSERI = custPO.TGLSELESAIKAROSERI,
                             KETKAROSERI = custBPKB.KETKAROSERI,
                             BENTUKAKHIRUNIT = custPO.BENTUKAKHIRUNIT,
                             MERK = custPO.MERK,
                             MAINTYPEUNIT = custPO.CARMODEL,
                             GARDAN = stream.Gardan,
                             VARIAN = custPO.TYPEUNIT,
                             APPROVED = stream.Approver,
                             QTYSATUAN = custPO.POQTY,
                             PRKAROSERI = stream.PRKaroseri
                         }).ToList();

            return model;
        }
    }
}