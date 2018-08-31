using EProcurement.Models;

namespace EProcurement.Services.Interface
{
    interface IUploadVehicleService
    {
        CUSTOMFILEUPLOAD Add(CUSTOMFILEUPLOAD model);
        CUSTOMIR UpdateCustomIR(string poNumber, CUSTOMIR model);
        CUSTOMPO UpdateDeliveryCustPO(string CompCode, CUSTOMPO model);
        CUSTOMPO UpdateInvoiceCustPO(string poNumber, CUSTOMPO model);
        CUSTOMBPKB UpdateInvoiceCustBpkb(string poNumber, CUSTOMBPKB model);
        CUSTOMGR UpdateInvoiceCustGR(string poNumber, CUSTOMGR model);
        CUSTOMIR UpdateInvoiceCustIR(string poNumber, CUSTOMIR model);
        CUSTOMBPKB UpdateBpkbCustBPKB(string poNumber, CUSTOMBPKB model);
    }
}
