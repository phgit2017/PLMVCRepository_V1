using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Utilities.CustomAttributes;
using PL.Business.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Dto.ChainSaw
{
    public class SupplierDetailsDto
    {
        public int SupplierId { get; set; }

        public string SupplierCode { get; set; }

        [Required]
        [Display(Name = "Supplier Name")]
        [StringLength(100, ErrorMessage = "Up to 100 characters only.")]
        public string SupplierName { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? CreatedBy { get; set; }
    }
}
