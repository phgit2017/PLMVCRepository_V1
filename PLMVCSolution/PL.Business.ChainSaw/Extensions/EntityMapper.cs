using System;
using System.Linq;

using PL.Business.Dto.ChainSaw;
using PL.Business.Common;
using ChainSawEntity = PL.Core.Entity.ChainSawDBV2;
using Infrastructure.Utilities.Extensions;

namespace PL.Business.ChainSaw.Extensions
{
    public static class EntityMapper
    {
        public static ChainSawEntity.Customer DtoToEntity(this CustomerDetailsDto dto)
        {
            ChainSawEntity.Customer entity = null;

            if (!dto.IsNull())
            {
                entity = new ChainSawEntity.Customer
                {
                    CustomerID = dto.CustomerId,
                    CustomerCode = dto.CustomerCode,
                    CustomerAddress = dto.CustomerAddress,
                    CustomerName = dto.CustomerName,
                    CreatedBy = dto.CreatedBy,
                    DateCreated = dto.DateCreated,
                    DateUpdated = dto.DateUpdated,
                    UpdatedBy = dto.UpdatedBy
                };
            }

            return entity;
        }

        public static ChainSawEntity.Supplier DtoToEntity(this SupplierDetailsDto dto)
        {
            ChainSawEntity.Supplier entity = null;

            if (!dto.IsNull())
            {
                entity = new ChainSawEntity.Supplier
                {
                    SupplierID = dto.SupplierId,
                    SupplierCode = dto.SupplierCode,
                    SupplierName = dto.SupplierName,
                    DateCreated = dto.DateCreated,
                    CreatedBy = dto.CreatedBy
                };
            }

            return entity;
        }
    }
}
