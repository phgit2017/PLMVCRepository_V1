namespace PL.Core.Entity.IOBalanceDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SalesOrderDetail
    {
        public long SalesOrderDetailID { get; set; }

        public long SalesOrderID { get; set; }

        public long ProductID { get; set; }

        public decimal? Quantity { get; set; }

        public int? UnitID { get; set; }

        public decimal? UnitPrice { get; set; }

        [StringLength(200)]
        public string OverrideDisplay { get; set; }

        [StringLength(200)]
        public string OverrideExtDisplay { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [ForeignKey("SalesOrderID")]
        public virtual SalesOrder SalesOrder { get; set; }

        [ForeignKey("UnitID")]
        public virtual Unit Unit { get; set; }
    }
}
