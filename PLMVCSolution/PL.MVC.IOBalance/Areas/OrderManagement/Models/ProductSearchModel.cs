using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.MVC.IOBalance.Areas.OrderManagement.Models
{
    public class ProductSearchModel
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductExt { get; set; }
        public int? UnitId { get; set; }
        public int? SupplierId { get; set; }
        public int? ModelId { get; set; }
        public int? CategoryId { get; set; }
        public int? BranchId { get; set; }
        public string BarCode { get; set; }
        public string Size { get; set; }
    }
}