using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.IOBalance;
using System.ComponentModel.DataAnnotations;
namespace PL.Business.Dto.IOBalance
{
    public class AuthenticationDto
    {
        public int UserID { get; set; }

        [Required]
        [Display(Name="Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string Password { get; set; }

        public string HashPassword { get; set; }

        public int? UserTypeID { get; set; }

        public string UserTypeName { get; set; }

        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? UpdatedBy { get; set; }

        public int? BranchId { get; set; }

        public string BranchName { get; set; }

        public string BranchAddress { get; set; }

        public string BranchDetails { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}
