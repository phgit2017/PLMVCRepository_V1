namespace PL.Core.Entity.IOBalanceDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PurchaseOrderDetail
    {
        public long PurchaseOrderDetailID { get; set; }

        public long? PurchaseOrderID { get; set; }

        public long? ProductID { get; set; }

        public int? SupplierID { get; set; }



        [StringLength(200)]
        public string OverrideDisplay { get; set; }

        [StringLength(200)]
        public string OverrideExtDisplay { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [ForeignKey("PurchaseOrderID")]
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        [ForeignKey("SupplierID")]
        public virtual Supplier Supplier { get; set; }
    }
}
