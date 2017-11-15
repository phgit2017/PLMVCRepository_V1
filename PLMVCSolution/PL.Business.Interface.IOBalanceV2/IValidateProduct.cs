using PL.Business.Common;
using PL.Business.Dto.IOBalanceV2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Interface.IOBalanceV2
{
    public interface IValidateProduct
    {
        ProductValidationDto SaveProduct(ProductDto dto);
    }
}
