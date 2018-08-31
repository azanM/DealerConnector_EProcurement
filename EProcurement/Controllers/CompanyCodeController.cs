using System.Web.Mvc;
using EProcurement.Services;
using EProcurement.Models;
using System.Collections.Generic;
using EProcurement.Extensions;
using System;
using ClosedXML;
using ClosedXML.Excel;

namespace EProcurement.Controllers
{
    public class CompanyCodeController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            ICompanyCodeService svc = new CompanyCodeService();
            var model = svc.GetAll();
            return View("~/Views/Master/CompanyCode/Index.cshtml", model);
        }

        public ActionResult Add()
        {
            this.ViewBag.Status = new SelectList(this.GetStatus(), "Key", "Value");
            return View("~/Views/Master/CompanyCode/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Add(Company_Code model)
        {
            try
            {
                ICompanyCodeService svc = new CompanyCodeService();
                var result = svc.Add(model);
                this.ViewBag.Status = new SelectList(this.GetStatus(), "Key", "Value");
                this.AddNotification("Your Data Has Been Successfully Saved. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("CompanyCode Add", ex.Message, ex.StackTrace);
                this.AddNotification("ID exist", NotificationType.ERROR);
                this.ViewBag.Status = new SelectList(this.GetStatus(), "Key", "Value");
                return View("~/Views/Master/CompanyCode/Add.cshtml");
            }
        }
        public ActionResult Edit(string companyCode)
        {
            ICompanyCodeService svc = new CompanyCodeService();
            var model = svc.Getdata(companyCode);
            if(model.status == null)
            {
                model.status = "Non Active";
            }
            this.ViewBag.Status = new SelectList(this.GetStatus(), "Key", "Value");
            return View("~/Views/Master/CompanyCode/Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult Edit(string companyCode, Company_Code model)
        {
            try
            {
                ICompanyCodeService svc = new CompanyCodeService();
                var result = svc.Edit(companyCode, model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("CompanyCode Edit", ex.Message, ex.StackTrace);
                return View("~/Views/Master/CompanyCode/Index.cshtml", model);
            }
        }
        protected Dictionary<string, string> GetStatus()
        {
            var result = new Dictionary<string, string>();
            result.Add("Active", "Active");
            result.Add("Inactive", "Inactive");

            return result;
        }
        public ActionResult Download(Company_Code model)
        {
            try
            {
                XLWorkbook xlWorkBook = new XLWorkbook();
                var xlWorkSheet = xlWorkBook.Worksheets.Add("Master CompanyCode");// xlWorkSheet;

                xlWorkSheet.Cell(1, 1).Value = "CompanyCode";
                xlWorkSheet.Cell(1, 2).Value = "CompanyName";

                ICompanyCodeService svc = new CompanyCodeService();
                var Data = svc.GetAll();
                int Row = 2;
                if (Data.Count > 0)
                {
                    for (int i = 0; i < Data.Count; i++)
                    {
                        xlWorkSheet.Cell(Row + i, 1).Value = Data[i].companyCode;
                        xlWorkSheet.Cell(Row + i, 2).Value = Data[i].companyName;

                    }
                    xlWorkSheet.Columns().AdjustToContents();
                    var path = Server.MapPath("..") + "\\Master-CompanyCode.xlsx";
                    xlWorkBook.SaveAs(path);
                    xlWorkBook.Dispose();
                    return File(path, "application/vnd.ms-excel", "Master-CompanyCode.xlsx");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("CompanyCode Download", ex.Message, ex.StackTrace);
                return View("~/Views/Master/CompanyCode/Index.cshtml", model);
            }

        }
    }
}