namespace PL.Core.Entity.IOBalanceDBV2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BatchInventoryLog")]
    public partial class BatchInventoryLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BatchInventoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string FileName { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        public string ResultMessage { get; set; }
    }
}
