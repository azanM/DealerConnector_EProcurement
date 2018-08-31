using System.Web.Mvc;
using EProcurement.Services.Implementation;
using EProcurement.Services.Interface;
using System;
using System.Collections.Generic;
using ClosedXML;
using ClosedXML.Excel;
using EProcurement.Models.ViewModel.Reporting;

namespace EProcurement.Controllers
{
    public class ReportingProcurementController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            //try
            //{
            //    IReportPOProcService svc = new ReportPOProcService();
            //    var model = svc.GetAll();
            //    return View("~/Views/Reporting/Procurement/Index.cshtml", model);
            //}
            //catch (Exception ex)
            //{
            //    List<ListReportProcViewModel> model = new List<ListReportProcViewModel>();
            //    general.AddLogError("Report Vendor", ex.Message, ex.StackTrace);
            //    return View("~/Views/Reporting/Procurement/Index.cshtml", model);
            //}
            return View("~/Views/Reporting/Procurement/Index.cshtml");
        }
        public ActionResult Download(ListReportProcViewModel model)
        {
            try
            {
                XLWorkbook xlWorkBook = new XLWorkbook();
                var xlWorkSheet = xlWorkBook.Worksheets.Add("Report Procurement");// xlWorkSheet;

                //xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
                //xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Cell(1, 1).Value = "TanngalPO";
                xlWorkSheet.Cell(1, 2).Value = "PONUMBER";
                xlWorkSheet.Cell(1, 3).Value = "TYPEUNIT";
                xlWorkSheet.Cell(1, 4).Value = "QTYSATUAN";
                xlWorkSheet.Cell(1, 5).Value = "WARNA";
                xlWorkSheet.Cell(1, 6).Value = "VENDORNAME";
                xlWorkSheet.Cell(1, 7).Value = "WILAYAH";
                xlWorkSheet.Cell(1, 8).Value = "PROMISEDELIVERYDATE";
                xlWorkSheet.Cell(1, 9).Value = "SO";
                xlWorkSheet.Cell(1, 10).Value = "DO";
                xlWorkSheet.Cell(1, 11).Value = "GO";
                xlWorkSheet.Cell(1, 12).Value = "BILLINGNUMBER";
                xlWorkSheet.Cell(1, 13).Value = "INVOICERECEIPTDATE";
                xlWorkSheet.Cell(1, 14).Value = "KWITANSINUMBER";
                xlWorkSheet.Cell(1, 15).Value = "GROUPINGCODE";
                xlWorkSheet.Cell(1, 16).Value = "PAYMENTDATE";
                xlWorkSheet.Cell(1, 17).Value = "NOCHASISVENDOR";
                xlWorkSheet.Cell(1, 18).Value = "NOENGINEVENDOR";
              

                IReportPOProcService svc = new ReportPOProcService();
                var Data = svc.GetAll();
                int Row = 2;
                if (Data.Count > 0)
                {
                    for (int i = 0; i < Data.Count; i++)
                    {
                        xlWorkSheet.Cell(Row + i, 1).Value = Data[i].TGLPO;
                        xlWorkSheet.Cell(Row + i, 2).Value = Data[i].PONUMBER;
                        xlWorkSheet.Cell(Row + i, 3).Value = Data[i].MAINTYPEUNIT;
                        xlWorkSheet.Cell(Row + i, 4).Value = Data[i].POQTY;
                        xlWorkSheet.Cell(Row + i, 5).Value = Data[i].WARNA;
                        xlWorkSheet.Cell(Row + i, 6).Value = Data[i].VENDORNAME;
                        xlWorkSheet.Cell(Row + i, 7).Value = Data[i].CITY;
                        xlWorkSheet.Cell(Row + i, 8).Value = Data[i].DELIVERYDATE;
                        xlWorkSheet.Cell(Row + i, 9).Value = Data[i].SALESORDERNO;
                        xlWorkSheet.Cell(Row + i, 10).Value = Data[i].DONUMBER;
                        xlWorkSheet.Cell(Row + i, 11).Value = Data[i].ACTUALDATEDELIVEREDUNIT;
                        xlWorkSheet.Cell(Row + i, 12).Value = Data[i].BILLINGNO;
                        xlWorkSheet.Cell(Row + i, 13).Value = Data[i].ACTUALRECEIVEDINV;


                        xlWorkSheet.Cell(Row + i, 14).Value = Data[i].INVNO;
                        xlWorkSheet.Cell(Row + i, 15).Value = Data[i].CODEGROUP;
                        xlWorkSheet.Cell(Row + i, 16).Value = Data[i].TGLPEMBAYARAN;
                        xlWorkSheet.Cell(Row + i, 17).Value = Data[i].NOCHASIS_INPUT;
                        xlWorkSheet.Cell(Row + i, 18).Value = Data[i].NOENGINE_INPUT;
                        


                    }
                    xlWorkSheet.Columns().AdjustToContents();
                    var path = Server.MapPath("..") + "\\Report-Procurement.xlsx";
                    xlWorkBook.SaveAs(path);
                    xlWorkBook.Dispose();
                    return File(path, "application/vnd.ms-excel", "Report-Procurement.xlsx");
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult GetSearch(string PONumber)
        {
            IReportPOProcService svc = new ReportPOProcService();
            //var model = svc.GetAll();
            var model = svc.GetSearch(PONumber);
            // return Json( new { data = model}, JsonRequestBehavior.AllowGet);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSearchx()
        {
            IReportPOProcService svc = new ReportPOProcService();
            //var model = svc.GetAll();
            var model = svc.GetAll();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}