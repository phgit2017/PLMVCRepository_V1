using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.IOBalance;
namespace PL.Business.Interface.IOBalance
{
    public interface IOrderService
    {
        IQueryable<SalesOrderDto> GetAllSalesOrder();
        IQueryable<SalesOrderDetailDto> GetAllSalesOrderDetail();
        SalesOrderDto FindBySalesOrderID(long salesOrderId);
        IQueryable<SalesOrderDetailDto> FindSalesOrderDetailBySalesOrderID(long salesOrderId);
        long SavePurchaseOrder(OrderDto dto);
        bool SavePurchaseOrderDetail(OrderDetailDto listDto);
        long SaveSalesOrder(OrderDto dto);
        bool SaveSalesOrderDetail(List<OrderDetailDto> listDto);
        int GenerateSONumber();
        int GeneratePONumber();
        bool SaveReportCombination(OrderDto dto, OrderDetailDto orderDetailDto, string orderTypeAbbrev, string remarksType = null);
        string SaveBatchUpload(ProductDto dto);

    }
}
