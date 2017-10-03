using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.MVC.IOBalanceV2.Areas.OrderManagement.Models
{
    public class InventorySearchModel
    {
        public int? CategoryId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductSize { get; set; }
        public string CurrentNum { get; set; }
        public string DRNum { get; set; }
        public string CartonNum { get; set; }
    }
}