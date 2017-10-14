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

        public bool SaveBatchInvetoryLogs(BatchInventoryLogDto newDetails)
        {
            this.batchInventoryLog = newDetails.DtoToEntity();

            if (this._batchInventoryLog.Insert(this.batchInventoryLog).IsNull())
            {
                return false;
            }

            return true;
        }

        #endregion Interface Implementations
    }
}
