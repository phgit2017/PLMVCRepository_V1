using PL.Business.Dto.IOBalanceV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Interface.IOBalanceV2
{
    public interface ICustomerPriceService
    {
        bool DeleteDetails(long customerId, long productId);
        IQueryable<CustomerPriceDto> GetAll();
        bool SaveDetails(CustomerPriceDto newDetails);
    }
}
