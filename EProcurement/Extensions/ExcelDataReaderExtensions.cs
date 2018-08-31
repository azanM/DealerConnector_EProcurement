using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Web;

namespace EProcurement.Extensions
{
    public static class ExcelDataReaderExtensions
    {
        public static DataTable ToDataTable(this HttpPostedFileBase upload)
        {
            Stream stream = upload.InputStream;

            IExcelDataReader excelReader;

            if (upload.FileName.EndsWith(".xlsx"))
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            else if (upload.FileName.EndsWith(".xls"))
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            else
                throw new Exception("Invalid FileName");

            ExcelDataSetConfiguration conf = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true
                }
            };

            DataSet dataSet = excelReader.AsDataSet(conf);

            excelReader.Close();

            return dataSet.Tables[0];
        }
    }
}