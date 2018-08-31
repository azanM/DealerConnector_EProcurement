using System.Collections.Generic;
using EProcurement.Models;

namespace EProcurement.Services.Interface
{
    interface IUploadMasterService
    {
        Master_User AddUser(Master_User model);
        Master_User UpdateUser(string userid,Master_User Model);
        CUSTOMCOMPANY AddCompany(CUSTOMCOMPANY model);
        CUSTOMCOMPANY UpdateCompany(string CompCode, CUSTOMCOMPANY model);
        MSMATERIAL AddMaterial(MSMATERIAL model);
        MSMATERIAL UpdateMaterial(string id, MSMATERIAL model);
        MSPLANT AddPlant(MSPLANT model);
        MSPLANT UpdatePlant(string id, MSPLANT model);
        MSVENDOR AddVendor(MSVENDOR model);
        MSVENDOR UpdateVendor(string id, MSVENDOR model);
    }
}
