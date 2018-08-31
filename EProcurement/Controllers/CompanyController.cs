using System.Web.Mvc;
using EProcurement.Services;
using EProcurement.Models;
using EProcurement.Extensions;
using System;
using System.Runtime.InteropServices;
using ClosedXML;
using ClosedXML.Excel;

namespace EProcurement.Controllers
{
    public class CompanyController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            ICompanyService svc = new CompanyService();
            var model = svc.GetAll();
            return View("~/Views/Master/Company/Index.cshtml", model);
        }

        public ActionResult Add()
        {
            return View("~/Views/Master/Company/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Add(CUSTOMCOMPANY model)
        {
            try
            {
                ICompanyService svc = new CompanyService();
                var result = svc.Add(model);
                this.AddNotification("Your Data Has Been Successfully Saved. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Company Add", ex.Message, ex.StackTrace);
                this.AddNotification("ID exist", NotificationType.ERROR);
                return View("~/Views/Master/Company/Add.cshtml");
            }
        }

        public ActionResult Edit(string companyId)
        {
            ICompanyService svc = new CompanyService();
            var model = svc.Getdata(companyId);
            return View("~/Views/Master/Company/Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult Edit(CUSTOMCOMPANY model)
        {
            try
            {
                ICompanyService svc = new CompanyService();
                var result = svc.Edit(model.COMPANYCODE, model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Company Edit", ex.Message, ex.StackTrace);
                return View("~/Views/Master/Company/Index.cshtml");
            }
        }
        
        public ActionResult Download (CUSTOMCOMPANY model)
        {
            try
            {
                XLWorkbook xlWorkBook = new XLWorkbook();
                var xlWorkSheet = xlWorkBook.Worksheets.Add("Master Company");// xlWorkSheet;

                xlWorkSheet.Cell(1, 1).Value = "COMPANYCODE";
                xlWorkSheet.Cell(1, 2).Value = "COMPANYCODETSO";
                xlWorkSheet.Cell(1, 3).Value = "COMPANYNAME";
                xlWorkSheet.Cell(1, 4).Value = "ALAMAT";
                xlWorkSheet.Cell(1, 5).Value = "KOTA";
                xlWorkSheet.Cell(1, 6).Value = "REGION";
                xlWorkSheet.Cell(1, 7).Value = "POSTALCODE";
                xlWorkSheet.Cell(1, 8).Value = "TELEPON";
                xlWorkSheet.Cell(1, 9).Value = "NPWP";
                xlWorkSheet.Cell(1, 10).Value = "KTPTDP";

                ICompanyService svc = new CompanyService();
                var Data = svc.GetAll();
                int Row = 2;
                if(Data.Count > 0)
                {
                    for(int i = 0; i < Data.Count; i++)
                    {
                        xlWorkSheet.Cell(Row + i, 1).Value = Data[i].COMPANYCODE;
                        xlWorkSheet.Cell(Row + i, 2).Value = Data[i].COMPANYCODETSO;
                        xlWorkSheet.Cell(Row + i, 3).Value = Data[i].COMPANYNAME;
                        xlWorkSheet.Cell(Row + i, 4).Value = Data[i].ALAMAT;
                        xlWorkSheet.Cell(Row + i, 5).Value = Data[i].KOTA;
                        xlWorkSheet.Cell(Row + i, 6).Value = Data[i].REGION;
                        xlWorkSheet.Cell(Row + i, 7).Value = Data[i].POSTALCODE;
                        xlWorkSheet.Cell(Row + i, 8).Value = Data[i].TELEPON;
                        xlWorkSheet.Cell(Row + i, 9).Value = Data[i].NPWP;
                        xlWorkSheet.Cell(Row + i, 10).Value = Data[i].KTPTDP;
                    }
                    xlWorkSheet.Columns().AdjustToContents();
                    var path = Server.MapPath("..") + "\\Master-Company.xlsx";
                    xlWorkBook.SaveAs(path);
                    xlWorkBook.Dispose();
                    return File(path, "application/vnd.ms-excel", "Master-Company.xlsx");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Company Edit", ex.Message, ex.StackTrace);
                return View("~/Views/Master/Company/Index.cshtml", model);
            }
        }        
    }
}