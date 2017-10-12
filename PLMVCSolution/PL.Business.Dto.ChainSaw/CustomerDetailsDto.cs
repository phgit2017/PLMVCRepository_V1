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
    public class CustomerDetailsDto
    {
        public long CustomerId { get; set; }

        [Required]
        [Display(Name = "Customer Code")]
        [StringLength(50, ErrorMessage = "Up to 50 characters only.")]
        public string CustomerCode { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        [StringLength(1000, ErrorMessage = "Up to 1000 characters only.")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Customer Address")]
        [StringLength(1000, ErrorMessage = "Up to 1000 characters only.")]
        public string CustomerAddress { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
