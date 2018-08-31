using System.Collections.Generic;
using System.Linq;
using EProcurement.Models;

namespace EProcurement.Services
{
    public class MaterialService : IMaterialService
    {
        public List<MSMATERIAL> GetAll()
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.MSMATERIALs select c).ToList();//.OrderBy(p => p.MATERIALNUMBER).Take(500).ToList();
            return model;
        }
        public MSMATERIAL Getdata(string materialNumber)
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.MSMATERIALs where c.MATERIALNUMBER == materialNumber select c).SingleOrDefault();
            return model;
        }
        public MSMATERIAL Edit(string materialNumber, MSMATERIAL model)
        {
            var dc = new eprocdbDataContext();
            var md = (from c in dc.MSMATERIALs where c.MATERIALNUMBER == materialNumber select c).SingleOrDefault();
            md.MATERIALIDVENDOR = model.MATERIALIDVENDOR;
            md.MATERIALTYPE = model.MATERIALTYPE;
            md.MATERIALGROUP = model.MATERIALGROUP;
            md.OLDMATERIAL = model.OLDMATERIAL;
            md.BOMMATERIAL = model.BOMMATERIAL;
            md.BRAND = model.BRAND;
            md.MODEL = model.MODEL;
            md.GARDAN = model.GARDAN;
            md.YEAR = model.YEAR;
            md.PURCHASEGROUPID = model.PURCHASEGROUPID;
            md.DESCRIPTION = model.DESCRIPTION;
            md.PRICELIST = model.PRICELIST;
            dc.SubmitChanges();
            return model;
        }
        public MSMATERIAL Add(MSMATERIAL model)
        {
            var dc = new eprocdbDataContext();
            dc.MSMATERIALs.InsertOnSubmit(model);
            dc.SubmitChanges();
            return model;
        }
    }
}