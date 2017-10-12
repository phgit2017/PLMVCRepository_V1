using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.IOBalance;
using System.Data;
namespace PL.Business.Interface.IOBalance
{
    public interface IReportCombinationService
    {
        DataTable GetAll(DateTime? dateFrom, DateTime? dateTo, int? categoryId, int? branchId = null, long? productId = null);
        DataTable GetAllPurchaseOrder(DateTime? dateFrom, DateTime? dateTo, int? categoryId, int? branchId = null, long? productId = null);
        DataTable GetAllSalesOrder(DateTime? dateFrom, DateTime? dateTo, int? categoryId, int? branchId = null, long? productId = null);
        DataTable GetAllSalesOrderReport(DateTime? dateFrom, DateTime? dateTo, int? categoryId, int? branchId = null, long? productId = null);
        DataTable GetAllSalesReport(DateTime? dateFrom, DateTime? dateTo, int? categoryId, int? branchId = null, long? productId = null);
        IQueryable<ReportCombinationDto> GetAll();
    }
}
