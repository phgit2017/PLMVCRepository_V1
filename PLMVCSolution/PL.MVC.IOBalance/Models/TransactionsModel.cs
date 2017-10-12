using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.MVC.IOBalance.Models
{
    public class TransactionsModel
    {
        public int? NumSalesOrders { get; set; }
        public int? NumCustomers { get; set; }
        public decimal? NumQty { get; set; }
        public decimal? NumTotalQty { get; set; }
    }
}