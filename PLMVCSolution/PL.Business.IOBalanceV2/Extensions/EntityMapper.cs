using System;
using System.Linq;

using PL.Business.Dto.IOBalanceV2;
using PL.Business.Common;
using IOBalanceDBV2Entity = PL.Core.Entity.IOBalanceDBV2;
using Infrastructure.Utilities.Extensions;

namespace PL.Business.IOBalanceV2.Extensions
{
    public static class EntityMapper
    {
        public static IOBalanceDBV2Entity.Customer DtoToEntity(this CustomerDto dto)
        {
            IOBalanceDBV2Entity.Customer entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceDBV2Entity.Customer
                {
                    CustomerID = dto.CustomerId,
                    CustomerCode = dto.CustomerCode,
                    CustomerAddress = dto.CustomerAddress,
                    CustomerName = dto.CustomerName,
                    CreatedBy = dto.CreatedBy,
                    DateCreated = dto.DateCreated,
                    DateUpdated = dto.DateUpdated,
                    UpdatedBy = dto.UpdatedBy,
                    IsActive = dto.IsActive
                };
            }

            return entity;
        }

        public static IOBalanceDBV2Entity.CustomerPrice DtoToEntity(this CustomerPriceDto dto)
        {
            IOBalanceDBV2Entity.CustomerPrice entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceDBV2Entity.CustomerPrice
                {
                    CustomerID = dto.CustomerId,
                    ProductID = dto.ProductId,
                    Price = dto.Price,
                    CreatedBy = dto.CreatedBy,
                    DateCreated = dto.DateCreated
                };
            }

            return entity;
        }

        public static IOBalanceDBV2Entity.Supplier DtoToEntity(this SupplierDto dto)
        {
            IOBalanceDBV2Entity.Supplier entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceDBV2Entity.Supplier
                {
                    SupplierID = dto.SupplierId,
                    SupplierCode = dto.SupplierCode,
                    SupplierName = dto.SupplierName,
                    CreatedBy = dto.CreatedBy,
                    DateCreated = dto.DateCreated,
                    DateUpdated = dto.DateUpdated,
                    UpdatedBy = dto.UpdatedBy
                };
            }

            return entity;
        }

        public static IOBalanceDBV2Entity.Category DtoToEntity(this CategoryDto dto)
        {
            IOBalanceDBV2Entity.Category entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceDBV2Entity.Category
                {
                    CategoryID = dto.CategoryId,
                    CategoryName = dto.CategoryName
                };
            }

            return entity;
        }

        public static IOBalanceDBV2Entity.Product DtoToEntity(this ProductDto dto)
        {
            IOBalanceDBV2Entity.Product entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceDBV2Entity.Product
                {
                    ProductID = dto.ProductId,
                    CategoryID = dto.CategoryId,
                    QuantityUnitID = dto.QuantityUnitId,
                    ProductCode = dto.ProductCode,
                    ProductName = dto.ProductName,
                    ProductDescription = dto.ProductDescription,
                    ProductSize = dto.ProductSize,
                    CurrentNum = dto.CurrentNum,
                    DRNum = dto.DRNum,
                    CartonNum = dto.CartonNum,
                    Quantity = dto.Quantity
                };
            }

            return entity;
        }

        public static IOBalanceDBV2Entity.PurchaseOrder DtoToEntity(this OrderDto dto)
        {
            IOBalanceDBV2Entity.PurchaseOrder entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceDBV2Entity.PurchaseOrder
                {
                    PurchaseOrderID = dto.OrderId,
                    DateCreated = dto.DateCreated,
                    CreatedBy = dto.CreatedBy
                };
            }

            return entity;
        }

        public static IOBalanceDBV2Entity.SalesOrder DtoToEntity(this SalesOrderDto dto)
        {
            IOBalanceDBV2Entity.SalesOrder entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceDBV2Entity.SalesOrder
                {
                    SalesOrderID = dto.SalesOrderId,
                    SalesNo = dto.SalesNo,
                    CustomerID = dto.CustomerId,
                    DateCreated = dto.DateCreated,
                    CreatedBy = dto.CreatedBy,
                    IsPrinted = dto.IsPrinted,
                    IsCorrected = dto.IsCorrected
                };
            }

            return entity;
        }

        public static IOBalanceDBV2Entity.PurchaseOrderDetail DtoToEntity(this OrderDetailDto dto)
        {
            IOBalanceDBV2Entity.PurchaseOrderDetail entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceDBV2Entity.PurchaseOrderDetail
                {
                    PurchaseOrderDetailID = dto.OrderDetailId,
                    PurchaseOrderID = dto.OrderId,
                    ProductID = dto.ProductId,
                    SupplierID = dto.SupplierId,
                    Quantity = dto.Quantity
                };
            }

            return entity;
        }

        public static IOBalanceDBV2Entity.SalesOrderDetail DtoToEntity(this SalesOrderDetailDto dto)
        {
            IOBalanceDBV2Entity.SalesOrderDetail entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceDBV2Entity.SalesOrderDetail
                {
                    SalesOrderID = dto.SalesOrderId,
                    ProductID = dto.ProductId,
                    SalesPrice = dto.SalesPrice,
                    UnitPrice = dto.UnitPrice,
                    Quantity = dto.Quantity,
                    DateCreated = dto.DateCreated,
                    CreatedBy = dto.CreatedBy
                };
            }

            return entity;
        }

        public static IOBalanceDBV2Entity.QuantityUnit DtoToEntity(this QuantityUnitDto dto)
        {
            IOBalanceDBV2Entity.QuantityUnit entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceDBV2Entity.QuantityUnit
                {
                    QuantityUnitID = dto.QuantityUnitID,
                    UnitName = dto.UnitName
                };
            }

            return entity;
        }

    }
}
