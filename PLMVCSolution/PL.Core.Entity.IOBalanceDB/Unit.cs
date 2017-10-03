namespace PL.Core.Entity.IOBalanceDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Unit
    {
        public Unit()
        {
            SalesOrderDetails = new HashSet<SalesOrderDetail>();
        }

        public int UnitID { get; set; }

        [Required]
        [StringLength(50)]
        public string UnitDesc { get; set; }

        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
    }
}
