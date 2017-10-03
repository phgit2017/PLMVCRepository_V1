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
using System.Data;
using System.Data.SqlClient;

namespace PL.Business.IOBalance
{
    public class InventoryService : IInventoryService
    {
        #region DeclarationAndConstructors
        IIOBalanceRepository<Product> _product;
        IIOBalanceRepository<BatchSummary> _batchSummary;
        IIOBalanceRepository<BatchProductLog> _batchProductLog;

        IOBalanceEntity.Product product;
        IOBalanceEntity.PurchaseOrder purchaseOrder;
        IOBalanceEntity.PurchaseOrderDetail purchaseOrderDetail;
        IOBalanceEntity.BatchSummary batchSummary;
        IOBalanceEntity.BatchProductLog batchProductLog;


        public InventoryService(IIOBalanceRepository<Product> product,
            IIOBalanceRepository<BatchSummary> batchSummary,
            IIOBalanceRepository<BatchProductLog> batchProductLog)
        {
            this._product = product;
            this._batchSummary = batchSummary;
            this._batchProductLog = batchProductLog;

            this.product = new Product();
            this.purchaseOrder = new PurchaseOrder();
            this.purchaseOrderDetail = new PurchaseOrderDetail();
            this.batchSummary = new BatchSummary();
            this.batchProductLog = new BatchProductLog();
        }
        #endregion DeclarationAndConstructors

        #region InterfaceImplementations
        public IQueryable<ProductDto> GetAll()
        {
            var list = from p in _product.GetAll()
                       select new ProductDto()
                       {
                           ProductID = p.ProductID,
                           ProductCode = p.ProductCode,
                           ProductName = p.ProductName,
                           ProductDesc = p.ProductDesc,
                           ProductExtension = p.ProductExtension,
                           Quantity = p.Quantity,
                           OriginalPrice = p.OriginalPrice,
                           Price = p.Price,
                           CategoryID = p.CategoryID,
                           CategoryName = p.Category == null ? null : p.Category.CategoryName,
                           CategoryCode = p.Category == null ? null : p.Category.CategoryCode,
                           ModelID = p.ModelID,
                           Model = p.Model == null ? null : p.Model.ModelName,
                           IsActive = p.IsActive,
                           Remarks = p.Remarks,
                           CreatedBy = p.CreatedBy,
                           DateCreated = p.DateCreated,
                           UpdatedBy = p.UpdatedBy,
                           DateUpdated = p.DateUpdated,
                           ProductImage = p.ProductImage,
                           Size = p.Size,
                           BranchID = p.BranchID,
                           BarCode = p.BarCode,
                           BranchName = p.Branch == null ? string.Empty : p.Branch.BranchName
                       };

            return list;
        }

        public ProductDto FindByProductId(long productId)
        {
            var list = GetAll().Where(p => p.ProductID == productId).FirstOrDefault();
            return list;
        }

        public long SaveProduct(ProductDto dto)
        {
            this.product = dto.DtoToEntity();

            if (this._product.Insert(this.product).IsNull())
            {
                return 0;
            }

            return this.product.ProductID;
        }

        public bool UpdateProductQty(long productId, ProductDto updatedProductDetails)
        {
            var oldProductDetails = FindByProductId(productId);
            this.product = new Product()
            {
                ProductID = productId,
                ProductCode = oldProductDetails.ProductCode,
                ProductName = oldProductDetails.ProductName,
                ProductDesc = oldProductDetails.ProductDesc,
                ProductExtension = oldProductDetails.ProductExtension,
                Quantity = (oldProductDetails.Quantity + updatedProductDetails.Quantity),
                Price = updatedProductDetails.Price,
                OriginalPrice = oldProductDetails.OriginalPrice,
                Size = oldProductDetails.Size,
                CategoryID = oldProductDetails.CategoryID,
                IsActive = oldProductDetails.IsActive,
                Remarks = oldProductDetails.Remarks,
                CreatedBy = oldProductDetails.CreatedBy,
                DateCreated = oldProductDetails.DateCreated,
                UpdatedBy = updatedProductDetails.UpdatedBy,
                DateUpdated = updatedProductDetails.DateUpdated,
                ProductImage = oldProductDetails.ProductImage,
                BranchID = oldProductDetails.BranchID,
                BarCode = oldProductDetails.BarCode
            };


            if (this._product.Update2(this.product).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateProductQtyOrder(long productId, ProductDto updatedProductDetails, string orderTypeAbbrev)
        {

            var oldProductDetails = FindByProductId(productId);
            this.product = new Product()
            {
                ProductID = productId,
                ProductCode = oldProductDetails.ProductCode,
                ProductName = oldProductDetails.ProductName,
                ProductDesc = oldProductDetails.ProductDesc,
                ProductExtension = oldProductDetails.ProductExtension,
                Quantity = orderTypeAbbrev == "PO" ? (oldProductDetails.Quantity + updatedProductDetails.Quantity) : (oldProductDetails.Quantity - updatedProductDetails.Quantity),
                Price = oldProductDetails.Price,
                OriginalPrice = oldProductDetails.OriginalPrice,
                Size = oldProductDetails.Size,
                CategoryID = oldProductDetails.CategoryID,
                IsActive = oldProductDetails.IsActive,
                Remarks = oldProductDetails.Remarks,
                CreatedBy = oldProductDetails.CreatedBy,
                DateCreated = oldProductDetails.DateCreated,
                UpdatedBy = updatedProductDetails.UpdatedBy,
                DateUpdated = updatedProductDetails.DateUpdated,
                ProductImage = oldProductDetails.ProductImage,
                BranchID = oldProductDetails.BranchID,
                BarCode = oldProductDetails.BarCode
            };


            if (this._product.Update2(this.product).IsNull())
            {
                return false;
            }
            return true;
        }

        public bool AddQty(ProductDto dto)
        {
            var oldProductDetails = FindByProductId(dto.ProductID);
            this.product = new Product()
            {
                ProductID = oldProductDetails.ProductID,
                ProductCode = oldProductDetails.ProductCode,
                ProductName = oldProductDetails.ProductName,
                ProductDesc = oldProductDetails.ProductDesc,
                ProductExtension = oldProductDetails.ProductExtension,
                Quantity = (dto.Quantity + oldProductDetails.Quantity),
                Price = oldProductDetails.Price,
                OriginalPrice = oldProductDetails.OriginalPrice,
                Size = oldProductDetails.Size,
                CategoryID = oldProductDetails.CategoryID,
                IsActive = oldProductDetails.IsActive,
                Remarks = oldProductDetails.Remarks,
                CreatedBy = oldProductDetails.CreatedBy,
                DateCreated = oldProductDetails.DateCreated,
                UpdatedBy = dto.UpdatedBy,
                DateUpdated = dto.DateUpdated,
                ProductImage = oldProductDetails.ProductImage,
                BranchID = oldProductDetails.BranchID,
                BarCode = oldProductDetails.BarCode
            };


            if (this._product.Update2(this.product).IsNull())
            {
                return false;
            }
            return true;
        }

        public bool VoidQty(ProductDto dto)
        {
            var oldProductDetails = FindByProductId(dto.ProductID);
            this.product = new Product()
            {
                ProductID = oldProductDetails.ProductID,
                ProductCode = oldProductDetails.ProductCode,
                ProductName = oldProductDetails.ProductName,
                ProductDesc = oldProductDetails.ProductDesc,
                ProductExtension = oldProductDetails.ProductExtension,
                Quantity = dto.CRType == "+" ? (dto.Quantity + oldProductDetails.Quantity) : (oldProductDetails.Quantity - dto.Quantity),
                Price = oldProductDetails.Price,
                OriginalPrice = oldProductDetails.OriginalPrice,
                Size = oldProductDetails.Size,
                CategoryID = oldProductDetails.CategoryID,
                IsActive = oldProductDetails.IsActive,
                Remarks = oldProductDetails.Remarks,
                CreatedBy = oldProductDetails.CreatedBy,
                DateCreated = oldProductDetails.DateCreated,
                UpdatedBy = dto.UpdatedBy,
                DateUpdated = dto.DateUpdated,
                ProductImage = oldProductDetails.ProductImage,
                BranchID = oldProductDetails.BranchID,
                BarCode = oldProductDetails.BarCode
            };


            if (this._product.Update2(this.product).IsNull())
            {
                return false;
            }
            return true;
        }

        public bool UpdateProductDetails(long productId, ProductDto updatedProductDetails)
        {
            var oldProductDetails = FindByProductId(productId);
            this.product = new Product()
            {
                ProductID = productId,
                ProductCode = updatedProductDetails.ProductCode,
                ProductName = updatedProductDetails.ProductName,
                ProductDesc = updatedProductDetails.ProductDesc,
                ProductExtension = updatedProductDetails.ProductExtension,
                Quantity = oldProductDetails.Quantity,
                Price = updatedProductDetails.Price,
                OriginalPrice = updatedProductDetails.OriginalPrice == null ? oldProductDetails.OriginalPrice : updatedProductDetails.OriginalPrice,
                Size = updatedProductDetails.Size,
                CategoryID = updatedProductDetails.CategoryID,
                ModelID = oldProductDetails.ModelID,
                IsActive = oldProductDetails.IsActive,
                Remarks = updatedProductDetails.Remarks,
                CreatedBy = oldProductDetails.CreatedBy,
                DateCreated = oldProductDetails.DateCreated,
                UpdatedBy = updatedProductDetails.UpdatedBy,
                DateUpdated = updatedProductDetails.DateUpdated,
                ProductImage = oldProductDetails.ProductImage,
                BranchID = updatedProductDetails.BranchID,
                BarCode = updatedProductDetails.BarCode
            };


            if (this._product.Update2(this.product).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool DeactivateProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProductImage(long productId, ProductDto updatedProductDetails)
        {
            var oldProductDetails = FindByProductId(productId);
            this.product = new Product()
            {
                ProductID = productId,
                ProductCode = oldProductDetails.ProductCode,
                ProductName = oldProductDetails.ProductName,
                ProductDesc = oldProductDetails.ProductDesc,
                ProductExtension = oldProductDetails.ProductExtension,
                Quantity = oldProductDetails.Quantity,
                Price = oldProductDetails.Price,
                OriginalPrice = oldProductDetails.OriginalPrice,
                Size = oldProductDetails.Size,
                CategoryID = oldProductDetails.CategoryID,
                ModelID = oldProductDetails.ModelID,
                IsActive = oldProductDetails.IsActive,
                Remarks = oldProductDetails.Remarks,
                CreatedBy = oldProductDetails.CreatedBy,
                DateCreated = oldProductDetails.DateCreated,
                UpdatedBy = oldProductDetails.UpdatedBy,
                DateUpdated = oldProductDetails.DateUpdated,
                ProductImage = updatedProductDetails.ProductImage,
                BranchID = oldProductDetails.BranchID,
                BarCode = oldProductDetails.BarCode
            };


            if (this._product.Update2(this.product).IsNull())
            {
                return false;
            }

            return true;
        }

        #region Bulk Upload of Products
        //return results:
        //result = 0 -> no error
        //result = 1 -> system error
        //result = 2 -> batch no error
        //result = 3 -> bulk insert batchproductlog error
        //result = 4 -> BATCH UPLOAD data message return <= 0
        //result = 5 -> Successfully inserted ALL records
        //result = 6 -> Successfully inserted but with errors
        //result = 7 -> All records error
        public int SaveBulkProducts(BatchSummariesDto dto, List<BatchProductLogDto> dtoList)
        {
            int result = 0;

            if (!dto.IsNull())
            {
                this.batchSummary = dto.DtoToEntity();
                var batchNoIdentity = _batchSummary.Insert(this.batchSummary).BatchNo;

                if (!batchNoIdentity.IsNull() && batchNoIdentity <= 0)
                {
                    result = 2;
                }
                else
                {
                    if (!SaveProductBulk(dtoList, batchNoIdentity))
                    {
                        result = 3;
                    }
                    else
                    {
                        DataTable dtResultSet = new DataTable();
                        SqlParameter[] sqlParameters = new SqlParameter[]
                        {
                            new SqlParameter() {ParameterName = "BatchNo", SqlValue = batchNoIdentity, SqlDbType = SqlDbType.BigInt}
                        };
                        dtResultSet = _product.ExecuteSPReturnTable("uspBatchProductCreation", true, sqlParameters);

                        if (dtResultSet.Rows.Count <= 0)
                        {
                            result = 4;
                        }
                        else
                        {
                            if (dtResultSet.Rows[0][Globals.Result].ToString() == "5")
                            {
                                result = 0;
                            }
                            else if (dtResultSet.Rows[0][Globals.Result].ToString() == "6")
                            {
                                result = 6;
                            }
                            else if (dtResultSet.Rows[0][Globals.Result].ToString() == "7")
                            {
                                result = 7;
                            }
                        }
                    }
                }
            }
            else
            {
                result = 1;
            }


            return result;
        }

        public IQueryable<BatchSummariesDto> GetAllBatchSummaries()
        {
            var list = from batch in _batchSummary.GetAll()
                       select new BatchSummariesDto()
                       {
                           BatchNo = batch.BatchNo,
                           FileName = batch.FileName,
                           FilePath = batch.FilePath,
                           TotalRecords = batch.TotalRecords,
                           Successful = batch.Successful,
                           Failed = batch.Failed,
                           UploadStatus = batch.UploadStatus,
                           IsDownload = batch.IsDownload,
                           ResultFileName = batch.ResultFileName,
                           ResultFilePath = batch.ResultFilePath,
                           UploadedBy = batch.UploadedBy,
                           StartUpload = batch.StartUpload,
                           EndUpload = batch.EndUpload
                       };

            return list;
        }
        #endregion Bulk Upload of Products


        #endregion InterfaceImplementations

        #region PrivateMethods
        private bool SaveProductLog(ProductLogDto dto)
        {
            return true;
        }
        private bool SaveProductBulk(List<BatchProductLogDto> dtoList, long batchNo)
        {
            List<IOBalanceEntity.BatchProductLog> listBatchProductLog = new List<BatchProductLog>();

            if (dtoList.Count == 0)
            {
                return false;
            }

            foreach (var item in dtoList)
            {
                this.batchProductLog = new BatchProductLog();
                item.BatchNo = batchNo;
                item.UploadStatus = Globals.Pending.ToUpper();
                this.batchProductLog = item.DtoToEntity();
                listBatchProductLog.Add(this.batchProductLog);

            }


            if (listBatchProductLog.Count == 0 || !_batchProductLog.BulkInsert(listBatchProductLog))
            {
                return false;
            }
            return true;
        }
        #endregion PrivateMethods
    }
}
