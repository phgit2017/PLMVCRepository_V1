namespace PL.Core.Entity.IOBalanceDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SalesOrder
    {
        public SalesOrder()
        {
            SalesOrderDetails = new HashSet<SalesOrderDetail>();
        }

        public long SalesOrderID { get; set; }

        [StringLength(100)]
        public string SalesOrderNum { get; set; }

        [StringLength(50)]
        public string Messenger { get; set; }

        [StringLength(200)]
        public string PaymentTerms { get; set; }

        public int? BranchID { get; set; }

        public int? CustomerID { get; set; }

        public int? DiscountPercentage { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        [ForeignKey("BranchID")]
        public virtual Branch Branch { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
    }
}
