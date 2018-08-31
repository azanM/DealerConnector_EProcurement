using System.Collections.Generic;
using EProcurement.Models;
using System.Data.Entity;

namespace EProcurement.Services
{
    public interface ICompanyService
    {
        List<CUSTOMCOMPANY> GetAll();
        CUSTOMCOMPANY Getdata(string companyId);
        CUSTOMCOMPANY Edit(string companyId, CUSTOMCOMPANY model);
        CUSTOMCOMPANY Add(CUSTOMCOMPANY model);
    }
}