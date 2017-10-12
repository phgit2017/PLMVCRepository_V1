namespace PL.Core.Entity.IOBalanceDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductLog
    {

        [Key]
        public long RecID { get; set; }

        public long? ProductID { get; set; }

        public decimal? Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public int? UnitID { get; set; }

        public int? CategoryID { get; set; }

        public int? SupplierID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [ForeignKey("UnitID")]
        public virtual Unit Unit { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

    }
}
