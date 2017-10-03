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
    public class ReportCombinationService : IReportCombinationService
    {
        #region DeclarationsAndConstructors
        IIOBalanceRepository<ReportCombination> _reportCombination;
        public ReportCombinationService(IIOBalanceRepository<ReportCombination> reportCombination)
        {
            this._reportCombination = reportCombination;
        }
        #endregion DeclarationsAndConstructors

        #region InterfaceImplementations

        public DataTable GetAll(DateTime? dateFrom, DateTime? dateTo, int? categoryId, int? branchId = null, long? productId = null)
        {
            DataTable dt = new DataTable();

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter(){ParameterName = "DateFrom", Value = dateFrom , SqlDbType = SqlDbType.DateTime },
                new SqlParameter(){ParameterName = "DateTo", Value = dateTo , SqlDbType = SqlDbType.DateTime },
                new SqlParameter(){ParameterName = "BranchID", Value = branchId , SqlDbType = SqlDbType.Int },
                new SqlParameter(){ParameterName = "CategoryID", Value = categoryId , SqlDbType = SqlDbType.Int},
                new SqlParameter(){ParameterName = "ProductID", Value = productId , SqlDbType = SqlDbType.BigInt }

            };
            dt = _reportCombination.ExecuteSPReturnTable("uspStockReport", true, sqlParameters);
            return dt;
        }

        public DataTable GetAllPurchaseOrder(DateTime? dateFrom, DateTime? dateTo, int? categoryId, int? branchId = null, long? productId = null)
        {
            DataTable dt = new DataTable();

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter(){ParameterName = "DateFrom", Value = dateFrom , SqlDbType = SqlDbType.DateTime },
                new SqlParameter(){ParameterName = "DateTo", Value = dateTo , SqlDbType = SqlDbType.DateTime },
                new SqlParameter(){ParameterName = "BranchID", Value = branchId , SqlDbType = SqlDbType.Int },
                new SqlParameter(){ParameterName = "CategoryID", Value = categoryId , SqlDbType = SqlDbType.Int},
                new SqlParameter(){ParameterName = "ProductID", Value = productId , SqlDbType = SqlDbType.BigInt }

            };
            dt = _reportCombination.ExecuteSPReturnTable("uspPurchaseOrderReport", true, sqlParameters);
            return dt;
        }

        public DataTable GetAllSalesOrder(DateTime? dateFrom, DateTime? dateTo, int? categoryId, int? branchId = null, long? productId = null)
        {
            DataTable dt = new DataTable();

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter(){ParameterName = "DateFrom", Value = dateFrom , SqlDbType = SqlDbType.DateTime },
                new SqlParameter(){ParameterName = "DateTo", Value = dateTo , SqlDbType = SqlDbType.DateTime },
                new SqlParameter(){ParameterName = "BranchID", Value = branchId , SqlDbType = SqlDbType.Int },
                new SqlParameter(){ParameterName = "CategoryID", Value = categoryId , SqlDbType = SqlDbType.Int},
                new SqlParameter(){ParameterName = "ProductID", Value = productId , SqlDbType = SqlDbType.BigInt }

            };
            dt = _reportCombination.ExecuteSPReturnTable("uspSalesOrderReport", true, sqlParameters);
            return dt;
        }

        public DataTable GetAllSalesOrderReport(DateTime? dateFrom, DateTime? dateTo, int? categoryId, int? branchId = null, long? productId = null)
        {
            DataTable dt = new DataTable();

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter(){ParameterName = "DateFrom", Value = dateFrom , SqlDbType = SqlDbType.DateTime },
                new SqlParameter(){ParameterName = "DateTo", Value = dateTo , SqlDbType = SqlDbType.DateTime },
                new SqlParameter(){ParameterName = "BranchID", Value = branchId , SqlDbType = SqlDbType.Int },
                new SqlParameter(){ParameterName = "CategoryID", Value = categoryId , SqlDbType = SqlDbType.Int},
                new SqlParameter(){ParameterName = "ProductID", Value = productId , SqlDbType = SqlDbType.BigInt }

            };
            dt = _reportCombination.ExecuteSPReturnTable("uspSalesReport", true, sqlParameters);
            return dt;
        }

        public DataTable GetAllSalesReport(DateTime? dateFrom, DateTime? dateTo, int? categoryId, int? branchId = null, long? productId = null)
        {
            DataTable dt = new DataTable();

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter(){ParameterName = "DateFrom", Value = dateFrom , SqlDbType = SqlDbType.DateTime },
                new SqlParameter(){ParameterName = "DateTo", Value = dateTo , SqlDbType = SqlDbType.DateTime },
                new SqlParameter(){ParameterName = "BranchID", Value = branchId , SqlDbType = SqlDbType.Int },
                new SqlParameter(){ParameterName = "CategoryID", Value = categoryId , SqlDbType = SqlDbType.Int},
                new SqlParameter(){ParameterName = "ProductID", Value = productId , SqlDbType = SqlDbType.BigInt }

            };
            dt = _reportCombination.ExecuteSPReturnTable("uspSalesReport", true, sqlParameters);
            return dt;
        }

        public IQueryable<ReportCombinationDto> GetAll()
        {
            var list = from report in _reportCombination.GetAll()
                       select new ReportCombinationDto()
                       {
                           TrackingID = report.TrackingID,
                           ProductID = report.ProductID,
                           ProductCode = report.Product == null ? string.Empty : report.Product.ProductCode,
                           ProductName = report.Product == null ? string.Empty : report.Product.ProductName,
                           ProductDesc = report.Product == null ? string.Empty : report.Product.ProductDesc,
                           ProductExtension = report.Product == null ? string.Empty : report.Product.ProductExtension,
                           ProductSize = report.Product == null ? string.Empty : report.Product.Size,
                           Remarks = report.Remarks,
                           PurchaseOrderQty = report.PurchaseOrderQty,
                           SalesOrderQty = report.SalesOrderQty,
                           ProductQty = report.ProductQty,
                           Qty = report.Qty,
                           RequestType = report.RequestType,
                           RequestNum = report.RequestNum,
                           BranchDetails = report.Product == null ? string.Empty : report.Product.Branch == null ? string.Empty : report.Product.Branch.BranchDetails,
                           BranchAddress = report.Product == null ? string.Empty : report.Product.Branch == null ? string.Empty : report.Product.Branch.BranchAddress,
                           BranchName = report.Product == null ? string.Empty : report.Product.Branch == null ? string.Empty : report.Product.Branch.BranchName,
                           BranchId = report.Product == null ? null : report.Product.Branch == null ? null : (int?)report.Product.Branch.BranchID,
                           DateCreated = report.DateCreated
                           //Branch
                       };

            return list;
        }
        #endregion InterfaceImplementations

        #region PrivateMethods

        #endregion PrivateMethods

    }
}
