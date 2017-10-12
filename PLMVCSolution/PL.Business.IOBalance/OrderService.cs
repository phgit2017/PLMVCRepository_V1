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
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;

namespace PL.Business.IOBalance
{
    public class OrderService : IOrderService
    {
        #region DeclarationAndConstructors
        IIOBalanceRepository<PurchaseOrder> _purchaseOrder;
        IIOBalanceRepository<PurchaseOrderDetail> _purchaseOrderDetail;

        IIOBalanceRepository<SalesOrder> _salesOrder;
        IIOBalanceRepository<SalesOrderDetail> _salesOrderDetail;

        IIOBalanceRepository<ReportCombination> _reportCombination;

        IInventoryService _inventoryService;

        IOBalanceEntity.PurchaseOrder purchaseOrder;
        IOBalanceEntity.PurchaseOrderDetail purchaseOrderDetail;
        IOBalanceEntity.SalesOrder salesOrder;
        IOBalanceEntity.SalesOrderDetail salesOrderDetail;
        IOBalanceEntity.ReportCombination reportCombination;
        public OrderService(IIOBalanceRepository<PurchaseOrder> purchaseOrder,
            IIOBalanceRepository<PurchaseOrderDetail> purchaseOrderDetail,
            IIOBalanceRepository<SalesOrder> salesOrder,
            IIOBalanceRepository<SalesOrderDetail> salesOrderDetail,
            IIOBalanceRepository<ReportCombination> reportCombination,
            IInventoryService inventoryService)
        {
            this._purchaseOrder = purchaseOrder;
            this._purchaseOrderDetail = purchaseOrderDetail;
            this._salesOrder = salesOrder;
            this._salesOrderDetail = salesOrderDetail;
            this._reportCombination = reportCombination;
            this._inventoryService = inventoryService;

            this.purchaseOrder = new IOBalanceEntity.PurchaseOrder();
            this.purchaseOrderDetail = new IOBalanceEntity.PurchaseOrderDetail();
            this.salesOrder = new IOBalanceEntity.SalesOrder();
            this.salesOrderDetail = new IOBalanceEntity.SalesOrderDetail();
            this.reportCombination = new IOBalanceEntity.ReportCombination();
        }
        #endregion DeclarationAndConstructors

        #region InterfaceImplementations
        public IQueryable<SalesOrderDto> GetAllSalesOrder()
        {
            var list = from s in _salesOrder.GetAll()
                       select new SalesOrderDto()
                       {
                           SalesOrderID = s.SalesOrderID,
                           SalesOrderNum = s.SalesOrderNum,
                           Messenger = s.Messenger,
                           PaymentTerms = s.PaymentTerms,
                           BranchID = s.BranchID,
                           BranchName = s.Branch == null ? string.Empty : s.Branch.BranchName,
                           BranchAddress = s.Branch == null ? string.Empty : s.Branch.BranchAddress,
                           BranchDetails = s.Branch == null ? string.Empty : s.Branch.BranchDetails,
                           CustomerID = s.CustomerID,
                           CustomerCode = s.Customer == null ? string.Empty : s.Customer.CustomerCode,
                           CustomerFirstName = s.Customer == null ? string.Empty : s.Customer.FirstName,
                           CustomerMiddleName = s.Customer == null ? string.Empty : s.Customer.MiddleName,
                           CustomerLastName = s.Customer == null ? string.Empty : s.Customer.LastName,
                           CustomerAddress = s.Customer == null ? string.Empty : s.Customer.Address,
                           CreatedBy = s.CreatedBy,
                           DateCreated = s.DateCreated,
                           DiscountPercentage = s.DiscountPercentage,
                           SalesOrderDetails = s.SalesOrderDetails.Where(sd => sd.SalesOrderID == s.SalesOrderID).Select(sod => new SalesOrderDetailDto()
                           {
                               SalesOrderID = sod.SalesOrderID,
                               SalesOrderDetailID = sod.SalesOrderDetailID,
                               ProductID = sod.ProductID,
                               Quantity = sod.Quantity,
                               UnitID = sod.UnitID,
                               UnitPrice = sod.UnitPrice

                           }).ToList()
                       };

            return list;
        }

        public IQueryable<SalesOrderDetailDto> GetAllSalesOrderDetail()
        {
            var list = from s in _salesOrderDetail.GetAll()
                       select new SalesOrderDetailDto()
                       {
                           SalesOrderID = s.SalesOrderID,
                           SalesOrderNum = s.SalesOrder == null ? string.Empty : s.SalesOrder.SalesOrderNum,
                           ProductID = s.ProductID,
                           ProductCode = s.Product == null ? string.Empty : s.Product.ProductCode,
                           ProductName = s.Product == null ? string.Empty : s.Product.ProductName,
                           ProductDesc = s.Product == null ? string.Empty : s.Product.ProductDesc,
                           ProductExt = s.Product == null ? string.Empty : s.Product.ProductExtension,
                           UnitID = s.UnitID,
                           UnitDesc = s.Unit == null ? string.Empty : s.Unit.UnitDesc,
                           Quantity = s.Quantity,
                           UnitPrice = s.UnitPrice,
                           SalesOrderDetailID = s.SalesOrderDetailID
                       };

            return list;
        }

        public SalesOrderDto FindBySalesOrderID(long salesOrderId)
        {
            var list = GetAllSalesOrder().Where(s => s.SalesOrderID == salesOrderId).FirstOrDefault();
            return list;
        }

        public IQueryable<SalesOrderDetailDto> FindSalesOrderDetailBySalesOrderID(long salesOrderId)
        {
            var list = GetAllSalesOrderDetail().Where(s => s.SalesOrderID == salesOrderId);
            return list;
        }

        public long SavePurchaseOrder(OrderDto dto)
        {
            this.purchaseOrder = dto.DtoToEntityPurchaseOrder();
            if (_purchaseOrder.Insert(this.purchaseOrder).IsNull())
            {
                return 0;
            }

            return this.purchaseOrder.PurchaseOrderID;
        }

        public bool SavePurchaseOrderDetail(OrderDetailDto listDto)
        {
            this.purchaseOrderDetail = listDto.DtoToEntityPurchaseOrderDetail();

            if (_purchaseOrderDetail.Insert(this.purchaseOrderDetail).IsNull())
            {
                return false;
            }

            return true;
        }

        public long SaveSalesOrder(OrderDto dto)
        {

            this.salesOrder = dto.DtoToEntitySalesOrder();
            if (_salesOrder.Insert(this.salesOrder).IsNull())
            {
                return 0;
            }

            return this.salesOrder.SalesOrderID;
        }

        public bool SaveSalesOrderDetail(List<OrderDetailDto> listDto)
        {
            if (listDto.IsNull())
            {
                return false;
            }

            List<IOBalanceEntity.SalesOrderDetail> entity = new List<SalesOrderDetail>();
            foreach (var details in listDto)
            {
                entity.Add(new SalesOrderDetail()
                {
                    SalesOrderDetailID = details.SalesOrderDetailId,
                    SalesOrderID = details.SalesOrderId,
                    ProductID = (long)details.ProductId,
                    Quantity = details.Quantity,
                    UnitID = details.UnitId,
                    UnitPrice = details.UnitPrice,
                    OverrideDisplay = details.OverrideDisplay,
                    OverrideExtDisplay = details.OverrideExtDisplay
                });
            }


            if (entity.Count == 0 || !this._salesOrderDetail.BulkInsert(entity))
            {
                return false;
            }



            return true;
        }

        public int GenerateSONumber()
        {
            DateTime today = System.DateTime.Now;
            DateTime endOfMonth = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
            var list = _salesOrder.GetAll().Where(s => DbFunctions.TruncateTime(s.DateCreated) >= DbFunctions.TruncateTime(today) && DbFunctions.TruncateTime(s.DateCreated) <= DbFunctions.TruncateTime(endOfMonth)).ToList();
            return list.Count;
        }

        public int GeneratePONumber()
        {
            DateTime today = System.DateTime.Now;
            DateTime endOfMonth = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
            var list = _purchaseOrder.GetAll().Where(s => DbFunctions.TruncateTime(s.DateCreated) >= DbFunctions.TruncateTime(today) && DbFunctions.TruncateTime(s.DateCreated) <= DbFunctions.TruncateTime(endOfMonth)).ToList();
            return list.Count;
        }

        public bool SaveReportCombination(OrderDto dto, OrderDetailDto orderDetailDto, string orderTypeAbbrev, string remarksType = null)
        {
            var productList = _inventoryService.FindByProductId((int)orderDetailDto.ProductId);
            ReportCombinationDto reportDto = new ReportCombinationDto()
            {
                TrackingID = 0,
                ProductID = (long)orderDetailDto.ProductId,
                RequestNum = dto.OrderNum,
                DateCreated = System.DateTime.Now,
                ProductQty = productList.Quantity,
                Qty = orderDetailDto.Quantity,
                BranchId = productList.BranchID
            };

            if (orderTypeAbbrev == Enums.OrderTypeAbbrev.SO.ToString())
            {
                reportDto.RequestType = Enums.OrderTypeAbbrev.SO.ToString();
                //reportDto.SalesOrderQty = (productList.Quantity - orderDetailDto.Quantity);
                reportDto.SalesOrderQty = orderDetailDto.Quantity;
            }
            else if (orderTypeAbbrev == Enums.OrderTypeAbbrev.PO.ToString())
            {
                reportDto.RequestType = Enums.OrderTypeAbbrev.PO.ToString();
                if (!orderDetailDto.isNewPurchaseOrder)
                {
                    //reportDto.PurchaseOrderQty = (productList.Quantity + orderDetailDto.Quantity);
                    reportDto.PurchaseOrderQty = orderDetailDto.Quantity;
                }
                else
                {
                    reportDto.PurchaseOrderQty = orderDetailDto.Quantity;
                }

            }
            else if (orderTypeAbbrev == Enums.OrderTypeAbbrev.CR.ToString())
            {
                reportDto.RequestType = Enums.OrderTypeAbbrev.CR.ToString();
                reportDto.Remarks = dto.Remarks;
                if (remarksType == "+")
                {
                    //reportDto.PurchaseOrderQty = (productList.Quantity + orderDetailDto.Quantity);
                    reportDto.PurchaseOrderQty = (orderDetailDto.Quantity);
                }
                else if (remarksType == "-")
                {
                    //reportDto.SalesOrderQty = (productList.Quantity - orderDetailDto.Quantity);
                    reportDto.SalesOrderQty = (orderDetailDto.Quantity);
                }
            }


            if (_reportCombination.Insert(reportDto.DtoToEntity()).IsNull())
            {
                return false;
            }

            return true;
        }

        public string SaveBatchUpload(ProductDto dto)
        {
            DataTable dt = new DataTable();
            if (!dto.IsNull())
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter(){ParameterName = "CategoryCode", Value = dto.CategoryCode , SqlDbType = SqlDbType.DateTime },
                    new SqlParameter(){ParameterName = "ProductCode", Value = dto.ProductCode, SqlDbType = SqlDbType.DateTime },
                    new SqlParameter(){ParameterName = "ProductName", Value = dto.ProductName , SqlDbType = SqlDbType.Int },
                    new SqlParameter(){ParameterName = "Extension", Value = dto.ProductExtension , SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ParameterName = "Quantity", Value = dto.Quantity , SqlDbType = SqlDbType.BigInt },
                    new SqlParameter(){ParameterName = "OriginalPrice", Value = dto.OriginalPrice, SqlDbType = SqlDbType.BigInt },
                    new SqlParameter(){ParameterName = "Price", Value = dto.Price , SqlDbType = SqlDbType.BigInt },
                    new SqlParameter(){ParameterName = "Size", Value = dto.Size , SqlDbType = SqlDbType.BigInt },
                    new SqlParameter(){ParameterName = "SupplierCode", Value = dto.SupplierCode, SqlDbType = SqlDbType.BigInt },
                    new SqlParameter(){ParameterName = "BranchName", Value = dto.BranchName, SqlDbType = SqlDbType.BigInt },
                    new SqlParameter(){ParameterName = "Description", Value = dto.ProductDesc , SqlDbType = SqlDbType.BigInt },
                    new SqlParameter(){ParameterName = "Remarks", Value = dto.Remarks , SqlDbType = SqlDbType.BigInt },
                    new SqlParameter(){ParameterName = "CreatedBy", Value = dto.CreatedBy , SqlDbType = SqlDbType.BigInt }
                };
                dt = _reportCombination.ExecuteSPReturnTable("uspSalesReport", true, sqlParameters);

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return Messages.ErrorOccuredDuringProcessing;
            }

        }
        #endregion InterfaceImplementations

        #region PrivateMethods
        //private bool SavePurchaseOrderDetail(List<OrderDetailDto> dto)
        //{
        //    List<IOBalanceEntity.PurchaseOrderDetail> List_PurchaseOrderDetail = new List<PurchaseOrderDetail>();

        //    if (dto.Count == 0)
        //    {
        //        return false;
        //    }

        //    foreach (var orderDetailDto in dto)
        //    {
        //        List_PurchaseOrderDetail.Add(new PurchaseOrderDetail()
        //        {

        //            PurchaseOrderDetailID = orderDetailDto.PurchaseOrderDetailId,
        //            PurchaseOrderID = orderDetailDto.PurchaseOrderId,
        //            ProductID = orderDetailDto.ProductId,
        //            SupplierID = orderDetailDto.SupplierId,
        //            OverrideDisplay = orderDetailDto.OverrideDisplay,
        //            OverrideExtDisplay = orderDetailDto.OverrideExtDisplay
        //        });
        //    }


        //    if (List_PurchaseOrderDetail.Count == 0 || !_purchaseOrderDetail.BulkInsert(List_PurchaseOrderDetail))
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //private bool SavePurchaseOrderDetail(OrderDetailDto dto)
        //{
        //    this.purchaseOrderDetail = dto.DtoToEntity();

        //    if (_purchaseOrderDetail.Insert(this.purchaseOrderDetail).IsNull())
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        #endregion PrivateMethods



    }
}
