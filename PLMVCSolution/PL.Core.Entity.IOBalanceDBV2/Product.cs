namespace PL.Core.Entity.IOBalanceDBV2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            CustomerPrices = new HashSet<CustomerPrice>();
            PurchaseOrderDetails = new HashSet<PurchaseOrderDetail>();
            SalesOrderDetails = new HashSet<SalesOrderDetail>();
        }

        public long ProductID { get; set; }

        public int CategoryID { get; set; }

        public int? QuantityUnitID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductCode { get; set; }

        [StringLength(255)]
        public string ProductName { get; set; }

        [StringLength(255)]
        public string ProductDescription { get; set; }

        [StringLength(20)]
        public string ProductSize { get; set; }

        [StringLength(50)]
        public string CurrentNum { get; set; }

        [StringLength(200)]
        public string DRNum { get; set; }

        [StringLength(20)]
        public string CartonNum { get; set; }

        public decimal? Quantity { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        public virtual ICollection<CustomerPrice> CustomerPrices { get; set; }

        [ForeignKey("QuantityUnitID")]
        public virtual QuantityUnit QuantityUnit { get; set; }

        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
    }
}
