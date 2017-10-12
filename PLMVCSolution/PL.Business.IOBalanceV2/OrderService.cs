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
using System.Data.Entity;

namespace PL.Business.IOBalanceV2
{
    public class OrderService : IOrderService
    {
        #region Declarations and constructor
        private readonly IIOBalanceV2Repository<PurchaseOrder> _purchaseOrder;
        private readonly IIOBalanceV2Repository<PurchaseOrderDetail> _purchaseOrderDetail;
        private readonly IIOBalanceV2Repository<SalesOrder> _salesOrder;
        private readonly IIOBalanceV2Repository<SalesOrderDetail> _salesOrderDetail;

        private readonly IInventoryService _inventoryService;
        private readonly ISupplierService _supplierService;
        private readonly ICustomerService _customerService;

        IOBalanceDBV2Entity.PurchaseOrder purchaseOrder;
        IOBalanceDBV2Entity.PurchaseOrderDetail purchaseOrderDetail;
        IOBalanceDBV2Entity.SalesOrder salesOrder;
        IOBalanceDBV2Entity.SalesOrderDetail salesOrderDetail;
        public OrderService(
            IIOBalanceV2Repository<PurchaseOrder> purchaseOrder,
            IIOBalanceV2Repository<PurchaseOrderDetail> purchaseOrderDetail,
            IIOBalanceV2Repository<SalesOrder> salesOrder,
            IIOBalanceV2Repository<SalesOrderDetail> salesOrderDetail,
            IInventoryService inventoryService,
            ISupplierService supplierService,
            ICustomerService customerService)
        {
            this._purchaseOrder = purchaseOrder;
            this._purchaseOrderDetail = purchaseOrderDetail;
            this._inventoryService = inventoryService;
            this._supplierService = supplierService;
            this._customerService = customerService;
            this._salesOrder = salesOrder;
            this._salesOrderDetail = salesOrderDetail;

            this.purchaseOrder = new IOBalanceDBV2Entity.PurchaseOrder();
            this.purchaseOrderDetail = new IOBalanceDBV2Entity.PurchaseOrderDetail();
            this.salesOrder = new IOBalanceDBV2Entity.SalesOrder();
            this.salesOrderDetail = new IOBalanceDBV2Entity.SalesOrderDetail();

        }
        #endregion Declarations and constructor

        #region Interface implementations
        public IQueryable<OrderDto> GetAllOrder()
        {
            var result = from po in _purchaseOrder.GetAll()
                         select new OrderDto()
                         {
                             OrderId = po.PurchaseOrderID,
                             DateCreated = po.DateCreated,
                             CreatedBy = po.CreatedBy
                         };

            return result;
        }

        public IQueryable<OrderDetailDto> GetAllOrderDetail()
        {
            var products = _inventoryService.GetAll();
            var suppliers = _supplierService.GetAll();

            var result = from poDetail in _purchaseOrderDetail.GetAll()
                         select new OrderDetailDto()
                         {
                             OrderDetailId = poDetail.PurchaseOrderDetailID,
                             OrderId = poDetail.PurchaseOrderID,
                             ProductId = poDetail.ProductID,
                             Quantity = poDetail.Quantity,
                             SupplierId = poDetail.SupplierID,
                             product = products.Where(prod => prod.ProductId == poDetail.ProductID).FirstOrDefault(),
                             supplier = suppliers.Where(supp => supp.SupplierId == supp.SupplierId).FirstOrDefault()
                         };

            return result;
        }

        public IQueryable<ReportPurchaseOrderDto> GetAllPurchaseOrderReport()
        {
            var products = _inventoryService.GetAll();
            var suppliers = _supplierService.GetAll();

            var result = from pOrder in _purchaseOrder.GetAll()
                         select new ReportPurchaseOrderDto()
                         {
                             Quantity = pOrder.PurchaseOrderDetails.Where(poDet => poDet.PurchaseOrderID == pOrder.PurchaseOrderID)
                                        .Select(det => det.Quantity).FirstOrDefault(),
                             ProductId = pOrder.PurchaseOrderDetails.Where(poDet => poDet.PurchaseOrderID == pOrder.PurchaseOrderID)
                                        .Select(det => det.ProductID).FirstOrDefault(),
                             product = products.Where(p => p.ProductId == pOrder.PurchaseOrderDetails.Where(poDet => poDet.PurchaseOrderID == pOrder.PurchaseOrderID)
                                        .Select(det => det.ProductID).FirstOrDefault()).FirstOrDefault(),
                             supplier = suppliers.Where(s => s.SupplierId == pOrder.PurchaseOrderDetails.Where(poDet => poDet.PurchaseOrderID == pOrder.PurchaseOrderID)
                                        .Select(det => det.SupplierID).FirstOrDefault()).FirstOrDefault(),
                             SupplierId = pOrder.PurchaseOrderDetails.Where(poDet => poDet.PurchaseOrderID == pOrder.PurchaseOrderID)
                                        .Select(det => det.SupplierID).FirstOrDefault(),
                             DateCreated = pOrder.DateCreated
                         };

            return result;
        }

        public IQueryable<ReportSalesOrderDto> GetAllSalesOrderReport()
        {
            var products = _inventoryService.GetAll();
            var customers = _customerService.GetAll();



            var result = from sOrder in _salesOrder.GetAll()
                         select new ReportSalesOrderDto()
                         {
                             SalesOrderId = sOrder.SalesOrderID,
                             SalesNo = sOrder.SalesNo,
                             CustomerId = sOrder.CustomerID,
                             customer = customers.Where(s => s.CustomerId == sOrder.CustomerID).FirstOrDefault(),
                             IsPrinted = sOrder.IsPrinted,
                             IsCorrected = sOrder.IsCorrected,
                             DateCreated = sOrder.DateCreated
                         };

            return result;

        }

        public IQueryable<ReportSalesOrderReceiptDto> GetAllSalesOrderReceiptReport()
        {
            var products = _inventoryService.GetAll();
            var customers = _customerService.GetAll();



            var result = from sOrder in _salesOrder.GetAll()
                         select new ReportSalesOrderReceiptDto()
                         {
                             SalesOrderId = sOrder.SalesOrderID,
                             SalesNo = sOrder.SalesNo,
                             CustomerId = sOrder.CustomerID,
                             customer = customers.Where(s => s.CustomerId == sOrder.CustomerID).FirstOrDefault(),
                             IsPrinted = sOrder.IsPrinted,
                             IsCorrected = sOrder.IsCorrected,
                             DateCreated = sOrder.DateCreated


                         };

            return result;

        }

        public IQueryable<OrderDto> GetAllSalesOrder()
        {
            var customers = _customerService.GetAll();

            var result = from sOrder in _salesOrder.GetAll()
                         select new OrderDto()
                         {
                             OrderId = sOrder.SalesOrderID,
                             SalesNo = sOrder.SalesNo,
                             CustomerId = sOrder.CustomerID,
                             DateCreated = sOrder.DateCreated,
                             IsPrinted = sOrder.IsPrinted,
                             IsCorrected = sOrder.IsCorrected,
                             customer = customers.Where(c => c.CustomerId == sOrder.CustomerID).FirstOrDefault()
                         };

            return result;
        }

        public IQueryable<SalesOrderListDto> GetAllSalesOrderDetail(long salesOrderId)
        {
            var products = _inventoryService.GetAll();

            var result = from sOrderDetail in _salesOrderDetail.GetAll().Where(so => so.SalesOrderID == salesOrderId)
                         select new SalesOrderListDto()
                         {
                             SalesOrderId = salesOrderId,
                             ProductId = sOrderDetail.ProductID,
                             product = products.Where(p => p.ProductId == sOrderDetail.ProductID).FirstOrDefault(),
                             SalesPrice = sOrderDetail.SalesPrice,
                             Quantity = sOrderDetail.Quantity,
                             DateCreated = sOrderDetail.DateCreated,
                             UnitPrice = sOrderDetail.UnitPrice

                         };

            return result;
        }

        public long SaveOrder(OrderDto newDetails)
        {
            this.purchaseOrder = newDetails.DtoToEntity();
            var insertedPurchaseOrder = this._purchaseOrder.Insert(this.purchaseOrder);
            if (insertedPurchaseOrder.IsNull())
            {
                return 0;
            }

            return insertedPurchaseOrder.PurchaseOrderID;
        }

        public long SaveSalesOrder(SalesOrderDto newDetails)
        {
            this.salesOrder = newDetails.DtoToEntity();
            var insertedSalesOrder = this._salesOrder.Insert(this.salesOrder);

            if (insertedSalesOrder.IsNull())
            {
                return 0;
            }

            return insertedSalesOrder.SalesOrderID;
        }

        public long UpdateSalesOrder(SalesOrderDto newDetails)
        {
            var details = newDetails.DtoToEntity();
            var updatedSalesOrder = this._salesOrder.Update2(details);

            if (updatedSalesOrder.IsNull())
            {
                return 0;
            }
            

            return updatedSalesOrder.SalesOrderID;
        }

        public bool SaveSalesOrderDetail(SalesOrderDetailDto newDetails)
        {
            this.salesOrderDetail = newDetails.DtoToEntity();

            if (this._salesOrderDetail.Insert(this.salesOrderDetail).IsNull())
            {
                return false;
            }

            //_inventoryService.UpdateQuantityOrder(newDetails.ProductId, newDetails.Quantity, Enums.OrderType.SalesOrder);

            return true;
        }

        public bool SaveOrderDetail(OrderDetailDto newDetails)
        {
            this.purchaseOrderDetail = newDetails.DtoToEntity();

            if (this._purchaseOrderDetail.Insert(this.purchaseOrderDetail).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool DeleteAllSalesOrderDetail(long salesOrderId)
        {
            if (_salesOrderDetail.Delete(s => s.SalesOrderID == salesOrderId).IsNull())
            {
                return false;
            }

            return true;
        }

        public string GetSalesNum()
        {
            string SalesNum = string.Empty, salesNumTemp;

            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            string year = DateTime.Now.Year.ToString();
            string dateComplete = string.Empty;

            int maxSalesId = _salesOrder.GetAll().Where(s => DbFunctions.TruncateTime(s.DateCreated) == DbFunctions.TruncateTime(DateTime.Now) && DbFunctions.TruncateTime(s.DateCreated) == DbFunctions.TruncateTime(DateTime.Now)).Count();

            if (maxSalesId == 0)
            {
                maxSalesId = 1;
                dateComplete = string.Format("{0}{1}{2}", year, month, day);
                salesNumTemp = string.Format("{0}{1}", dateComplete, maxSalesId);
                SalesNum = string.Format("{0}{1:" + Constants.SalesTemplate + "}", "PL", Convert.ToInt32(salesNumTemp));
            }
            else
            {
                maxSalesId += 1;
                dateComplete = string.Format("{0}{1}{2}", year, month, day);
                salesNumTemp = string.Format("{0}{1}", dateComplete, maxSalesId);
                SalesNum = string.Format("{0}{1:" + Constants.SalesTemplate + "}", "PL", Convert.ToInt32(salesNumTemp));
            }

            return SalesNum;
        }
        #endregion Interface implementations
    }
}
