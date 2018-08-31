using System.Web.Mvc;
using EProcurement.Services;
using EProcurement.Models.ViewModel.Master;
using EProcurement.Extensions;
using System;

namespace EProcurement.Controllers
{
    public class GroupVendorController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            IGroupVendorService svc = new GroupVendorService();
            var model = svc.GetAll();
            return View("~/Views/Master/GroupVendor/Index.cshtml", model);
        }

        public ActionResult Add()
        {
            return View("~/Views/Master/GroupVendor/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Add(GroupingVendorViewModel model)
        {
            try
            {
                IGroupVendorService svc = new GroupVendorService();
                var result = svc.Add(model);
                this.AddNotification("Your Data Has Been Successfully Saved. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("GroupVendor Add", ex.Message, ex.StackTrace);
                this.AddNotification("ID exist", NotificationType.ERROR);
                return View("~/Views/Master/GroupVendor/Add.cshtml");
            }
        }
        public ActionResult Edit(string groupId)
        {
            IGroupVendorService svc = new GroupVendorService();
            var model = svc.Getdata(groupId);
            return View("~/Views/Master/GroupVendor/Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult Edit(string groupId, GroupingVendorViewModel model)
        {
            try
            {
                IGroupVendorService svc = new GroupVendorService();
                var result = svc.Edit(groupId, model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("GroupVendor Edit", ex.Message, ex.StackTrace);
                return View("~/Views/Master/GroupVendor/Index.cshtml");
            }
        }
    }
}