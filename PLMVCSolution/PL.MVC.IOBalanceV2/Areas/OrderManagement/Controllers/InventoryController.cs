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
using OfficeOpenXml;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace PL.MVC.IOBalanceV2.Areas.OrderManagement.Controllers
{
    public partial class InventoryController : BaseController
    {
        #region Declarations and constructors
        private IInventoryService _inventoryService;
        private IOrderService _orderService;

        public InventoryController(IInventoryService inventoryService, IOrderService orderService)
        {
            this._inventoryService = inventoryService;
            this._orderService = orderService;
        }
        #endregion Declarations and constructors

        #region Public methods
        public virtual ActionResult Index()
        {
            return View();
        }



        [System.Web.Mvc.HttpPost]
        public virtual ActionResult SaveDetails(ProductDto dto)
        {
            bool isSuccess = false;
            string alertMessage = string.Empty;

            if (ModelState.IsValid)
            {
                dto.ProductId = 0;
                var duplicate = _inventoryService.GetAll().Where(c => c.ProductCode == dto.ProductCode).Count();

                if (duplicate >= 1)
                {
                    alertMessage = string.Format(Messages.DuplicateItem, "Product");
                }
                else
                {
                    var retProductId = _inventoryService.SaveDetails(dto);

                    if (retProductId > 0)
                    {
                        OrderDto orders = new OrderDto() { OrderId = 0, DateCreated = DateTime.Now, CreatedBy = 1 };
                        var retPurchaseOrderId = _orderService.SaveOrder(orders);
                        if (retPurchaseOrderId > 0)
                        {
                            OrderDetailDto orderDetails = new OrderDetailDto()
                            {
                                OrderDetailId = 0,
                                OrderId = retPurchaseOrderId,
                                ProductId = retProductId,
                                Quantity = Convert.ToDecimal(dto.Quantity),
                                SupplierId = dto.SupplierId
                            };

                            if (_orderService.SaveOrderDetail(orderDetails))
                            {
                                isSuccess = true;
                                alertMessage = Messages.InsertSuccess;
                            }
                        }
                    }
                    else
                    {
                        alertMessage = string.Format(Messages.ErrorOccuredDuringProcessingThis, "saving details of product");
                    }
                }

            }
            else
            {
                alertMessage = Messages.ErrorOccuredDuringProcessingOrRequiredFields;
            }


            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }


        [System.Web.Mvc.HttpPost]
        public virtual ActionResult UpdateDetails(EditProductDto dto)
        {
            bool isSuccess = false;
            string alertMessage = string.Empty;

            if (ModelState.IsValid)
            {


                var duplicate = _inventoryService.GetAll().Where(i => i.ProductCode == dto.ProductCode && i.ProductId != dto.ProductId).Count();

                if (duplicate >= 1)
                {
                    alertMessage = string.Format(Messages.DuplicateItem, "Product");
                }
                else
                {
                    ProductDto newDetails = new ProductDto()
                    {
                        ProductId = dto.ProductId,
                        CategoryId = dto.CategoryId,
                        QuantityUnitId = dto.QuantityUnitId,
                        ProductCode = dto.ProductCode,
                        ProductName = dto.ProductName,
                        ProductDescription = dto.ProductDescription,
                        ProductSize = dto.ProductSize,
                        CurrentNum = dto.CurrentNum,
                        DRNum = dto.DRNum,
                        CartonNum = dto.CartonNum,
                        Quantity = dto.Quantity
                    };


                    isSuccess = _inventoryService.UpdateDetails(newDetails);

                    if (!isSuccess)
                    {
                        alertMessage = string.Format(Messages.ErrorOccuredDuringProcessingThis, "updating in product");
                    }
                    else
                    {
                        alertMessage = Messages.UpdateSuccess;
                    }

                }

            }
            else
            {
                alertMessage = Messages.ErrorOccuredDuringProcessingOrRequiredFields;
            }


            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public virtual ActionResult UpdateQtyDetails(EditQtyProductDto dto, string qtyType)
        {
            bool isSuccess = false;
            string alertMessage = string.Empty;
            decimal? qtyBefore = dto.EditQuantity;
            int? createdBy = WebSecurity.GetUserId(User.Identity.Name);

            if (ModelState.IsValid)
            {

                if (!qtyType.IsNull())
                {
                    switch (qtyType.ToLower())
                    {
                        case "add":
                            dto.EditQuantity = dto.EditQuantity + dto.OldQuantity;
                            break;
                        case "minus":
                            dto.EditQuantity = dto.OldQuantity - dto.EditQuantity;
                            break;
                        default:
                            break;
                    }

                    ProductDto newDetails = new ProductDto()
                    {
                        ProductId = dto.ProductId,
                        CategoryId = dto.CategoryId,
                        QuantityUnitId = dto.QuantityUnitId,
                        ProductCode = dto.ProductCode,
                        ProductName = dto.ProductName,
                        ProductDescription = dto.ProductDescription,
                        ProductSize = dto.ProductSize,
                        CurrentNum = dto.CurrentNum,
                        DRNum = dto.DRNum,
                        CartonNum = dto.CartonNum,
                        Quantity = dto.EditQuantity
                    };

                    if (qtyType.ToLower() == "minus")
                    {
                        SalesOrderDto orderDto = new SalesOrderDto()
                        {
                            CreatedBy = createdBy,
                            DateCreated = DateTime.Now,
                            SalesNo = "PL" + Constants.SalesTemplate,
                            CustomerId = 0,
                            SalesOrderId = 0,
                            IsPrinted = false,
                            IsCorrected = false
                        };
                        var salesOrderId = _orderService.SaveSalesOrder(orderDto);

                        if (salesOrderId == 0)
                        {
                            alertMessage = string.Format(Messages.ErrorOccuredDuringProcessingThis, "saving in sales order");
                        }
                        else
                        {

                            SalesOrderDetailDto orderDetailDto = new SalesOrderDetailDto()
                            {
                                SalesOrderId = salesOrderId,
                                ProductId = newDetails.ProductId,
                                SalesPrice = 0,
                                Quantity = (decimal)qtyBefore,
                                UnitPrice = 0,
                                DateCreated = DateTime.Now,
                                CreatedBy = createdBy
                            };
                            isSuccess = _orderService.SaveSalesOrderDetail(orderDetailDto);


                            if (!isSuccess)
                            {
                                alertMessage = string.Format(Messages.ErrorOccuredDuringProcessingThis, "saving in sales order details");
                            }
                            else
                            {
                                isSuccess = _inventoryService.UpdateDetails(newDetails);

                                if (!isSuccess)
                                {
                                    alertMessage = string.Format(Messages.ErrorOccuredDuringProcessingThis, "updating in product");
                                }
                                else
                                {
                                    alertMessage = Messages.UpdateSuccess;
                                }
                            }

                        }
                    }
                    else
                    {
                        OrderDto orders = new OrderDto() { OrderId = 0, DateCreated = DateTime.Now, CreatedBy = createdBy };
                        var retPurchaseOrderId = _orderService.SaveOrder(orders);
                        if (retPurchaseOrderId == 0)
                        {
                            alertMessage = string.Format(Messages.ErrorOccuredDuringProcessingThis, "saving in purchase order");

                        }
                        else
                        {
                            OrderDetailDto orderDetails = new OrderDetailDto()
                            {
                                OrderDetailId = 0,
                                OrderId = retPurchaseOrderId,
                                ProductId = newDetails.ProductId,
                                Quantity = Convert.ToDecimal(qtyBefore),
                                SupplierId = dto.SupplierId
                            };

                            if (!_orderService.SaveOrderDetail(orderDetails))
                            {
                                alertMessage = (string.Format(Messages.ErrorOccuredDuringProcessingThis, "saving in purchase order details"));
                            }
                            else
                            {
                                isSuccess = _inventoryService.UpdateDetails(newDetails);

                                if (!isSuccess)
                                {
                                    alertMessage = string.Format(Messages.ErrorOccuredDuringProcessingThis, "updating in product");
                                }
                                else
                                {
                                    alertMessage = Messages.UpdateSuccess;
                                }
                            }
                        }
                    }
                }
            }

            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }


        public virtual ActionResult GetDetails([DataSourceRequest] DataSourceRequest request, InventorySearchModel searchModel)
        {
            var list = GetDetail(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #region Batch Inventory
        public virtual ActionResult BulkInventory()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public virtual ActionResult UploadInventory(HttpPostedFileBase file)
        {
            string content = string.Empty, messageResult = string.Empty, xmlResult = string.Empty;
            bool isSuccess = true;
            int rowId = 2;
            int? createdBy = 1;


            var dateNow = System.DateTime.Now;
            var dataResult = string.Empty;

            if (!file.IsNull())
            {

                if (!file.IsValidExcelFile())
                {
                    isSuccess = false;
                    messageResult = Messages.InvalidFileType;
                }
                else
                {
                    var package = new ExcelPackage(file.InputStream);
                    var currentSheet = package.Workbook.Worksheets;
                    var workSheet = currentSheet.First();
                    var noOfCol = workSheet.Dimension.End.Column;
                    var noOfRow = workSheet.Dimension.End.Row;

                    if (noOfRow < 2)
                    {
                        isSuccess = false;
                        messageResult = Messages.ExcelUploadedEmpty;
                    }
                    else if (noOfCol != 13)
                    {
                        isSuccess = false;
                        messageResult = Messages.ExcelUploadNumberOfColumns;
                    }
                    else
                    {

                        var dtValues = new DataTable("Items");
                        dtValues.Columns.Add("PRODUCT_CODE");
                        dtValues.Columns.Add("PLUS_MINUS");
                        dtValues.Columns.Add("QUANTITY");
                        dtValues.Columns.Add("IS_NEW");
                        dtValues.Columns.Add("CATEGORY_NAME");
                        dtValues.Columns.Add("SUPPLIER_CODE");
                        dtValues.Columns.Add("UNIT_NAME");
                        dtValues.Columns.Add("PRODUCT_NAME");
                        dtValues.Columns.Add("PRODUCT_DESC");
                        dtValues.Columns.Add("PRODUCT_SIZE");
                        dtValues.Columns.Add("CURRENT_NUM");
                        dtValues.Columns.Add("DRNUM");
                        dtValues.Columns.Add("CARTON_NUM");

                        for (int i = 0; i < (noOfRow - 1); i++)
                        {
                            var productCode = workSheet.Cells["A" + rowId.ToString()].Value;
                            var plusMinus = workSheet.Cells["B" + rowId.ToString()].Value;
                            var quantity = workSheet.Cells["C" + rowId.ToString()].Value;
                            var isNew = workSheet.Cells["D" + rowId.ToString()].Value;
                            var categoryName = workSheet.Cells["E" + rowId.ToString()].Value;
                            var supplierCode = workSheet.Cells["F" + rowId.ToString()].Value;
                            var unitName = workSheet.Cells["G" + rowId.ToString()].Value;
                            var productName = workSheet.Cells["H" + rowId.ToString()].Value;
                            var productDesc = workSheet.Cells["I" + rowId.ToString()].Value;
                            var productSize = workSheet.Cells["J" + rowId.ToString()].Value;
                            var currentNum = workSheet.Cells["K" + rowId.ToString()].Value;
                            var drNum = workSheet.Cells["L" + rowId.ToString()].Value;
                            var cartonNum = workSheet.Cells["M" + rowId.ToString()].Value;

                            dtValues.Rows.Add(
                                productCode == null ? string.Empty : productCode.ToString(),
                                plusMinus == null ? string.Empty : plusMinus.ToString(),
                                quantity == null ? string.Empty : quantity.ToString(),
                                isNew == null ? string.Empty : isNew.ToString(),
                                categoryName == null ? string.Empty : categoryName.ToString(),
                                supplierCode == null ? string.Empty : supplierCode.ToString(),
                                unitName == null ? string.Empty : unitName.ToString(),
                                productName == null ? string.Empty : productName.ToString(),
                                productDesc == null ? string.Empty : productDesc.ToString(),
                                productSize == null ? string.Empty : productSize.ToString(),
                                currentNum == null ? string.Empty : currentNum.ToString(),
                                drNum == null ? string.Empty : drNum.ToString(),
                                cartonNum == null ? string.Empty : cartonNum.ToString()
                                );

                            rowId++;
                        }


                        using (StringWriter sw = new StringWriter())
                        {

                            dtValues.WriteXml(sw);
                            xmlResult = sw.ToString();
                            xmlResult = xmlResult.Replace("<DocumentElement>", "<NewDataSet>");
                            xmlResult = xmlResult.Replace("</DocumentElement>", "</NewDataSet>");

                        }


                        if (xmlResult.IsEmptyString())
                        {
                            isSuccess = false;
                            messageResult = string.Format(Messages.ErrorOccuredDuringProcessingThis, "XML Empty");
                        }
                        else
                        {
                            BatchInventoryLogDto batchInventoryDto = new BatchInventoryLogDto()
                            {
                                BatchInventoryId = 0,
                                CreatedBy = createdBy,
                                DateCreated = dateNow,
                                FileName = file.FileName
                            };
                            if (!_inventoryService.SaveBatchInvetoryLogs(batchInventoryDto))
                            {
                                isSuccess = false;
                                messageResult = string.Format(Messages.ErrorOccuredDuringProcessingThis, "Batch Inventory Log");
                            }
                            else
                            {
                                var result = _inventoryService.SaveBatchInventory(xmlResult, createdBy);




                                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                                List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                                Dictionary<string, object> childRow;
                                foreach (DataRow row in result.Rows)
                                {
                                    childRow = new Dictionary<string, object>();
                                    foreach (DataColumn col in result.Columns)
                                    {
                                        childRow.Add(col.ColumnName, row[col]);
                                    }
                                    parentRow.Add(childRow);
                                }

                                dataResult = jsSerializer.Serialize(parentRow);


                                isSuccess = true;
                                messageResult = "Successfully uploaded!";

                            }
                        }



                    }

                }

            }
            else
            {
                isSuccess = false;
            }

            var json = new
            {
                result = isSuccess,
                messageResult = messageResult,
                dataResult = dataResult
            };

            return Json(json, Globals.ContentTypeTextPlain, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult ExportBatchUploaded(string dataResult)
        {
            int rowId = 0;
            int colId = 0;




            var dir = Server.MapPath(string.Format("~/{0}", Constants.ProductExcelTemplateDir));
            var fileNameTemplate = string.Format("{0}{1}", Constants.BatchUploadInventoryReturnTemplate, ".xlsx");
            var path = System.IO.Path.Combine(dir, fileNameTemplate);
            var fileNameGenerated = string.Format("{0}{1}", DateTime.Now.ToString(Globals.DefaultRecordDateFormat), ".xlsx");

            var contentType = "application/vnd.ms-excel";

            var templateFile = new FileInfo(path);

            var package = new ExcelPackage(templateFile);
            var workSheet = package.Workbook.Worksheets[1];


            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<BatchInventoryResult> list = serializer.Deserialize<List<BatchInventoryResult>>(dataResult);

            rowId = 2;
            foreach (var detail in list)
            {

                workSheet.Cells["A" + rowId.ToString()].Value = detail.STATUSMESSAGE;
                workSheet.Cells["B" + rowId.ToString()].Value = detail.PRODUCT_CODE;
                workSheet.Cells["C" + rowId.ToString()].Value = detail.PLUS_MINUS;
                workSheet.Cells["D" + rowId.ToString()].Value = detail.QUANTITY;
                workSheet.Cells["E" + rowId.ToString()].Value = detail.IS_NEW;
                workSheet.Cells["F" + rowId.ToString()].Value = detail.CATEGORY_NAME;
                workSheet.Cells["G" + rowId.ToString()].Value = detail.SUPPLIER_CODE;
                workSheet.Cells["H" + rowId.ToString()].Value = detail.UNIT_NAME;
                workSheet.Cells["I" + rowId.ToString()].Value = detail.PRODUCT_NAME;
                workSheet.Cells["J" + rowId.ToString()].Value = detail.PRODUCT_DESC;
                workSheet.Cells["K" + rowId.ToString()].Value = detail.PRODUCT_SIZE;
                workSheet.Cells["L" + rowId.ToString()].Value = detail.CURRENT_NUM;
                workSheet.Cells["M" + rowId.ToString()].Value = detail.DRNUM;
                workSheet.Cells["N" + rowId.ToString()].Value = detail.CARTON_NUM;

                rowId++;
            }

            var memoryStream = new MemoryStream();
            //package.Save();
            package.SaveAs(memoryStream);
            memoryStream.Position = 0;

            return File(memoryStream, contentType, fileNameGenerated);
        }
        #endregion Batch Inventory


        #endregion Public methods

        #region Private methods
        private IQueryable<ProductDto> GetDetail(InventorySearchModel searchModel)
        {

            IQueryable<ProductDto> list = null;

            if (!searchModel.HasAnyValue())
            {
                list = _inventoryService.GetAll().OrderByDescending(i => i.ProductId);
            }
            else
            {
                var predicate = PredicateBuilder.True<ProductDto>();



                if (!searchModel.CategoryId.IsNull())
                {
                    predicate = predicate.And(c => c.CategoryId == searchModel.CategoryId);
                }

                if (!searchModel.ProductCode.IsNull())
                {
                    predicate = predicate.And(c => c.ProductCode == searchModel.ProductCode);
                }

                if (!searchModel.ProductName.IsNull())
                {
                    predicate = predicate.And(c => c.ProductName.Contains(searchModel.ProductName));
                }

                if (!searchModel.ProductDescription.IsNull())
                {
                    predicate = predicate.And(c => c.ProductDescription.Contains(searchModel.ProductDescription));
                }

                if (!searchModel.ProductSize.IsNull())
                {
                    predicate = predicate.And(c => c.ProductSize == searchModel.ProductSize);
                }

                if (!searchModel.CurrentNum.IsNull())
                {
                    predicate = predicate.And(c => c.CurrentNum == searchModel.CurrentNum);
                }

                if (!searchModel.DRNum.IsNull())
                {
                    predicate = predicate.And(c => c.DRNum == searchModel.DRNum);
                }

                if (!searchModel.CartonNum.IsNull())
                {
                    predicate = predicate.And(c => c.CartonNum == searchModel.CartonNum);
                }



                list = _inventoryService.GetAll().AsExpandable().Where(predicate).OrderByDescending(i => i.ProductId);

            }

            return list;

        }
        #endregion Private methods


        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_inventoryService.IsNull())
                {
                    _inventoryService = null;
                }

                if (!_orderService.IsNull())
                {
                    _orderService = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion Dispose


    }
}