using System.Collections.Generic;
using EProcurement.Models;
using System.Data.Entity;
using EProcurement.Models.ViewModel.Master;

namespace EProcurement.Services
{
    public interface IVendorService
    {
        List<MSVENDOR> GetAll();
        VendorViewModel Add(VendorViewModel model);
        VendorViewModel Getdata(string vendorId);
        VendorViewModel Edit(string vendorId, VendorViewModel model);
    }
}