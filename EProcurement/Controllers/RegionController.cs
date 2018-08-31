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
    public class RegionController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            IRegionService svc = new RegionService();
            var model = svc.GetAll();
            return View("~/Views/Master/Region/Index.cshtml", model);
        }
        public ActionResult Add()
        {
            this.ViewBag.Status = new SelectList(this.GetStatus(), "Key", "Value");
            return View("~/Views/Master/Region/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Add(CUSTOMREGION model)
        {
            try
            {
                IRegionService svc = new RegionService();
                var result = svc.Add(model);
                this.AddNotification("Your Data Has Been Successfully Saved. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Region Add", ex.Message, ex.StackTrace);
                this.AddNotification("ID exist", NotificationType.ERROR);
                this.ViewBag.Status = new SelectList(this.GetStatus(), "Key", "Value");
                return View("~/Views/Master/Region/Add.cshtml");
            }
        }
        public ActionResult Edit(string regionId)
        {

            // this.ViewBag.Status = new SelectList(this.GetStatus(), "Key", "Value");
            IRegionService svc = new RegionService();
            var model = svc.Getdata(regionId);
            ViewBag.Status = new SelectList(this.GetStatus(), "Key", "Value", model.STATUS);
            return View("~/Views/Master/Region/Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult Edit(string regionId, CUSTOMREGION model)
        {
            try
            {
                IRegionService svc = new RegionService();
                var result = svc.Edit(regionId, model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Region Edit", ex.Message, ex.StackTrace);
                return View("~/Views/Master/Region/Index.cshtml");
            }
        }
        protected Dictionary<bool, string> GetStatus()
        {
            var result = new Dictionary<bool, string>();
            result.Add(true, "Yes");
            result.Add(false, "No");

            return result;
        }
        public ActionResult Download(CUSTOMREGION model)
        {
            try
            {
                XLWorkbook xlWorkBook = new XLWorkbook();
                var xlWorkSheet = xlWorkBook.Worksheets.Add("Master Region");// xlWorkSheet;

                xlWorkSheet.Cell(1, 1).Value = "RegionId";
                xlWorkSheet.Cell(1, 2).Value = "RegionName";
                xlWorkSheet.Cell(1, 3).Value = "IsActive";

                IRegionService svc = new RegionService();
                var Data = svc.GetAll();
                int Row = 2;
                if (Data.Count > 0)
                {
                    for (int i = 0; i < Data.Count; i++)
                    {
                        xlWorkSheet.Cell(Row + i, 1).Value = Data[i].REGIONID;
                        xlWorkSheet.Cell(Row + i, 2).Value = Data[i].REGIONNAME;
                        xlWorkSheet.Cell(Row + i, 3).Value = Data[i].STATUS;
                    }
                    xlWorkSheet.Columns().AdjustToContents();
                    var path = Server.MapPath("..") + "\\Master-Region.xlsx";
                    xlWorkBook.SaveAs(path);
                    xlWorkBook.Dispose();
                    return File(path, "application/vnd.ms-excel", "Master-Region.xlsx");
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