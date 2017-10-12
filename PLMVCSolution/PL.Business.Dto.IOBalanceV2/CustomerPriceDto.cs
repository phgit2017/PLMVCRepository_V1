using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Dto.IOBalanceV2
{
    public class CustomerPriceDto
    {
        
        public long CustomerId { get; set; }

        public long ProductId { get; set; }

        public decimal? Price { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        public ProductDto product { get; set; }
        public CustomerDto customer { get; set; }
    }
}
