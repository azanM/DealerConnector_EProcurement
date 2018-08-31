using System.Collections.Generic;
using System.Linq;
using EProcurement.Models;

namespace EProcurement.Services
{
    public class RegionService : IRegionService
    {
        public List<CUSTOMREGION> GetAll()
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.CUSTOMREGIONs select c).ToList();
            return model;
        }
        public CUSTOMREGION Getdata(string regionId)
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.CUSTOMREGIONs where c.REGIONID == regionId select c).SingleOrDefault();
            return model;
        }
        public CUSTOMREGION Edit(string regionId, CUSTOMREGION model)
        {
            var dc = new eprocdbDataContext();
            var md = (from c in dc.CUSTOMREGIONs where c.REGIONID == regionId select c).SingleOrDefault();
            md.REGIONNAME = model.REGIONNAME;
            if (model.STATUS != null)
            {
                md.STATUS = model.STATUS;
            }else
            {
                md.STATUS = md.STATUS;
            }
            dc.SubmitChanges();
            return model;
        }
        public CUSTOMREGION Add(CUSTOMREGION model)
        {
            var dc = new eprocdbDataContext();
            dc.CUSTOMREGIONs.InsertOnSubmit(model);          
            dc.SubmitChanges();
            return model;
        }
    }
}