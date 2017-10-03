namespace PL.Core.Entity.IOBalanceDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
            ProductLogs = new HashSet<ProductLog>();
        }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string CategoryCode { get; set; }

        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; }

        public bool IsActive { get; set; }

        public int SortOrder { get; set; }

        public virtual ICollection<ProductLog> ProductLogs { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
