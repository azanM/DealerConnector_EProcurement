using System.Collections.Generic;
using System.Linq;
using EProcurement.Models;

namespace EProcurement.Services
{
    public class CompanyService : ICompanyService
    {
        public List<CUSTOMCOMPANY> GetAll()
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.CUSTOMCOMPANies select c).ToList();
            return model;
        }
        public CUSTOMCOMPANY Getdata(string companyId)
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.CUSTOMCOMPANies where c.COMPANYCODE == companyId select c).SingleOrDefault();
            return model;
        }
        public CUSTOMCOMPANY Edit(string companyId, CUSTOMCOMPANY model)
        {
            var dc = new eprocdbDataContext();
            var md = (from c in dc.CUSTOMCOMPANies where c.COMPANYCODE == companyId select c).SingleOrDefault();
            md.COMPANYNAME = model.COMPANYNAME;
            md.ALAMAT = model.ALAMAT;
            md.KOTA = model.KOTA;
            md.REGION = model.REGION;
            md.POSTALCODE = model.POSTALCODE;
            md.TELEPON = model.TELEPON;
            md.NPWP = model.NPWP;
            md.KTPTDP = model.KTPTDP;
            md.COMPANYCODETSO = model.COMPANYCODETSO;
            dc.SubmitChanges();
            return model;
        }
        public CUSTOMCOMPANY Add(CUSTOMCOMPANY model)
        {
            var dc = new eprocdbDataContext();
            dc.CUSTOMCOMPANies.InsertOnSubmit(model);          
            dc.SubmitChanges();
            return model;
        }
    }
}