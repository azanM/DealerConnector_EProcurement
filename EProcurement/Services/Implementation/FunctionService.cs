using System.Collections.Generic;
using System.Linq;
using EProcurement.Models;

namespace EProcurement.Services
{
    public class FunctionService : IFunctionService
    {
        public List<Master_Menu> GetAll()
        {
            var dc = new eprocdbDataContext();
            var model = dc.Master_Menus.OrderByDescending(a => a.MenuID).ToList();
            return model;
        }
        public Master_Menu Getdata(string menuId)
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.Master_Menus where c.MenuID == menuId select c).SingleOrDefault();
            return model;
        }
        public Master_Menu Edit(string menuId, Master_Menu model)
        {
            var dc = new eprocdbDataContext();
            var md = (from c in dc.Master_Menus where c.MenuID == menuId select c).SingleOrDefault();
            md.MenuID = model.MenuID;
            md.ParentID = model.ParentID;
            md.MenuName = model.MenuName;
            md.Text = model.Text;
            md.Url = model.Url;
            dc.SubmitChanges();
            return model;
        }
        public Master_Menu Add(Master_Menu model)
        {
            var dc = new eprocdbDataContext();
            dc.Master_Menus.InsertOnSubmit(model);
            dc.SubmitChanges();
            return model;
        }

        public static string GenerateID()
        {
            string uniqueID = "";

            using (eprocdbDataContext db = new eprocdbDataContext())
            {
                int lastID = (from a in db.Master_Menus
                              select a).Count();

                int seqID = lastID + 1;

                uniqueID = seqID.ToString().PadLeft(3, '0');

            }

            return uniqueID;
        }
        public Master_Menu Delete(string menuId)
        {
            var dc = new eprocdbDataContext();
            var model = dc.Master_Menus.FirstOrDefault(v => v.MenuID == menuId);
            dc.Master_Menus.DeleteOnSubmit(model);
            dc.SubmitChanges();
            return model;
        }
        public List<Master_Menu> GetModul()
        {
            var dc = new eprocdbDataContext();
            var model = dc.Master_Menus.Where(a => a.ParentID == null).OrderByDescending(a => a.MenuID).ToList();
            return model;
        }
    }
}