using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PL.Business.Dto.IOBalance
{
    public class CustomerPriceDto
    {
        public long CustomerPriceId { get; set; }

        public int? CustomerId { get; set; }

        public string CustomerCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Extension { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string ZipCode { get; set; }

        public int? ProductId { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string ProductDesc { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? ProductPrice { get; set; }

        public string UnitDesc { get; set; }

        public string CategoryName { get; set; }

        public string SupplierName { get; set; }

        public decimal? Price { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }


    }
}
