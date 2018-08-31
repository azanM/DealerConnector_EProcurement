using System.Web.Mvc;
using EProcurement.Models;
using EProcurement.Services;
using EProcurement.Extensions;
using System;
using ClosedXML;
using ClosedXML.Excel;

namespace EProcurement.Controllers
{
    public class ModulController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            IModulService objIMS = new ModulService();
            var model = objIMS.GetAll();
            return View("~/Views/Master/Modul/Index.cshtml", model);
        }

        public ActionResult Add()
        {
            return View("~/Views/Master/Modul/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Add(Master_Menu model)
        {
            try
            {
                // TODO: Add insert logic here

                IModulService objIMS = new ModulService();
                var result = objIMS.Add(model);
                this.AddNotification("Your Data Has Been Successfully Saved. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Modul Add", ex.Message, ex.StackTrace);
                this.AddNotification("ID exist", NotificationType.ERROR);
                return View("~/Views/Master/Modul/Add.cshtml");
            }
        }

        public ActionResult Edit(string menuId)
        {
            IModulService objIMS = new ModulService();
            var model = objIMS.Getdata(menuId);
            return View("~/Views/Master/Modul/Edit.cshtml", model);
        }
        public ActionResult Delete(string menuId)
        {
            IModulService svc = new ModulService();
            var model = svc.Delete(menuId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(string menuId, Master_Menu model)
        {
            try
            {
                // TODO: Add update logic here
                IModulService objIMS = new ModulService();
                var result = objIMS.Edit(menuId, model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Modul Edit", ex.Message, ex.StackTrace);
                return View("~/Views/Master/Modul/Index.cshtml");
            }
        }

        public ActionResult Download(Master_Menu model)
        {
            try
            {
                XLWorkbook xlWorkBook = new XLWorkbook();
                var xlWorkSheet = xlWorkBook.Worksheets.Add("Master Modul");// xlWorkSheet;

                xlWorkSheet.Cell(1, 1).Value = "MenuId";
                xlWorkSheet.Cell(1, 2).Value = "MenuName";
                xlWorkSheet.Cell(1, 3).Value = "Text";
                xlWorkSheet.Cell(1, 4).Value = "Order";
              

                IModulService svc = new ModulService();
                var Data = svc.GetAll();
                int Row = 2;
                if (Data.Count > 0)
                {
                    for (int i = 0; i < Data.Count; i++)
                    {
                        xlWorkSheet.Cell(Row + i, 1).Value = Data[i].MenuID;
                        xlWorkSheet.Cell(Row + i, 2).Value = Data[i].MenuName;
                        xlWorkSheet.Cell(Row + i, 3).Value = Data[i].Text;
                        xlWorkSheet.Cell(Row + i, 4).Value = Data[i].OrderNumber;
                    }
                    xlWorkSheet.Columns().AdjustToContents();
                    var path = Server.MapPath("..") + "\\Master-Modul.xlsx";
                    xlWorkBook.SaveAs(path);
                    xlWorkBook.Dispose();
                    return File(path, "application/vnd.ms-excel", "Master-Modul.xlsx");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Function Download", ex.Message, ex.StackTrace);
                return View("~/Views/Master/Function/Index.cshtml");
            }

        }
    }
}