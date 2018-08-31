using EProcurement.Models.ViewModel.Reporting;
using EProcurement.Services.Implementation;
using EProcurement.Services.Interface;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ClosedXML;
using ClosedXML.Excel;

namespace EProcurement.Controllers
{
    public class ReportingVendorController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            try
            {
                IReportPOVendorService svc = new ReportPOVendorService();
                var model = svc.GetAll();
                return View("~/Views/Reporting/Vendor/Index.cshtml", model);
            }
            catch (Exception ex)
            {
                List<ReportPOVendorViewModel> model = new List<ReportPOVendorViewModel>();
                general.AddLogError("Report Vendor", ex.Message, ex.StackTrace);
                return View("~/Views/Reporting/Vendor/Index.cshtml", model);
            }
        }
        public ActionResult Download(ReportPOVendorViewModel model)
        {
            try
            {
                XLWorkbook xlWorkBook = new XLWorkbook();
                var xlWorkSheet = xlWorkBook.Worksheets.Add("Report Vendor");// xlWorkSheet;

                //xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
                //xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Cell(1, 1).Value = "OFFICER";
                xlWorkSheet.Cell(1, 2).Value = "PLANT";
                xlWorkSheet.Cell(1, 3).Value = "WARNA";
                xlWorkSheet.Cell(1, 4).Value = "TYPEUNIT";
                xlWorkSheet.Cell(1, 5).Value = "PONUMBER";
                xlWorkSheet.Cell(1, 6).Value = "TGLPO";
                xlWorkSheet.Cell(1, 7).Value = "POSTATUS";
                xlWorkSheet.Cell(1, 8).Value = "NAMAPEMILIKVENDOR";
                xlWorkSheet.Cell(1, 9).Value = "TGLPROMISEDELIVERYBPKB";
                xlWorkSheet.Cell(1, 10).Value = "AGINGOVERDUEOUTSTANDINGUNITBPKB";
                xlWorkSheet.Cell(1, 11).Value = "AGINGOUTSTANDINGUNITBPKB";
                xlWorkSheet.Cell(1, 12).Value = "AGINGOVERDUEOUTSTANDINGUNIT";
                xlWorkSheet.Cell(1, 13).Value = "AGINGOUTSTANDINGUNIT";
                xlWorkSheet.Cell(1, 14).Value = "REMARKSDETAILPROBLEM";
                xlWorkSheet.Cell(1, 15).Value = "DETAILPROBLEM";
                xlWorkSheet.Cell(1, 16).Value = "STATUSBPKB";
                xlWorkSheet.Cell(1, 17).Value = "POSISIBPKB";
                xlWorkSheet.Cell(1, 18).Value = "KETBPKB";
                xlWorkSheet.Cell(1, 19).Value = "ACTUALRECEIVEDBPKBCABANG";

                xlWorkSheet.Cell(1, 20).Value = "NOBPKB";
                xlWorkSheet.Cell(1, 21).Value = "NAMAPEMILIK";
                xlWorkSheet.Cell(1, 22).Value = "INVOICESTATUS";
                xlWorkSheet.Cell(1, 23).Value = "WARNA";
                xlWorkSheet.Cell(1, 24).Value = "TGLPEMBAYARAN";
                xlWorkSheet.Cell(1, 25).Value = "ESTIMATEDPAYMENTDATE";
                xlWorkSheet.Cell(1, 26).Value = "ACTUALRECEIVEDINVOICE";

                xlWorkSheet.Cell(1, 27).Value = "NOAP";
                xlWorkSheet.Cell(1, 28).Value = "NOFAKTURPAJAK";
                xlWorkSheet.Cell(1, 29).Value = "HARGAPPNUNITVENDOR";
                xlWorkSheet.Cell(1, 30).Value = "HARGADPPVENDOR";
                xlWorkSheet.Cell(1, 31).Value = "HARGABBNVENDOR";
                xlWorkSheet.Cell(1, 32).Value = "HARGABBNUNIT";
                xlWorkSheet.Cell(1, 33).Value = "HARGADPP";

                xlWorkSheet.Cell(1, 34).Value = "NETPRICE";
                xlWorkSheet.Cell(1, 35).Value = "DISC";
                xlWorkSheet.Cell(1, 36).Value = "OTRPRICE";
                xlWorkSheet.Cell(1, 37).Value = "INVOICENUMBER";
                xlWorkSheet.Cell(1, 38).Value = "INVOICEDATE";
                xlWorkSheet.Cell(1, 39).Value = "TGLSELESAIKAROSERI";

                xlWorkSheet.Cell(1, 40).Value = "TGLMASUKKAROSERI";
                xlWorkSheet.Cell(1, 41).Value = "KETKAROSERI";
                xlWorkSheet.Cell(1, 42).Value = "TGLSTNKVENDOR";
                xlWorkSheet.Cell(1, 43).Value = "NOENGINEVENDOR";
                xlWorkSheet.Cell(1, 44).Value = "NOCHASISVENDOR";
                xlWorkSheet.Cell(1, 45).Value = "NOPOLISIVENDOR";
                xlWorkSheet.Cell(1, 46).Value = "TGLSTNK";

                xlWorkSheet.Cell(1, 47).Value = "NOENGINE";
                xlWorkSheet.Cell(1, 48).Value = "NOCHASIS";
                xlWorkSheet.Cell(1, 49).Value = "NOPOLISI";
                xlWorkSheet.Cell(1, 50).Value = "VARIANVENDOR";
                xlWorkSheet.Cell(1, 51).Value = "MERKVENDOR";
                xlWorkSheet.Cell(1, 52).Value = "VARIAN";

                xlWorkSheet.Cell(1, 53).Value = "MERK";
                xlWorkSheet.Cell(1, 54).Value = "YEAR";
                xlWorkSheet.Cell(1, 55).Value = "STATUSBELI";
                xlWorkSheet.Cell(1, 56).Value = "BBN";

                IReportPOVendorService svc = new ReportPOVendorService();
                var Data = svc.GetAll();
                int Row = 2;
                if (Data.Count > 0)
                {
                    for (int i = 0; i < Data.Count; i++)
                    {
                        xlWorkSheet.Cell(Row + i, 1).Value = Data[i].Officer;
                        xlWorkSheet.Cell(Row + i, 2).Value = Data[i].Plant;
                        xlWorkSheet.Cell(Row + i, 3).Value = Data[i].Warna;
                        xlWorkSheet.Cell(Row + i, 4).Value = Data[i].TypeUnit;
                        xlWorkSheet.Cell(Row + i, 5).Value = Data[i].PONumber;
                        xlWorkSheet.Cell(Row + i, 6).Value = Data[i].TglPO;
                        xlWorkSheet.Cell(Row + i, 7).Value = Data[i].POStatus;
                        xlWorkSheet.Cell(Row + i, 8).Value = Data[i].NamaPemilikVendor;
                        xlWorkSheet.Cell(Row + i, 9).Value = Data[i].TglPromiseDeliveryBPKB;
                        xlWorkSheet.Cell(Row + i, 10).Value = Data[i].AgingOverdueOutstandingUnitBPKB;
                        xlWorkSheet.Cell(Row + i, 11).Value = Data[i].AgingOutstandingUnitBPKB;
                        xlWorkSheet.Cell(Row + i, 12).Value = Data[i].AgingOverdueOutstandongUnit;
                        xlWorkSheet.Cell(Row + i, 13).Value = Data[i].AgingOverdueOutstandongUnit;


                        xlWorkSheet.Cell(Row + i, 14).Value = Data[i].RemarksDetailProblem;
                        xlWorkSheet.Cell(Row + i, 15).Value = Data[i].DetailProblem;
                        xlWorkSheet.Cell(Row + i, 16).Value = Data[i].StatusBPKB;
                        xlWorkSheet.Cell(Row + i, 17).Value = Data[i].PosisiBPKB;
                        xlWorkSheet.Cell(Row + i, 18).Value = Data[i].KeteranganBPKB;
                        xlWorkSheet.Cell(Row + i, 19).Value = Data[i].ActualReceivedBPKBCabang;
                        xlWorkSheet.Cell(Row + i, 20).Value = Data[i].NoBPKB;
                        xlWorkSheet.Cell(Row + i, 21).Value = Data[i].NamaPemilik;
                        xlWorkSheet.Cell(Row + i, 22).Value = Data[i].InvoiceStatus;
                        xlWorkSheet.Cell(Row + i, 23).Value = Data[i].Warna;
                        xlWorkSheet.Cell(Row + i, 24).Value = Data[i].TglPembayaran;
                        xlWorkSheet.Cell(Row + i, 25).Value = Data[i].EstimatedPaymentDate;
                        xlWorkSheet.Cell(Row + i, 26).Value = Data[i].ActualReceivedInvoice;

                        xlWorkSheet.Cell(Row + i, 27).Value = Data[i].NoAP;
                        xlWorkSheet.Cell(Row + i, 28).Value = Data[i].NoFakturPajak;
                        xlWorkSheet.Cell(Row + i, 29).Value = Data[i].HargaPPNUnitVendor;
                        xlWorkSheet.Cell(Row + i, 30).Value = Data[i].HargaDPPVendor;
                        xlWorkSheet.Cell(Row + i, 31).Value = Data[i].HargaBBNVendor;
                        xlWorkSheet.Cell(Row + i, 32).Value = Data[i].HargaPPNUnit;
                        xlWorkSheet.Cell(Row + i, 33).Value = Data[i].HargaDPP;
                        xlWorkSheet.Cell(Row + i, 34).Value = Data[i].NetPrice;
                        xlWorkSheet.Cell(Row + i, 35).Value = Data[i].Disc;
                        xlWorkSheet.Cell(Row + i, 36).Value = Data[i].OTRPrice;
                        xlWorkSheet.Cell(Row + i, 37).Value = Data[i].InvoiceNumber;
                        xlWorkSheet.Cell(Row + i, 38).Value = Data[i].InvoiceDate;
                        xlWorkSheet.Cell(Row + i, 39).Value = Data[i].TglSelesaiKaroseri;

                        xlWorkSheet.Cell(Row + i, 40).Value = Data[i].TglMasukKaroseri;
                        xlWorkSheet.Cell(Row + i, 41).Value = Data[i].KeteranganKaroseri;
                        xlWorkSheet.Cell(Row + i, 42).Value = Data[i].TglSTNKVendor;
                        xlWorkSheet.Cell(Row + i, 43).Value = Data[i].NoEngineVendor;
                        xlWorkSheet.Cell(Row + i, 44).Value = Data[i].NoChasisVendor;
                        xlWorkSheet.Cell(Row + i, 45).Value = Data[i].NoPolisiVendor;
                        xlWorkSheet.Cell(Row + i, 46).Value = Data[i].TglSTNK;
                        xlWorkSheet.Cell(Row + i, 47).Value = Data[i].NoEngine;
                        xlWorkSheet.Cell(Row + i, 48).Value = Data[i].NoChasis;
                        xlWorkSheet.Cell(Row + i, 49).Value = Data[i].NoPolisi;
                        xlWorkSheet.Cell(Row + i, 50).Value = Data[i].VarianVendor;
                        xlWorkSheet.Cell(Row + i, 51).Value = Data[i].MerkVendor;
                        xlWorkSheet.Cell(Row + i, 52).Value = Data[i].Varian;
                        xlWorkSheet.Cell(Row + i, 53).Value = Data[i].Merk;
                        xlWorkSheet.Cell(Row + i, 54).Value = Data[i].YEAR;
                        xlWorkSheet.Cell(Row + i, 55).Value = Data[i].StatusBeli;
                        xlWorkSheet.Cell(Row + i, 56).Value = Data[i].BBN;
                    }
                    xlWorkSheet.Columns().AdjustToContents();
                    var path = Server.MapPath("..") + "\\Report-Vendor.xlsx";
                    xlWorkBook.SaveAs(path);
                    xlWorkBook.Dispose();
                    return File(path, "application/vnd.ms-excel", "Report-Vendor.xlsx");
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