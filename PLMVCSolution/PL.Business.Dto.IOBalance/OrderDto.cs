using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PL.Business.Dto.IOBalance
{
    public class OrderDto
    {
        public long OrderId { get; set; }
        public long? OrderDetailId { get; set; }
        public string OrderNum { get; set; }
        public string Messenger { get; set; }
        public string PaymentTerms { get; set; }
        public int? CustomerId { get; set; }
        public int? BranchId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Remarks { get; set; }
        public int? DiscountPercentage { get; set; }

    }
}
