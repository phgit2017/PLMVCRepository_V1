using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.IOBalance;
namespace PL.Business.Interface.IOBalance
{
    public interface ICustomerService
    {
        IQueryable<CustomerDto> GetAll();
        CustomerDto FindCustomerById(int customerId);
        IQueryable<CustomerDto> FindCustomerByCustomerCode(string customerCode);
        bool SaveCustomer(CustomerDto customerDetails);
        bool UpdateCustomerDetails(CustomerDto newCustomerDetails);
        bool UpdateInactive(int customerId, int? updatedBy);
    }
}
