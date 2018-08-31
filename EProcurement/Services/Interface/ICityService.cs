using System.Collections.Generic;
using EProcurement.Models;
using System.Data.Entity;

namespace EProcurement.Services
{
    public interface ICityService
    {
        List<CUSTOMCITY> GetAll();
        CUSTOMCITY Getdata(string cityId);
        CUSTOMCITY Edit(string cityId, CUSTOMCITY model);
        CUSTOMCITY Add(CUSTOMCITY model);
    }
}