using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.IOBalanceV2;
namespace PL.Business.Interface.IOBalanceV2
{
    public interface ICustomerService
    {
        IQueryable<CustomerDto> GetAll();
        CustomerDto FindById(long customerId);
        bool SaveDetails(CustomerDto newDetails);
        bool UpdateDetails(CustomerDto newDetails);
        bool UpdateInactive(long customerId, int? updatedBy);
    }
}
