using PL.Business.Dto.IOBalanceV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.Business.Interface.IOBalanceV2
{
    public interface IOrderService
    {
        IQueryable<OrderDto> GetAllOrder();
        IQueryable<OrderDetailDto> GetAllOrderDetail();
        IQueryable<OrderDto> GetAllSalesOrder();
        IQueryable<SalesOrderListDto> GetAllSalesOrderDetail(long salesOrderId);
        long SaveOrder(OrderDto newDetails);
        bool SaveOrderDetail(OrderDetailDto newDetails);
        long SaveSalesOrder(SalesOrderDto newDetails);
        bool SaveSalesOrderDetail(SalesOrderDetailDto newDetails);

        IQueryable<ReportPurchaseOrderDto> GetAllPurchaseOrderReport();
        IQueryable<ReportSalesOrderDto> GetAllSalesOrderReport();
        string GetSalesNum();
        long UpdateSalesOrder(SalesOrderDto newDetails);
        bool DeleteAllSalesOrderDetail(long salesOrderId);
    }
}
