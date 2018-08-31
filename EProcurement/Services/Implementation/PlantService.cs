using System.Collections.Generic;
using System.Linq;
using EProcurement.Models;

namespace EProcurement.Services
{
    public class PlantService : IPlantService
    {
        public List<MSPLANT> GetAll()
        {
            var dc = new eprocdbDataContext();
            var model = dc.MSPLANTs.OrderByDescending(a => a.PLANTID).ToList();
            return model;
        }
        public MSPLANT Getdata(string plantId)
        {
            var dc = new eprocdbDataContext();
            var model = (from c in dc.MSPLANTs where c.PLANTID == plantId select c).SingleOrDefault();
            return model;
        }
        public MSPLANT Edit(string plantId, MSPLANT model)
        {
            var dc = new eprocdbDataContext();
            var md = (from c in dc.MSPLANTs where c.PLANTID == plantId select c).SingleOrDefault();
            md.NAME = model.NAME;
            md.ALAMAT = model.ALAMAT;
            md.ALAMAT2 = model.ALAMAT2;
            md.ALAMAT3 = model.ALAMAT3;
            md.ALAMAT4 = model.ALAMAT4;
            md.ALAMAT5 = model.ALAMAT5;
            md.KELURAHAN = model.KELURAHAN;
            md.KECAMATAN = model.KECAMATAN;
            md.CITY = model.CITY;
            md.PROPINSI = model.PROPINSI;
            md.REGION = model.REGION;
            md.NOTELPPLANT = model.NOTELPPLANT;
            md.CONTACTPERSONNAME = model.CONTACTPERSONNAME;
            if (model.TITLECP == null)
            {
                md.TITLECP = md.TITLECP;
            }
            else
            {
                md.TITLECP = model.TITLECP;
            }
            
            md.NOHPCP = model.NOHPCP;
            md.PIC = model.PIC;
            md.POSTALCODE = model.POSTALCODE;
            md.CUSTOMERNO = model.CUSTOMERNO;
            dc.SubmitChanges();
            return model;
        }
        public MSPLANT Add(MSPLANT model)
        {
            var dc = new eprocdbDataContext();
            MSPLANT md = new MSPLANT();
            md.PLANTID = model.PLANTID;
            md.NAME = model.NAME;
            md.ALAMAT = model.ALAMAT;
            md.ALAMAT2 = model.ALAMAT2;
            md.ALAMAT3 = model.ALAMAT3;
            md.ALAMAT4 = model.ALAMAT4;
            md.ALAMAT5 = model.ALAMAT5;
            md.KELURAHAN = model.KELURAHAN;
            md.KECAMATAN = model.KECAMATAN;
            md.CITY = model.CITY;
            md.PROPINSI = model.PROPINSI;
            md.REGION = model.REGION;
            md.NOTELPPLANT = model.NOTELPPLANT;
            md.CONTACTPERSONNAME = model.CONTACTPERSONNAME;
            md.TITLECP = model.TITLECP;
            md.NOHPCP = model.NOHPCP;
            md.PIC = model.PIC;
            md.POSTALCODE = model.POSTALCODE;
            md.CUSTOMERNO = model.CUSTOMERNO;
            dc.MSPLANTs.InsertOnSubmit(md);
            dc.SubmitChanges();
            return model;
        }
    }
}