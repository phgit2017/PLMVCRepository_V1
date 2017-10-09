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
using System.IO;
using OfficeOpenXml;

namespace PL.MVC.IOBalanceV2.Areas.OrderManagement.Controllers
{
    public partial class SalesOrderController : Controller
    {
        #region Declarations and constructors
        private readonly IOrderService _orderService;
        private readonly IInventoryService _inventoryService;
        private readonly ICustomerPriceService _customerPriceService;
        private readonly ICustomerService _customerService;

        public SalesOrderController(IOrderService orderService, IInventoryService inventoryService, ICustomerPriceService customerPriceService,ICustomerService customerService)
        {
            this._orderService = orderService;
            this._inventoryService = inventoryService;
            this._customerPriceService = customerPriceService;
            this._customerService = customerService;
        }
        #endregion Declarations and constructors

        #region Interface implementations
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult SaveSalesOrder(List<SalesOrderListDto> dto, string SalesNo, int CustomerId)
        {
            bool isSuccess = false;
            int createdBy = 1;
            DateTime dateNow = DateTime.Now;
            string alertMsg = string.Empty;
            long salesOrderId = 0;

            if (!dto.IsNull())
            {
                SalesOrderDto orderDto = new SalesOrderDto()
                {
                    CreatedBy = createdBy,
                    DateCreated = dateNow,
                    SalesNo = SalesNo,
                    CustomerId = CustomerId,
                    SalesOrderId = 0,
                    IsPrinted = true,
                    IsCorrected = false
                };
                salesOrderId = _orderService.SaveSalesOrder(orderDto);

                if (salesOrderId == 0)
                {
                    alertMsg = "Error in inserting Order Id";
                }
                else
                {
                    foreach (var oDetail in dto)
                    {
                        SalesOrderDetailDto orderDetailDto = new SalesOrderDetailDto()
                        {
                            SalesOrderId = salesOrderId,
                            ProductId = oDetail.ProductId,
                            SalesPrice = oDetail.SalesPrice,
                            Quantity = oDetail.Quantity,
                            UnitPrice = oDetail.UnitPrice,
                            DateCreated = dateNow,
                            CreatedBy = createdBy
                        };
                        _orderService.SaveSalesOrderDetail(orderDetailDto);
                        var productDetails = _inventoryService.GetAll().Where(p => p.ProductId == oDetail.ProductId).FirstOrDefault();
                        productDetails.Quantity = (productDetails.Quantity - oDetail.Quantity);
                        _inventoryService.UpdateDetails(productDetails);

                        CustomerPriceDto customerPriceDto = new CustomerPriceDto()
                        {
                            Price = oDetail.UnitPrice,
                            ProductId = oDetail.ProductId,
                            CustomerId = CustomerId,
                            CreatedBy = createdBy,
                            DateCreated = dateNow
                        };

                        SaveCustomerPrice(customerPriceDto);
                    }

                    isSuccess = true;
                    alertMsg = Messages.InsertSuccess;
                }
            }

            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMsg = alertMsg,
                salesOrderId = salesOrderId,
                customerId = CustomerId,
                salesNo = SalesNo

            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult QueueSalesOrder(List<SalesOrderListDto> dto, string SalesNo, int CustomerId)
        {
            bool isSuccess = false;
            int createdBy = 1;
            DateTime dateNow = DateTime.Now;
            string alertMsg = string.Empty;

            if (!dto.IsNull())
            {
                SalesOrderDto orderDto = new SalesOrderDto()
                {
                    CreatedBy = createdBy,
                    DateCreated = dateNow,
                    SalesNo = SalesNo,
                    CustomerId = CustomerId,
                    SalesOrderId = 0,
                    IsPrinted = false,
                    IsCorrected = false
                };
                var salesOrderId = _orderService.SaveSalesOrder(orderDto);

                if (salesOrderId == 0)
                {
                    alertMsg = "Error in inserting Order Id";
                }
                else
                {
                    foreach (var oDetail in dto)
                    {
                        SalesOrderDetailDto orderDetailDto = new SalesOrderDetailDto()
                        {
                            SalesOrderId = salesOrderId,
                            ProductId = oDetail.ProductId,
                            SalesPrice = oDetail.SalesPrice,
                            Quantity = oDetail.Quantity,
                            UnitPrice = oDetail.UnitPrice,
                            DateCreated = dateNow,
                            CreatedBy = createdBy
                        };
                        _orderService.SaveSalesOrderDetail(orderDetailDto);


                        CustomerPriceDto customerPriceDto = new CustomerPriceDto()
                        {
                            Price = oDetail.UnitPrice,
                            ProductId = oDetail.ProductId,
                            CustomerId = CustomerId,
                            CreatedBy = createdBy,
                            DateCreated = dateNow
                        };

                        SaveCustomerPrice(customerPriceDto);
                    }

                    isSuccess = true;
                    alertMsg = Messages.InsertSuccess;
                }
            }

            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMsg = alertMsg
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult GetCustomerAndProductPrice(int customerId, int productId)
        {
            decimal? price = 0;
            var customerPrice = _customerPriceService.GetAll().Where(c => c.CustomerId == customerId && c.ProductId == productId).FirstOrDefault();

            if (!customerPrice.IsNull())
            {
                price = customerPrice.Price;
            }

            var jsonResult = new
            {
                price = price
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);

        }

        public virtual ActionResult GetPLNum()
        {
            bool isSuccess = false;
            string SalesNum = string.Empty;

            SalesNum = _orderService.GetSalesNum();
            if (!SalesNum.IsNull())
            {
                isSuccess = true;
            }

            var jsonResult = new
            {
                isSuccess = isSuccess,
                SalesNum = SalesNum
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult GenerateReceipt(long salesOrderId, string salesNo, int customerId)
        {
            return ReportRecieptExtract(salesOrderId, salesNo, customerId);
        }

        #endregion Interface implementations

        #region Private methods
        private bool SaveCustomerPrice(CustomerPriceDto dto)
        {
            bool isSuccess = false;
            var customerPrices = _customerPriceService.GetAll().Where(cp => cp.ProductId == dto.ProductId && cp.CustomerId == dto.CustomerId).FirstOrDefault();

            if (!customerPrices.IsNull())
            {
                if (_customerPriceService.DeleteDetails(dto.CustomerId, dto.ProductId))
                {
                    if (_customerPriceService.SaveDetails(dto))
                    {
                        isSuccess = true;
                    }
                }


            }
            else
            {
                if (_customerPriceService.SaveDetails(dto))
                {
                    isSuccess = true;
                }
            }

            return isSuccess;
        }

        private System.Web.Mvc.FileResult ReportRecieptExtract(long salesOrderId, string salesNo, int customerId)
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
            workSheet.Cells["H5"].Value = DateTime.Now.ToString(Globals.DefaultRecordDateFormat);

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