namespace PL.Core.Entity.IOBalanceDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Branch
    {
        public Branch()
        {
            SalesOrders = new HashSet<SalesOrder>();
            User = new HashSet<User>();
            ReportCombinations = new HashSet<ReportCombination>();
            Products = new HashSet<Product>();
            Discounts = new HashSet<Discount>();
        }

        public int BranchID { get; set; }

        [Required]
        [StringLength(200)]
        public string BranchName { get; set; }

        public string BranchAddress { get; set; }

        public string BranchDetails { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }

        public virtual ICollection<SalesOrder> SalesOrders { get; set; }

        public virtual ICollection<User> User { get; set; }

        public virtual ICollection<ReportCombination> ReportCombinations { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Discount> Discounts { get; set; }
    }
}
