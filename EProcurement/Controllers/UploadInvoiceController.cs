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

namespace EProcurement.Controllers
{
    public class UploadInvoiceController : Controller
    {
        HomeController general = new HomeController();
        // GET: UploadInvoice
        public ActionResult Index()
        {
            return View("~/Views/Upload/UploadInvoice/Index.cshtml");
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
                    type = "Invoice";
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

                                #region CUSTOMIR
                                CUSTOMIR CUSTOMIR = new CUSTOMIR();
                                CUSTOMIR.NOAP = row["BPHNUMBER"].ToString();
                                var resultPO = svcUpldVehicle.UpdateCustomIR(row["PONUMBER"].ToString(), CUSTOMIR);
                                #endregion

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
                    general.AddLogError("Upload Vehicle", ex.Message, ex.StackTrace);
                    this.AddNotification("File uploaded failed.", NotificationType.ERROR);
                    return View("~/Views/Upload/UploadInvoice/Index.cshtml");
                    throw ex;
                }
            }

            return View("~/Views/Upload/UploadInvoice/Index.cshtml");
        }
    }
}