using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.MVC.IOBalance.Areas.AdminManagement.Models
{
    public class CustomerPriceSearchModel
    {
        public int? CustomerId { get; set; }

        public int? ProductId { get; set; }
    }
}