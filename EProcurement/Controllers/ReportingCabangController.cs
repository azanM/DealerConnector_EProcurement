using System.Web.Mvc;
using EProcurement.Services.Implementation;
using EProcurement.Services.Interface;
using System;
using System.Collections.Generic;
using EProcurement.Models.ViewModel.Reporting;
using ClosedXML;
using ClosedXML.Excel;

namespace EProcurement.Controllers
{
    public class ReportingCabangController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            try
            {
                string VendorID = System.Web.HttpContext.Current.Session["VendorID"] == null ? "" : System.Web.HttpContext.Current.Session["VendorID"].ToString();
                IReportPOCabangService svc = new ReportPOCabangService();
                var model = svc.GetAll();
                return View("~/Views/Reporting/Cabang/Index.cshtml", model);
            }
            catch (Exception ex)
            {
                List<ListPOCabangViewModel> model = new List<ListPOCabangViewModel>();
                general.AddLogError("Report Vendor", ex.Message, ex.StackTrace);
                return View("~/Views/Reporting/Cabang/Index.cshtml", model);
            }
        }

        public ActionResult Download(ListPOCabangViewModel model)
        {
            try
            {
                string VendorID = System.Web.HttpContext.Current.Session["VendorID"] == null ? "" : System.Web.HttpContext.Current.Session["VendorID"].ToString();
                XLWorkbook xlWorkBook = new XLWorkbook();
                var xlWorkSheet = xlWorkBook.Worksheets.Add("Report Cabang");// xlWorkSheet;

                //xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
                //xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Cell(1, 1).Value = "OFFICER";
                xlWorkSheet.Cell(1, 2).Value = "POSTATUS";
                xlWorkSheet.Cell(1, 3).Value = "PLANT";
                xlWorkSheet.Cell(1, 4).Value = "REQUEST";
                xlWorkSheet.Cell(1, 5).Value = "REQUESTDELIVERYDATE";
                xlWorkSheet.Cell(1, 6).Value = "PRNUMBERDATE";
                xlWorkSheet.Cell(1, 7).Value = "PRNUMBER";
                xlWorkSheet.Cell(1, 8).Value = "PRDATESAP";
                xlWorkSheet.Cell(1, 9).Value = "PERIODEPO";
                xlWorkSheet.Cell(1, 10).Value = "TANGGALPO";
                xlWorkSheet.Cell(1, 11).Value = "PONUMBER";
                xlWorkSheet.Cell(1, 12).Value = "TYPEUNIT";
                xlWorkSheet.Cell(1, 13).Value = "COLOR";
                xlWorkSheet.Cell(1, 14).Value = "BBN";
                xlWorkSheet.Cell(1, 15).Value = "STATUSBELI";
                xlWorkSheet.Cell(1, 16).Value = "ASTRA/NONASTRA";
                xlWorkSheet.Cell(1, 17).Value = "OVERDUEDELIVERY";
                xlWorkSheet.Cell(1, 18).Value = "ACTUALRECEIVEDUNIT";
                xlWorkSheet.Cell(1, 19).Value = "KETPO";

                xlWorkSheet.Cell(1, 20).Value = "NOCHASISVENDOR";
                xlWorkSheet.Cell(1, 21).Value = "NOENGINEVENDOR";
                xlWorkSheet.Cell(1, 22).Value = "NOPOLISIVENDOR";
                xlWorkSheet.Cell(1, 23).Value = "TGLSTNKVENDOR";
                xlWorkSheet.Cell(1, 24).Value = "NOCHASIS";
                xlWorkSheet.Cell(1, 25).Value = "NOENGINE";
                xlWorkSheet.Cell(1, 26).Value = "NOPOLISI";

                xlWorkSheet.Cell(1, 27).Value = "TGLSTNK";
                xlWorkSheet.Cell(1, 28).Value = "TGLGRSAP";
                xlWorkSheet.Cell(1, 29).Value = "YEAR";
                xlWorkSheet.Cell(1, 30).Value = "TGLMASUKKAROSERI";
                xlWorkSheet.Cell(1, 31).Value = "KETKAROSERI";
                xlWorkSheet.Cell(1, 32).Value = "BENTUKAKHIRUNIT";
                xlWorkSheet.Cell(1, 33).Value = "MERK";

                xlWorkSheet.Cell(1, 34).Value = "MAINTYPEUNIT";
                xlWorkSheet.Cell(1, 35).Value = "GARDAN";
                xlWorkSheet.Cell(1, 36).Value = "VARIAN";
                xlWorkSheet.Cell(1, 37).Value = "APPROVED";
                xlWorkSheet.Cell(1, 38).Value = "QTYSATUAN";
                xlWorkSheet.Cell(1, 39).Value = "PRKAROSERI";
               
                IReportPOCabangService svc = new ReportPOCabangService();
                var Data = svc.GetAll();
                int Row = 2;
                if (Data.Count > 0)
                {
                    for (int i = 0; i < Data.Count; i++)
                    {
                        xlWorkSheet.Cell(Row + i, 1).Value = Data[i].OFFICER;
                        xlWorkSheet.Cell(Row + i, 2).Value = Data[i].POSTATUS;
                        xlWorkSheet.Cell(Row + i, 3).Value = Data[i].PLANT;
                        xlWorkSheet.Cell(Row + i, 4).Value = Data[i].REQUESTERNAME;
                        xlWorkSheet.Cell(Row + i, 5).Value = Data[i].REQUESTDELIVERYDATE;
                        xlWorkSheet.Cell(Row + i, 6).Value = Data[i].PRNUMBERDATE;
                        xlWorkSheet.Cell(Row + i, 7).Value = Data[i].PRNUMBER;
                        xlWorkSheet.Cell(Row + i, 8).Value = Data[i].PRDATESAP;
                        xlWorkSheet.Cell(Row + i, 9).Value = Data[i].PERIODEPO;
                        xlWorkSheet.Cell(Row + i, 10).Value = Data[i].TGLPO;
                        xlWorkSheet.Cell(Row + i, 11).Value = Data[i].PONUMBER;
                        xlWorkSheet.Cell(Row + i, 12).Value = Data[i].TYPEUNIT;
                        xlWorkSheet.Cell(Row + i, 13).Value = Data[i].COLOR;


                        xlWorkSheet.Cell(Row + i, 14).Value = Data[i].BBN;
                        xlWorkSheet.Cell(Row + i, 15).Value = Data[i].PURCHASESTATUS;
                        xlWorkSheet.Cell(Row + i, 16).Value = Data[i].ASTRA;
                        xlWorkSheet.Cell(Row + i, 17).Value = Data[i].OVERDUEDLV;
                        xlWorkSheet.Cell(Row + i, 18).Value = Data[i].ACTUALRECEIVEDUNIT;
                        xlWorkSheet.Cell(Row + i, 19).Value = Data[i].KETPO;
                        xlWorkSheet.Cell(Row + i, 20).Value = Data[i].NOCHASIS_INPUT;
                        xlWorkSheet.Cell(Row + i, 21).Value = Data[i].NOENGINE_INPUT;
                        xlWorkSheet.Cell(Row + i, 22).Value = Data[i].NOPOLISI_INPUT;
                        xlWorkSheet.Cell(Row + i, 23).Value = Data[i].TGLSTNK_INPUT;
                        xlWorkSheet.Cell(Row + i, 24).Value = Data[i].NOCHASIS;
                        xlWorkSheet.Cell(Row + i, 25).Value = Data[i].NOENGINE;
                        xlWorkSheet.Cell(Row + i, 26).Value = Data[i].NOPOLISI;

                        xlWorkSheet.Cell(Row + i, 27).Value = Data[i].TGLSTNK;
                        xlWorkSheet.Cell(Row + i, 28).Value = Data[i].TGLGRSAP;
                        xlWorkSheet.Cell(Row + i, 29).Value = Data[i].YEAR;
                        xlWorkSheet.Cell(Row + i, 30).Value = Data[i].TGLMASUKKAROSERI;
                        xlWorkSheet.Cell(Row + i, 31).Value = Data[i].KETKAROSERI;
                        xlWorkSheet.Cell(Row + i, 32).Value = Data[i].BENTUKAKHIRUNIT;
                        xlWorkSheet.Cell(Row + i, 33).Value = Data[i].MERK;
                        xlWorkSheet.Cell(Row + i, 34).Value = Data[i].MAINTYPEUNIT;
                        xlWorkSheet.Cell(Row + i, 35).Value = Data[i].GARDAN;
                        xlWorkSheet.Cell(Row + i, 36).Value = Data[i].VARIAN;
                        xlWorkSheet.Cell(Row + i, 37).Value = Data[i].APPROVED;
                        xlWorkSheet.Cell(Row + i, 38).Value = Data[i].QTYSATUAN;
                        xlWorkSheet.Cell(Row + i, 39).Value = Data[i].PRKAROSERI;

                    
                    }
                    xlWorkSheet.Columns().AdjustToContents();
                    var path = Server.MapPath("..") + "\\Report-Cabang.xlsx";
                    xlWorkBook.SaveAs(path);
                    xlWorkBook.Dispose();
                    return File(path, "application/vnd.ms-excel", "Report-Cabang.xlsx");
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}