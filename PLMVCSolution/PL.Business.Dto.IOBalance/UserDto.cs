using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PL.Business.Dto.IOBalance
{
    public class UserDto
    {
        public int UserID { get; set; }

        [Required]
        [Display(Name="User Name")]
        [StringLength(50, ErrorMessage = "Up to 50 characters only.")]
        public string UserName { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Up to 255 characters only.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string HashPassword { get; set; }

        [Required]
        [Display(Name = "User Type")]
        public int? UserTypeID { get; set; }

        public string UserTypeName { get; set; }

        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }

        [Required]
        [Display(Name = "Branch")]
        public int? BranchId { get; set; }

        public string BranchName { get; set; }

        public string BranchAddress { get; set; }

        public string BranchDetails { get; set; }
    }

}
