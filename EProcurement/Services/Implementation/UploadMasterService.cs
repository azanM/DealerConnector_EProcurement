using System.Linq;
using EProcurement.Models;
using EProcurement.Services.Interface;
using System;
using System.Data;

namespace EProcurement.Services.Implementation
{
    public class UploadMasterService : IUploadMasterService
    {
        public CUSTOMCOMPANY AddCompany(CUSTOMCOMPANY model)
        {
            var dc = new eprocdbDataContext();
            dc.CUSTOMCOMPANies.InsertOnSubmit(model);
            dc.SubmitChanges();
            return model;
        }

        public CUSTOMCOMPANY UpdateCompany(string CompCode, CUSTOMCOMPANY model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.CUSTOMCOMPANies
                      where c.COMPANYCODE == CompCode
                      select c).SingleOrDefault();

            md.COMPANYCODE = model.COMPANYCODE;
            md.COMPANYNAME = model.COMPANYNAME;
            md.ALAMAT = model.ALAMAT;
            md.KOTA = model.KOTA;
            md.REGION = model.REGION;
            md.POSTALCODE = model.POSTALCODE;
            md.TELEPON = model.TELEPON;
            md.NPWP = model.NPWP;
            md.KTPTDP = model.KTPTDP;
            md.COMPANYCODETSO = model.COMPANYCODETSO;
            md.DIUBAHOLEH = model.DIUBAHOLEH;
            md.DIUBAHTGL = model.DIUBAHTGL;
            dc.SubmitChanges();

            return model;
        }

        public MSMATERIAL AddMaterial(MSMATERIAL model)
        {
            var dc = new eprocdbDataContext();
            dc.MSMATERIALs.InsertOnSubmit(model);
            dc.SubmitChanges();
            return model;
        }

        public MSMATERIAL UpdateMaterial(string id, MSMATERIAL model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.MSMATERIALs
                      where c.MATERIALNUMBER == id
                      select c).SingleOrDefault();

            //md.MATERIALNUMBER = model.MATERIALNUMBER;
            //md.DESCRIPTION = model.DESCRIPTION;
            //md.MATERIALTYPE = model.MATERIALTYPE;
            //md.MATERIALGROUP = model.MATERIALGROUP;
            //md.OLDMATERIAL = model.OLDMATERIAL;
            //md.BOMMATERIAL = model.BOMMATERIAL;
            //md.BRAND = model.BRAND;
            //md.MODEL = model.MODEL;
            //md.GARDAN = model.GARDAN;
            //md.YEAR = model.YEAR;
            //md.PURCHASEGROUPID = model.PURCHASEGROUPID;
            md.MATERIALIDVENDOR = model.MATERIALIDVENDOR;
            //md.PRICELIST = model.PRICELIST;
            dc.SubmitChanges();

            return model;
        }

        public MSPLANT AddPlant(MSPLANT model)
        {
            var dc = new eprocdbDataContext();
            dc.MSPLANTs.InsertOnSubmit(model);
            dc.SubmitChanges();
            return model;
        }

        public MSPLANT UpdatePlant(string id, MSPLANT model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.MSPLANTs
                      where c.PLANTID == id
                      select c).SingleOrDefault();

            md.PLANTID = model.PLANTID;
            md.NAME = model.NAME;
            md.ALAMAT = model.ALAMAT;
            md.KELURAHAN = model.KELURAHAN;
            md.KECAMATAN = model.KECAMATAN;
            md.CITY = model.CITY;
            md.REGION = model.REGION;
            md.CONTACTPERSONNAME = model.CONTACTPERSONNAME;
            md.ALAMAT2 = model.ALAMAT2;
            md.ALAMAT3 = model.ALAMAT3;
            md.ALAMAT4 = model.ALAMAT4;
            md.ALAMAT5 = model.ALAMAT5;
            md.PROPINSI = model.PROPINSI;
            md.PIC = model.PIC;
            md.TITLECP = model.TITLECP;
            md.NOHPCP = model.NOHPCP;
            md.NOTELPPLANT = model.NOTELPPLANT;
            md.POSTALCODE = model.POSTALCODE;
            md.CUSTOMERNO = model.CUSTOMERNO;
            dc.SubmitChanges();

            return model;
        }

        public MSVENDOR AddVendor(MSVENDOR model)
        {
            var dc = new eprocdbDataContext();
            dc.MSVENDORs.InsertOnSubmit(model);
            dc.SubmitChanges();
            return model;
        }

        public MSVENDOR UpdateVendor(string id, MSVENDOR model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.MSVENDORs
                      where c.VENDORID == id
                      select c).SingleOrDefault();

            md.VENDORID = model.VENDORID;
            //md.VENDORNAME = model.VENDORNAME;
            //md.STREET = model.STREET;
            //md.DISTRIC = model.DISTRIC;
            //md.CITY = model.CITY;
            //md.POSTALCODE = model.POSTALCODE;
            //md.TELEPHONE = model.TELEPHONE;
            //md.EMAIL = model.EMAIL;
            //md.EMAIL2 = model.EMAIL2;
            //md.EMAIL3 = model.EMAIL3;
            //md.EMAIL4 = model.EMAIL4;
            //md.EMAIL5 = model.EMAIL5;
            //md.EMAIL6 = model.EMAIL6;
            //md.EMAIL7 = model.EMAIL7;
            //md.EMAIL8 = model.EMAIL8;
            //md.PLANTIDTSO = model.PLANTIDTSO;
            md.B2B = model.B2B;
            dc.SubmitChanges();

            return model;
        }
        public Master_User AddUser(Master_User model)
        {
            IHashingService sec = new HashingService();
            var dc = new eprocdbDataContext();
            string Password = "dc123";
            string pwd = sec.CreatePasswordHash(Password);
            model.Password = pwd;
            dc.Master_Users.InsertOnSubmit(model);
            dc.SubmitChanges();
            return model;
        }
        public Master_User UpdateUser(string id, Master_User model)
        {
            var dc = new eprocdbDataContext();

            var md = (from c in dc.Master_Users
                      where c.UserID == id
                      select c).SingleOrDefault();

           
            md.FullName = model.FullName;
            md.GroupID = model.GroupID;
            md.id_vendor = model.id_vendor;
         
            dc.SubmitChanges();

            return model;
        }
    }
}