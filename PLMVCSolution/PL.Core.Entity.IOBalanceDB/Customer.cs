namespace PL.Core.Entity.IOBalanceDB
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
            CustomerPrices = new HashSet<CustomerPrice>();
            SalesOrders = new HashSet<SalesOrder>();
        }

        public int CustomerID { get; set; }

        [Required]
        [StringLength(10)]
        public string CustomerCode { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(5)]
        public string Extension { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        [StringLength(1000)]
        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string Region { get; set; }

        [StringLength(20)]
        public string ZipCode { get; set; }

        public bool IsActive { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }

        public virtual ICollection<CustomerPrice> CustomerPrices { get; set; }

        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
