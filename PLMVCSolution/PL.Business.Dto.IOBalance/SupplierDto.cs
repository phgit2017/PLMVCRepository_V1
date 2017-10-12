using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PL.Business.Dto.IOBalance
{
    public class SupplierDto
    {
        public int SupplierID { get; set; }

        [Display(Name="Supplier Code")]
        [Required(ErrorMessage="Supplier Code is required")]
        [StringLength(50, ErrorMessage = "Up to 50 characters only.")]
        public string SupplierCode { get; set; }

        [Display(Name="Supplier Name")]
        [Required(ErrorMessage="Supplier Name is required")]
        [StringLength(255, ErrorMessage = "Up to 255 characters only.")]
        public string SupplierName { get; set; }

        public string SupplierDisplay { get; set; }

        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}
