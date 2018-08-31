using System.Web.Mvc;
using EProcurement.Services;
using EProcurement.Models;
using EProcurement.Models.ViewModel;
using EProcurement.Extensions;
using System;
using ClosedXML;
using ClosedXML.Excel;

namespace EProcurement.Controllers
{
    public class AksesGroupController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            IAksesGroupService svc = new AksesGroupService();
            var model = svc.GetAll();
            return View("~/Views/Master/AksesGroup/Index.cshtml", model);
        }

        public ActionResult Add()
        {
            IAksesGroupService svc = new AksesGroupService();
            var model = svc.GetAllMenu();

            var viewModel = new AksesGroupViewModel()
            {
                AksesGroup = new Master_Group(),
                Menu = model
            };
            return View("~/Views/Master/AksesGroup/Add.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult Add(AksesGroupViewModel model)
        {
            try
            {
                IAksesGroupService svc = new AksesGroupService();
                var result = svc.Add(model.AksesGroup);
                this.AddNotification("Your Data Has Been Successfully Saved. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("AksesGroup Add", ex.Message, ex.StackTrace);
                this.AddNotification("ID exist", NotificationType.ERROR);
                return View("~/Views/Master/AksesGroup/Add.cshtml");
            }
        }
        public ActionResult View(string groupId)
        {
            IAksesGroupService svc = new AksesGroupService();
            var model = svc.Getdata(groupId);
            return View("~/Views/Master/AksesGroup/View.cshtml", model);
        }
        public ActionResult Edit(string groupId)
        {
            IAksesGroupService svc = new AksesGroupService();
            var model = svc.Getdata(groupId);
            return View("~/Views/Master/AksesGroup/Edit.cshtml", model);
        }
        public ActionResult Delete(string groupId)
        {
            IAksesGroupService svc = new AksesGroupService();
            var model = svc.Delete(groupId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(string groupId, Master_Group model)
        {
            try
            {
                IAksesGroupService svc = new AksesGroupService();
                var result = svc.Edit(groupId, model);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                general.AddLogError("AksesGroup Edit", ex.Message, ex.StackTrace);
                return View("~/Views/Master/AksesGroup/Index.cshtml");
            }
        }

        public ActionResult Download(AksesGroupViewModel model)
        {
            try
            {
                XLWorkbook xlWorkBook = new XLWorkbook();
                var xlWorkSheet = xlWorkBook.Worksheets.Add("Master AksesGroup");// xlWorkSheet;

                xlWorkSheet.Cell(1, 1).Value = "GroupId";
                xlWorkSheet.Cell(1, 2).Value = "Deskripsi";

                IAksesGroupService svc = new AksesGroupService();
                var Data = svc.GetAll();
                int Row = 2;
                if (Data.Count > 0)
                {
                    for (int i = 0; i < Data.Count; i++)
                    {
                        xlWorkSheet.Cell(Row + i, 1).Value = Data[i].GroupID;
                        xlWorkSheet.Cell(Row + i, 2).Value = Data[i].Description;

                    }
                    xlWorkSheet.Columns().AdjustToContents();
                    var path = Server.MapPath("..") + "\\Master-AksesGroup.xlsx";
                    xlWorkBook.SaveAs(path);
                    xlWorkBook.Dispose();
                    return File(path, "application/vnd.ms-excel", "Master-AksesGroup.xlsx");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Akses Group", ex.Message, ex.StackTrace);
                return View("~/Views/Master/AksesGroup/Index.cshtml");
            }

        }
    }
}