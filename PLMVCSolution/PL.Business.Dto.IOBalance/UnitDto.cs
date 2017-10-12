using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PL.Business.Dto.IOBalance
{
    public class UnitDto
    {
        public int UnitID { get; set; }

        [Required]
        public string UnitDesc { get; set; }

    }
}
