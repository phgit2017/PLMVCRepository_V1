namespace PL.Core.Entity.IOBalanceDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BatchProductLog
    {
        [Key]
        public long RecID { get; set; }

        public long BatchNo { get; set; }

        [StringLength(100)]
        public string ProductID { get; set; }

        [StringLength(500)]
        public string BranchName { get; set; }

        [StringLength(500)]
        public string CategoryCode { get; set; }

        [StringLength(500)]
        public string CategoryName { get; set; }

        [StringLength(500)]
        public string ProductCode { get; set; }

        [StringLength(500)]
        public string ProductName { get; set; }

        [StringLength(500)]
        public string Extension { get; set; }

        [StringLength(500)]
        public string Quantity { get; set; }

        [StringLength(500)]
        public string OriginalPrice { get; set; }

        [StringLength(500)]
        public string Price { get; set; }

        [StringLength(500)]
        public string Size { get; set; }

        [StringLength(500)]
        public string SupplierCode { get; set; }

        [StringLength(500)]
        public string SupplierName { get; set; }

        [StringLength(3000)]
        public string Description { get; set; }

        [StringLength(3000)]
        public string Remarks { get; set; }

        [Required]
        [StringLength(50)]
        public string UploadStatus { get; set; }

        [StringLength(1000)]
        public string UploadRemarks { get; set; }

        public DateTime StartProcessed { get; set; }

        public DateTime? EndProcessed { get; set; }

        public virtual BatchSummary BatchSummary { get; set; }
        
        [StringLength(500)]
        public string BarCode { get; set; }
    }
}
