using System.Web.Mvc;
using EProcurement.Services;
using EProcurement.Models.ViewModel.Transaksi;
using EProcurement.Extensions;
using System.Collections.Generic;
using System;

namespace EProcurement.Controllers
{
    public class ListPOProcurementController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            ITransaksiPOProcService svc = new TransaksiPOProcService();
            //var model = svc.GetAll();
            //var model = svc.GetAll();
            //return View("~/Views/Transaksi/Procurement/Index.cshtml", model);
            return View("~/Views/Transaksi/Procurement/Index.cshtml");
        }

        public ActionResult DetailAssignment(string poNumber)
        {
            ITransaksiPOProcService svc = new TransaksiPOProcService();
            this.ViewBag.TujuanDeliveryUnit = new SelectList(this.GetTujuanDelivery(), "Key", "Value");
            var model = svc.GetDetailAssignment(poNumber);
            if (model != null)
            {
                model.PriceFormated = model.OnTheRoadPrice == null ? "" : model.OnTheRoadPrice.Value.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
                model.DiscountFormated = model.Discount == null ? "" : model.Discount.Value.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
                model.NetFormated = model.NetPrice == null ? "" : model.NetPrice.Value.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
            }
            return View("~/Views/Transaksi/Procurement/DetailAssignment.cshtml", model);
        }

        [HttpPost]
        public ActionResult DetailAssignment(DetailAssignmentPOProcViewModel model)
        {
            try
            {
                ITransaksiPOProcService svc = new TransaksiPOProcService();
                var result = svc.UpdateAssignment(model);
                this.AddNotification("Your Data Has Been Successfully Updated. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("POProcurement DetailAssignment", ex.Message, ex.StackTrace);
                return View("~/Views/Transaksi/Procurement/Index.cshtml", model);
            }
        }

        public ActionResult DetailDelivery(string poNumber)
        {
            ITransaksiPOProcService svc = new TransaksiPOProcService();
            var model = svc.GetDetailDelivery(poNumber);
            if (model != null)
            {
                model.PriceFormated = model.OnTheRoadPrice == null ? "" : model.OnTheRoadPrice.Value.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
                model.DiscountFormated = model.Discount == null ? "" : model.Discount.Value.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
                model.NetFormated = model.NetPrice == null ? "" : model.NetPrice.Value.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
            }
            return View("~/Views/Transaksi/Procurement/DetailDelivery.cshtml", model);
        }

        [HttpPost]
        public ActionResult DetailDelivery(DetailDeliveryPOProcViewModel model)
        {
            try
            {
                ITransaksiPOProcService svc = new TransaksiPOProcService();
                var result = svc.UpdateDelivery(model);
                this.AddNotification("Your Data Has Been Successfully Updated. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("POProcurement DetailDelivery", ex.Message, ex.StackTrace);
                return View("~/Views/Transaksi/Procurement/Index.cshtml", model);
            }
        }
        public ActionResult DetailBPKB(string poNumber)
        {
            ITransaksiPOProcService svc = new TransaksiPOProcService();
            this.ViewBag.DetailProblem = new SelectList(this.GetDetailProblem(), "Key", "Value");
            var model = svc.GetDetailBPKB(poNumber);
            return View("~/Views/Transaksi/Procurement/DetailBPKB.cshtml", model);
        }

        [HttpPost]
        public ActionResult DetailBPKB(DetailBPKBPOProcViewModel model)
        {
            try
            {
                ITransaksiPOProcService svc = new TransaksiPOProcService();
                var result = svc.UpdateBPKB(model);
                this.AddNotification("Your Data Has Been Successfully Updated. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("POProcurement DetailBPKB", ex.Message, ex.StackTrace);
                return View("~/Views/Transaksi/Procurement/Index.cshtml", model);
            }
        }
        public ActionResult DetailInvoice(string poNumber)
        {
            ITransaksiPOProcService svc = new TransaksiPOProcService();
            var model = svc.GetDetailInvoice(poNumber);
            if (model != null)
            {
                model.InvoiceStatus = model.ActualInvoiceReceived != null ? "completed" : model.InvoiceNo != "" ? "in progress" : "";
                model.PriceFormated = model.OnTheRoadPrice == null ? "" : model.OnTheRoadPrice.Value.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
                model.DiscountFormated = model.Discount == null ? "" : model.Discount.Value.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
                model.NetFormated = model.NetPrice == null ? "" : model.NetPrice.Value.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
                model.DPPFormated = model.DPPByVendor == null ? "" : model.DPPByVendor.Value.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
                model.PPNFormated = model.PPNByVendor == null ? "" : model.PPNByVendor.Value.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
                model.BBNFormated = model.BBNPriceByVendor == null ? "" : model.BBNPriceByVendor.Value.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
            }
            return View("~/Views/Transaksi/Procurement/DetailInvoice.cshtml", model);
        }

        [HttpPost]
        public ActionResult DetailInvoice(DetailInvoicePOProcViewModel model)
        {
            try
            {
                ITransaksiPOProcService svc = new TransaksiPOProcService();
                var result = svc.UpdateInvoice(model);
                this.AddNotification("Your Data Has Been Successfully Updated. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("POProcurement DetailInvoice", ex.Message, ex.StackTrace);
                return View("~/Views/Transaksi/Procurement/Index.cshtml", model);
            }
        }
        public ActionResult DownloadFile(string fileName, string fileDownloadName)
        {
            string folder = "~/Uploads/";
            string type = "application/octetstream";

            var sDocument = Server.MapPath(folder + fileName.Trim());
            if (!System.IO.File.Exists(sDocument))
            {
                return HttpNotFound();
            }

            return File(sDocument, type, fileDownloadName);
        }

        public ActionResult GetSearch(string PONumber, string NoRangka)
        {
           
            ITransaksiPOProcService svc = new TransaksiPOProcService();
            //var model = svc.GetAll();
            var model = svc.GetSearch(PONumber, NoRangka);
            // return Json( new { data = model}, JsonRequestBehavior.AllowGet);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSearchx()
        {
            ITransaksiPOProcService svc = new TransaksiPOProcService();
            //var model = svc.GetAll();
            var model = svc.GetAll();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public void ReviseDetailInvoice(string PONumber, string ReasonForRevise)
        {
            ITransaksiPOProcService svc = new TransaksiPOProcService();
            var model = svc.ReviseDetailInvoice(PONumber, ReasonForRevise);
        }
        [HttpPost]
        public void ReviseDetailBPKB(string PONumber, string ReasonForRevise)
        {
            ITransaksiPOProcService svc = new TransaksiPOProcService();
            var model = svc.ReviseDetailBPKB(PONumber, ReasonForRevise);
        }

        protected Dictionary<string, string> GetDetailProblem()
        {
            var result = new Dictionary<string, string>();
            result.Add("OK", "OK");
            result.Add("Problem", "Problem");

            return result;
        }
        protected Dictionary<string, string> GetTujuanDelivery()
        {
            var result = new Dictionary<string, string>();
            result.Add("Cabang TRAC", "Cabang TRAC");
            result.Add("Customer", "Customer");
            result.Add("Bengkel Karoseri / Aksesoris", "Bengkel Karoseri / Aksesoris");
            return result;
        }
    }
}