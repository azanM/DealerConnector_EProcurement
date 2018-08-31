using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EProcurement.Models.ViewModel
{
    public class ErrorUploadViewModel
    {
        public int row { get; set; }
        public string column { get; set; }
        public string message { get; set; }
    }
}