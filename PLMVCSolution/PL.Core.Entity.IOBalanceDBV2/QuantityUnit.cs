namespace PL.Core.Entity.IOBalanceDBV2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuantityUnit")]
    public partial class QuantityUnit
    {
        public QuantityUnit()
        {
            Products = new HashSet<Product>();
        }

        public int QuantityUnitID { get; set; }

        [Required]
        [StringLength(20)]
        public string UnitName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
