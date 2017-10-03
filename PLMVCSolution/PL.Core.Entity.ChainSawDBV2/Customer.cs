namespace PL.Core.Entity.ChainSawDBV2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        public Customer()
        {
            SalesOrders = new HashSet<SalesOrder>();
        }

        public long CustomerID { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerCode { get; set; }

        [StringLength(1000)]
        public string CustomerName { get; set; }

        [StringLength(1000)]
        public string CustomerAddress { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }

        public int? UpdatedBy { get; set; }

        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
