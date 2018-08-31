using System.Collections.Generic;
using System.Linq;
using EProcurement.Models;
using EProcurement.Models.ViewModel.Master;
using System;

namespace EProcurement.Services
{
    public class VendorService : IVendorService
    {
        public List<MSVENDOR> GetAll()
        {
            var dc = new eprocdbDataContext();
            var model = dc.MSVENDORs.Where(a => a.VENDORID.StartsWith("13") || a.VENDORID.StartsWith("31") || a.VENDORID.StartsWith("33")).OrderByDescending(a => a.VENDORID).ToList();
            return model;
        }

        public VendorViewModel Add(VendorViewModel model)
        {
            var dc = new eprocdbDataContext();
            var md = new MSVENDOR();
            md.VENDORNAME = model.VENDORNAME;
            md.STREET = model.STREET;
            md.DISTRIC = model.DISTRIC;
            md.CITY = model.CITY;
            md.TELEPHONE = model.TELEPHONE;
            md.POSTALCODE = model.POSTALCODE;
            md.EMAIL = model.EMAIL;
            md.EMAIL2 = model.EMAIL2;
            md.EMAIL3 = model.EMAIL3;
            md.EMAIL4 = model.EMAIL4;
            md.EMAIL5 = model.EMAIL5;
            md.EMAIL6 = model.EMAIL6;
            md.EMAIL7 = model.EMAIL7;
            md.EMAIL8 = model.EMAIL8;
            md.PLANTIDTSO = model.PLANTIDTSO;
            md.B2B = model.B2Bs == true ? '1' : '0';
            dc.MSVENDORs.InsertOnSubmit(md);
            dc.SubmitChanges();
            return model;
        }

        public VendorViewModel Getdata(string vendorId)
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.MSVENDORs where c.VENDORID == vendorId select c).SingleOrDefault();
            var md = new VendorViewModel();
            md.VENDORNAME = model.VENDORNAME;
            md.STREET = model.STREET;
            md.DISTRIC = model.DISTRIC;
            md.CITY = model.CITY;
            md.TELEPHONE = model.TELEPHONE;
            md.POSTALCODE = model.POSTALCODE;
            md.EMAIL = model.EMAIL;
            md.EMAIL2 = model.EMAIL2;
            md.EMAIL3 = model.EMAIL3;
            md.EMAIL4 = model.EMAIL4;
            md.EMAIL5 = model.EMAIL5;
            md.EMAIL6 = model.EMAIL6;
            md.EMAIL7 = model.EMAIL7;
            md.EMAIL8 = model.EMAIL8;
            md.PLANTIDTSO = model.PLANTIDTSO;
            md.B2B = model.B2B;
            md.B2Bs = model.B2B == '1' ? true : false;
            return md;
        }

        public VendorViewModel Edit(string vendorId, VendorViewModel model)
        {
            var dc = new eprocdbDataContext();
            var md = (from c in dc.MSVENDORs where c.VENDORID == vendorId select c).SingleOrDefault();
            md.VENDORNAME = model.VENDORNAME;
            md.STREET = model.STREET;
            md.DISTRIC = model.DISTRIC;
            md.CITY = model.CITY;
            md.TELEPHONE = model.TELEPHONE;
            md.POSTALCODE = model.POSTALCODE;
            md.EMAIL = model.EMAIL;
            md.EMAIL2 = model.EMAIL2;
            md.EMAIL3 = model.EMAIL3;
            md.EMAIL4 = model.EMAIL4;
            md.EMAIL5 = model.EMAIL5;
            md.EMAIL6 = model.EMAIL6;
            md.EMAIL7 = model.EMAIL7;
            md.EMAIL8 = model.EMAIL8;
            md.PLANTIDTSO = model.PLANTIDTSO;
            md.B2B = model.B2Bs == true ? '1' : '0';
            dc.SubmitChanges();
            return model;
        }
    }
}