using PL.Business.Common;
using PL.Business.Dto.IOBalanceV2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Interface.IOBalanceV2
{
    public interface IInventoryService
    {
        IQueryable<ProductDto> GetAll();
        IQueryable<BatchInventoryLogDto> GetAllBatchInventory();
        long SaveDetails(ProductDto newDetails);
        bool UpdateDetails(ProductDto newDetails);
        bool UpdateQuantityOrder(long productId, decimal qtyUpdate, Enums.OrderType orderType = Enums.OrderType.SalesOrder);
        DataTable SaveBatchInventory(string xml, int? createdBy);
        long SaveBatchInvetoryLogs(BatchInventoryLogDto newDetails);
        bool UpdateBatchInventoryLogs(long batchInventoryLogId, string resultMessage);
        List<InventoryReportDto> GetAllInventoryReport(long productId);
        ProductValidationDto ValidateSaveProduct(ProductDto dto);
    }
}
