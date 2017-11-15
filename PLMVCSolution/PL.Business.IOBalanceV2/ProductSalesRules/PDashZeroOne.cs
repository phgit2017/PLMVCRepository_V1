using PL.Business.Dto.IOBalanceV2;
using PL.Business.Interface.IOBalanceV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.IOBalanceV2.ProductSalesRules
{
    public class PDashZeroOne : IValidateProduct
    {

        public ProductValidationDto SaveProduct(ProductDto dto)
        {
            ProductValidationDto errorList = new ProductValidationDto();
            errorList.IsSuccess = true;

            //if (dto.Quantity > 1)
            //{
            //    errorList.ErrorMessage = "Quantity should be only be one";
            //    errorList.IsSuccess = false;
            //}

            return errorList;
        }
    }
}
