using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EProcurement.Controllers
{
    public class UserManualProcurementController : Controller
    {
        public ActionResult Index()
        {
            string fileName = "UserManualProcurement.pdf";
            string fileDownloadName = "User Manual Procurement E-Procurement.pdf";
            string folder = "~/Templates/";
            string type = "application/pdf";

            var sDocument = Server.MapPath(folder + fileName);
            if (!System.IO.File.Exists(sDocument))
            {
                return HttpNotFound();
            }

            return File(sDocument, type, fileDownloadName);
        }
    }
}