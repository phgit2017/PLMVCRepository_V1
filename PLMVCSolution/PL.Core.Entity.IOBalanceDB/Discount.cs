namespace PL.Core.Entity.IOBalanceDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Discount
    {
        public long DiscountID { get; set; }

        public int DiscountPercentage { get; set; }

        public int? BranchID { get; set; }

        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }

        [ForeignKey("BranchID")]
        public virtual Branch Branch { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual User User_CreatedBy { get; set; }

        [ForeignKey("UpdatedBy")]
        public virtual User User_UpdatedBy { get; set; }
    }
}
