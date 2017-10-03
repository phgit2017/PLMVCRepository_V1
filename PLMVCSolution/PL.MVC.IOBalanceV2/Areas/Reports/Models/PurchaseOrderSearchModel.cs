using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.MVC.IOBalanceV2.Areas.Reports.Models
{
    public class PurchaseOrderSearchModel
    {
        public int ProductId { get; set; }
        public int SupplierId { get; set; }

        [Display(Name = "Date From")]
        public DateTime? DateFrom { get; set; }

        [Display(Name = "Date To")]
        public DateTime? DateTo { get; set; }
    }
}