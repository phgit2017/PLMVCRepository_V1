using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.MVC.IOBalance.Areas.ReportManagement.Models
{
    public class ReportInventorySearchModel
    {
        public int? BranchID { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? ProductID { get; set; }
    }
}