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
    public class CityController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            ICityService svc = new CityService();
            var model = svc.GetAll();
            return View("~/Views/Master/City/Index.cshtml", model);
        }
        public ActionResult Add()
        {
            this.ViewBag.Status = new SelectList(this.GetStatus(), "Key", "Value");
            return View("~/Views/Master/City/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Add(CUSTOMCITY model)
        {
            try
            {
                ICityService svc = new CityService();
                var result = svc.Add(model);
                this.AddNotification("Your Data Has Been Successfully Saved. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("City Add", ex.Message, ex.StackTrace);
                this.AddNotification("ID exist", NotificationType.ERROR);
                this.ViewBag.Status = new SelectList(this.GetStatus(), "Key", "Value");
                return View("~/Views/Master/City/Add.cshtml");
            }
        }
        public ActionResult Edit(string cityId)
        {
            this.ViewBag.Status = new SelectList(this.GetStatus(), "Key", "Value");
            ICityService svc = new CityService();
            var model = svc.Getdata(cityId);
            return View("~/Views/Master/City/Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult Edit(string cityId, CUSTOMCITY model)
        {
            try
            {
                ICityService svc = new CityService();
                var result = svc.Edit(cityId, model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("City Edit", ex.Message, ex.StackTrace);
                return View("~/Views/Master/City/Index.cshtml");
            }
        }
        protected Dictionary<bool, string> GetStatus()
        {
            var result = new Dictionary<bool, string>();
            result.Add(true, "Yes");
            result.Add(false, "No");

            return result;
        }
        public ActionResult Download(CUSTOMCITY model)
        {
            try
            {
                XLWorkbook xlWorkBook = new XLWorkbook();
                var xlWorkSheet = xlWorkBook.Worksheets.Add("Master City");// xlWorkSheet;

                xlWorkSheet.Cell(1, 1).Value = "CityId";
                xlWorkSheet.Cell(1, 2).Value = "CityName";

                ICityService svc = new CityService();
                var Data = svc.GetAll();
                int Row = 2;
                if (Data.Count > 0)
                {
                    for (int i = 0; i < Data.Count; i++)
                    {
                        xlWorkSheet.Cell(Row + i, 1).Value = Data[i].CITYID;
                        xlWorkSheet.Cell(Row + i, 2).Value = Data[i].CITYNAME;

                    }
                    xlWorkSheet.Columns().AdjustToContents();
                    var path = Server.MapPath("..") + "\\Master-City.xlsx";
                    xlWorkBook.SaveAs(path);
                    xlWorkBook.Dispose();
                    return File(path, "application/vnd.ms-excel", "Master-City.xlsx");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Akses Group", ex.Message, ex.StackTrace);
                return View("~/Views/Master/City/Index.cshtml");
            }

        }
    }
}