using System.Collections.Generic;
using EProcurement.Models.ViewModel.Master;

namespace EProcurement.Services
{
    public interface IGroupVendorService
    {
        List<GroupingVendorViewModel> GetAll();
        GroupingVendorViewModel Getdata(string groupId);
        GroupingVendorViewModel Edit(string groupId, GroupingVendorViewModel model);
        GroupingVendorViewModel Add(GroupingVendorViewModel model);
    }
}