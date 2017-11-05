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
using System.Data;
using System.Data.SqlClient;

namespace PL.Business.IOBalanceV2
{
    public class InventoryService : IInventoryService
    {
        #region Declarations And Constructors
        IIOBalanceV2Repository<Product> _product;
        IIOBalanceV2Repository<BatchInventoryLog> _batchInventoryLog;

        IOBalanceDBV2Entity.Product product;
        IOBalanceDBV2Entity.BatchInventoryLog batchInventoryLog;
        public InventoryService(IIOBalanceV2Repository<Product> product,
            IIOBalanceV2Repository<BatchInventoryLog> batchInventoryLog)
        {
            this._product = product;
            this._batchInventoryLog = batchInventoryLog;

            this.batchInventoryLog = new IOBalanceDBV2Entity.BatchInventoryLog();
            this.product = new IOBalanceDBV2Entity.Product();
        }
        #endregion Declarations And Constructors

        #region Interface Implementations
        public IQueryable<ProductDto> GetAll()
        {
            var result = from det in this._product.GetAll()
                         select new ProductDto()
                         {
                             ProductId = det.ProductID,
                             CategoryId = det.CategoryID,
                             CategoryName = det.Category.CategoryName,
                             QuantityUnitId = det.QuantityUnitID,
                             UnitName = det.QuantityUnit.UnitName,
                             ProductCode = det.ProductCode,
                             ProductName = det.ProductName,
                             ProductDescription = det.ProductDescription,
                             ProductSize = det.ProductSize,
                             CurrentNum = det.CurrentNum,
                             DRNum = det.DRNum,
                             CartonNum = det.CartonNum,
                             Quantity = det.Quantity,
                         };

            return result;
        }

        public IQueryable<BatchInventoryLogDto> GetAllBatchInventory()
        {
            var result = from batchLogs in _batchInventoryLog.GetAll()
                         select new BatchInventoryLogDto() 
                         {
                             BatchInventoryId = batchLogs.BatchInventoryId,
                             FileName = batchLogs.FileName,
                             CreatedBy = batchLogs.CreatedBy,
                             DateCreated = batchLogs.DateCreated,
                             ResultMessage = batchLogs.ResultMessage
                         };

            return result;
        }

        public long SaveDetails(ProductDto newDetails)
        {

            this.product = newDetails.DtoToEntity();
            var insertedProduct = this._product.Insert(this.product);

            if (insertedProduct.IsNull())
            {
                return 0;
            }


            return insertedProduct.ProductID;
        }

        public bool UpdateDetails(ProductDto newDetails)
        {
            var details = newDetails.DtoToEntity();

            if (this._product.Update2(details).IsNull())
            {
                return false;
            }


            return true;

        }



        public bool UpdateQuantityOrder(long productId, decimal qtyUpdate, Enums.OrderType orderType = Enums.OrderType.SalesOrder)
        {
            var oldDetails = GetAll().Where(p => p.ProductId == productId).FirstOrDefault();
            var newDetails = new ProductDto();

            if (oldDetails.IsNull())
            {
                return false;
            }
            else
            {
                newDetails = oldDetails;

                switch (orderType)
                {
                    case Enums.OrderType.SalesOrder:
                        newDetails.Quantity = newDetails.Quantity - qtyUpdate;
                        break;
                }

                var details = newDetails.DtoToEntity();
                if (this._product.Update2(details).IsNull())
                {
                    return false;
                }
            }


            return true;
        }

        public DataTable SaveBatchInventory(string xml, int? createdBy)
        {

            DataTable dtResult = new DataTable();

            SqlParameter[] parameters = 
            {
                new SqlParameter("@Items", SqlDbType.Xml) { Value = xml },
                new SqlParameter("@UploadedBy", SqlDbType.Int) { Value = createdBy }
            };

            dtResult = _product.ExecuteSPReturnTable("dbo.uspBatchInventory", true, parameters);

            return dtResult;
        }

        public List<InventoryReportDto> GetAllInventoryReport(long productId)
        {
            List<InventoryReportDto> result = new List<InventoryReportDto>();
            DataTable dtResult = new DataTable();

            SqlParameter[] parameters = 
            {
                new SqlParameter("@ProductID", SqlDbType.BigInt) { Value = productId }
                
            };

            dtResult = _product.ExecuteSPReturnTable("dbo.usp_StockReport", true, parameters);


            foreach (DataRow row in dtResult.Rows)
            {
                result.Add(new InventoryReportDto()
                {
                    ProductId = Convert.ToInt32(row["ProductId"].ToString()),
                    ProductDisplay = row["Product"].ToString(),
                    OldQuantity = Convert.ToDecimal(row["OldQuantity"].ToString()),
                    Plus = row["+"].ToString(),
                    Minus = row["-"].ToString(),
                    NewQuantity = Convert.ToDecimal(row["NewQuantity"].ToString()),
                    TransactionDate = Convert.ToDateTime(row["TransactionDate"].ToString()),
                    SupplierDisplay = row["Supplier"].ToString(),
                    CustomerDisplay = row["Customer"].ToString()
                });
            }

            return result;
        }

        public long SaveBatchInvetoryLogs(BatchInventoryLogDto newDetails)
        {
            this.batchInventoryLog = newDetails.DtoToEntity();
            var insertedBatchInventoryLog = this._batchInventoryLog.Insert(this.batchInventoryLog);

            if (insertedBatchInventoryLog.IsNull())
            {
                return 0;
            }

            return insertedBatchInventoryLog.BatchInventoryId;
        }

        public bool UpdateBatchInventoryLogs(long batchInventoryId,string resultMessage)
        {
            var oldDetails = GetAllBatchInventory().Where(b => b.BatchInventoryId == batchInventoryId).FirstOrDefault();
            oldDetails.ResultMessage = resultMessage;

            this.batchInventoryLog = new IOBalanceDBV2Entity.BatchInventoryLog();
            this.batchInventoryLog = oldDetails.DtoToEntity();
            if (this._batchInventoryLog.Update(this.batchInventoryLog, x => x.BatchInventoryId == batchInventoryId).IsNull())
            {
                return false;
            }


            return true;
        }



        public ProductValidationDto ValidateSaveProduct(ProductDto dto)
        {
            ProductValidationDto validProduct = new ProductValidationDto();


            var type = Type.GetType(string.Format("{0}.{1}", "PL.Business.IOBalanceV2.ProductSalesRules", "PDashZeroOne"));
            IValidateProduct productRule = (IValidateProduct)Activator.CreateInstance(type);
            validProduct = productRule.SaveProduct(dto);

            return validProduct;
        }

        #endregion Interface Implementations
    }
}
