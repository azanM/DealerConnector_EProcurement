using System.Collections.Generic;
using EProcurement.Models;

namespace EProcurement.Services
{
    public interface IRegionService
    {
        List<CUSTOMREGION> GetAll();
        CUSTOMREGION Getdata(string cityId);
        CUSTOMREGION Edit(string cityId, CUSTOMREGION model);
        CUSTOMREGION Add(CUSTOMREGION model);
    }
}