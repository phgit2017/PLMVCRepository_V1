using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Business
using PL.Business.Common;
using PL.Business.Dto.IOBalanceV2;
using PL.Business.Interface.IOBalanceV2;

//MVC
using PL.MVC.IOBalanceV2.Models;

using PL.MVC.IOBalanceV2.Infrastructure;
using Infrastructure.Utilities.Extensions;
using Infrastructure.Utilities;
using System.Web.Http;

//Kendo
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using PL.MVC.IOBalanceV2.Controllers;
using PL.MVC.IOBalanceV2.Areas.OrderManagement.Models;
using LinqKit;
using PL.MVC.IOBalanceV2.Areas.Reports.Models;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Data.Entity;

namespace PL.MVC.IOBalanceV2.Areas.Reports.Controllers
{
    public partial class OrderController : BaseController
    {
        #region Declarations and constructors
        private readonly IInventoryService _inventoryService;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController(
            IInventoryService inventoryService,
            IOrderService orderService,
            ICustomerService customerService
            )
        {
            this._inventoryService = inventoryService;
            this._orderService = orderService;
            this._customerService = customerService;
        }
        #endregion Declarations and constructors

        #region Action methods

        #region PO
        public virtual ActionResult PurchaseOrder()
        {
            return View();
        }

        public virtual ActionResult GetPOReport([DataSourceRequest] DataSourceRequest request, PurchaseOrderSearchModel searchModel)
        {
            var list = GetPurchaseOrderReport(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult ExportPOExcel(PurchaseOrderSearchModel searchModel)
        {
            return ReportPurchaseOrderExtract(searchModel);
        }
        #endregion PO

        #region SO
        public virtual ActionResult SalesOrder()
        {
            return View();
        }

        public virtual ActionResult SalesOrderReceipt()
        {
            return View();
        }

        public virtual ActionResult GetSOReport([DataSourceRequest] DataSourceRequest request, SalesOrderSearchModel searchModel)
        {
            var list = GetSalesOrderReport(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult GetSOReceiptReport([DataSourceRequest] DataSourceRequest request, SalesOrderSearchModel searchModel)
        {
            var list = GetSalesOrderReceiptReport(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult ExportSOExcel(SalesOrderSearchModel searchModel)
        {
            return ReportSalesOrderExtract(searchModel);
        }

        public virtual ActionResult ExportSOReceiptExcel(int salesOrderId, int customerId, string salesNo)
        {
            return ReportSalesOrderReceiptExtract(salesOrderId, customerId, salesNo);
        }
        #endregion SO

        #region Inventory Report
        public virtual ActionResult InventoryReport()
        {
            return View();
        }


        public virtual ActionResult GetInventoryReport([DataSourceRequest] DataSourceRequest request, long productId)
        {
            var list = _inventoryService.GetAllInventoryReport(productId);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult ExportInventoryReport(long ProductId)
        {
            int rowId = 0;
            int colId = 0;


            var list = _inventoryService.GetAllInventoryReport(ProductId);

            var dir = Server.MapPath(string.Format("~/{0}", Constants.ProductExcelTemplateDir));
            var fileNameTemplate = string.Format("{0}{1}", Constants.ReportInventoryTemplate, ".xlsx");
            var path = System.IO.Path.Combine(dir, fileNameTemplate);
            var fileNameGenerated = string.Format("{0}{1}", Constants.InventoryReport, ".xlsx");

            var contentType = "application/vnd.ms-excel";

            var templateFile = new FileInfo(path);
            //var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);


            var package = new ExcelPackage(templateFile);
            var workSheet = package.Workbook.Worksheets[1];

            rowId = 2;
            foreach (var detail in list)
            {
                    workSheet.Cells["A" + rowId.ToString()].Value = detail.ProductDisplay;
                    workSheet.Cells["B" + rowId.ToString()].Value = detail.OldQuantity;
                    workSheet.Cells["C" + rowId.ToString()].Value = detail.Plus;
                    workSheet.Cells["D" + rowId.ToString()].Value = detail.Minus;
                    workSheet.Cells["E" + rowId.ToString()].Value = detail.NewQuantity;
                    workSheet.Cells["F" + rowId.ToString()].Value = detail.TransactionDateWithFormat;
                    workSheet.Cells["G" + rowId.ToString()].Value = detail.SupplierDisplay;
                    workSheet.Cells["H" + rowId.ToString()].Value = detail.CustomerDisplay;
                    rowId++;
                

            }


            var memoryStream = new MemoryStream();
            //package.Save();
            package.SaveAs(memoryStream);
            memoryStream.Position = 0;

            return File(memoryStream, contentType, fileNameGenerated);
        }
        #endregion Inventory Report

        #endregion Action methods

        #region Private methods
        private IQueryable<ReportPurchaseOrderDto> GetPurchaseOrderReport(PurchaseOrderSearchModel searchModel)
        {
            IQueryable<ReportPurchaseOrderDto> result = null;


            if ((searchModel.DateFrom.IsNull() && searchModel.DateTo.IsNull() && searchModel.ProductId == 0 && searchModel.SupplierId == 0))
            {
                result = _orderService.GetAllPurchaseOrderReport().Where(s =>  DbFunctions.TruncateTime(s.DateCreated) == DbFunctions.TruncateTime(DateTime.Now));
            }
            else
            {
                var predicate = PredicateBuilder.True<ReportPurchaseOrderDto>();


                if (searchModel.ProductId != 0)
                {
                    predicate = predicate.And(p => p.ProductId == searchModel.ProductId);
                }

                if (searchModel.SupplierId != 0)
                {
                    predicate = predicate.And(p => p.SupplierId == searchModel.SupplierId);
                }

                if (!searchModel.DateFrom.IsNull() && searchModel.DateTo.IsNull())
                {
                    predicate = predicate.And(a => DbFunctions.TruncateTime(a.DateCreated) >= DbFunctions.TruncateTime(searchModel.DateFrom));
                }
                else if (searchModel.DateFrom.IsNull() && !searchModel.DateTo.IsNull())
                {
                    predicate = predicate.And(a => DbFunctions.TruncateTime(a.DateCreated) <= DbFunctions.TruncateTime(searchModel.DateTo));
                }
                else if (!searchModel.DateFrom.IsNull() && !searchModel.DateTo.IsNull())
                {
                    predicate = predicate.And(a => DbFunctions.TruncateTime(a.DateCreated) >= DbFunctions.TruncateTime(searchModel.DateFrom) && DbFunctions.TruncateTime(a.DateCreated) <= DbFunctions.TruncateTime(searchModel.DateTo));
                }

                result = _orderService.GetAllPurchaseOrderReport().AsExpandable().Where(predicate);
            }

            return result;

        }

        private IQueryable<ReportSalesOrderDto> GetSalesOrderReport(SalesOrderSearchModel searchModel)
        {
            IQueryable<ReportSalesOrderDto> result = null;


            if ((searchModel.DateFrom.IsNull() && searchModel.DateTo.IsNull() && searchModel.CustomerId == 0 && searchModel.SalesNo.IsNull()))
            {
                result = _orderService.GetAllSalesOrderReport().Where(s => s.IsPrinted && !s.IsCorrected && s.CustomerId != Constants.CustomerIdAdmin && DbFunctions.TruncateTime(s.DateCreated) == DbFunctions.TruncateTime(DateTime.Now));
            }
            else
            {
                var predicate = PredicateBuilder.True<ReportSalesOrderDto>();


                if (searchModel.CustomerId != 0)
                {
                    predicate = predicate.And(p => p.CustomerId == searchModel.CustomerId);
                }

                if (!searchModel.SalesNo.IsNull())
                {
                    predicate = predicate.And(p => p.SalesNo.Contains(searchModel.SalesNo));
                }

                if (!searchModel.DateFrom.IsNull() && searchModel.DateTo.IsNull())
                {
                    predicate = predicate.And(a => DbFunctions.TruncateTime(a.DateCreated) >= DbFunctions.TruncateTime(searchModel.DateFrom));
                }
                else if (searchModel.DateFrom.IsNull() && !searchModel.DateTo.IsNull())
                {
                    predicate = predicate.And(a => DbFunctions.TruncateTime(a.DateCreated) <= DbFunctions.TruncateTime(searchModel.DateTo));
                }
                else if (!searchModel.DateFrom.IsNull() && !searchModel.DateTo.IsNull())
                {
                    predicate = predicate.And(a => DbFunctions.TruncateTime(a.DateCreated) >= DbFunctions.TruncateTime(searchModel.DateFrom) && DbFunctions.TruncateTime(a.DateCreated) <= DbFunctions.TruncateTime(searchModel.DateTo));
                }

                predicate = predicate.And(s => s.IsPrinted && !s.IsCorrected && s.CustomerId != Constants.CustomerIdAdmin);

                result = _orderService.GetAllSalesOrderReport().AsExpandable().Where(predicate);
            }

            return result;

        }

        private IQueryable<ReportSalesOrderReceiptDto> GetSalesOrderReceiptReport(SalesOrderSearchModel searchModel)
        {
            IQueryable<ReportSalesOrderReceiptDto> result = null;


            if ((searchModel.DateFrom.IsNull() && searchModel.DateTo.IsNull() && searchModel.CustomerId == 0 && searchModel.SalesNo.IsNull()))
            {
                result = _orderService.GetAllSalesOrderReceiptReport().Where(s => s.IsPrinted && !s.IsCorrected && s.CustomerId != Constants.CustomerIdAdmin && DbFunctions.TruncateTime(s.DateCreated) == DbFunctions.TruncateTime(DateTime.Now));
            }
            else
            {
                var predicate = PredicateBuilder.True<ReportSalesOrderReceiptDto>();


                if (searchModel.CustomerId != 0)
                {
                    predicate = predicate.And(p => p.CustomerId == searchModel.CustomerId);
                }

                if (!searchModel.SalesNo.IsNull())
                {
                    predicate = predicate.And(p => p.SalesNo.Contains(searchModel.SalesNo));
                }

                if (!searchModel.DateFrom.IsNull() && searchModel.DateTo.IsNull())
                {
                    predicate = predicate.And(a => DbFunctions.TruncateTime(a.DateCreated) >= DbFunctions.TruncateTime(searchModel.DateFrom));
                }
                else if (searchModel.DateFrom.IsNull() && !searchModel.DateTo.IsNull())
                {
                    predicate = predicate.And(a => DbFunctions.TruncateTime(a.DateCreated) <= DbFunctions.TruncateTime(searchModel.DateTo));
                }
                else if (!searchModel.DateFrom.IsNull() && !searchModel.DateTo.IsNull())
                {
                    predicate = predicate.And(a => DbFunctions.TruncateTime(a.DateCreated) >= DbFunctions.TruncateTime(searchModel.DateFrom) && DbFunctions.TruncateTime(a.DateCreated) <= DbFunctions.TruncateTime(searchModel.DateTo));
                }


                predicate = predicate.And(s => s.IsPrinted && !s.IsCorrected && s.CustomerId != Constants.CustomerIdAdmin);

                result = _orderService.GetAllSalesOrderReceiptReport().AsExpandable().Where(predicate);
            }

            return result;

        }


        private System.Web.Mvc.FileResult ReportPurchaseOrderExtract(PurchaseOrderSearchModel searchModel)
        {

            int rowId = 0;
            int colId = 0;

            var list = GetPurchaseOrderReport(searchModel).ToList();

            var dir = Server.MapPath(string.Format("~/{0}", Constants.ProductExcelTemplateDir));
            var fileNameTemplate = string.Format("{0}{1}", Constants.ReportPOTemplate, ".xlsx");
            var path = System.IO.Path.Combine(dir, fileNameTemplate);
            var fileNameGenerated = string.Format("{0}{1}", Constants.ReportPerPurchaseOrder, ".xlsx");

            var contentType = "application/vnd.ms-excel";

            var templateFile = new FileInfo(path);
            //var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            ;

            var package = new ExcelPackage(templateFile);
            var workSheet = package.Workbook.Worksheets[1];

            rowId = 2;
            foreach (var detail in list)
            {
                workSheet.Cells["A" + rowId.ToString()].Value = detail.product.ProductInfoDisplay;
                workSheet.Cells["B" + rowId.ToString()].Value = detail.SupplierInfoDisplay;
                workSheet.Cells["C" + rowId.ToString()].Value = detail.Quantity;
                workSheet.Cells["D" + rowId.ToString()].Value = detail.DateCreatedWithFormat;
                rowId++;
            }




            var memoryStream = new MemoryStream();
            //package.Save();
            package.SaveAs(memoryStream);
            memoryStream.Position = 0;

            return File(memoryStream, contentType, fileNameGenerated);
        }

        private System.Web.Mvc.FileResult ReportSalesOrderExtract(SalesOrderSearchModel searchModel)
        {

            int rowId = 0;
            int colId = 0;

            var list = GetSalesOrderReport(searchModel).ToList();

            var dir = Server.MapPath(string.Format("~/{0}", Constants.ProductExcelTemplateDir));
            var fileNameTemplate = string.Format("{0}{1}", Constants.ReportSOTemplate, ".xlsx");
            var path = System.IO.Path.Combine(dir, fileNameTemplate);
            var fileNameGenerated = string.Format("{0}{1}", Constants.ReportPerSalesOrder, ".xlsx");

            var contentType = "application/vnd.ms-excel";

            var templateFile = new FileInfo(path);
            //var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);


            var package = new ExcelPackage(templateFile);
            var workSheet = package.Workbook.Worksheets[1];

            rowId = 2;
            foreach (var detail in list)
            {


                var salesOrderList = _orderService.GetAllSalesOrderDetail(detail.SalesOrderId).ToList();

                foreach (var salesDetail in salesOrderList)
                {
                    workSheet.Cells["A" + rowId.ToString()].Value = detail.SalesNo;
                    workSheet.Cells["B" + rowId.ToString()].Value = detail.customer.CustomerDropDownDisplay;
                    workSheet.Cells["C" + rowId.ToString()].Value = salesDetail.product.ProductInfoDisplay;
                    workSheet.Cells["D" + rowId.ToString()].Value = salesDetail.UnitPrice;
                    workSheet.Cells["E" + rowId.ToString()].Value = salesDetail.SalesPrice;
                    workSheet.Cells["F" + rowId.ToString()].Value = salesDetail.Quantity;
                    workSheet.Cells["G" + rowId.ToString()].Value = detail.DateCreatedWithFormat;
                    rowId++;
                }




            }




            var memoryStream = new MemoryStream();
            //package.Save();
            package.SaveAs(memoryStream);
            memoryStream.Position = 0;

            return File(memoryStream, contentType, fileNameGenerated);
        }


        private System.Web.Mvc.FileResult ReportSalesOrderReceiptExtract(int salesOrderId, int customerId, string salesNo)
        {

            int rowId = 0;
            int colId = 0;

            decimal qtyTotal = 0;
            decimal salesPriceTotal = 0;


            var customerDetails = _customerService.GetAll().Where(c => c.CustomerId == customerId).FirstOrDefault();
            var list = _orderService.GetAllSalesOrderDetail(salesOrderId).ToList();

            var dir = Server.MapPath(string.Format("~/{0}", Constants.ProductExcelTemplateDir));
            var fileNameTemplate = string.Format("{0}{1}", Constants.ReceiptTemplate, ".xlsx");
            var path = System.IO.Path.Combine(dir, fileNameTemplate);
            var fileNameGenerated = string.Format("{0}{1}", salesNo, ".xlsx");

            var contentType = "application/vnd.ms-excel";

            var templateFile = new FileInfo(path);
            //var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            ;

            var package = new ExcelPackage(templateFile);
            var workSheet = package.Workbook.Worksheets[1];



            workSheet.Cells["H4"].Value = salesNo;
            workSheet.Cells["B5"].Value = customerDetails.CustomerDropDownDisplay;
            workSheet.Cells["B7"].Value = customerDetails.CustomerAddress;
            workSheet.Cells["H5"].Value = DateTime.Now.ToString(Globals.DefaultRecordDateTimeFormat);

            rowId = 11;
            foreach (var detail in list)
            {


                workSheet.Cells["A" + rowId.ToString()].Value = detail.Quantity;
                workSheet.Cells["E" + rowId.ToString()].Value = detail.product.ProductInfoDisplay;
                workSheet.Cells["G" + rowId.ToString()].Value = detail.UnitPrice;
                workSheet.Cells["H" + rowId.ToString()].Value = detail.SalesPrice;



                qtyTotal = qtyTotal + detail.Quantity;
                salesPriceTotal = salesPriceTotal + detail.SalesPrice;

                rowId++;
            }

            workSheet.Cells["A27"].Value = qtyTotal;
            workSheet.Cells["H27"].Value = salesPriceTotal;



            var memoryStream = new MemoryStream();
            //package.Save();
            package.SaveAs(memoryStream);
            memoryStream.Position = 0;

            return File(memoryStream, contentType, fileNameGenerated);
        }
        #endregion Private methods


    }
}