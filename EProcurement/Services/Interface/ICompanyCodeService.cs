using System.Collections.Generic;
using EProcurement.Models;

namespace EProcurement.Services
{
    public interface ICompanyCodeService
    {
        List<Company_Code> GetAll();
        Company_Code Getdata(string companyCode);
        Company_Code Edit(string companyCode, Company_Code model);
        Company_Code Add(Company_Code model);
    }
}