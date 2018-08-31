using System.Collections.Generic;
using EProcurement.Models;

namespace EProcurement.Services.Interface
{
    interface IUploadVendorService
    {
        bool IfExist(string poNumber);
        CUSTOMPO UpdateDeliveryCustPO(string CompCode, CUSTOMPO model);
        bool CheckAutheicatePoNumberByVendorID(string poNumber, string VendorID);
        CUSTOMBPKB UpdateDeliveryCustBPKB(string poNumber, CUSTOMBPKB model);
        CUSTOMGR UpdateDeliveryCustGR(string poNumber, CUSTOMGR model);
        CUSTOMPO UpdateInvoiceCustPO(string poNumber, CUSTOMPO model);
        CUSTOMBPKB UpdateInvoiceCustBpkb(string poNumber, CUSTOMBPKB model);
        CUSTOMGR UpdateInvoiceCustGR(string poNumber, CUSTOMGR model);
        CUSTOMIR UpdateInvoiceCustIR(string poNumber, CUSTOMIR model);
        CUSTOMBPKB UpdateBpkbCustBPKB(string poNumber, CUSTOMBPKB model);
    }
}
