namespace PL.Core.Entity.IOBalanceDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ReportCombination
    {
        [Key]
        public long TrackingID { get; set; }

        public long ProductID { get; set; }

        public decimal? PurchaseOrderQty { get; set; }

        public decimal? SalesOrderQty { get; set; }

        public decimal? ProductQty { get; set; }

        [StringLength(10)]
        public string RequestType { get; set; }

        [StringLength(100)]
        public string RequestNum { get; set; }

        public DateTime? DateCreated { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        public decimal? Qty { get; set; }

        public int? BranchID { get; set; }

        [ForeignKey("BranchID")]
        public virtual Branch Branches { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }
    }
}
