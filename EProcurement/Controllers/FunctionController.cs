using EProcurement.Models;
using EProcurement.Services;
using System.Web.Mvc;
using EProcurement.Extensions;
using System;
using ClosedXML;
using ClosedXML.Excel;

namespace EProcurement.Controllers
{
    public class FunctionController : Controller
    {
        HomeController general = new HomeController();
        // GET: Vendor
        public ActionResult Index()
        {
            IFunctionService svc = new FunctionService();
            var model = svc.GetAll();
            return View("~/Views/Master/Function/Index.cshtml", model);
        }

        public ActionResult Add()
        {
            this.ViewBag.menuId = GetModul();
            return View("~/Views/Master/Function/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Add(Master_Menu model)
        {
            try
            {
                IFunctionService svc = new FunctionService();
                model.MenuID = FunctionService.GenerateID();
                var result = svc.Add(model);
                this.AddNotification("Your Data Has Been Successfully Saved. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Function Add", ex.Message, ex.StackTrace);
                this.AddNotification("ID exist", NotificationType.ERROR);
                return View("~/Views/Master/Function/Add.cshtml");
            }
        }

        public ActionResult Edit(string menuId)
        {
            this.ViewBag.menuId = GetModul();

            IFunctionService svc = new FunctionService();
            var model = svc.Getdata(menuId);
            return View("~/Views/Master/Function/Edit.cshtml", model);
        }
        public ActionResult Delete(string menuId)
        {
            IFunctionService svc = new FunctionService();
            var model = svc.Delete(menuId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(string menuId, Master_Menu model)
        {
            try
            {
                IFunctionService svc = new FunctionService();
                var result = svc.Edit(menuId, model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Function Edit", ex.Message, ex.StackTrace);
                return View("~/Views/Master/Function/Index.cshtml");
            }
        }

        protected static SelectList GetModul()
        {
            IFunctionService svc = new FunctionService();
            var data = svc.GetModul();

            SelectList selectList = new SelectList(data, "ParentID", "Text");

            return selectList;
        }
        public ActionResult Download(Master_Menu model)
        {
            try
            {
                XLWorkbook xlWorkBook = new XLWorkbook();
                var xlWorkSheet = xlWorkBook.Worksheets.Add("Master Function");// xlWorkSheet;

                xlWorkSheet.Cell(1, 1).Value = "MenuId";
                xlWorkSheet.Cell(1, 2).Value = "ParentId";
                xlWorkSheet.Cell(1, 3).Value = "MenuName";
                xlWorkSheet.Cell(1, 4).Value = "Text";
                xlWorkSheet.Cell(1, 5).Value = "Form";
                xlWorkSheet.Cell(1, 6).Value = "Order";

                IFunctionService svc = new FunctionService();
                var Data = svc.GetAll();
                int Row = 2;
                if (Data.Count > 0)
                {
                    for (int i = 0; i < Data.Count; i++)
                    {
                        xlWorkSheet.Cell(Row + i, 1).Value = Data[i].MenuID;
                        xlWorkSheet.Cell(Row + i, 2).Value = Data[i].ParentID;
                        xlWorkSheet.Cell(Row + i, 3).Value = Data[i].MenuName;
                        xlWorkSheet.Cell(Row + i, 4).Value = Data[i].Text;
                        xlWorkSheet.Cell(Row + i, 5).Value = Data[i].Url;
                        xlWorkSheet.Cell(Row + i, 6).Value = Data[i].OrderNumber;
                    }
                    xlWorkSheet.Columns().AdjustToContents();
                    var path = Server.MapPath("..") + "\\Master-Function.xlsx";
                    xlWorkBook.SaveAs(path);
                    xlWorkBook.Dispose();
                    return File(path, "application/vnd.ms-excel", "Master-Function.xlsx");
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