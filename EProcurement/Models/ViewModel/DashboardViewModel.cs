using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EProcurement.Models.ViewModel
{
    public class DashboardViewModel
    {
        public decimal JmlPO { get; set; }
        public decimal JmlPR { get; set; }
        public decimal JmlGR { get; set; }
        public decimal JmlNonGR { get; set; }
        public decimal JmlPOClearing { get; set; }
        public decimal JmlPONotClearing { get; set; }
        public decimal JmlBPKB { get; set; }
        public decimal JmlBPKBOutstanding { get; set; }
    }
}