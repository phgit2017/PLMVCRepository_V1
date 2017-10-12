using System;
using System.Linq;

using PL.Business.Dto.IOBalance;
using PL.Business.Common;
using IOBalanceEntity = PL.Core.Entity.IOBalanceDB;
using Infrastructure.Utilities.Extensions;

namespace PL.Business.IOBalance.Extensions
{
    public static class EntityMapper
    {
        public static IOBalanceEntity.Category DtoToEntity(this CategoryDto dto)
        {
            IOBalanceEntity.Category entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.Category
                {
                    CategoryCode = dto.CategoryCode.Trim(),
                    CategoryName = dto.CategoryName.Trim(),
                    CategoryID = dto.CategoryID,
                    IsActive = true,
                    SortOrder = 0
                };
            }

            return entity;
        }

        public static IOBalanceEntity.Unit DtoToEntity(this UnitDto dto)
        {
            IOBalanceEntity.Unit entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.Unit()
                {
                    UnitDesc = dto.UnitDesc
                };
            }

            return entity;
        }

        public static IOBalanceEntity.Supplier DtoToEntity(this SupplierDto dto)
        {
            IOBalanceEntity.Supplier entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.Supplier()
                {
                    SupplierName = dto.SupplierName,
                    SupplierCode = dto.SupplierCode,
                    IsActive = true,
                    CreatedBy = dto.CreatedBy,
                    DateCreated = System.DateTime.Now,
                    DateUpdated = dto.DateUpdated,
                    UpdatedBy = dto.UpdatedBy
                };
            }

            return entity;
        }

        public static IOBalanceEntity.Customer DtoToEntity(this CustomerDto dto)
        {
            IOBalanceEntity.Customer entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.Customer
                {
                    CustomerCode = dto.CustomerCode.Trim(),
                    FirstName = dto.FirstName.Trim(),
                    LastName = dto.LastName.Trim(),
                    MiddleName = dto.MiddleName,
                    Extension = dto.Extension,
                    BirthDate = dto.BirthDate,
                    Address = dto.Address,
                    City = dto.City,
                    Region = dto.Region,
                    ZipCode = dto.ZipCode,
                    IsActive = true

                };
            }

            return entity;
        }

        public static IOBalanceEntity.User DtoToEntity(this UserDto dto)
        {
            IOBalanceEntity.User entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.User
                {
                    UserID = 0,
                    UserName = dto.UserName.Trim(),
                    Password = dto.Password,
                    CreatedBy = dto.CreatedBy,
                    DateCreated = dto.DateCreated,
                    DateUpdated = dto.DateUpdated,
                    HashPassword = dto.HashPassword,
                    UserTypeID = dto.UserTypeID,
                    IsActive = true,
                    UpdatedBy = dto.UpdatedBy,
                    BranchID = dto.BranchId

                };
            }

            return entity;
        }

        public static IOBalanceEntity.Branch DtoToEntity(this BranchDto dto)
        {
            IOBalanceEntity.Branch entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.Branch
                {
                    BranchName = dto.BranchName.ToUpper(),
                    BranchAddress = dto.BranchAddress,
                    BranchDetails = dto.BranchDetails,
                    CreatedBy = dto.CreatedBy,
                    UpdatedBy = dto.UpdatedBy,
                    CreatedDate = dto.CreatedDate,
                    DateUpdated = dto.DateUpdated,
                    BranchID = 0
                };
            }

            return entity;
        }

        public static IOBalanceEntity.Discount DtoToEntity(this DiscountDto dto)
        {
            IOBalanceEntity.Discount entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.Discount() 
                {
                    DiscountID = 0,
                    DiscountPercentage = dto.DiscountPercentage,
                    BranchID = dto.BranchID,
                    CreatedBy = dto.CreatedBy,
                    DateCreated = System.DateTime.Now,
                    UpdatedBy = dto.UpdatedBy,
                    DateUpdated = dto.DateUpdated,
                    IsActive = true
                };
            }


            return entity;
        }

        public static IOBalanceEntity.Product DtoToEntity(this ProductDto dto)
        {
            IOBalanceEntity.Product entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.Product
                {
                    ProductID = dto.ProductID,
                    ProductCode = dto.ProductCode,
                    ProductName = dto.ProductName,
                    ProductDesc = dto.ProductDesc,
                    ProductExtension = dto.ProductExtension,
                    Quantity = dto.Quantity,
                    Price = dto.Price,
                    OriginalPrice = dto.OriginalPrice,
                    CategoryID = dto.CategoryID,
                    IsActive = dto.IsActive,
                    Remarks = dto.Remarks,
                    CreatedBy = dto.CreatedBy,
                    DateCreated = dto.DateCreated,
                    UpdatedBy = dto.UpdatedBy,
                    DateUpdated = dto.DateUpdated,
                    ModelID = dto.ModelID,
                    ProductImage = dto.ProductImage,
                    Size = dto.Size,
                    BranchID = dto.BranchID,
                    BarCode = dto.BarCode

                };
            }

            return entity;
        }

        public static IOBalanceEntity.PurchaseOrder DtoToEntityPurchaseOrder(this OrderDto dto)
        {
            IOBalanceEntity.PurchaseOrder entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.PurchaseOrder
                {
                    PurchaseOrderID = (long)dto.OrderId,
                    PurchaseOrderNum = dto.OrderNum,
                    BranchID = dto.BranchId,
                    CreatedBy = dto.CreatedBy,
                    DateCreated = dto.DateCreated
                };
            }

            return entity;
        }

        public static IOBalanceEntity.ReportCombination DtoToEntity(this ReportCombinationDto dto)
        {
            IOBalanceEntity.ReportCombination entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.ReportCombination
                {
                    TrackingID = dto.TrackingID,
                    ProductID = dto.ProductID,
                    DateCreated = dto.DateCreated,
                    ProductQty = dto.ProductQty,
                    PurchaseOrderQty = dto.PurchaseOrderQty,
                    SalesOrderQty = dto.SalesOrderQty,
                    RequestNum = dto.RequestNum,
                    RequestType = dto.RequestType,
                    Qty = dto.Qty,
                    BranchID = dto.BranchId,
                    Remarks = dto.Remarks
                };
            }

            return entity;
        }

        public static IOBalanceEntity.SalesOrder DtoToEntitySalesOrder(this OrderDto dto)
        {
            IOBalanceEntity.SalesOrder entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.SalesOrder
                {
                    SalesOrderID = dto.OrderId,
                    SalesOrderNum = dto.OrderNum,
                    Messenger = dto.Messenger,
                    PaymentTerms = dto.PaymentTerms,
                    BranchID = dto.BranchId,
                    CustomerID = dto.CustomerId,
                    CreatedBy = dto.CreatedBy,
                    DateCreated = dto.DateCreated,
                    DiscountPercentage = dto.DiscountPercentage
                };
            }

            return entity;
        }

        public static IOBalanceEntity.PurchaseOrderDetail DtoToEntityPurchaseOrderDetail(this OrderDetailDto dto)
        {
            IOBalanceEntity.PurchaseOrderDetail entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.PurchaseOrderDetail
                {
                    PurchaseOrderDetailID = dto.PurchaseOrderDetailId,
                    PurchaseOrderID = (long)dto.PurchaseOrderId,
                    ProductID = dto.ProductId,
                    SupplierID = dto.SupplierId,
                    OverrideDisplay = dto.OverrideDisplay,
                    OverrideExtDisplay = dto.OverrideExtDisplay
                };
            }

            return entity;
        }

        public static IOBalanceEntity.SalesOrderDetail DtoToEntitySalesOrderDetail(this OrderDetailDto dto)
        {
            IOBalanceEntity.SalesOrderDetail entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.SalesOrderDetail
                {
                    SalesOrderDetailID = dto.SalesOrderDetailId,
                    SalesOrderID = dto.SalesOrderId,
                    ProductID = (long)dto.ProductId,
                    Quantity = dto.Quantity,
                    UnitID = dto.UnitId,
                    UnitPrice = dto.UnitPrice,
                    OverrideDisplay = dto.OverrideDisplay,
                    OverrideExtDisplay = dto.OverrideExtDisplay
                };
            }

            return entity;
        }

        public static IOBalanceEntity.ProductLog DtoToEntity(this ProductLogDto dto)
        {
            IOBalanceEntity.ProductLog entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.ProductLog
                {
                    RecID = dto.RecId,
                    ProductID = dto.ProductId,
                    Quantity = dto.Quantity,
                    Price = dto.Price,
                    UnitID = dto.UnitID,
                    CategoryID = dto.CategoryID
                };
            }

            return entity;
        }

        public static IOBalanceEntity.CustomerPrice DtoToEntity(this CustomerPriceDto dto)
        {
            IOBalanceEntity.CustomerPrice entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.CustomerPrice
                {
                    CustomerID = dto.CustomerId,
                    Price = dto.Price,
                    ProductID = dto.ProductId
                };
            }

            return entity;
        }

        public static IOBalanceEntity.BatchSummary DtoToEntity(this BatchSummariesDto dto)
        {
            IOBalanceEntity.BatchSummary entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.BatchSummary
                {
                    BatchNo = dto.BatchNo == null ? 0 : (long)dto.BatchNo,
                    FileName = dto.FileName,
                    FilePath = dto.FilePath,
                    TotalRecords = dto.TotalRecords,
                    Successful = dto.Successful,
                    Failed = dto.Failed,
                    UploadStatus = dto.UploadStatus,
                    IsDownload = false,
                    ResultFileName = null,
                    ResultFilePath = null,
                    UploadedBy = dto.UploadedBy,
                    StartUpload = dto.StartUpload,
                    EndUpload = dto.EndUpload
                };
            }

            return entity;
        }

        public static IOBalanceEntity.BatchProductLog DtoToEntity(this BatchProductLogDto dto)
        {
            IOBalanceEntity.BatchProductLog entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.BatchProductLog
                {
                    RecID = dto.RecID,
                    BatchNo = dto.BatchNo,
                    ProductID = dto.ProductID,
                    BranchName = dto.BranchName,
                    CategoryCode = dto.CategoryCode,
                    CategoryName = dto.CategoryName,
                    ProductCode = dto.ProductCode,
                    ProductName = dto.ProductName,
                    Extension = dto.Extension,
                    Quantity = dto.Quantity,
                    OriginalPrice = dto.OriginalPrice,
                    Price = dto.Price,
                    Size = dto.Size,
                    BarCode = dto.BarCode,
                    SupplierCode = dto.SupplierCode,
                    SupplierName = dto.SupplierName,
                    Description = dto.Description,
                    Remarks = dto.Remarks,
                    UploadStatus = dto.UploadStatus,
                    UploadRemarks = dto.UploadRemarks,
                    StartProcessed = System.DateTime.Now,
                    EndProcessed = dto.EndProcessed
                };
            }

            return entity;
        }

        public static IOBalanceEntity.Model DtoToEntity(this ModelDto dto)
        {
            IOBalanceEntity.Model entity = null;

            if (!dto.IsNull())
            {
                entity = new IOBalanceEntity.Model
                {
                    ModelID = dto.ModelID,
                    ModelName = dto.ModelName
                };
            }

            return entity;
        }
    }
}
