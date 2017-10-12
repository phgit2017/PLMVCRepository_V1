using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.MVC.IOBalance.Areas.AdminManagement.Models
{
    public class SupplierSearchModel
    {
        public string SupplierCode { get; set; }

        public string SupplierName { get; set; }

        public string isActive { get; set; }
    }
}