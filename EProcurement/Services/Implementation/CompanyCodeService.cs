using System.Collections.Generic;
using System.Linq;
using EProcurement.Models;

namespace EProcurement.Services
{
    public class CompanyCodeService : ICompanyCodeService
    {
        public List<Company_Code> GetAll()
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.Company_Codes select c).ToList();
            return model;
        }
        public Company_Code Getdata(string companyCode)
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.Company_Codes where c.companyCode == companyCode select c).SingleOrDefault();
            return model;
        }
        public Company_Code Edit(string companyCode, Company_Code model)
        {
            var dc = new eprocdbDataContext();
            var md = (from c in dc.Company_Codes where c.companyCode == companyCode select c).SingleOrDefault();
            md.companyName = model.companyName;
            if (model.status == null)
            {
                md.status = md.status;
               
            }else
            {
                md.status = model.status;
            }
            dc.SubmitChanges();
            return model;
        }
        public Company_Code Add(Company_Code model)
        {
            var dc = new eprocdbDataContext();
            dc.Company_Codes.InsertOnSubmit(model);          
            dc.SubmitChanges();
            return model;
        }
    }
}