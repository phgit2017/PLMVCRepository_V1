using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-- Business
using PL.Business.Common;
using PL.Business.Dto.ChainSaw;
using PL.Business.Interface.ChainSaw;
using PL.Business.ChainSaw.Extensions;

//-- Core
using PL.Core.Entity.ChainSawDBV2;
using PL.Core.Entity.Repository.Interface;
using ChainSawDBEntity = PL.Core.Entity.ChainSawDBV2;

//-- Infra
using PL.Infra.DbContext.Interface;

//-- Infrastructure Utilities
using Infrastructure.Utilities.Extensions;

namespace PL.Business.ChainSaw
{
    public class SupplierService : ISupplierService
    {
        #region Declarations And Constructors
        IIOBalanceRepository<Supplier> _supplier;

        ChainSawDBEntity.Supplier supplier;
        public SupplierService(IIOBalanceRepository<Supplier> supplier)
        {
            this._supplier = supplier;
            this.supplier = new ChainSawDBEntity.Supplier();
        }
        #endregion Declarations And Constructors

        #region Interface Implementations
        public IQueryable<SupplierDetailsDto> GetAll()
        {

            var result = from det in _supplier.GetAll()
                         select new SupplierDetailsDto() 
                         {
                             SupplierId = det.SupplierID,
                             SupplierCode = det.SupplierCode,
                             SupplierName = det.SupplierName,
                             DateCreated = det.DateCreated,
                             CreatedBy = det.CreatedBy
                         };

            return result;
        }

        public bool Save(SupplierDetailsDto newDetails)
        {
            this.supplier = newDetails.DtoToEntity();

            if (this._supplier.Insert(this.supplier).IsNull())
            {
                return false;
            }

            return true;
        }
        #endregion Interface Implementations


        
    }
}
