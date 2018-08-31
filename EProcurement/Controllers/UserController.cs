using EProcurement.Models;
using EProcurement.Services;
using System.Web.Mvc;
using System.Linq;
using EProcurement.Extensions;
using System;
using ClosedXML.Excel;
using System.Web.Script.Serialization;

namespace EProcurement.Controllers
{
    public class UserController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            IUserService svc = new UserService();
            var model = svc.GetAll();
            return View("~/Views/Master/User/Index.cshtml", model);
        }

        public ActionResult Add()
        {
            this.ViewBag.groupId = GetGroupId();
            this.ViewBag.vendorId = GetVendor();
            this.ViewBag.groupVendorId = GetGroupVendor();
            return View("~/Views/Master/User/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Add(Master_User model, string ConfirmPass)
        {
            try
            {
                IUserService svc = new UserService();
                var user = svc.Getdata(model.UserID);
                if (user != null)
                {
                    ViewBag.Message = "Username exist.!!";
                    this.ViewBag.groupId = GetGroupId();
                    this.ViewBag.vendorId = GetVendor();
                    this.ViewBag.groupVendorId = GetGroupVendor();
                    return View("~/Views/Master/User/Add.cshtml");
                }
                else if (ConfirmPass == model.Password)
                {
                    model.Group_VendorID = "";
                    var result = svc.Add(model);
                    this.AddNotification("Your Data Has Been Successfully Saved. ", NotificationType.SUCCESS);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Password not match.!!";
                    this.ViewBag.groupId = GetGroupId();
                    this.ViewBag.vendorId = GetVendor();
                    this.ViewBag.groupVendorId = GetGroupVendor();
                    return View("~/Views/Master/User/Add.cshtml");
                }
            }
            catch (Exception ex)
            {
                general.AddLogError("User Add", ex.Message, ex.StackTrace);
                this.AddNotification("ID exist", NotificationType.ERROR);
                return View("~/Views/Master/User/Add.cshtml");
            }
        }

        public ActionResult Edit(string userId)
        {
            this.ViewBag.groupId = GetGroupId();
            this.ViewBag.groupVendorId = GetGroupVendor();
            IUserService svc = new UserService();
            var model = svc.Getdata(userId);

            SelectList selectList = model.GroupID == "VENDOR" ? GetVendor() : model.GroupID == "Cabang" ? GetPlant() : new SelectList("", "");
            this.ViewBag.vendorId = selectList;
            return View("~/Views/Master/User/Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult Edit(string userId, Master_User Model, string VENDORID)
        {
            try
            {
                if (VENDORID != "")
                {
                    Model.id_vendor = VENDORID;
                }
                IUserService svc = new UserService();
                var result = svc.Edit(userId, Model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("User Edit", ex.Message, ex.StackTrace);
                return View("~/Views/Master/User/Index.cshtml");
            }
        }

        protected static SelectList GetGroupId()
        {
            eprocdbDataContext db = new eprocdbDataContext();

            var data = from a in db.Master_Groups
                       select new
                       {
                           a.GroupID,
                           Description = a.GroupID
                       };

            SelectList selectList = new SelectList(data, "GroupID", "Description");

            return selectList;
        }

        protected static SelectList GetVendor()
        {
            eprocdbDataContext db = new eprocdbDataContext();

            var data = from a in db.MSVENDORs
                       where a.VENDORID.StartsWith("13")
                       || a.VENDORID.StartsWith("31")
                       || a.VENDORID.StartsWith("33")
                       select new
                       {
                           a.VENDORID,
                           a.VENDORNAME
                       };

            int coun = data.Count();
            SelectList selectList = new SelectList(data, "VENDORID", "VENDORNAME");

            return selectList;
        }
        [HttpGet]
        public string FetchVendor(string role)
        {
            var data = new SelectList("", "");
            if (role.Trim() == "VENDOR")
                data = GetVendor();
            else if (role.Trim() == "Cabang")
                data = GetPlant();

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };

            return serializer.Serialize(data);// Json(serializer.Serialize(data), JsonRequestBehavior.AllowGet);
        }
        protected static SelectList GetPlant()
        {
            eprocdbDataContext db = new eprocdbDataContext();

            var data = from a in db.MSPLANTs
                       select new
                       {
                           VENDORID = a.PLANTID,
                           VENDORNAME = a.NAME
                       };

            SelectList selectList = new SelectList(data, "VENDORID", "VENDORNAME");

            return selectList;
        }

        protected static SelectList GetGroupVendor()
        {
            eprocdbDataContext db = new eprocdbDataContext();

            var data = from a in db.VENDOR_GROUPs
                       select new
                       {
                           a.Group_VendorID,
                           a.Group_VendorName
                       };

            SelectList selectList = new SelectList(data, "Group_VendorID", "Group_VendorName");

            return selectList;
        }

        [HttpPost]
        public ActionResult ResetAJAX(string UserID, Master_User model)
        {
            try
            {
                IUserService svc = new UserService();
                var result = svc.Reset(UserID, model);
                this.AddNotification("Reset Password Successfully.", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("User Reset", ex.Message, ex.StackTrace);
                this.AddNotification("Something Went Wrong.", NotificationType.ERROR);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Download(Master_User model)
        {
            try
            {
                XLWorkbook xlWorkBook = new XLWorkbook();
                var xlWorkSheet = xlWorkBook.Worksheets.Add("Master User");// xlWorkSheet;

                xlWorkSheet.Cell(1, 1).Value = "UserId";
                xlWorkSheet.Cell(1, 2).Value = "NamaLengkap";
                xlWorkSheet.Cell(1, 3).Value = "Group";
                xlWorkSheet.Cell(1, 4).Value = "IdVendor";

                IUserService svc = new UserService();
                var Data = svc.GetAll();
                int Row = 2;
                if (Data.Count > 0)
                {
                    for (int i = 0; i < Data.Count; i++)
                    {
                        xlWorkSheet.Cell(Row + i, 1).Value = Data[i].UserID;
                        xlWorkSheet.Cell(Row + i, 2).Value = Data[i].FullName;
                        xlWorkSheet.Cell(Row + i, 3).Value = Data[i].GroupID;
                        xlWorkSheet.Cell(Row + i, 4).Value = Data[i].id_vendor;
                    }
                    xlWorkSheet.Columns().AdjustToContents();
                    var path = Server.MapPath("..") + "\\Master-User.xlsx";
                    xlWorkBook.SaveAs(path);
                    xlWorkBook.Dispose();
                    return File(path, "application/vnd.ms-excel", "Master-User.xlsx");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("User Download", ex.Message, ex.StackTrace);
                this.AddNotification("Something Went Wrong.", NotificationType.ERROR);
                return RedirectToAction("Index");
            }

        }
    }
}