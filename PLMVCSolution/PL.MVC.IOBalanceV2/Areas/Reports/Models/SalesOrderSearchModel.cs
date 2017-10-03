using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.MVC.IOBalanceV2.Areas.Reports.Models
{
    public class SalesOrderSearchModel
    {
        public long CustomerId { get; set; }
        public string SalesNo { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}