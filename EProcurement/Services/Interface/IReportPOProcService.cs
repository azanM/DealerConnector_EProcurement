using System.Collections.Generic;
using EProcurement.Models.ViewModel.Reporting;

namespace EProcurement.Services.Interface
{
    interface IReportPOProcService
    {
        List<ListReportProcViewModel> GetAll();
        List<ListReportProcViewModel> GetSearch(string PoNumber);
    }
}
