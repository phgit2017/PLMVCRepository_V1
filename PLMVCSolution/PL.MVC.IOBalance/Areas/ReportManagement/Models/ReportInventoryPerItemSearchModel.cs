using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace PL.MVC.IOBalance.Areas.ReportManagement.Models
{
    public class ReportInventoryPerItemSearchModel
    {
        [Required]
        [Display(Name="Date")]
        public DateTime? DateGenerated { get; set; }

        [Required]
        [Display(Name="Category")]
        public int? CategoryID { get; set; }

        [Required]
        [Display(Name="Branch")]
        public int? BranchID { get; set; }
    }
}