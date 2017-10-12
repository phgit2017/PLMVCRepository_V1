using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.MVC.IOBalance.Areas.AdminManagement.Models
{
    public class CategorySearchModel
    {
        public string CategoryCode { get; set; }
        
        public string CategoryName { get; set; }

        public string isActive { get; set; }

    }

    public class Active
    {
        public string text { get; set; }
        public string value { get; set; }
        public bool BoolValue { get; set; }
    }
}