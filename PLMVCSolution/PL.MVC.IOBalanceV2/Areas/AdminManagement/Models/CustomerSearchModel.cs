using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.MVC.IOBalanceV2.Areas.AdminManagement.Models
{
    public class CustomerSearchModel
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public bool? IsActive { get; set; }
    }
}