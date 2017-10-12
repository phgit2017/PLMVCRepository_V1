using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Dto.IOBalanceV2
{
    public class QuantityUnitDto
    {
        public int QuantityUnitID { get; set; }

        [Required]
        [Display(Name = "Unit Name")]
        [StringLength(20, ErrorMessage = "Up to 20 characters only.")]
        public string UnitName { get; set; }
    }
}
