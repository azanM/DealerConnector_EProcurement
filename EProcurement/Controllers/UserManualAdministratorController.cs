using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EProcurement.Controllers
{
    public class UserManualAdministratorController : Controller
    {
        // GET: UserManualAdministrator
        public ActionResult Index()
        {
            string fileName = "UserManualAdministrator.pdf";
            string fileDownloadName = "User Manual Administrator E-Procurement.pdf";
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
