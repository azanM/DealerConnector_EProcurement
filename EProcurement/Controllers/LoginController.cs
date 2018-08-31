using System.Web.Mvc;
using System.Web.Security;
using EProcurement.Models;
using EProcurement.Services;
using System;
using System.Net;
using System.Net.Mail;

namespace EProcurement.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Session.Clear();
            Session.Abandon();
            return View();
        }

        [HttpPost]
        public ActionResult Index(Master_User user)
        {
            if (ModelState.IsValid)
            {
                IUserService svc = new UserService();
                var result = svc.Login(user.UserID, user.Password);
                if (result != null)
                {
                    FormsAuthentication.SetAuthCookie(user.UserID, true);
                    System.Web.HttpContext.Current.Session["USERS_DATA"] = result;
                    System.Web.HttpContext.Current.Session["Fullname"] = result.FullName;
                    System.Web.HttpContext.Current.Session["UserID"] = result.UserID;
                    System.Web.HttpContext.Current.Session["VendorID"] = result.id_vendor;
                    System.Web.HttpContext.Current.Session["GroupID"] = result.GroupID;
                    AddLogLogin(result.SessionID == null ? "" : result.SessionID, result.UserID);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["error"] = "Login data is incorrect!";
                }
            }
            return View("Index", user);
        }
        public void AddLogLogin(string session, string user)
        {
            IUserService svc = new UserService();
            var model = new Log_Login();
            model.SessionID = session;
            model.UserID = user;
            svc.AddLogLogin(model);
        }
    }
}