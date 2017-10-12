﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.MVC.IOBalance.Areas.OrderManagement.Models
{
    public class SavedSalesOrderSearchModel
    {
        public string ReceiptNumber { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? BranchId { get; set; }
    }
}