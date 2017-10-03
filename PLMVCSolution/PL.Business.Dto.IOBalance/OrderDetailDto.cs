using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PL.Business.Dto.IOBalance
{
    public class OrderDetailDto
    {
        public long PurchaseOrderDetailId { get; set; }
        public long PurchaseOrderId { get; set; }
        public bool isNewPurchaseOrder { get; set; }

        public long SalesOrderDetailId { get; set; }
        public long SalesOrderId { get; set; }
        public decimal? Quantity { get; set; }
        public int? UnitId { get; set; }
        public decimal? UnitPrice { get; set; }


        public long? ProductId { get; set; }
        public int? SupplierId { get; set; }
        public string OverrideDisplay { get; set; }
        public string OverrideExtDisplay { get; set; }
    }
}
