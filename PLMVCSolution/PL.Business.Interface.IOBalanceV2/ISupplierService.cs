using PL.Business.Dto.IOBalanceV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Interface.IOBalanceV2
{
    public interface ISupplierService
    {
        IQueryable<SupplierDto> GetAll();
        SupplierDto FindById(long customerId);
        bool SaveDetails(SupplierDto newDetails);
        bool UpdateDetails(SupplierDto newDetails);
    }
}
