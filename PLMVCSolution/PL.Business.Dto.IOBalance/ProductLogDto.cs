using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PL.Business.Dto.IOBalance
{
    public class ProductLogDto
    {
        public int RecId { get; set; }

        public long? ProductId { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? Price { get; set; }

        public int? UnitID { get; set; }

        public int? CategoryID { get; set; }

    }
}
