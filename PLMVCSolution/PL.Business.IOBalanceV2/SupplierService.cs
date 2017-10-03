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
    public class SupplierService : ISupplierService
    {
        #region Declarations And Constructors
        IIOBalanceV2Repository<Supplier> _supplier;

        IOBalanceDBV2Entity.Supplier supplier;
        public SupplierService(IIOBalanceV2Repository<Supplier> supplier)
        {
            this._supplier = supplier;
            this.supplier = new IOBalanceDBV2Entity.Supplier();
        }
        #endregion Declarations And Constructors

        #region Interface Implementations
        public IQueryable<SupplierDto> GetAll()
        {
            var result = from det in this._supplier.GetAll()
                         select new SupplierDto()
                         {
                             SupplierId = det.SupplierID,
                             SupplierCode = det.SupplierCode,
                             SupplierName = det.SupplierName,
                             DateCreated = det.DateCreated,
                             CreatedBy = det.CreatedBy,
                             DateUpdated = det.DateUpdated,
                             UpdatedBy = det.UpdatedBy
                         };

            return result;
        }

        public SupplierDto FindById(long supplierId)
        {
            var results = GetAll().Where(c => c.SupplierId == supplierId).FirstOrDefault();
            return results;
        }

        public bool SaveDetails(SupplierDto newDetails)
        {
            this.supplier = newDetails.DtoToEntity();

            if (this._supplier.Insert(this.supplier).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateDetails(SupplierDto newDetails)
        {
            //var oldDetails = FindById(newDetails.CustomerId);
            var details = newDetails.DtoToEntity();

            if (this._supplier.Update2(details).IsNull())
            {
                return false;
            }


            return true;

        }
        #endregion Interface Implementations
    }
}
