using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-- Business
using PL.Business.Common;
using PL.Business.Dto.IOBalance;
using PL.Business.Interface.IOBalance;
using PL.Business.IOBalance.Extensions;
//-- Core
using PL.Core.Entity.IOBalanceDB;
using PL.Core.Entity.Repository.Interface;
using IOBalanceEntity = PL.Core.Entity.IOBalanceDB;

//-- Infra
using PL.Infra.DbContext.Interface;

//-- Infrastructure Utilities
using Infrastructure.Utilities.Extensions;

namespace PL.Business.IOBalance
{
    public class UnitService : IUnitService
    {
        #region DeclarationsAndConstructors
        IIOBalanceRepository<Unit> _unit;

        IOBalanceEntity.Unit unit;
        public UnitService(IIOBalanceRepository<Unit> unit)
        {
            this._unit = unit;

            this.unit = new IOBalanceEntity.Unit();
        }
        #endregion DeclarationsAndConstructors

        #region InterfaceImplementation
        public IQueryable<UnitDto> GetAll()
        {
            var list = from u in this._unit.GetAll()
                       select new UnitDto()
                       {
                           UnitID = u.UnitID,
                           UnitDesc = u.UnitDesc
                       };
            return list;
        }

        public UnitDto FindUnitById(int unitId)
        {
            var list = GetAll().Where(u => u.UnitID == unitId).FirstOrDefault();
            return list;
        }

        public bool SaveUnit(UnitDto unitDetails)
        {
            this.unit = unitDetails.DtoToEntity();
            if (this._unit.Insert(this.unit).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateUnitDetails(UnitDto newUnitDetails)
        {
            var updatedUnitDetails = this.unit;

            updatedUnitDetails = new Unit() 
            {
                UnitID = newUnitDetails.UnitID,
                UnitDesc = newUnitDetails.UnitDesc
            };

            if (this._unit.Update2(updatedUnitDetails).IsNull())
            {
                return false;
            }

            return true;

        }

        public bool DeleteUnit(int unitId)
        {
            if (_unit.Delete(u => u.UnitID == unitId).IsNull())
            {
                return false;
            }
            return true;
        }
        #endregion InterfaceImplementation

        #region PrivateMethods

        #endregion PrivateMethods

    }
}
