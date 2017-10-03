using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-- Business
using PL.Business.Common;
using PL.Business.Dto.IOBalanceV2;
using PL.Business.Interface.IOBalanceV2;
using PL.Business.IOBalanceV2.Extensions;

//-- Core
using PL.Core.Entity.IOBalanceDBV2;
using PL.Core.Entity.Repository.Interface;
using IOBalanceDBV2Entity = PL.Core.Entity.IOBalanceDBV2;

//-- Infra
using PL.Infra.DbContext.Interface;

//-- Infrastructure Utilities
using Infrastructure.Utilities.Extensions;

namespace PL.Business.IOBalanceV2
{
    public class QuantityUnitService : IQuantityUnitService
    {
        #region Declarations And Constructors
        IIOBalanceV2Repository<QuantityUnit> _quantityUnit;

        IOBalanceDBV2Entity.QuantityUnit quantityUnit;
        public QuantityUnitService(IIOBalanceV2Repository<QuantityUnit> quantityUnit)
        {
            this._quantityUnit = quantityUnit;
            this.quantityUnit = new IOBalanceDBV2Entity.QuantityUnit();
        }
        #endregion Declarations And Constructors

        #region Interface Implementations
        public IQueryable<QuantityUnitDto> GetAll()
        {
            var result = from det in this._quantityUnit.GetAll()
                         select new QuantityUnitDto()
                         {
                             QuantityUnitID = det.QuantityUnitID,
                             UnitName = det.UnitName
                         };

            return result;
        }

        public QuantityUnitDto FindById(int quantityUnitId)
        {
            var results = GetAll().Where(c => c.QuantityUnitID == quantityUnitId).FirstOrDefault();
            return results;
        }

        public bool SaveDetails(QuantityUnitDto newDetails)
        {
            this.quantityUnit = newDetails.DtoToEntity();

            if (this._quantityUnit.Insert(this.quantityUnit).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateDetails(QuantityUnitDto newDetails)
        {
            var details = newDetails.DtoToEntity();

            if (_quantityUnit.Update2(details).IsNull())
            {
                return false;
            }

            return true;
        }
        #endregion Interface Implementations
    }
}
