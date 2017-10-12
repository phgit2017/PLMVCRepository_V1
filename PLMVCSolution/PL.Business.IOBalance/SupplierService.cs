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
    public class SupplierService : ISupplierService
    {
        #region DeclarationsAndConstructors
        IIOBalanceRepository<Supplier> _supplier;

        IOBalanceEntity.Supplier supplier;

        public SupplierService(IIOBalanceRepository<Supplier> supplier)
        {
            _supplier = supplier;
            this.supplier = new IOBalanceEntity.Supplier();
        }
        #endregion DeclarationsAndConstructors

        #region InterfaceImplementation
        public IQueryable<SupplierDto> GetAll()
        {
            var list = from s in this._supplier.GetAll()
                       select new SupplierDto()
                       {
                           SupplierID = s.SupplierID,
                           SupplierCode = s.SupplierCode,
                           SupplierName = s.SupplierName,
                           SupplierDisplay = s.SupplierCode + " - " + s.SupplierName,
                           CreatedBy = s.CreatedBy,
                           DateCreated = s.DateCreated,
                           DateUpdated = s.DateUpdated,
                           IsActive = s.IsActive,
                           UpdatedBy = s.UpdatedBy
                       };

            return list;
        }

        public SupplierDto FindSupplierById(int supplierId)
        {
            var list = GetAll().Where(s => s.SupplierID == supplierId).FirstOrDefault();
            return list;
        }

        public bool SaveSupplier(SupplierDto supplierDetails)
        {
            this.supplier = supplierDetails.DtoToEntity();

            if (this._supplier.Insert(this.supplier).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateSupplierDetails(SupplierDto newSupplierDetails)
        {
            var oldSupplierDetails = FindSupplierById(newSupplierDetails.SupplierID);
            var updatedSupplierDetails = this.supplier;

            updatedSupplierDetails = new Supplier()
            {
                SupplierID = newSupplierDetails.SupplierID,
                SupplierCode = newSupplierDetails.SupplierCode,
                SupplierName = newSupplierDetails.SupplierName,
                IsActive = newSupplierDetails.IsActive,
                CreatedBy = oldSupplierDetails.CreatedBy,
                DateCreated = oldSupplierDetails.DateCreated,
                DateUpdated = System.DateTime.Now,
                UpdatedBy = newSupplierDetails.UpdatedBy
            };

            if (_supplier.Update2(updatedSupplierDetails).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateInactive(int supplierId, int? updatedBy)
        {
            var ActiveSupplier = FindSupplierById(supplierId);

            if (ActiveSupplier.IsNull())
            {
                return false;
            }

            this.supplier = new Supplier()
            {
                SupplierID = supplierId,
                SupplierCode = ActiveSupplier.SupplierCode,
                SupplierName = ActiveSupplier.SupplierName,
                IsActive = ActiveSupplier.IsActive ? false : true,
                DateCreated = ActiveSupplier.DateCreated,
                CreatedBy = ActiveSupplier.CreatedBy,
                UpdatedBy = updatedBy,
                DateUpdated = System.DateTime.Now
            };


            if (this._supplier.Update(this.supplier, s => s.SupplierID == supplierId).IsNull())
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
