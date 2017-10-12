using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.IOBalance;
namespace PL.Business.Interface.IOBalance
{
    public interface ICustomerPriceService
    {
        IQueryable<CustomerPriceDto> GetAll();
        IQueryable<CustomerPriceDto> FindPriceByProductId(int productId);
        IQueryable<CustomerPriceDto> FindPriceByCustomerId(int customerId);
        IQueryable<CustomerPriceDto> FindPriceByCustomerIdAndProductId(int customerId, int productId);
        CustomerPriceDto FindCustomerPriceById(int customerPriceId);
        bool SaveCustomerPrice(CustomerPriceDto customerPriceDetails);
        bool UpdateCustomerPrice(CustomerPriceDto newCustomerPriceDetails);
    }
}
