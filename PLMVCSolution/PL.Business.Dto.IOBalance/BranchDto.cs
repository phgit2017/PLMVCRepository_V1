using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PL.Business.Dto.IOBalance
{
    public class BranchDto
    {
        public int BranchId { get; set; }

        [Required]
        [Display(Name = "Branch Name")]
        [StringLength(200,ErrorMessage="Up to 200 characters only.")]
        public string BranchName { get; set; }

        [StringLength(200, ErrorMessage = "Up to 200 characters only.")]
        [Display(Name = "Branch Address")]
        public string BranchAddress { get; set; }

        [StringLength(300, ErrorMessage = "Up to 300 characters only.")]
        [Display(Name = "Branch Details")]
        public string BranchDetails { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}
