using System.Collections.Generic;
using EProcurement.Models.ViewModel.Reporting;

namespace EProcurement.Services.Interface
{
    interface IReportPOVendorService
    {
        List<ReportPOVendorViewModel> GetAll();
    }
}
