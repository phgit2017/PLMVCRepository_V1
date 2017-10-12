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
                //dto.DateCreated = DateTime.Now;
                //dto.CreatedBy = 1;
                //dto.IsActive = true;
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
                        }
                    }
                }
                else
                {
                    Danger(string.Format(Messages.ErrorOccuredDuringProcessing, "saving details of product"));
                }
            }
            else
            {
                Danger(string.Format(Messages.ErrorOccuredDuringProcessing, "saving models of product"));
            }

            alertMessage = this.RenderRazorViewToString(IOBALANCEMVCV2.Shared.Views._Alerts, string.Empty);
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
                //dto.DateUpdated = DateTime.Now;
                //dto.UpdatedBy = 1;
                isSuccess = _inventoryService.UpdateDetails(newDetails);
            }

            if (!isSuccess)
            {
                Danger(Messages.ErrorOccuredDuringProcessing);
            }
            else
            {
                Success(Messages.UpdateSuccess);
            }

            alertMessage = this.RenderRazorViewToString(IOBALANCEMVCV2.Shared.Views._Alerts, string.Empty);
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
                    //dto.DateUpdated = DateTime.Now;
                    //dto.UpdatedBy = 1;

                    if (qtyType.ToLower() == "minus")
                    {
                        SalesOrderDto orderDto = new SalesOrderDto()
                        {
                            CreatedBy = 1,
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
                                CreatedBy = 1
                            };
                            isSuccess = _orderService.SaveSalesOrderDetail(orderDetailDto);


                            if (!isSuccess)
                            {
                                Danger(string.Format(Messages.ErrorOccuredDuringProcessingThis, "saving in sales order details"));
                            }
                            else
                            {
                                isSuccess = _inventoryService.UpdateDetails(newDetails);
                            }

                        }
                    }
                    else
                    {
                        OrderDto orders = new OrderDto() { OrderId = 0, DateCreated = DateTime.Now, CreatedBy = 1 };
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
                                Danger(string.Format(Messages.ErrorOccuredDuringProcessingThis, "saving in purchase order details"));
                            }
                            else
                            {
                                isSuccess = _inventoryService.UpdateDetails(newDetails);
                            }
                        }
                    }
                }
            }

            
            if (isSuccess)
            {
                //_inventoryService.UpdateDetails(newDetails);
                Success(Messages.UpdateSuccess);
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
        #endregion Public methods

        #region Private methods
        private IQueryable<ProductDto> GetDetail(InventorySearchModel searchModel)
        {

            IQueryable<ProductDto> list = null;

            if (!searchModel.HasAnyValue())
            {
                list = _inventoryService.GetAll();
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



                list = _inventoryService.GetAll().AsExpandable().Where(predicate);

            }

            return list;

        }

        private bool testonlyignorebin()
        {
            return false;
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