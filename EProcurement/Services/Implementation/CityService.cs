using System.Collections.Generic;
using System.Linq;
using EProcurement.Models;

namespace EProcurement.Services
{
    public class CityService : ICityService
    {
        public List<CUSTOMCITY> GetAll()
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.CUSTOMCITies select c).ToList();
            return model;
        }
        public CUSTOMCITY Getdata(string cityId)
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.CUSTOMCITies where c.CITYID == cityId select c).SingleOrDefault();
            return model;
        }
        public CUSTOMCITY Edit(string cityId, CUSTOMCITY model)
        {
            var dc = new eprocdbDataContext();
            var md = (from c in dc.CUSTOMCITies where c.CITYID == cityId select c).SingleOrDefault();
            md.CITYNAME = model.CITYNAME;
            if (model.STATUS == null)
            {
                md.STATUS = md.STATUS;
            }else
            {
                md.STATUS = model.STATUS;
            }
            dc.SubmitChanges();
            return model;
        }
        public CUSTOMCITY Add(CUSTOMCITY model)
        {
            var dc = new eprocdbDataContext();
            dc.CUSTOMCITies.InsertOnSubmit(model);          
            dc.SubmitChanges();
            return model;
        }
    }
}