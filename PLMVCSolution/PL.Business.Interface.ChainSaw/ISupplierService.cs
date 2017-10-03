using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.ChainSaw;
namespace PL.Business.Interface.ChainSaw
{
    public interface ISupplierService
    {
        IQueryable<SupplierDetailsDto> GetAll();
        bool Save(SupplierDetailsDto newDetails);
    }
}
