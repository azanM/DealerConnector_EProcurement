using EProcurement.Models;
using EProcurement.Services;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using EProcurement.Extensions;
using System;
using ClosedXML.Excel;

namespace EProcurement.Controllers
{
    public class PlantController : Controller
    {
        HomeController general = new HomeController();
        // GET: Vendor
        public ActionResult Index()
        {
            IPlantService svc = new PlantService();
            var model = svc.GetAll();
            return View("~/Views/Master/Plant/Index.cshtml", model);
        }

        public ActionResult Add()
        {
            this.ViewBag.cityId = GetCity();
            this.ViewBag.PROPINSI = new SelectList(this.GetPropinsi(), "Key", "Value");
            this.ViewBag.regionId = GetRegion();
            this.ViewBag.TITLECP = new SelectList(this.GetCpTitle(), "Key", "Value");
            return View("~/Views/Master/Plant/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Add(MSPLANT model)
        {
            try
            {
                IPlantService svc = new PlantService();
                var result = svc.Add(model);
                this.AddNotification("Your Data Has Been Successfully Saved. ", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Plant Add", ex.Message, ex.StackTrace);
                this.AddNotification("ID exist", NotificationType.ERROR);
                this.ViewBag.cityId = GetCity();
                this.ViewBag.PROPINSI = new SelectList(this.GetPropinsi(), "Key", "Value");
                this.ViewBag.regionId = GetRegion();
                this.ViewBag.TITLECP = new SelectList(this.GetCpTitle(), "Key", "Value");
                return View("~/Views/Master/Plant/Add.cshtml");
            }
        }

        public ActionResult Edit(string plantId)
        {
            this.ViewBag.cityId = GetCity();
            this.ViewBag.PROPINSI = new SelectList(this.GetPropinsi(), "Key", "Value");
            this.ViewBag.regionId = GetRegion();
            this.ViewBag.TITLECP = new SelectList(this.GetCpTitle(), "Key", "Value");

            IPlantService svc = new PlantService();
            var model = svc.Getdata(plantId);
            return View("~/Views/Master/Plant/Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult Edit(string plantId, MSPLANT model)
        {
            try
            {
                IPlantService svc = new PlantService();
                var result = svc.Edit(plantId, model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                general.AddLogError("Plant Edit", ex.Message, ex.StackTrace);
                return View("~/Views/Master/Plant/Index.cshtml");
            }
        }

        protected static SelectList GetCity()
        {
            eprocdbDataContext db = new eprocdbDataContext();

            var data = from a in db.CUSTOMCITies
                       select new
                       {
                           CITY = a.CITYID,
                           CITYNAME = a.CITYNAME
                       };

            SelectList selectList = new SelectList(data, "CITY", "CITYNAME");

            return selectList;
        }

        protected static SelectList GetRegion()
        {
            eprocdbDataContext db = new eprocdbDataContext();

            var data = from a in db.CUSTOMREGIONs
                       select new
                       {
                           REGION = a.REGIONID,
                           REGIONNAME = a.REGIONNAME
                       };

            SelectList selectList = new SelectList(data, "REGION", "REGIONNAME");

            return selectList;
        }

        protected Dictionary<string, string> GetCpTitle()
        {
            var result = new Dictionary<string, string>();

            result.Add("Mr.", "Mr.");
            result.Add("Mrs.", "Mrs.");
            result.Add("Ms.", "Ms.");

            return result;
        }

        protected Dictionary<string, string> GetPropinsi()
        {
            var result = new Dictionary<string, string>();

            result.Add("Aceh", "Aceh");
            result.Add("Bali", "Bali");
            result.Add("Banten", "Banten");
            result.Add("Bengkulu", "Bengkulu");
            result.Add("Gorontalo", "Gorontalo");
            result.Add("Irian Jaya Barat", "Irian Jaya Barat");
            result.Add("Jakarta Raya", "Jakarta Raya");
            result.Add("Jambi", "Jambi");
            result.Add("Jawa Barat", "Jawa Barat");
            result.Add("Jawa Tengah", "Jawa Tengah");
            result.Add("Jawa Timur", "Jawa Timur");
            result.Add("Kalimantan Barat", "Kalimantan Barat");
            result.Add("Kalimantan Selatan", "Kalimantan Selatan");
            result.Add("Kalimantan Tengah", "Kalimantan Tengah");
            result.Add("Kalimantan Timur", "Kalimantan Timur");
            result.Add("Kepulauan Bangka Belitung", "Kepulauan Bangka Belitung");
            result.Add("Kepulauan Riau", "Kepulauan Riau");
            result.Add("Lampung", "Lampung");
            result.Add("Maluku", "Maluku");
            result.Add("Maluku Utara", "Maluku Utara");
            result.Add("Nusa Tenggara Barat", "Nusa Tenggara Barat");
            result.Add("Nusa Tenggara Timur", "Nusa Tenggara Timur");
            result.Add("Papua", "Papua");
            result.Add("Riau", "Riau");
            result.Add("Sulawesi Barat", "Sulawesi Barat");
            result.Add("Sulawesi Tengah", "Sulawesi Tengah");
            result.Add("Sulawesi Tenggara", "Sulawesi Tenggara");
            result.Add("Sulawesi Utara", "Sulawesi Utara");
            result.Add("Sumatera Barat", "Sumatera Barat");
            result.Add("Sumatera Selatan", "Sumatera Selatan");
            result.Add("Sumatera Utara", "Sumatera Utara");
            result.Add("Yogyakarta", "Yogyakarta");

            return result;
        }

        public FileResult Download()
        {
            XLWorkbook xlWorkBook = new XLWorkbook();
            var xlWorkSheet = xlWorkBook.Worksheets.Add("Master Plant");
            
            IPlantService svc = new PlantService();
            var model = svc.GetAll();

            xlWorkSheet.Cell(1, 1).Value = "PLANTID";
            xlWorkSheet.Cell(1, 2).Value = "NAME";
            xlWorkSheet.Cell(1, 3).Value = "ALAMAT";
            xlWorkSheet.Cell(1, 4).Value = "KELURAHAN";
            xlWorkSheet.Cell(1, 5).Value = "KECAMATAN";
            xlWorkSheet.Cell(1, 6).Value = "CITY";
            xlWorkSheet.Cell(1, 7).Value = "REGION";
            xlWorkSheet.Cell(1, 8).Value = "CONTACTPERSONNAME";
            xlWorkSheet.Cell(1, 9).Value = "ALAMAT2";
            xlWorkSheet.Cell(1, 10).Value = "ALAMAT3";
            xlWorkSheet.Cell(1, 11).Value = "ALAMAT4";
            xlWorkSheet.Cell(1, 12).Value = "ALAMAT5";
            xlWorkSheet.Cell(1, 13).Value = "PROPINSI";
            xlWorkSheet.Cell(1, 14).Value = "PIC";
            xlWorkSheet.Cell(1, 15).Value = "TITLECP";
            xlWorkSheet.Cell(1, 16).Value = "NOHPCP";
            xlWorkSheet.Cell(1, 17).Value = "NOTELPPLANT";
            xlWorkSheet.Cell(1, 18).Value = "POSTALCODE";
            xlWorkSheet.Cell(1, 19).Value = "CUSTOMER_NUMBER";
            xlWorkSheet.Range("A1", "T1").Style.Font.Bold = true;

            int i = 2;
            foreach (var m in model)
            {
                xlWorkSheet.Cell(i, 1).Value = m.PLANTID;
                xlWorkSheet.Cell(i, 2).Value = m.NAME;
                xlWorkSheet.Cell(i, 3).Value = m.ALAMAT;
                xlWorkSheet.Cell(i, 4).Value = m.KELURAHAN;
                xlWorkSheet.Cell(i, 5).Value = m.KECAMATAN;
                xlWorkSheet.Cell(i, 6).Value = m.CITY;
                xlWorkSheet.Cell(i, 7).Value = m.REGION;
                xlWorkSheet.Cell(i, 8).Value = m.CONTACTPERSONNAME;
                xlWorkSheet.Cell(i, 9).Value = m.ALAMAT2;
                xlWorkSheet.Cell(i, 10).Value = m.ALAMAT3;
                xlWorkSheet.Cell(i, 11).Value = m.ALAMAT4;
                xlWorkSheet.Cell(i, 12).Value = m.ALAMAT5;
                xlWorkSheet.Cell(i, 13).Value = m.PROPINSI;
                xlWorkSheet.Cell(i, 14).Value = m.PIC;
                xlWorkSheet.Cell(i, 15).Value = m.TITLECP;
                xlWorkSheet.Cell(i, 16).Value = m.NOHPCP;
                xlWorkSheet.Cell(i, 17).Value = m.NOTELPPLANT;
                xlWorkSheet.Cell(i, 18).Value = m.POSTALCODE;
                xlWorkSheet.Cell(i, 19).Value = m.CUSTOMERNO;
                i++;
            }

            xlWorkSheet.Columns().AdjustToContents();
            var path = Server.MapPath("..") + "\\Master-Plant.xlsx";
            xlWorkBook.SaveAs(path);
            xlWorkBook.Dispose();
            return File(path, "application/vnd.ms-excel", "Master-Plant.xlsx");
        }
    }
}