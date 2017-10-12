namespace PL.Core.Entity.ChainSawDBV2
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

        [Required]
        [StringLength(100)]
        public string SalesOrderNum { get; set; }

        [StringLength(500)]
        public string Messenger { get; set; }

        [StringLength(1000)]
        public string PaymentTerms { get; set; }

        public long CustomerID { get; set; }

        public bool IsQueued { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? CreatedBy { get; set; }

        public bool IsPrinted { get; set; }

        public DateTime? DatePrinted { get; set; }

        public DateTime? DateUpdated { get; set; }

        public int? UpdatedBy { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
    }
}
