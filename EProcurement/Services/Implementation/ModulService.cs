using System.Collections.Generic;
using System.Linq;
using EProcurement.Models;

namespace EProcurement.Services
{
    public class ModulService : IModulService
    {
        public List<Master_Menu> GetAll()
        {
            var dc = new eprocdbDataContext();
            var model = dc.Master_Menus.Where(a => a.ParentID == null).OrderByDescending(a => a.MenuID).ToList();
            return model;
        }

        public Master_Menu Add(Master_Menu model)
        {
            var dc = new eprocdbDataContext();
            var id = "0" + (dc.Master_Menus.Count()+1).ToString();
            model.MenuID = id;
            dc.Master_Menus.InsertOnSubmit(model);
            dc.SubmitChanges();
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
            md.MenuName = model.MenuName;
            md.Text = model.Text;
            dc.SubmitChanges();
            return model;
        }
        public Master_Menu Delete(string menuId)
        {
            var dc = new eprocdbDataContext();
            var model = dc.Master_Menus.FirstOrDefault(v => v.MenuID == menuId);
            dc.Master_Menus.DeleteOnSubmit(model);
            dc.SubmitChanges();
            return model;
        }
    }
}