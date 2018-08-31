using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.Mvc;
using EProcurement.Services.Interface;
using EProcurement.Services.Implementation;
using EProcurement.Models;
using EProcurement.Extensions;
using System.Linq;
using System.Text;

namespace EProcurement.Controllers
{
    public class UploadVehicleController : Controller
    {
        HomeController general = new HomeController();
        public ActionResult Index()
        {
            return View("~/Views/Upload/Vehicle/Index.cshtml");
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
        public static Int32? TryParseNullInt(string stringInt)
        {
            Int32 angka;
            return Int32.TryParse(stringInt, out angka) ? angka : (Int32?)null;
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile, CUSTOMFILEUPLOAD model, string DdlForm)
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
                string errorPoNumber = string.Empty;
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
                        foreach (DataRow row in dt.Rows)
                        {
                            //cek row null atau tidak
                            if (row["PONUMBER"] != DBNull.Value)
                            {
                                var dc = new eprocdbDataContext();

                                IUploadVehicleService svcUpldVehicle = new UploadVehicleService();

                                #region CUSTOMPO
                                CUSTOMPO customPO = new CUSTOMPO();

                                customPO.PURCHASESTATUS = row["PURCHASESTATUS"].ToString();
                                customPO.CARROSERIEVENDORNAME = row["CARROSERIEVENDORNAME"].ToString();
                                customPO.ACCESORIESADDRESS = row["ACCESORIESADDRESS"].ToString();
                                customPO.REMARKSCARROSSERIE = row["REMARKSCARROSSERIE"].ToString();
                                customPO.PICKAROSERI = row["PICKAROSERI"].ToString();
                                customPO.NOTELEPONKAROSERI = row["NOTELEPONKAROSERI"].ToString();
                                customPO.BENTUKKAROSERI = row["BENTUKKAROSERI"].ToString();

                                customPO.PROMISEDLVDATEPO = TryParseNull(row["PROMISEDLVDATEPO"].ToString());
                                customPO.TGLSELESAIKAROSERI = TryParseNull(row["PROMISEDLVDATEPO"].ToString());

                                //DateTime BSTB;
                                //DateTime.TryParse(row["TANGGALBSTB"].ToString(), out BSTB);
                                //customPO.ACTUALDATEDELIVEREDUNIT = BSTB;

                                customPO.REMARKS = row["REMARKS"].ToString();
                                customPO.MODIFIED_BY = Session["UserID"].ToString();
                                customPO.MODIFIED_DATE = DateTime.Now.Date;

                                var resultPO = svcUpldVehicle.UpdateDeliveryCustPO(row["PONUMBER"].ToString(), customPO);
                                #endregion
                            }
                        }
                    }
                    #endregion

                    #region Invoice
                    if (type == "Invoice")
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            //cek row null atau tidak
                            if (row["PONUMBER"] != DBNull.Value)
                            {
                                var dc = new eprocdbDataContext();

                                IUploadVehicleService svcUpldVehicle = new UploadVehicleService();

                                #region CUSTOMPO
                                CUSTOMPO customPO = new CUSTOMPO();
                                customPO.TGLMASUKKAROSERI = TryParseNull(row["TGLMASUKKAROSERI"].ToString());
                                customPO.DONUMBER = row["DONUMBER"].ToString();
                                customPO.DODATE = TryParseNull(row["DODATE"].ToString());
                                customPO.REASONREVISEDBYVENDOR = row["REASONREVISEDBYVENDOR"].ToString();
                                customPO.ACTUALDATEDELIVEREDUNIT = TryParseNull(row["TANGGALBSTB"].ToString());
                                customPO.MODIFIED_BY = Session["UserID"].ToString();
                                customPO.MODIFIED_DATE = DateTime.Now.Date;

                                var resultPO = svcUpldVehicle.UpdateInvoiceCustPO(row["PONUMBER"].ToString(), customPO);
                                #endregion
                                /*
                                #region CUSTOMBPKB
                                CUSTOMBPKB customBpkb = new CUSTOMBPKB();

                                customBpkb.NOFAKTUR = row["NOFAKTUR"].ToString();
                                customBpkb.MODIFIED_BY = Session["UserID"].ToString();
                                customBpkb.MODIFIED_DATE = DateTime.Now.Date;

                                var resultBPKB = svcUpldVehicle.UpdateInvoiceCustBpkb(row["PONUMBER"].ToString(), customBpkb);
                                #endregion

                                #region CUSTOMGR
                                CUSTOMGR customGR = new CUSTOMGR();

                                customGR.NOCHASIS_INPUT = row["NOCHASIS_INPUT"].ToString();
                                customGR.NOENGINE_INPUT = row["NOENGINE_INPUT"].ToString();
                                customGR.ACTUALRECEIVEDUNITINBDEL = TryParseNull(row["ACTUALRECEIVEDUNITINBDEL"].ToString());
                                customGR.MODIFIED_BY = Session["UserID"].ToString();
                                customGR.MODIFIED_DATE = DateTime.Now.Date;

                                var resultGR = svcUpldVehicle.UpdateInvoiceCustGR(row["PONUMBER"].ToString(), customGR);
                                #endregion

                                #region CUSTOMIR
                                CUSTOMIR customIR = new CUSTOMIR();

                                customIR.NOFAKTURPAJAK = row["NOFAKTURPAJAK"].ToString();
                                customIR.TERMOFPAYMENT = TryParseNullInt(row["TERMOFPAYMENT"].ToString());
                                customIR.TGLSERAHTAGIHAN = TryParseNull(row["TGLSERAHTAGIHAN"].ToString());
                                customIR.KETBAYAR = row["KETBAYAR"].ToString();
                                customIR.KETTAGIHAN = row["KETTAGIHAN"].ToString();
                                customIR.MODIFIED_BY = Session["UserID"].ToString();
                                customIR.MODIFIED_DATE = DateTime.Now.Date;

                                var resultIR = svcUpldVehicle.UpdateInvoiceCustIR(row["PONUMBER"].ToString(), customIR);
                                #endregion
                                */
                            }
                        }
                    }
                    #endregion

                    #region Bpkb
                    string Message = string.Empty;
                    StringBuilder listPONumber = new StringBuilder();
                    if (type == "BPKB")
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            //cek row null atau tidak
                            if (row["PONUMBER"] != DBNull.Value)
                            {
                                var dc = new eprocdbDataContext();                               
                                var md = (from c in dc.CUSTOMBPKBs
                                          where c.PONUMBER == row["PONUMBER"].ToString().Trim()
                                          select c).SingleOrDefault();
                                if (md != null)
                                {
                                    IUploadVehicleService svcUpldVehicle = new UploadVehicleService();

                                    #region CUSTOMBPKB
                                    CUSTOMBPKB customBPKB = new CUSTOMBPKB();

                                    customBPKB.ACTUALRECEIVEDBPKBDIHO = TryParseNull(row["ACTUALRECEIVEDBPKBDIHO"].ToString());

                                    customBPKB.ACTUALRECEIVEDBPKBDICAB = TryParseNull(row["ACTUALRECEIVEDBPKBDICAB"].ToString());

                                    customBPKB.DETAILPROBLEM = row["DETAILPROBLEM"].ToString();
                                    customBPKB.REASONREVISEBPKB = row["REASONREVISEBPKB"].ToString();

                                    customBPKB.TGLSERAHBPKB = TryParseNull(row["TGLSERAHBPKB"].ToString());

                                    customBPKB.ACTUALDELIVERYBPKBTOFINANCE = TryParseNull(row["ACTUALDELIVERYBPKBTOFINANCE"].ToString());

                                    customBPKB.ACTUALRECEIVEDBPKBHOBACK = TryParseNull(row["ACTUALRECEIVEDBPKBHOBACK"].ToString());

                                    customBPKB.DATEDELIVERYTOBRANCHORVENDOR = TryParseNull(row["DATEDELIVERYTOBRANCHORVENDOR"].ToString());

                                    customBPKB.NOBPKB = row["NOBPKB"].ToString();
                                    customBPKB.NOFAKTUR = row["NOFAKTUR"].ToString();
                                    customBPKB.NOSERTIFIKAT = row["NOSERTIFIKAT"].ToString();
                                    customBPKB.NOFORMULIRA = row["NOFORMULIRA"].ToString();
                                    customBPKB.KETSURATUBAHBENTUK = row["KETSURATUBAHBENTUK"].ToString();
                                    customBPKB.NOSURATUBAHBENTUK = row["NOSURATUBAHBENTUK"].ToString();

                                    customBPKB.TGLSURATUBAHBENTUK = TryParseNull(row["TGLSURATUBAHBENTUK"].ToString());

                                    customBPKB.KETERANGANSURATRUBAHWARNA = row["KETERANGANSURATRUBAHWARNA"].ToString();
                                    customBPKB.NOSURATRUBAHWARNA = row["NOSURATRUBAHWARNA"].ToString();

                                    customBPKB.TANGGALSURATRUBAHWARNA = TryParseNull(row["TANGGALSURATRUBAHWARNA"].ToString());

                                    customBPKB.NOSERTIFIKATREGUJITIPE = row["NOSERTIFIKATREGUJITIPE"].ToString();
                                    customBPKB.REMARKSDETAILPROBLEM = row["REMARKSDETAILPROBLEM"].ToString();
                                    customBPKB.KETBPKB = row["KETBPKB"].ToString();
                                    customBPKB.MODIFIED_BY = Session["UserID"].ToString();
                                    customBPKB.MODIFIED_DATE = DateTime.Now.Date;

                                    var resultBPKB = svcUpldVehicle.UpdateBpkbCustBPKB(row["PONUMBER"].ToString(), customBPKB);
                                    #endregion
                                }
                                else
                                {
                                    Message = "PONUMBER doesn't exist ";
                                    listPONumber.Append(row["PONUMBER"].ToString()).Append(", ");                        
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
                    if (!string.IsNullOrEmpty(Message))
                    {
                        this.AddNotification("File uploaded failed. " + Message + listPONumber, NotificationType.ERROR);
                    }
                    else
                    {
                        this.AddNotification("File uploaded successfully.", NotificationType.SUCCESS);
                    }
                }
                catch (Exception ex)
                {
                    general.AddLogError("Upload Vehicle", ex.Message, ex.StackTrace);
                    this.AddNotification("File uploaded failed.", NotificationType.ERROR);
                    this.AddNotification("PONumber Tidak ditemukan", NotificationType.ERROR);
                    return View("~/Views/Upload/Vehicle/Index.cshtml");
                    throw ex;
                }
            }

            return View("~/Views/Upload/Vehicle/Index.cshtml");
        }
    }
}