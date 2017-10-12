using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PL.Business.Dto.IOBalance
{
    public class ModelDto
    {
        public int ModelID { get; set; }

        [StringLength(100)]
        public string ModelName { get; set; }
    }
}
