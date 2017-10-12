using PL.Business.Dto.IOBalanceV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Interface.IOBalanceV2
{
    public interface IQuantityUnitService
    {
        IQueryable<QuantityUnitDto> GetAll();
        QuantityUnitDto FindById(int categoryId);
        bool SaveDetails(QuantityUnitDto newDetails);
        bool UpdateDetails(QuantityUnitDto newDetails);
    }
}
