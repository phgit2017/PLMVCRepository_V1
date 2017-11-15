using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Dto.IOBalanceV2
{
    public class ProductValidationDto
    {
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
