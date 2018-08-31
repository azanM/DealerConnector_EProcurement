using EProcurement.Models;
using EProcurement.Services.Implementation;
using EProcurement.Services.Interface;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;
using EProcurement.Extensions;
using EProcurement.Models.ViewModel;
using System.Collections.Generic;

namespace EProcurement.Controllers
{
    public class UploadVendorController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            return View("~/Views/Upload/Vendor/Index.cshtml");
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

        public static DateTime? TryParseNull(string stringDate)
        {
            DateTime date;
            return DateTime.TryParse(stringDate, out date) ? date : (DateTime?)null;
        }
        public static Double? TryParseNullDouble(string stringDouble)
        {
            Double angka;
            return Double.TryParse(stringDouble, out angka) ? angka : (Double?)null;
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile, CUSTOMFILEUPLOAD model, string DdlForm)
        {
            string filePath = string.Empty;
            List<ErrorUploadViewModel> errorUpload = new List<ErrorUploadViewModel>();
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

                if (DdlForm == "1")
                {
                    type = "Delivery";
                }
                else if (DdlForm == "2")
                {
                    type = "Invoice";
                }
                else if (DdlForm == "3")
                {
                    type = "BPKB";
                }

                string FileName = type + tanggalSekarang + ".xls";
                filePath = path + FileName;

                try
                {
                    postedFile.SaveAs(filePath);

                    IUploadVehicleService svc = new UploadVehicleService();

                    model.ID = Convert.ToInt32(UploadVehicleService.GenerateID());
                    model.FILEUPLOAD = FileName;
                    model.MODIFIED_BY = "namaUser";
                    model.MODIFIED_DATE = DateTime.Now.Date;

                    var result = svc.Add(model);

                    DataTable dt = postedFile.ToDataTable();

                    #region Insert

                    #region Delivery
                    if (type == "Delivery")
                    {
                        int i = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            IUploadVendorService svcUpldVendor = new UploadVendorService();
                            bool AutheticateData = svcUpldVendor.CheckAutheicatePoNumberByVendorID(row["PONUMBER"].ToString(), System.Web.HttpContext.Current.Session["VendorID"].ToString());
                            //cek row null atau tidak
                            if (row["PONUMBER"] != DBNull.Value)
                            {

                                if (!svcUpldVendor.IfExist(row["PONUMBER"].ToString()))
                                {
                                    //create new model
                                    ErrorUploadViewModel error = new ErrorUploadViewModel();
                                    error.row = i;
                                    error.column = "PONUMBER";
                                    error.message = "PONUMBER doesn't exis";
                                    errorUpload.Add(error);
                                }
                                else
                                {
                                    if (System.Web.HttpContext.Current.Session["GroupID"].ToString() == "Super" || System.Web.HttpContext.Current.Session["GroupID"].ToString() == "PROCUREMENT" || !AutheticateData)
                                    {
                                        //create new model
                                        ErrorUploadViewModel error = new ErrorUploadViewModel();
                                        error.row = i;
                                        error.column = "PONUMBER";
                                        error.message = "PONUMBER AND VENDOR ID NOT MATCH";
                                        errorUpload.Add(error);
                                    }
                                    else
                                    {
                                        var dc = new eprocdbDataContext();

                                        #region CUSTOMPO
                                        CUSTOMPO customPO = new CUSTOMPO();
                                        customPO.HARGADPP_INPUT = TryParseNullDouble(row["HARGADPP_INPUT"].ToString());
                                        customPO.HARGAPPNUNIT_INPUT = TryParseNullDouble(row["HARGAPPNUNIT_INPUT"].ToString()); ;
                                        customPO.HARGABBN_INPUT = TryParseNullDouble(row["HARGABBN_INPUT"].ToString());

                                        customPO.DODATE = TryParseNull(row["DODATE"].ToString());

                                        customPO.DONUMBER = row["DONUMBER"].ToString();
                                        customPO.CARROSERIEVENDORNAME = row["CARROSERIEVENDORNAME"].ToString();
                                        customPO.ACCESORIESADDRESS = row["ACCESORIESADDRESS"].ToString();
                                        customPO.REMARKSCARROSSERIE = row["REMARKSCARROSSERIE"].ToString();

                                        customPO.TGLMASUKKAROSERI = TryParseNull(row["TGLMASUKKAROSERI"].ToString());
                                        customPO.TGLSELESAIKAROSERI = TryParseNull(row["TGLSELESAIKAROSERI"].ToString());

                                        customPO.REMARKS = row["REMARKS"].ToString();
                                        customPO.MODIFIED_BY = Session["UserID"].ToString();
                                        customPO.MODIFIED_DATE = DateTime.Now.Date;

                                        customPO.ACTUALDATEDELIVEREDUNIT = TryParseNull(row["TANGGALBSTB"].ToString());

                                        var resultPO = svcUpldVendor.UpdateDeliveryCustPO(row["PONUMBER"].ToString(), customPO);
                                        #endregion

                                        #region CUSTOMBPKB
                                        CUSTOMBPKB customBPKB = new CUSTOMBPKB();

                                        customBPKB.NOPOLISI_INPUT = row["NOPOLISI_INPUT"].ToString();
                                        customBPKB.TGLSTNK_INPUT = TryParseNull(row["TGLSTNK_INPUT"].ToString());
                                        customBPKB.STCKDATE = TryParseNull(row["STCKDATE"].ToString());
                                        customBPKB.TGLSERAHBPKB = TryParseNull(row["TGLSERAHBPKB"].ToString());
                                        customPO.MODIFIED_BY = Session["UserID"].ToString();
                                        customPO.MODIFIED_DATE = DateTime.Now.Date;

                                        var resultBPKB = svcUpldVendor.UpdateDeliveryCustBPKB(row["PONUMBER"].ToString(), customBPKB);
                                        #endregion

                                        #region CUSTOMGR
                                        CUSTOMGR customGR = new CUSTOMGR();

                                        customGR.NOCHASIS_INPUT = row["NOCHASIS_INPUT"].ToString();
                                        customGR.NOENGINE_INPUT = row["NOENGINE_INPUT"].ToString();
                                        customPO.MODIFIED_BY = Session["UserID"].ToString();
                                        customPO.MODIFIED_DATE = DateTime.Now.Date;

                                        var resultGR = svcUpldVendor.UpdateDeliveryCustGR(row["PONUMBER"].ToString(), customGR);
                                        #endregion
                                    }
                                }
                            }
                        }
                        i++;
                    }

                    #endregion

                    #region Invoice
                    if (type == "Invoice")
                    {
                        int i = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            //cek row null atau tidak
                            if (row["PONUMBER"] != DBNull.Value)
                            {
                                IUploadVendorService svcUpldVendor = new UploadVendorService();
                                bool AutheticateData = svcUpldVendor.CheckAutheicatePoNumberByVendorID(row["PONUMBER"].ToString(), System.Web.HttpContext.Current.Session["VendorID"].ToString());

                                if (!svcUpldVendor.IfExist(row["PONUMBER"].ToString()))
                                {
                                    //create new model
                                    ErrorUploadViewModel error = new ErrorUploadViewModel();
                                    error.row = i;
                                    error.column = "PONUMBER";
                                    error.message = "PONUMBER doesn't exist";
                                    errorUpload.Add(error);
                                }
                                else
                                {
                                    if (System.Web.HttpContext.Current.Session["GroupID"].ToString() == "Super" || System.Web.HttpContext.Current.Session["GroupID"].ToString() == "PROCUREMENT" || !AutheticateData)
                                    {
                                        //create new model
                                        ErrorUploadViewModel error = new ErrorUploadViewModel();
                                        error.row = i;
                                        error.column = "PONUMBER";
                                        error.message = "PONUMBER AND VENDOR ID NOT MATCH";
                                        errorUpload.Add(error);
                                    }
                                    else
                                    {
                                        var dc = new eprocdbDataContext();

                                        #region CUSTOMPO
                                        CUSTOMPO customPO = new CUSTOMPO();
                                        customPO.TGLGI = TryParseNull(row["TGLGI"].ToString());
                                        customPO.MODIFIED_BY = Session["UserID"].ToString();
                                        customPO.MODIFIED_DATE = DateTime.Now.Date;

                                        var resultPO = svcUpldVendor.UpdateInvoiceCustPO(row["PONUMBER"].ToString(), customPO);

                                        #endregion

                                        #region CUSTOMBPKB
                                        CUSTOMBPKB customBpkb = new CUSTOMBPKB();

                                        customBpkb.NOFAKTUR = row["NOFAKTUR"].ToString();
                                        customBpkb.MODIFIED_BY = Session["UserID"].ToString();
                                        customBpkb.MODIFIED_DATE = DateTime.Now.Date;

                                        var resultBPKB = svcUpldVendor.UpdateInvoiceCustBpkb(row["PONUMBER"].ToString(), customBpkb);
                                        #endregion

                                        #region CUSTOMGR
                                        CUSTOMGR customGR = new CUSTOMGR();

                                        customGR.STATUSDELIVERYUNIT = row["STATUSDELIVERYUNIT"].ToString();
                                        customGR.MODIFIED_BY = Session["UserID"].ToString();
                                        customGR.MODIFIED_DATE = DateTime.Now.Date;

                                        var resultGR = svcUpldVendor.UpdateInvoiceCustGR(row["PONUMBER"].ToString(), customGR);
                                        #endregion

                                        #region CUSTOMIR
                                        CUSTOMIR customIR = new CUSTOMIR();

                                        customIR.INVNO = row["INVNO"].ToString();
                                        customIR.INVDATE = TryParseNull(row["INVDATE"].ToString());
                                        customIR.MODIFIED_BY = Session["UserID"].ToString();
                                        customIR.MODIFIED_DATE = DateTime.Now.Date;

                                        var resultIR = svcUpldVendor.UpdateInvoiceCustIR(row["PONUMBER"].ToString(), customIR);
                                        #endregion
                                    }
                                }
                            }
                            i++;
                        }
                    }
                    #endregion

                    #region Bpkb
                    if (type == "BPKB")
                    {
                        int i = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            //cek row null atau tidak
                            if (row["PONUMBER"] != DBNull.Value)
                            {
                                IUploadVendorService svcUpldVendor = new UploadVendorService();
                                bool AutheticateData = svcUpldVendor.CheckAutheicatePoNumberByVendorID(row["PONUMBER"].ToString(), System.Web.HttpContext.Current.Session["VendorID"].ToString());
                                if (!svcUpldVendor.IfExist(row["PONUMBER"].ToString()))
                                {
                                    //create new model
                                    ErrorUploadViewModel error = new ErrorUploadViewModel();
                                    error.row = i;
                                    error.column = "PONUMBER";
                                    error.message = "PONUMBER doesn't exist";
                                    errorUpload.Add(error);
                                }
                                else
                                {
                                    if (System.Web.HttpContext.Current.Session["GroupID"].ToString() == "Super" || System.Web.HttpContext.Current.Session["GroupID"].ToString() == "PROCUREMENT" || !AutheticateData)
                                    {
                                        //create new model
                                        ErrorUploadViewModel error = new ErrorUploadViewModel();
                                        error.row = i;
                                        error.column = "PONUMBER";
                                        error.message = "PONUMBER AND VENDOR ID NOT MATCH";
                                        errorUpload.Add(error);
                                    }
                                    else
                                    {


                                        var dc = new eprocdbDataContext();

                                        #region CUSTOMBPKB
                                        CUSTOMBPKB customBPKB = new CUSTOMBPKB();

                                        customBPKB.ACTUALDELIVERYBPKBTOFINANCE = TryParseNull(row["ACTUALDELIVERYBPKBTOFINANCE"].ToString());
                                        customBPKB.ACTUALRECEIVEDBPKBDICAB = TryParseNull(row["ACTUALRECEIVEDBPKBDICAB"].ToString());
                                        customBPKB.KETBPKB = row["KETBPKB"].ToString();
                                        customBPKB.NOBPKB = row["NOBPKB"].ToString();
                                        customBPKB.KETSURATUBAHBENTUK = row["KETSURATUBAHBENTUK"].ToString();
                                        customBPKB.KETERANGANSURATRUBAHWARNA = row["KETERANGANSURATRUBAHWARNA"].ToString();
                                        customBPKB.NOFAKTUR = row["NOFAKTUR"].ToString();
                                        customBPKB.TGLFAKTUR = TryParseNull(row["TGLFAKTUR"].ToString());
                                        customBPKB.NOSERTIFIKAT = row["NOSERTIFIKAT"].ToString();
                                        customBPKB.TGLSERTIFIKAT = TryParseNull(row["TGLSERTIFIKAT"].ToString());
                                        customBPKB.NOFORMULIRA = row["NOFORMULIRA"].ToString();
                                        customBPKB.TGLFORMULIRA = TryParseNull(row["TGLFORMULIRA"].ToString());
                                        customBPKB.NOSURATUBAHBENTUK = row["NOSURATUBAHBENTUK"].ToString();
                                        customBPKB.TGLSURATUBAHBENTUK = TryParseNull(row["TGLSURATUBAHBENTUK"].ToString());
                                        customBPKB.NOSURATRUBAHWARNA = row["NOSURATRUBAHWARNA"].ToString();
                                        customBPKB.TANGGALSURATRUBAHWARNA = TryParseNull(row["TANGGALSURATRUBAHWARNA"].ToString());
                                        customBPKB.NOSERTIFIKATREGUJITIPE = row["NOSERTIFIKATREGUJITIPE"].ToString();
                                        customBPKB.ACTUALRECEIVEDBPKBDIHO = TryParseNull(row["ACTUALRECEIVEDBPKBDIHO"].ToString());
                                        customBPKB.ACTUALDELIVERYBPKBTOFINANCE = TryParseNull(row["ACTUALDELIVERYBPKBTOFINANCE"].ToString());
                                        customBPKB.ACTUALRECEIVEDBPKBHOBACK = TryParseNull(row["ACTUALRECEIVEDBPKBHOBACK"].ToString());
                                        customBPKB.DATEDELIVERYTOBRANCHORVENDOR = TryParseNull(row["DATEDELIVERYTOBRANCHORVENDOR"].ToString());
                                        customBPKB.REMARKSDETAILPROBLEM = row["REMARKSDETAILPROBLEM"].ToString();
                                        customBPKB.MODIFIED_BY = Session["UserID"].ToString();
                                        customBPKB.MODIFIED_DATE = DateTime.Now.Date;

                                        var resultBPKB = svcUpldVendor.UpdateBpkbCustBPKB(row["PONUMBER"].ToString(), customBPKB);
                                        #endregion
                                    }
                                }
                            }
                            i++;
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
                    if (errorUpload.Count > 0)
                        this.AddNotification("File uploaded failed.", NotificationType.ERROR);
                    else
                        this.AddNotification("File uploaded successfully.", NotificationType.SUCCESS);
                }
                catch (Exception ex)
                {
                    general.AddLogError("Upload Vendor", ex.Message, ex.StackTrace);
                    this.AddNotification("File uploaded failed.", NotificationType.ERROR);
                    return View("~/Views/Upload/Vendor/Index.cshtml");
                    throw ex;
                }
            }

            IEnumerable<ErrorUploadViewModel> eu = errorUpload;
            return View("~/Views/Upload/Vendor/Index.cshtml", eu);
        }
    }
}