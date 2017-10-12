using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.ChainSaw;
namespace PL.Business.Interface.ChainSaw
{
    public interface ICustomerService
    {
        IQueryable<CustomerDetailsDto> GetAll();
        bool SaveDetails(CustomerDetailsDto newDetails);
        bool UpdateDetails(CustomerDetailsDto newDetails);
    }
}
