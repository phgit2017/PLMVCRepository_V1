namespace PL.Core.Entity.ChainSawDBV2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SalesOrderDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SalesOrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ProductID { get; set; }

        [StringLength(1000)]
        public string OverwriteProductName { get; set; }

        [StringLength(200)]
        public string OverwriteProductExtension { get; set; }

        public decimal Quantity { get; set; }

        public int UnitID { get; set; }

        public decimal UnitPrice { get; set; }

        public virtual Product Product { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }

        public virtual Unit Unit { get; set; }
    }
}
