using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.IOBalance;
namespace PL.Business.Interface.IOBalance
{
    public interface IUnitService
    {
        IQueryable<UnitDto> GetAll();
        UnitDto FindUnitById(int unitId);
        bool SaveUnit(UnitDto unitDetails);
        bool UpdateUnitDetails(UnitDto newUnitDetails);
        bool DeleteUnit(int unitId);

    }
}
