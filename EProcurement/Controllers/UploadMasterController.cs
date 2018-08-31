using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;
using EProcurement.Services.Interface;
using EProcurement.Services.Implementation;
using EProcurement.Models;
using System.Collections.Generic;
using System.Linq;
using EProcurement.Extensions;

namespace EProcurement.Controllers
{
    public class UploadMasterController : Controller
    {
        HomeController general = new HomeController();
        // GET: Vendor
        public ActionResult Index()
        {
            return View("~/Views/Master/UploadMaster/Index.cshtml");
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile, CUSTOMFILEUPLOAD model, string DdlMasterType)
        {
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string tahun = DateTime.Now.Year.ToString();
                string bulan = DateTime.Now.Month.ToString();
                string tgl = DateTime.Now.Day.ToString();
                string jam = DateTime.Now.Hour.ToString();
                string menit = DateTime.Now.Minute.ToString();
                string detik = DateTime.Now.Second.ToString();

                string tanggalSekarang = "_" + tahun + bulan + tgl + jam + menit + detik;

                string type = "";

                if (DdlMasterType == "CUSTOMCOMPANY")
                {
                    type = "CUSTOMCOMPANY";
                }
                else if (DdlMasterType == "MSMATERIAL")
                {
                    type = "MSMATERIAL";
                }
                else if (DdlMasterType == "MSPLANT")
                {
                    type = "MSPLANT";
                }
                else if (DdlMasterType == "MSVENDOR")
                {
                    type = "MSVENDOR";
                }else if(DdlMasterType == "MSUSER")
                {
                      type = "MSUSER";
                }
                string FileName = type + tanggalSekarang + ".xls";
                filePath = path + FileName;

                try
                {
                    postedFile.SaveAs(filePath);

                    IUploadVehicleService svcUploadFilename = new UploadVehicleService();

                    model.ID = Convert.ToInt32(UploadVehicleService.GenerateID());
                    model.FILEUPLOAD = FileName;
                    model.MODIFIED_BY = "UserName";
                    model.MODIFIED_DATE = DateTime.Now;
                    svcUploadFilename.Add(model);

                    DataTable dt = postedFile.ToDataTable();

                    #region Insert

                    #region CUSTOMCOMPANY
                    if (type == "CUSTOMCOMPANY")
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            //cek row null atau tidak
                            if (row["COMPANYCODE"] != DBNull.Value)
                            {
                                var dc = new eprocdbDataContext();

                                //cek code ada atau tidak
                                var compCode = (from c in dc.CUSTOMCOMPANies
                                                where c.COMPANYCODE == row["COMPANYCODE"].ToString()
                                                select new
                                                {
                                                    c.COMPANYCODE
                                                }).ToList();

                                string code = string.Empty;

                                foreach (var value in compCode)
                                {
                                    code = value.COMPANYCODE;
                                }

                                //isi model customcompany sesuai row data table
                                CUSTOMCOMPANY objModelComp = new CUSTOMCOMPANY();

                                objModelComp.COMPANYCODE = row["COMPANYCODE"].ToString();
                                objModelComp.COMPANYNAME = row["COMPANYNAME"].ToString();
                                objModelComp.ALAMAT = row["ALAMAT"].ToString();
                                objModelComp.KOTA = row["KOTA"].ToString();
                                objModelComp.REGION = row["REGION"].ToString();
                                objModelComp.POSTALCODE = row["POSTALCODE"].ToString();
                                objModelComp.TELEPON = row["TELEPON"].ToString();
                                objModelComp.NPWP = row["NPWP"].ToString();
                                objModelComp.KTPTDP = row["KTPTDP"].ToString();
                                objModelComp.COMPANYCODETSO = row["COMPANYCODETSO"].ToString();

                                //jika ada update
                                if (code.ToString() != "")
                                {
                                    objModelComp.DIUBAHOLEH = Session["UserID"].ToString();
                                    objModelComp.DIUBAHTGL = DateTime.Now.Date;

                                    IUploadMasterService svc = new UploadMasterService();
                                    var result = svc.UpdateCompany(row["COMPANYCODE"].ToString(), objModelComp);
                                }

                                //jika tidak ada Insert
                                else
                                {
                                    objModelComp.DIBUATOLEH = Session["UserID"].ToString();
                                    objModelComp.DIBUATTGL = DateTime.Now.Date;

                                    IUploadMasterService svc = new UploadMasterService();
                                    var result = svc.AddCompany(objModelComp);
                                }
                            }
                        }
                    }
                    #endregion

                    #region MSMATERIAL
                    if (type == "MSMATERIAL")
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            //cek row null atau tidak
                            if (row["MATERIALNUMBER"] != DBNull.Value)
                            {
                                var dc = new eprocdbDataContext();

                                //cek code ada atau tidak
                                var materialNumber = (from c in dc.MSMATERIALs
                                                      where c.MATERIALNUMBER == row["MATERIALNUMBER"].ToString()
                                                      select new
                                                      {
                                                          c.MATERIALNUMBER
                                                      }).ToList();

                                string code = string.Empty;

                                foreach (var value in materialNumber)
                                {
                                    code = value.MATERIALNUMBER;
                                }

                                //isi model MSMATERIAL sesuai row data table
                                MSMATERIAL objModelMaterial = new MSMATERIAL();

                                objModelMaterial.MATERIALNUMBER = row["MATERIALNUMBER"].ToString();
                                //objModelMaterial.DESCRIPTION = row["DESCRIPTION"].ToString();
                                //objModelMaterial.MATERIALTYPE = row["MATERIALTYPE"].ToString();
                                //objModelMaterial.MATERIALGROUP = row["MATERIALGROUP"].ToString();
                                //objModelMaterial.OLDMATERIAL = row["OLDMATERIAL"].ToString();
                                //objModelMaterial.BOMMATERIAL = row["BOMMATERIAL"].ToString();
                                //objModelMaterial.BRAND = row["BRAND"].ToString();
                                //objModelMaterial.MODEL = row["MODEL"].ToString();
                                //objModelMaterial.GARDAN = row["GARDAN"].ToString();
                                //objModelMaterial.YEAR = row["YEAR"].ToString();
                                //objModelMaterial.PURCHASEGROUPID = row["PURCHASEGROUPID"].ToString();
                                objModelMaterial.MATERIALIDVENDOR = row["MATERIALIDVENDOR"].ToString();

                                //double price = 0;
                                //Double.TryParse(row["PRICELIST"].ToString(), out price);
                                //objModelMaterial.PRICELIST = price;

                                //jika ada update
                                if (code.ToString() != "")
                                {
                                    IUploadMasterService svc = new UploadMasterService();
                                    var result = svc.UpdateMaterial(row["MATERIALNUMBER"].ToString(), objModelMaterial);
                                }

                                //jika tidak ada Insert
                                else
                                {
                                    IUploadMasterService svc = new UploadMasterService();
                                    //var result = svc.AddMaterial(objModelMaterial);
                                }
                            }
                        }
                    }
                    #endregion

                    #region MSPLANT
                    if (type == "MSPLANT")
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            //cek row null atau tidak
                            if (row["PLANTID"] != DBNull.Value)
                            {
                                var dc = new eprocdbDataContext();

                                //cek code ada atau tidak
                                var plantID = (from c in dc.MSPLANTs
                                               where c.PLANTID == row["PLANTID"].ToString()
                                               select new
                                               {
                                                   c.PLANTID
                                               }).ToList();

                                string code = string.Empty;

                                foreach (var value in plantID)
                                {
                                    code = value.PLANTID;
                                }

                                //isi model MSPLANT sesuai row data table
                                MSPLANT objModelPlant = new MSPLANT();

                                objModelPlant.PLANTID = row["PLANTID"].ToString();
                                objModelPlant.NAME = row["NAME"].ToString();
                                objModelPlant.ALAMAT = row["ALAMAT"].ToString();
                                objModelPlant.KELURAHAN = row["KELURAHAN"].ToString();
                                objModelPlant.KECAMATAN = row["KECAMATAN"].ToString();
                                objModelPlant.CITY = row["CITY"].ToString();
                                objModelPlant.REGION = row["REGION"].ToString();
                                objModelPlant.CONTACTPERSONNAME = row["CONTACTPERSONNAME"].ToString();
                                objModelPlant.ALAMAT2 = row["ALAMAT2"].ToString();
                                objModelPlant.ALAMAT3 = row["ALAMAT3"].ToString();
                                objModelPlant.ALAMAT4 = row["ALAMAT4"].ToString();
                                objModelPlant.ALAMAT5 = row["ALAMAT5"].ToString();
                                objModelPlant.PROPINSI = row["PROPINSI"].ToString();
                                objModelPlant.PIC = row["PIC"].ToString();
                                objModelPlant.TITLECP = row["TITLECP"].ToString();
                                objModelPlant.NOHPCP = row["NOHPCP"].ToString();
                                objModelPlant.NOTELPPLANT = row["NOTELPPLANT"].ToString();
                                objModelPlant.POSTALCODE = row["POSTALCODE"].ToString();
                                objModelPlant.CUSTOMERNO = row["CUSTOMER_NUMBER"].ToString();
                                //jika ada update
                                if (code.ToString() != "")
                                {
                                    IUploadMasterService svc = new UploadMasterService();
                                    var result = svc.UpdatePlant(row["PLANTID"].ToString(), objModelPlant);
                                }

                                //jika tidak ada Insert
                                else
                                {
                                    IUploadMasterService svc = new UploadMasterService();
                                    var result = svc.AddPlant(objModelPlant);
                                }
                            }
                        }
                    }
                    #endregion

                    #region MSVENDOR
                    if (type == "MSVENDOR")
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            //cek row null atau tidak
                            if (row["VENDORID"] != DBNull.Value)
                            {
                                var dc = new eprocdbDataContext();

                                //cek code ada atau tidak
                                var vendorID = (from c in dc.MSVENDORs
                                                where c.VENDORID == row["VENDORID"].ToString()
                                                select new
                                                {
                                                    c.VENDORID
                                                }).ToList();

                                string code = string.Empty;

                                foreach (var value in vendorID)
                                {
                                    code = value.VENDORID;
                                }

                                //isi model MSVENDOR sesuai row data table
                                MSVENDOR objModelVendor = new MSVENDOR();

                                objModelVendor.VENDORID = row["VENDORID"].ToString();
                                //objModelVendor.VENDORNAME = row["VENDORNAME"].ToString();
                                //objModelVendor.STREET = row["STREET"].ToString();
                                //objModelVendor.DISTRIC = row["DISTRIC"].ToString();
                                //objModelVendor.CITY = row["CITY"].ToString();
                                //objModelVendor.POSTALCODE = row["POSTALCODE"].ToString();
                                //objModelVendor.TELEPHONE = row["TELEPHONE"].ToString();
                                //objModelVendor.EMAIL = row["EMAIL"].ToString();
                                //objModelVendor.EMAIL2 = row["EMAIL2"].ToString();
                                //objModelVendor.EMAIL3 = row["EMAIL3"].ToString();
                                //objModelVendor.EMAIL4 = row["EMAIL4"].ToString();
                                //objModelVendor.EMAIL5 = row["EMAIL5"].ToString();
                                //objModelVendor.EMAIL6 = row["EMAIL6"].ToString();
                                //objModelVendor.EMAIL7 = row["EMAIL7"].ToString();
                                //objModelVendor.EMAIL8 = row["EMAIL8"].ToString();
                                //objModelVendor.PLANTIDTSO = row["PLANTIDTSO"].ToString();
                                objModelVendor.B2B = row["ISB2B"].ToString() == "1" ? '1' : '0';

                                //jika ada update
                                if (code.ToString() != "")
                                {
                                    IUploadMasterService svc = new UploadMasterService();
                                    var result = svc.UpdateVendor(row["VENDORID"].ToString(), objModelVendor);
                                }

                                //jika tidak ada Insert
                                else
                                {
                                    //IUploadMasterService svc = new UploadMasterService();
                                    //var result = svc.AddVendor(objModelVendor);
                                }
                            }
                        }
                    }
                    #endregion

                    #region MSUSER
                    if (type == "MSUSER")
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            //cek row null atau tidak
                            if (row["UserId"] != DBNull.Value)
                            {
                                var dc = new eprocdbDataContext();

                                //cek code ada atau tidak
                                var UserId = (from c in dc.Master_Users
                                                where c.UserID == row["UserId"].ToString()
                                                select new
                                                {
                                                    c.UserID
                                                }).ToList();

                                string code = string.Empty;

                                foreach (var value in UserId)
                                {
                                    code = value.UserID;
                                }

                                //isi model MSVENDOR sesuai row data table
                                Master_User objModelUser = new Master_User();

                                objModelUser.UserID = row["UserId"].ToString();
                                objModelUser.FullName = row["NamaLengkap"].ToString();
                                objModelUser.GroupID = row["Group"].ToString();
                                objModelUser.id_vendor = row["IdVendor"].ToString();
                              

                                //jika ada update
                                if (code.ToString() != "")
                                {
                                    IUploadMasterService svc = new UploadMasterService();
                                    var result = svc.UpdateUser(row["UserId"].ToString(), objModelUser);
                                }

                                //jika tidak ada Insert
                                else
                                {
                                    IUploadMasterService svc = new UploadMasterService();
                                    var result = svc.AddUser(objModelUser);
                                }
                            }
                        }
                    }
                    #endregion

                    #endregion

                    if (filePath != null || filePath != string.Empty)
                    {
                        if ((System.IO.File.Exists(filePath)))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }

                    this.AddNotification("File uploaded successfully.", NotificationType.SUCCESS);
                }
                catch (Exception ex)
                {
                    general.AddLogError("Upload Master", ex.Message, ex.StackTrace);
                    this.AddNotification("File uploaded failed.", NotificationType.ERROR);
                    return View("~/Views/Master/UploadMaster/Index.cshtml");
                    throw ex;
                }
            }

            return View("~/Views/Master/UploadMaster/Index.cshtml");
        }

        public ActionResult DownloadFile(string fileName, string fileDownloadName)
        {
            string folder = "~/Templates/";
            string type = "application/xls";

            var sDocument = Server.MapPath(folder + fileName);
            if (!System.IO.File.Exists(sDocument))
            {
                return HttpNotFound();
            }

            return File(sDocument, type, fileDownloadName);
        }
    }
}