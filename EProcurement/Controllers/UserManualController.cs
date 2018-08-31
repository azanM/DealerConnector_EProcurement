using System.Web.Mvc;

namespace EProcurement.Controllers
{
    public class UserManualController : Controller
    {
        public ActionResult Index()
        {
            string fileName = "UserManualVendor.pdf";
            string fileDownloadName = "User Manual Vendor E-Procurement.pdf";
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