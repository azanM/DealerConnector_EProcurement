using EProcurement.Models;
using EProcurement.Services;
using System.Web.Mvc;
using System.Collections.Generic;
using EProcurement.Extensions;
using System;
using ClosedXML.Excel;
using EProcurement.Models.ViewModel.Master;

namespace EProcurement.Controllers
{
    public class VendorController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            IVendorService svc = new VendorService();
            var model = svc.GetAll();
            return View("~/Views/Master/Vendor/Index.cshtml", model);
        }

        public ActionResult Add()
        {
            this.ViewBag.B2B = new SelectList(this.GetStatus(), "Key", "Value");
            return View("~/Views/Master/Vendor/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Add(VendorViewModel model)
        {
            try
            {
                IVendorService objIVS = new VendorService();
                var result = objIVS.Add(model);
                this.AddNotification("Your Data Has Been Successfully Saved. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Vendor Add", ex.Message, ex.StackTrace);
                this.AddNotification("ID exist", NotificationType.ERROR);
                return View("~/Views/Master/Vendor/Add.cshtml");
            }
        }

        public ActionResult Edit(string vendorId)
        {
            this.ViewBag.B2B = new SelectList(this.GetStatus(), "Key", "Value");
            IVendorService objIVS = new VendorService();
            var model = objIVS.Getdata(vendorId);
            return View("~/Views/Master/Vendor/Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult Edit(string vendorId, VendorViewModel model)
        {
            try
            {
                IVendorService objIVS = new VendorService();
                var result = objIVS.Edit(vendorId, model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Vendor Edit", ex.Message, ex.StackTrace);
                return View("~/Views/Master/Vendor/Index.cshtml");
            }
        }

        protected Dictionary<string, string> GetStatus()
        {
            var result = new Dictionary<string, string>();
            result.Add("1", "Yes");
            result.Add("0", "No");

            return result;
        }


        public FileResult Download()
        {
            XLWorkbook xlWorkBook = new XLWorkbook();
            var xlWorkSheet = xlWorkBook.Worksheets.Add("Master Vendor");

            IVendorService svc = new VendorService();
            var model = svc.GetAll();
            xlWorkSheet.Cell(1, 1).Value = "VENDORID";
            xlWorkSheet.Cell(1, 2).Value = "VENDORNAME";
            xlWorkSheet.Cell(1, 3).Value = "STREET";
            xlWorkSheet.Cell(1, 4).Value = "DISTRIC";
            xlWorkSheet.Cell(1, 5).Value = "CITY";
            xlWorkSheet.Cell(1, 6).Value = "POSTALCODE";
            xlWorkSheet.Cell(1, 7).Value = "TELEPHONE";
            xlWorkSheet.Cell(1, 8).Value = "EMAIL";
            xlWorkSheet.Cell(1, 9).Value = "EMAIL2";
            xlWorkSheet.Cell(1, 10).Value = "EMAIL3";
            xlWorkSheet.Cell(1, 11).Value = "EMAIL4";
            xlWorkSheet.Cell(1, 12).Value = "EMAIL5";
            xlWorkSheet.Cell(1, 13).Value = "EMAIL6";
            xlWorkSheet.Cell(1, 14).Value = "EMAIL7";
            xlWorkSheet.Cell(1, 15).Value = "EMAIL8";
            xlWorkSheet.Cell(1, 16).Value = "PLANTIDTSO";
            xlWorkSheet.Cell(1, 16).Value = "ISB2B";
            xlWorkSheet.Range("A1", "Q1").Style.Font.Bold = true;

            int i = 2;
            foreach (var m in model)
            {
                xlWorkSheet.Cell(i, 1).Value = "'"+m.VENDORID;
                xlWorkSheet.Cell(i, 2).Value = m.VENDORNAME;
                xlWorkSheet.Cell(i, 3).Value = m.STREET;
                xlWorkSheet.Cell(i, 4).Value = m.DISTRIC;
                xlWorkSheet.Cell(i, 5).Value = m.CITY;
                xlWorkSheet.Cell(i, 6).Value = m.POSTALCODE;
                xlWorkSheet.Cell(i, 7).Value = m.TELEPHONE;
                xlWorkSheet.Cell(i, 8).Value = m.EMAIL;
                xlWorkSheet.Cell(i, 9).Value = m.EMAIL2;
                xlWorkSheet.Cell(i, 10).Value = m.EMAIL3;
                xlWorkSheet.Cell(i, 11).Value = m.EMAIL4;
                xlWorkSheet.Cell(i, 12).Value = m.EMAIL5;
                xlWorkSheet.Cell(i, 13).Value = m.EMAIL6;
                xlWorkSheet.Cell(i, 14).Value = m.EMAIL7;
                xlWorkSheet.Cell(i, 15).Value = m.EMAIL8;
                xlWorkSheet.Cell(i, 16).Value = m.PLANTIDTSO;
                xlWorkSheet.Cell(i, 16).Value = m.B2B == '1' ? "1" : "0";
                i++;
            }

            xlWorkSheet.Columns().AdjustToContents();
            var path = Server.MapPath("..") + "\\Master-Vendor.xlsx";
            xlWorkBook.SaveAs(path);
            xlWorkBook.Dispose();
            return File(path, "application/vnd.ms-excel", "Master-Vendor.xlsx");
        }
    }
}