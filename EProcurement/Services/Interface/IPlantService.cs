using System.Collections.Generic;
using EProcurement.Models;

namespace EProcurement.Services
{
    public interface IPlantService
    {
        List<MSPLANT> GetAll();
        MSPLANT Getdata(string plantId);
        MSPLANT Edit(string plantId, MSPLANT model);
        MSPLANT Add(MSPLANT model);
    }
}