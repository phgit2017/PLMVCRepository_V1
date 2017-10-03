namespace PL.Core.Entity.IOBalanceDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public User()
        {
            User_CreatedBy = new HashSet<Discount>();
            User_UpdatedBy = new HashSet<Discount>();
        }

        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        public int? BranchID { get; set; }

        public string HashPassword { get; set; }

        public int? UserTypeID { get; set; }

        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }

        [ForeignKey("UserTypeID")]
        public virtual UserType UserType { get; set; }

        [ForeignKey("BranchID")]
        public virtual Branch Branch { get; set; }

        [InverseProperty("User_CreatedBy")]
        public virtual ICollection<Discount> User_CreatedBy { get; set; }

        [InverseProperty("User_UpdatedBy")]
        public virtual ICollection<Discount> User_UpdatedBy { get; set; }
    }
}
