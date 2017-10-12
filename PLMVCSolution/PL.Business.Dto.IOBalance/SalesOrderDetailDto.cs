using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PL.Business.Dto.IOBalance
{
    public class SalesOrderDetailDto
    {
        public long SalesOrderDetailID { get; set; }

        public long SalesOrderID { get; set; }

        public long ProductID { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string ProductDesc { get; set; }

        public string ProductExt { get; set; }

        public decimal? Quantity { get; set; }

        public int? UnitID { get; set; }

        public string UnitDesc { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? TotalPriceAmount { get; set; }

        public string OverrideDisplay { get; set; }

        public string OverrideExtDisplay { get; set; }

        public decimal? TotalPricePerItemQty
        {
            get
            {
                return (UnitPrice * Quantity);
            }
        }

        public string ProductFullDisplayWithExtension
        {
            get
            {
                string fulldisplay = string.Format("{0} - {1} - {2}", ProductCode, ProductName, ProductExt);
                return fulldisplay;
            }
        }

        public string SalesOrderNum { get; set; }

    }
}
