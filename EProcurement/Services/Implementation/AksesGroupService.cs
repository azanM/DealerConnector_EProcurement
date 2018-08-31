using System.Collections.Generic;
using System.Linq;
using EProcurement.Models;

namespace EProcurement.Services
{
    public class AksesGroupService : IAksesGroupService
    {
        public List<Master_Group> GetAll()
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.Master_Groups select c).ToList();
            return model;
        }
        public Master_Group Getdata(string groupId)
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.Master_Groups where c.GroupID == groupId select c).SingleOrDefault();
            return model;
        }
        public Master_Group Edit(string groupId, Master_Group model)
        {
            var dc = new eprocdbDataContext();
            var md = (from c in dc.Master_Groups where c.GroupID == groupId select c).SingleOrDefault();
            md.Description = model.Description;
            dc.SubmitChanges();
            return model;
        }
        public Master_Group Add(Master_Group model)
        {
            var dc = new eprocdbDataContext();
            Master_Group DataNew = new Master_Group();
            DataNew.ID = model.ID;
            DataNew.GroupID = model.GroupID;
            DataNew.Description = model.Description;
            DataNew.CreatedBy = model.CreatedBy;
            DataNew.CreatedDate = model.CreatedDate;
            DataNew.DeleteStatus = model.DeleteStatus;
            DataNew.IsAdmin = model.IsAdmin;
            dc.Master_Groups.InsertOnSubmit(DataNew);          
            dc.SubmitChanges();
            return model;
        }
        public IEnumerable<Master_Menu> GetAllMenu()
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.Master_Menus select c).ToList();
            return model;
        }
        public Master_Group Delete(string groupId)
        {
            var dc = new eprocdbDataContext();
            var model = dc.Master_Groups.FirstOrDefault(v => v.GroupID == groupId);
            dc.Master_Groups.DeleteOnSubmit(model);
            dc.SubmitChanges();
            return model;      
        }        
    }
}