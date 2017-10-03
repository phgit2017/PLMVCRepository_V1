using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Dto.IOBalanceV2
{
    public class SalesOrderListDto
    {
        public long SalesOrderId { get; set; }
        public string SalesNo { get; set; }
        public int CustomerId { get; set; }

        public long ProductId { get; set; }
        public string ProductDisplay { get; set; }

        public decimal SalesPrice { get; set; }
        public decimal UnitPrice { get; set; }
        
        public decimal Quantity { get; set; }

        public DateTime? DateCreated { get; set; }
        public int? CreatedBy { get; set; }

        public ProductDto product { get; set; }
    }
}
