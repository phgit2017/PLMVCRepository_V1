namespace PL.Core.Entity.IOBalanceDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        public Product()
        {
            ProductLogs = new HashSet<ProductLog>();
            PurchaseOrderDetails = new HashSet<PurchaseOrderDetail>();
            CustomerPrices = new HashSet<CustomerPrice>();
            SalesOrderDetails = new HashSet<SalesOrderDetail>();
            ReportCombinations = new HashSet<ReportCombination>();
        }

        public long ProductID { get; set; }

        [Required]
        [StringLength(30)]
        public string ProductCode { get; set; }

        [StringLength(255)]
        public string ProductName { get; set; }

        [Column(TypeName = "text")]
        public string ProductDesc { get; set; }

        [StringLength(100)]
        public string ProductExtension { get; set; }

        public decimal Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? OriginalPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int? CategoryID { get; set; }

        public bool IsActive { get; set; }

        [StringLength(20)]
        public string Size { get; set; }

        public int? ModelID { get; set; }

        [StringLength(1000)]
        public string Remarks { get; set; }

        [StringLength(300)]
        public string BarCode { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        public virtual ICollection<ProductLog> ProductLogs { get; set; }

        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        public virtual ICollection<ReportCombination> ReportCombinations { get; set; }

        public virtual ICollection<CustomerPrice> CustomerPrices { get; set; }

        [ForeignKey("ModelID")]
        public virtual Model Model { get; set; }

        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }

        public string ProductImage { get; set; }

        public int? BranchID { get; set; }

        [ForeignKey("BranchID")]
        public virtual Branch Branch { get; set; }
    }
}
