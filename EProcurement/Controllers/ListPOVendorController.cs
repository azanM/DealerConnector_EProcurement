using System.Web.Mvc;
using EProcurement.Services;
using EProcurement.Models.ViewModel.Transaksi;
using EProcurement.Extensions;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using System;

namespace EProcurement.Controllers
{
    public class ListPOVendorController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            ITransaksiPOVendorService svc = new TransaksiPOVendorService();
            //string VendorID = System.Web.HttpContext.Current.Session["VendorID"] == null ? "" : System.Web.HttpContext.Current.Session["VendorID"].ToString();
            //var model = svc.GetAll(VendorID);
            return View("~/Views/Transaksi/Vendor/Index.cshtml");
            //return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string PONUmber, string NoRangka)
        {
            ITransaksiPOVendorService svc = new TransaksiPOVendorService();
            string VendorID = System.Web.HttpContext.Current.Session["VendorID"] == null ? "" : System.Web.HttpContext.Current.Session["VendorID"].ToString();
            var model = svc.GetSearch(VendorID, PONUmber, NoRangka);
            // return View("~/Views/Transaksi/Vendor/Index.cshtml", model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DetailAssignment(string poNumber)
        {
            ITransaksiPOVendorService svc = new TransaksiPOVendorService();
            var model = svc.GetDetailAssignment(poNumber);
            if (model != null)
            {
                //model.strPromiseDeliveryDate = model.PromiseDeliveryDate == null ? "" : model.PromiseDeliveryDate.Value.ToString("dd/MM/yyyy");
                //model.strPODate = model.PODate == null ? "" : model.PODate.Value.ToString("dd/MM/yyyy");
                //model.strOTR = model.OnTheRoadPrice.Value.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
                //model.strNetPrice = model.NetPrice.Value.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
                //model.strDiscount = model.NetPrice.Value.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
            }
            return View("~/Views/Transaksi/Vendor/DetailAssignment.cshtml", model);
        }

        [HttpPost]
        public ActionResult DetailAssignment(DetailAssignmentPOVendorViewModel model)
        {
            try
            {
                ITransaksiPOVendorService svc = new TransaksiPOVendorService();
                //if (model.strPODate != "" && model.strPODate != null)
                //{
                //    model.PODate = DateTime.Parse(model.strPODate);
                //}
                //if (model.strPromiseDeliveryDate != "" && model.strPromiseDeliveryDate != null)
                //{
                //    model.PromiseDeliveryDate = DateTime.Parse(model.strPromiseDeliveryDate);
                //}
                var result = svc.SubmitAssignment(model);
                this.AddNotification("Your Data Has Been Successfully Updated. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("POVendor DetailAssignment", ex.Message, ex.StackTrace);
                return View("~/Views/Transaksi/Vendor/Index.cshtml", model);
            }
        }
        public ActionResult DetailDelivery(string poNumber)
        {
            ITransaksiPOVendorService svc = new TransaksiPOVendorService();
            var model = svc.GetDetailDelivery(poNumber);
            if (model != null)
            {
                //var PoConverDate = model.PODate.Value.ToString("dd/MM/yyyy");
                //model.PoDateString = PoConverDate;
                //if (model.DateFinishCarrosserieAccessories != null)
                //{
                //    model.strDateFinishCarrosserieAccessories = model.DateFinishCarrosserieAccessories.Value.ToString("dd/MM/yyyy");
                //}
                //if (model.TanggalBSTB != null)
                //{
                //    model.strTanggalBSTB = model.TanggalBSTB.Value.ToString("dd/MM/yyyy");
                //}
                //if (model.FakturDODate != null)
                //{
                //    model.strDoDate = model.FakturDODate.Value.ToString("dd/MM/yyyy");
                //}
                //if (model.DateEntryCarrosserieAccessories != null)
                //{
                //    model.strDateEntryCarrosserieAccessories = model.DateEntryCarrosserieAccessories.Value.ToString("dd/MM/yyyy");
                //}
                //if (model.STNKDateByVendor != null)
                //{
                //    model.strSTNKDateByVendor = model.STNKDateByVendor.Value.ToString("dd/MM/yyyy");
                //}else
                //{
                //    model.strSTNKDateByVendor = null;
                //}
            }

            return View("~/Views/Transaksi/Vendor/DetailDelivery.cshtml", model);
        }

        [HttpPost]
        public ActionResult DetailDelivery(DetailDeliveryPOVendorViewModel model, HttpPostedFileBase postedFile)
        {
            try
            {
                if (postedFile != null)
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    ITransaksiPOVendorService res = new TransaksiPOVendorService();
                    var re = res.InsertUploadFile(model.PONumber, "Scan Copy PO", postedFile.FileName);
                    postedFile.SaveAs(path + Path.GetFileName(model.PONumber.Trim() + "_" + postedFile.FileName.Trim()));
                    this.AddNotification("Your Data Has Been Successfully Updated. ", NotificationType.SUCCESS);
                    return RedirectToAction("Index");
                }
                ITransaksiPOVendorService svc = new TransaksiPOVendorService();
                var result = svc.UpdateDelivery(model);
                this.AddNotification("Your Data Has Been Successfully Updated. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("POVendor DetailDelivery", ex.Message, ex.StackTrace);
                return View("~/Views/Transaksi/Vendor/Index.cshtml", model);
            }
        }
        public ActionResult DetailBPKB(string poNumber)
        {
            ITransaksiPOVendorService svc = new TransaksiPOVendorService();
            var model = svc.GetDetailBPKB(poNumber);
            if (model != null)
            {
                // model.strClearingDate = model.ClearingDate == null ? "" : model.ClearingDate.Value.ToString("dd/MM/yyyy");
                //model.strActualReceivedUnit = model.ActualReceivedUnit == null ? "" : model.ActualReceivedUnit.Value.ToString("dd/MM/yyyy");
                //model.strActualReceivedBPKBHOBack = model.ActualReceivedBPKBHOBack == null ? "" : model.ActualReceivedBPKBHOBack.Value.ToString("dd/MM/yyyy");
                //model.strActualDeliveredBPKBDateCabang = model.ActualDeliveredBPKBDateCabang == null ? "" : model.ActualDeliveredBPKBDateCabang.Value.ToString("dd/MM/yyyy");
                //model.strPoDate = model.PODate == null ? "" : model.PODate.Value.ToString("dd/MM/yyyy");
                //model.strActualDeliveryBPKBDateHO = model.ActualDeliveryBPKBDateHO == null ? "" : model.ActualDeliveryBPKBDateHO.Value.ToString("dd/MM/yyyy");
                //model.strTanggalSertifikat = model.TanggalSertifikat == null ? "" : model.TanggalSertifikat.Value.ToString("dd/MM/yyyy");
                //model.strTanggalFormulirA = model.TanggalFormulirA == null ? "" : model.TanggalFormulirA.Value.ToString("dd/MM/yyyy");
                //model.strTanggalSuratUbahBentuk = model.TanggalSuratUbahBentuk == null ? "" : model.TanggalSuratUbahBentuk.Value.ToString("dd/MM/yyyy");
                //model.strTanggalSuratUbahWarna = model.TanggalSuratUbahWarna == null ? "" : model.TanggalSuratUbahWarna.Value.ToString("dd/MM/yyyy");
                //model.strActualDeliveredDateBPKBToFinance = model.ActualDeliveredDateBPKBToFinance == null ? "" : model.ActualDeliveredDateBPKBToFinance.Value.ToString("dd/MM/yyyy");
                //model.strBPKBDateSentBack = model.BPKBDateSentBack == null ? "" : model.BPKBDateSentBack.Value.ToString("dd/MM/yyyy");
            }
            return View("~/Views/Transaksi/Vendor/DetailBPKB.cshtml", model);
        }

        [HttpPost]
        public ActionResult DetailBPKB(DetailBPKBPOVendorViewModel model, HttpPostedFileBase postedFile)
        {
            try
            {
                if (postedFile != null)
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                    this.AddNotification("Your Data Has Been Successfully Updated. ", NotificationType.SUCCESS);
                }
                ITransaksiPOVendorService svc = new TransaksiPOVendorService();
                //if (model.strClearingDate != "" && model.strClearingDate != null)
                //{
                //    model.ClearingDate = DateTime.Parse(model.strClearingDate);
                //}else
                //{
                //    model.ClearingDate = null;
                //}
                //if (model.strActualReceivedUnit != "" && model.strActualReceivedUnit != null)
                //{
                //    model.ActualReceivedUnit = DateTime.Parse(model.strActualReceivedUnit);
                //}else
                //{
                //    model.ActualReceivedUnit = null;
                //}
                //if (model.strActualReceivedBPKBHOBack != "" && model.strActualReceivedBPKBHOBack != null)
                //{
                //    model.ActualReceivedBPKBHOBack = DateTime.Parse(model.strActualReceivedBPKBHOBack);
                //}else
                //{
                //    model.ActualReceivedBPKBHOBack = null;
                //}
                //if (model.strActualDeliveredBPKBDateCabang != "" && model.strActualDeliveredBPKBDateCabang != null)
                //{
                //    model.ActualDeliveredBPKBDateCabang = DateTime.Parse(model.strActualDeliveredBPKBDateCabang);
                //}
                //else
                //{
                //    model.ActualDeliveredBPKBDateCabang = null;
                //}
                //if (model.strPoDate != "" && model.strPoDate != null)
                //{
                //    model.PODate = DateTime.Parse(model.strPoDate);
                //}else
                //{
                //    model.PODate = null;
                //}
                //if (model.strActualDeliveryBPKBDateHO != "" && model.strActualDeliveryBPKBDateHO != null)
                //{
                //    model.ActualDeliveryBPKBDateHO = DateTime.Parse(model.strActualDeliveryBPKBDateHO);
                //}else
                //{
                //    model.ActualDeliveryBPKBDateHO = null;
                //}
                //if (model.strTanggalSertifikat != "" && model.strTanggalSertifikat != null)
                //{
                //    model.TanggalSertifikat = DateTime.Parse(model.strTanggalSertifikat);
                //}else
                //{
                //    model.TanggalSertifikat = null;
                //}
                //if (model.strTanggalFormulirA != "" && model.strTanggalFormulirA != null)
                //{
                //    model.TanggalFormulirA = DateTime.Parse(model.strTanggalFormulirA);
                //}else
                //{
                //    model.TanggalFormulirA = null;
                //}
                //if (model.strTanggalSuratUbahBentuk != "" && model.strTanggalSuratUbahBentuk != null)
                //{
                //    model.TanggalSuratUbahBentuk = DateTime.Parse(model.strTanggalSuratUbahBentuk);
                //}else
                //{
                //    model.TanggalSuratUbahBentuk = null;
                //}
                //if (model.strTanggalSuratUbahWarna != "" && model.strTanggalSuratUbahWarna != null)
                //{
                //    model.TanggalSuratUbahWarna = DateTime.Parse(model.strTanggalSuratUbahWarna);
                //}else
                //{
                //    model.TanggalSuratUbahWarna = null;
                //}
                //if (model.strActualDeliveredDateBPKBToFinance != "" && model.strActualDeliveredDateBPKBToFinance != null)
                //{
                //    model.ActualDeliveredDateBPKBToFinance = DateTime.Parse(model.strActualDeliveredDateBPKBToFinance);
                //}
                //else
                //{
                //    model.ActualDeliveredDateBPKBToFinance = null;
                //}
                //if (model.strBPKBDateSentBack != "" && model.strBPKBDateSentBack != null)
                //{
                //    model.BPKBDateSentBack = DateTime.Parse(model.strBPKBDateSentBack);
                //}else
                //{
                //    model.BPKBDateSentBack = null;
                //}
                var result = svc.UpdateBPKB(model);
                this.AddNotification("Your Data Has Been Successfully Updated. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("POVendor DetailBPKB", ex.Message, ex.StackTrace);
                return View("~/Views/Transaksi/Vendor/Index.cshtml", model);
            }
        }
        public ActionResult DetailInvoice(string poNumber)
        {
            ITransaksiPOVendorService svc = new TransaksiPOVendorService();
            this.ViewBag.StatusDeliveryUnit = new SelectList(this.GetStatusDelivery(), "Key", "Value");
            var model = svc.GetDetailInvoice(poNumber);
            model.StatusInvoice = model.ActualInvoiceReceived != null ? "completed" : model.InvoiceNo != "" ? "in progress" : "";
            //model.strInvoiceDate = model.InvoiceDate == null ? "" : model.InvoiceDate.Value.ToString("dd/MM/yyyy");
            //model.strPoDate = model.PODate == null ? "" : model.PODate.Value.ToString("dd/MM/yyyy");
            //model.strPromiseDeliveryDate = model.PromiseDeliveryDate == null ? "" : model.PromiseDeliveryDate.Value.ToString("dd/MM/yyyy");
            //model.strExpectedDateDelivered = model.ExpectedDateDelivered == null ? "" : model.ExpectedDateDelivered.Value.ToString("dd/MM/yyyy");
            // model.strPayPlan = model.PayPlan == null ? "" : model.PayPlan.Value.ToString("dd/MM/yyyy");
            //model.strClearingDate = model.ClearingDate == null ? "" : model.ClearingDate.Value.ToString("dd/MM/yyyy");
            return View("~/Views/Transaksi/Vendor/DetailInvoice.cshtml", model);
        }

        [HttpPost]
        public ActionResult DetailInvoice(DetailInvoicePOVendorViewModel model, HttpPostedFileBase postedFile, IEnumerable<HttpPostedFileBase> txtFileName)
        {
            try
            {
                if (txtFileName != null)
                {
                    foreach (var x in txtFileName)
                    {
                        string path = Server.MapPath("~/Uploads/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        x.SaveAs(path + Path.GetFileName(x.FileName));
                    }
                }
                if (postedFile != null)
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                    this.AddNotification("Your Data Has Been Successfully Updated. ", NotificationType.SUCCESS);
                }
                ITransaksiPOVendorService svc = new TransaksiPOVendorService();
                //if (model.strInvoiceDate != "" && model.strInvoiceDate != null)
                //{
                //    model.InvoiceDate = DateTime.Parse(model.strInvoiceDate);
                //}
                //if (model.strPoDate != "" && model.strPoDate != null)
                //{
                //    model.PODate = DateTime.Parse(model.strPoDate);
                //}
                //if (model.strPromiseDeliveryDate != "" && model.strPromiseDeliveryDate != null)
                //{
                //    model.PromiseDeliveryDate = DateTime.Parse(model.strPromiseDeliveryDate);
                //}
                //if (model.strExpectedDateDelivered != "" && model.strExpectedDateDelivered != null)
                //{
                //    model.ExpectedDateDelivered = DateTime.Parse(model.strExpectedDateDelivered);
                //}
                //if (model.strPayPlan != "" && model.strPayPlan != null)
                //{
                //    model.PayPlan = DateTime.Parse(model.strPayPlan);
                //}
                //if (model.strClearingDate != "" && model.strClearingDate != null)
                //{
                //    model.ClearingDate = DateTime.Parse(model.strClearingDate);
                //}
                var result = svc.UpdateInvoice(model);
                this.AddNotification("Your Data Has Been Successfully Updated. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("POVendor DetailInvoice", ex.Message, ex.StackTrace);
                return View("~/Views/Transaksi/Vendor/Index.cshtml", model);
            }
        }
        public ActionResult DetailBSTK(string poNumber)
        {
            ITransaksiPOVendorService svc = new TransaksiPOVendorService();
            var model = svc.GetDetailBSTK(poNumber);
            return View("~/Views/Transaksi/Vendor/DetailBSTK.cshtml", model);
        }
        public ActionResult DownloadFile(string fileName, string fileDownloadName)
        {
            string folder = "~/Uploads/";
            string type = "application/octetstream";

            var sDocument = Server.MapPath(folder + fileName);
            if (!System.IO.File.Exists(sDocument))
            {
                return HttpNotFound();
            }

            return File(sDocument, type, fileDownloadName);
        }
        protected Dictionary<string, string> GetStatusDelivery()
        {
            var result = new Dictionary<string, string>();
            result.Add("OK", "OK");
            result.Add("Problem", "Problem");

            return result;
        }
    }
}