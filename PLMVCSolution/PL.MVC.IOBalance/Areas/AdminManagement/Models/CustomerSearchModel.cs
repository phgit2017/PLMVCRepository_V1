using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.MVC.IOBalance.Areas.AdminManagement.Models
{
    public class CustomerSearchModel
    {
        public string CustomerCode { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string isActive { get; set; }

    }

}