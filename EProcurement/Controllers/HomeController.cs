using System.Web.Mvc;
using EProcurement.Services;
using EProcurement.Models;
using EProcurement.Extensions;

namespace EProcurement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IUserService svc = new UserService();
            var model = svc.GetDashboard();
           // AddLogError("dashboard", "error", "test");
            return View(model);
        }
        public ActionResult ChangePassword(string userId)
        {
            IUserService svc = new UserService();
            var model = svc.Getdata(userId);
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(Master_User user, string NewPassword, string ConfirmPassword)
        {
            IUserService svc = new UserService();
            var model = svc.ChangedPassword(user, NewPassword);
            this.AddNotification("Your Password Has Been Successfully Changed. ", NotificationType.SUCCESS);
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {

            IUserService svc = new UserService();
            var model = svc.GetMenu();

            return PartialView("menu", model);
        }
        
        public void AddLogError(string Note, string ErrorMessage, string StackTrace)
        {
            IUserService svc = new UserService();
            var model = new Log_Error();
            model.UserID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            model.Note = Note;
            model.ErrorMessage = ErrorMessage;
            model.StackTrace = StackTrace;
            svc.AddLogError(model);
        }
    }
}