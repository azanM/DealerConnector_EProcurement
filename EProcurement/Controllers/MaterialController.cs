using EProcurement.Models;
using EProcurement.Services;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using EProcurement.Extensions;
using System;
using ClosedXML;
using ClosedXML.Excel;

namespace EProcurement.Controllers
{
    public class MaterialController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            IMaterialService svc = new MaterialService();
            var model = svc.GetAll();
            return View("~/Views/Master/Material/Index.cshtml", model);
        }
        public ActionResult Add()
        {
            this.ViewBag.purchaseId = GetPurchaseID();
            return View("~/Views/Master/Material/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Add(string materialNumber,string MATERIALIDVENDOR,string MATERIALTYPE,string MATERIALGROUP,string OLDMATERIAL,string BOMMATERIAL,string BRAND,string MODEL,
            string GARDAN,string YEAR,string PURCHASEGROUPID,string DESCRIPTION, string PRICELIST)
        {
            try
            {
                MSMATERIAL model = new MSMATERIAL();
                model.MATERIALNUMBER = materialNumber;
                model.MATERIALIDVENDOR = MATERIALIDVENDOR;
                model.MATERIALGROUP = MATERIALGROUP;
                model.OLDMATERIAL = OLDMATERIAL;
                model.BRAND = BRAND;
                model.MODEL = MODEL;
                model.GARDAN = GARDAN;
                model.YEAR = YEAR;
                model.PURCHASEGROUPID = PURCHASEGROUPID;
                model.DESCRIPTION = DESCRIPTION;
                model.PRICELIST = PRICELIST == "" ? 0 : double.Parse(PRICELIST);
                IMaterialService svc = new MaterialService();
                var result = svc.Add(model);
                this.AddNotification("Your Data Has Been Successfully Saved. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Material Add", ex.Message, ex.StackTrace);
                this.AddNotification("ID exist", NotificationType.ERROR);
                this.ViewBag.purchaseId = GetPurchaseID();
                return View("~/Views/Master/Material/Add.cshtml");
                //throw;
            }
        }

        public ActionResult Edit(string materialNumber, string MDL)
        {
            this.ViewBag.purchaseId = GetPurchaseID();
            IMaterialService svc = new MaterialService();
            var model = svc.Getdata(materialNumber);
            return View("~/Views/Master/Material/Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult Edit(string materialNumber, string MATERIALIDVENDOR, string MATERIALTYPE, string MATERIALGROUP, string OLDMATERIAL, string BOMMATERIAL, string BRAND, string MODEL,
            string GARDAN, string YEAR, string PURCHASEGROUPID, string DESCRIPTION, string PRICELIST)
        {
            try
            {
                MSMATERIAL model = new MSMATERIAL();
                model.MATERIALNUMBER = materialNumber;
                model.MATERIALIDVENDOR = MATERIALIDVENDOR;
                model.MATERIALGROUP = MATERIALGROUP;
                model.OLDMATERIAL = OLDMATERIAL;
                model.BRAND = BRAND;
                model.MODEL = MODEL;
                model.GARDAN = GARDAN;
                model.YEAR = YEAR;
                model.PURCHASEGROUPID = PURCHASEGROUPID;
                model.DESCRIPTION = DESCRIPTION;
                model.PRICELIST = PRICELIST == "" ? 0 : double.Parse(PRICELIST);
                IMaterialService svc = new MaterialService();
                //model.MODEL = MDL;
                var result = svc.Edit(materialNumber, model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Material Edit", ex.Message, ex.StackTrace);
                return View("~/Views/Master/Material/Edit.cshtml");
            }
        }

        protected static SelectList GetPurchaseID()
        {
            eprocdbDataContext db = new eprocdbDataContext();

            var data = (from a in db.MSMATERIALs
                       select new
                       {
                           PURCHASEGROUPID = a.PURCHASEGROUPID
                       }).Distinct();

            SelectList selectList = new SelectList(data, "PURCHASEGROUPID", "PURCHASEGROUPID");

            return selectList;
        }
        public ActionResult Download(CUSTOMCOMPANY model)
        {
            try
            {
                XLWorkbook xlWorkBook = new XLWorkbook();
                var xlWorkSheet = xlWorkBook.Worksheets.Add("Master Material");// xlWorkSheet;

                //xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
                //xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Cell(1, 1).Value = "PLANTID";
                xlWorkSheet.Cell(1, 1).Value = "MATERIALNUMBER";
                xlWorkSheet.Cell(1, 2).Value = "DESCRIPTION";
                xlWorkSheet.Cell(1, 3).Value = "MATERIALTYPE";
                xlWorkSheet.Cell(1, 4).Value = "MATERIALGROUP";
                xlWorkSheet.Cell(1, 5).Value = "OLDMATERIAL";
                xlWorkSheet.Cell(1, 6).Value = "BOMMATERIAL";
                xlWorkSheet.Cell(1, 7).Value = "BRAND";
                xlWorkSheet.Cell(1, 8).Value = "MODEL";
                xlWorkSheet.Cell(1, 9).Value = "GARDAN";
                xlWorkSheet.Cell(1, 10).Value = "YEAR";
                xlWorkSheet.Cell(1, 11).Value = "PURCHASEGROUPID";
                xlWorkSheet.Cell(1, 12).Value = "MATERIALIDVENDOR";
                xlWorkSheet.Cell(1, 13).Value = "PRICELIST";

                IMaterialService svc = new MaterialService();
                var Data = svc.GetAll();
                int Row = 2;
                if (Data.Count > 0)
                {
                    for (int i = 0; i < Data.Count; i++)
                    {
                        xlWorkSheet.Cell(Row + i, 1).Value = Data[i].MATERIALNUMBER;
                        xlWorkSheet.Cell(Row + i, 2).Value = Data[i].DESCRIPTION;
                        xlWorkSheet.Cell(Row + i, 3).Value = Data[i].MATERIALTYPE;
                        xlWorkSheet.Cell(Row + i, 4).Value = Data[i].MATERIALGROUP;
                        xlWorkSheet.Cell(Row + i, 5).Value = Data[i].OLDMATERIAL;
                        xlWorkSheet.Cell(Row + i, 6).Value = Data[i].BOMMATERIAL;
                        xlWorkSheet.Cell(Row + i, 7).Value = Data[i].BRAND;
                        xlWorkSheet.Cell(Row + i, 8).Value = Data[i].MODEL;
                        xlWorkSheet.Cell(Row + i, 9).Value = Data[i].GARDAN;
                        xlWorkSheet.Cell(Row + i, 10).Value = Data[i].YEAR;
                        xlWorkSheet.Cell(Row + i, 11).Value = Data[i].PURCHASEGROUPID;
                        xlWorkSheet.Cell(Row + i, 12).Value = Data[i].MATERIALIDVENDOR;
                        xlWorkSheet.Cell(Row + i, 13).Value = Data[i].PRICELIST;
                    }
                    xlWorkSheet.Columns().AdjustToContents();
                    var path = Server.MapPath("..") + "\\Master-Material.xlsx";
                    xlWorkBook.SaveAs(path);
                    xlWorkBook.Dispose();
                    return File(path, "application/vnd.ms-excel", "Master-Material.xlsx");
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