using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EProcurement.Models;
using EProcurement.Models.ViewModel;
using EProcurement.Services.Interface;
using EProcurement.Services.Implementation;


namespace EProcurement.Services
{
    public class UserService : IUserService
    {
        public Master_User Login(string username, string password)
        {
            IHashingService sec = new HashingService();
            string pwd = sec.CreatePasswordHash(password.Trim());

            var dc = new eprocdbDataContext();
            var model = (from c in dc.Master_Users where c.UserID == username && c.Password == pwd select c).FirstOrDefault();
            return model;
        }

        public List<Master_User> GetAll()
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.Master_Users where c.UserID != "Superadmin" select c).ToList();
            return model;
        }
        public Master_User Getdata(string userId)
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.Master_Users where c.UserID == userId select c).FirstOrDefault();
            return model;
        }
        public DashboardViewModel GetDashboard()
        {
            var dc = new eprocdbDataContext();
            var model = new DashboardViewModel();
            if (System.Web.HttpContext.Current.Session["GroupID"].ToString() == "PROCUREMENT")
            {
                model.JmlPO = (from p in dc.CUSTOMPOs where p.TGLPO.Value.Year == DateTime.Today.Year select p.PONUMBER).Count();
                model.JmlPR = (from p in dc.CUSTOMPRs join q in dc.CUSTOMPOs on p.PRSAP equals q.PRNUMBERSAP where q.TGLPO.Value.Year == DateTime.Today.Year select p).Count();
                model.JmlGR = (from p in dc.CUSTOMGRs join q in dc.CUSTOMPOs on p.PONUMBER equals q.PONUMBER where q.TGLPO.Value.Year == DateTime.Today.Year && p.TGLGRSAP != null select p).Count();
                model.JmlNonGR = (from p in dc.CUSTOMGRs join q in dc.CUSTOMPOs on p.PONUMBER equals q.PONUMBER where q.TGLPO.Value.Year == DateTime.Today.Year && p.TGLGRSAP == null select p).Count();
                model.JmlPOClearing = (from p in dc.CUSTOMIRs join q in dc.CUSTOMPOs on p.PONUMBER equals q.PONUMBER where q.TGLPO.Value.Year == DateTime.Today.Year && p.TGLPEMBAYARAN != null select p).Count();
                model.JmlPONotClearing = (from p in dc.CUSTOMIRs join q in dc.CUSTOMPOs on p.PONUMBER equals q.PONUMBER where q.TGLPO.Value.Year == DateTime.Today.Year && p.TGLPEMBAYARAN == null select p).Count();
                model.JmlBPKB = (from p in dc.CUSTOMBPKBs join q in dc.CUSTOMPOs on p.PONUMBER equals q.PONUMBER where q.TGLPO.Value.Year == DateTime.Today.Year && p.TGLGRBPKB != null select p).Count();
                model.JmlBPKBOutstanding = (from p in dc.CUSTOMBPKBs join q in dc.CUSTOMPOs on p.PONUMBER equals q.PONUMBER where q.TGLPO.Value.Year == DateTime.Today.Year && q.POSTATUSID != "10" && q.POSTATUSID != "12" select p).Count();
            }
            else if (System.Web.HttpContext.Current.Session["GroupID"].ToString() == "VENDOR")
            {
                string vendorId = System.Web.HttpContext.Current.Session["VendorID"].ToString();
                model.JmlPO = (from p in dc.CUSTOMPOs where p.TGLPO.Value.Year == DateTime.Today.Year && p.VENDORID == vendorId select p.PONUMBER).Count();
                model.JmlPR = (from p in dc.CUSTOMPRs join q in dc.CUSTOMPOs on p.PRSAP equals q.PRNUMBERSAP where q.TGLPO.Value.Year == DateTime.Today.Year && q.VENDORID == vendorId select p).Count();
                model.JmlGR = (from p in dc.CUSTOMGRs join q in dc.CUSTOMPOs on p.PONUMBER equals q.PONUMBER where q.TGLPO.Value.Year == DateTime.Today.Year && q.VENDORID == vendorId && p.TGLGRSAP != null select p).Count();
                model.JmlNonGR = (from p in dc.CUSTOMGRs join q in dc.CUSTOMPOs on p.PONUMBER equals q.PONUMBER where q.TGLPO.Value.Year == DateTime.Today.Year && q.VENDORID == vendorId && p.TGLGRSAP == null select p).Count();
                model.JmlPOClearing = (from p in dc.CUSTOMIRs join q in dc.CUSTOMPOs on p.PONUMBER equals q.PONUMBER where q.TGLPO.Value.Year == DateTime.Today.Year && q.VENDORID == vendorId && p.TGLPEMBAYARAN != null select p).Count();
                model.JmlPONotClearing = (from p in dc.CUSTOMIRs join q in dc.CUSTOMPOs on p.PONUMBER equals q.PONUMBER where q.TGLPO.Value.Year == DateTime.Today.Year && q.VENDORID == vendorId && p.TGLPEMBAYARAN == null select p).Count();
                model.JmlBPKB = (from p in dc.CUSTOMBPKBs join q in dc.CUSTOMPOs on p.PONUMBER equals q.PONUMBER where q.TGLPO.Value.Year == DateTime.Today.Year && q.VENDORID == vendorId && p.TGLGRBPKB != null select p).Count();
                model.JmlBPKBOutstanding = (from p in dc.CUSTOMBPKBs join q in dc.CUSTOMPOs on p.PONUMBER equals q.PONUMBER where q.TGLPO.Value.Year == DateTime.Today.Year && q.VENDORID == vendorId && q.POSTATUSID != "10" && q.POSTATUSID != "12" select p).Count();
            }
            return model;
        }
        public Master_User Edit(string userId, Master_User model)
        {
            IHashingService sec = new HashingService();
            string pwd = sec.CreatePasswordHash(model.Password.Trim());

            var dc = new eprocdbDataContext();
            var md = (from c in dc.Master_Users
                      where c.UserID == userId
                      select c).FirstOrDefault();
            md.UserID = model.UserID;
            md.FullName = model.FullName;
            md.GroupID = model.GroupID == null ? md.GroupID : model.GroupID;
            md.id_vendor = model.id_vendor == null ? md.id_vendor : model.id_vendor;
            //md.Group_VendorID = model.Group_VendorID;
            md.Email = model.Email;
            if (model.Password != null && model.Password != "" && model.Password != md.Password)
                md.Password = pwd;
            dc.SubmitChanges();
            return model;
        }

        public Master_User ChangedPassword(Master_User model, string NewPassword)
        {
            IHashingService sec = new HashingService();
            string pwd = sec.CreatePasswordHash(NewPassword.Trim());

            var dc = new eprocdbDataContext();
            var md = (from c in dc.Master_Users
                      where c.UserID == model.UserID
                      select c).FirstOrDefault();

            md.Password = pwd;
            dc.SubmitChanges();
            return model;
        }
        public Master_User Add(Master_User model)
        {
            IHashingService sec = new HashingService();
            string pwd = sec.CreatePasswordHash(model.Password.Trim());

            var dc = new eprocdbDataContext();
            model.ID = Guid.NewGuid();
            model.Password = pwd;
            dc.Master_Users.InsertOnSubmit(model);
            dc.SubmitChanges();
            return model;
        }
        public MenuViewModel GetMenu()
        {
            var dc = new eprocdbDataContext();
            Master_User sessionUser = (Master_User)HttpContext.Current.Session["USERS_DATA"];
            string role = sessionUser.GroupID;

            var sourcedata = dc.Mgmt_Akses.Where(x => x.GroupID == role).ToList();

            var vw = new MenuItemViewModel();
            var parent = (from b in sourcedata
                          join a in dc.Master_Menus on b.MenuID.Trim() equals a.MenuID.Trim()
                          where a.ParentID == null
                          orderby a.OrderNumber
                          select new MenuItemViewModel
                          {
                              MenuItemId = a.MenuID,
                              MenuItemName = a.Text,
                              MenuItemPath = a.Url,
                              ParentItemId = a.ParentID,
                              ParentIconClass = a.ParentIconClass,
                              ChildIconClass = a.ChildIconClass
                          }).ToList();

            var child = (from c in sourcedata
                         join b in dc.Master_Menus on c.MenuID.Trim() equals b.MenuID.Trim()
                         where b.MenuID != "007"
                         select new MenuItemViewModel
                         {
                             MenuItemId = b.MenuID,
                             MenuItemName = b.Text,
                             MenuItemPath = b.Url,
                             ParentItemId = b.ParentID,
                             ParentIconClass = b.ParentIconClass,
                             ChildIconClass = b.ChildIconClass
                         }).ToList();

            MenuViewModel mod = new MenuViewModel();
            foreach (var a in parent)
            {
                vw = a;
                foreach (var z in child)
                {
                    if (a.MenuItemId == z.ParentItemId)
                    {
                        vw.ChildMenuItems.Add(new MenuItemViewModel()
                        {
                            MenuItemName = z.MenuItemName,
                            MenuItemPath = z.MenuItemPath,
                            MenuItemId = z.MenuItemId,
                            ParentItemId = z.ParentItemId,
                            ParentIconClass = z.ParentIconClass,
                            ChildIconClass = z.ChildIconClass
                        });
                    }
                }

                mod.Items.Add(a);
            }

            return mod;
        }

        public Master_User Reset(string UserID, Master_User model)
        {
            IHashingService sec = new HashingService();
            string pwd = sec.CreatePasswordHash("dc123");

            var dc = new eprocdbDataContext();
            var md = (from c in dc.Master_Users
                      where c.UserID == UserID
                      select c).FirstOrDefault();
            md.Password = pwd;
            dc.SubmitChanges();
            return model;
        }

        public Log_Error AddLogError(Log_Error model)
        {
            var dc = new eprocdbDataContext();
            model.ErrorTime = DateTime.Now;
            dc.Log_Errors.InsertOnSubmit(model);
            dc.SubmitChanges();
            return model;
        }

        public Log_Login AddLogLogin(Log_Login model)
        {
            var dc = new eprocdbDataContext();
            model.LoginID= Guid.NewGuid();
            model.LoginDate = DateTime.Now;
            dc.Log_Logins.InsertOnSubmit(model);
            dc.SubmitChanges();
            return model;
        }
    }
}