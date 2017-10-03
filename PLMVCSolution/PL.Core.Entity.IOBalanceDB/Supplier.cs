namespace PL.Core.Entity.IOBalanceDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Supplier
    {

        public Supplier()
        {
            PurchaseOrderDetails = new HashSet<PurchaseOrderDetail>();
        }

        public int SupplierID { get; set; }

        [Required]
        [StringLength(50)]
        public string SupplierCode { get; set; }

        [Required]
        [StringLength(255)]
        public string SupplierName { get; set; }

        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime DateCreated { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }

        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    }
}
