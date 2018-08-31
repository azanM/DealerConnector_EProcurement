using System.Collections.Generic;
using System.Linq;
using EProcurement.Models.ViewModel.Master;
using EProcurement.Models;

namespace EProcurement.Services
{
    public class GroupVendorService : IGroupVendorService
    {
        public List<GroupingVendorViewModel> GetAll()
        {
            var dc = new eprocdbDataContext();
            var model = (from vg in dc.VENDOR_GROUPs
                         //join mu in dc.Master_Users on vg.Group_VendorID equals mu.Group_VendorID
                         select new
                         {
                             GroupVendorId = vg.Group_VendorID,
                             GroupVendor = vg.Group_VendorName
                         }).ToList();
            return model.Select(c => new GroupingVendorViewModel()).ToList();
        }
        public GroupingVendorViewModel Getdata(string groupId)
        {
            var dc = new eprocdbDataContext();
           
            var model = (from vg in dc.VENDOR_GROUPs
                        // from mu in dc.Master_Users.Where(mapping => mapping.Group_VendorID == vg.Group_VendorID).DefaultIfEmpty() //on vg.Group_VendorID equals mu.Group_VendorID
                         where vg.Group_VendorID == groupId
                         select new GroupingVendorViewModel
                         {
                             GroupVendorId = vg.Group_VendorID,
                             GroupVendor = vg.Group_VendorName
                         }).SingleOrDefault();           
            return model; 
        }
        public GroupingVendorViewModel Edit(string groupId, GroupingVendorViewModel model)
        {
            var dc = new eprocdbDataContext();
            var md = (from c in dc.Master_Groups where c.GroupID == groupId select c).SingleOrDefault();
            md.Description = model.Description;
            dc.SubmitChanges();
            return model;
        }
        public GroupingVendorViewModel Add(GroupingVendorViewModel model)
        {
            var dc = new eprocdbDataContext();
            VENDOR_GROUP obj = new VENDOR_GROUP();
            obj.Group_VendorID = model.GroupVendorId;
            obj.Group_VendorName = model.Description;

            dc.VENDOR_GROUPs.InsertOnSubmit(obj);          
            dc.SubmitChanges();
            return model;
        }
    }
}