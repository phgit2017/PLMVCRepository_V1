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


namespace PL.MVC.IOBalanceV2.Controllers
{
    public partial class HomeController : Controller
    {

        #region Declarations and constructors
        private readonly IOrderService _orderService;
        private readonly IInventoryService _inventoryService;
        private readonly ICustomerPriceService _customerPriceService;

        public HomeController(IOrderService orderService, IInventoryService inventoryService, ICustomerPriceService customerPriceService)
        {
            this._orderService = orderService;
            this._inventoryService = inventoryService;
            this._customerPriceService = customerPriceService;
        }
        #endregion Declarations and constructors

        public virtual ActionResult GetQueueOrders([DataSourceRequest] DataSourceRequest request, QueueOrderSearchModel searchModel)
        {
            var list = GetDetail(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult GetQueueOrderList([DataSourceRequest] DataSourceRequest request, long salesOrderId)
        {
            var list = _orderService.GetAllSalesOrderDetail(salesOrderId);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult GetOrderAndOrderDetails(long salesOrderId)
        {

            var order = _orderService.GetAllSalesOrder().Where(s => s.OrderId == salesOrderId).FirstOrDefault();
            var orderDetailList = _orderService.GetAllSalesOrderDetail(salesOrderId).ToList();

            var jsonResult = new
            {
                order = order,
                orderDetailList = orderDetailList
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult SaveSalesOrder(List<SalesOrderListDto> dto, int CustomerId, long salesOrderId)
        {
            bool isSuccess = false;
            int createdBy = 1;
            DateTime dateNow = DateTime.Now;
            string alertMsg = string.Empty;

            if (!dto.IsNull())
            {

                var oldSalesOrderDto = _orderService.GetAllSalesOrder().Where(so => so.OrderId == salesOrderId).FirstOrDefault();

                if (oldSalesOrderDto.IsNull())
                {
                    alertMsg = "No sales order selected";
                }
                else
                {
                    SalesOrderDto orderDto = new SalesOrderDto()
                    {
                        CreatedBy = createdBy,
                        DateCreated = dateNow,
                        SalesNo = oldSalesOrderDto.SalesNo,
                        CustomerId = oldSalesOrderDto.CustomerId,
                        SalesOrderId = salesOrderId,
                        IsPrinted = true,
                        IsCorrected = false
                    };
                    if (_orderService.UpdateSalesOrder(orderDto) <= 0)
                    {
                        alertMsg = "Error in updating sales order id";
                    }
                    else
                    {
                        if (!_orderService.DeleteAllSalesOrderDetail(salesOrderId))
                        {
                            alertMsg = "Error in deleting all sales order details";
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


                }

            }

            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMsg = alertMsg
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }


        public virtual ActionResult QueueSalesOrder(List<SalesOrderListDto> dto, int CustomerId, long salesOrderId)
        {
            bool isSuccess = false;
            int createdBy = 1;
            DateTime dateNow = DateTime.Now;
            string alertMsg = string.Empty;

            if (!dto.IsNull())
            {

                var oldSalesOrderDto = _orderService.GetAllSalesOrder().Where(so => so.OrderId == salesOrderId).FirstOrDefault();
                if (oldSalesOrderDto.IsNull())
                {
                    alertMsg = "No queue order selected";
                }
                else
                {
                    SalesOrderDto orderDto = new SalesOrderDto()
                    {
                        CreatedBy = createdBy,
                        DateCreated = dateNow,
                        SalesNo = oldSalesOrderDto.SalesNo,
                        CustomerId = oldSalesOrderDto.CustomerId,
                        SalesOrderId = salesOrderId,
                        IsPrinted = false,
                        IsCorrected = false
                    };
                    if (_orderService.UpdateSalesOrder(orderDto) <= 0)
                    {
                        alertMsg = "Error in updating queue order id";
                    }
                    else
                    {
                        if (!_orderService.DeleteAllSalesOrderDetail(salesOrderId))
                        {
                            alertMsg = "Error in deleting all queue order details";
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
                            alertMsg = Messages.UpdateSuccess;
                        }
                    }
                }
                
            }

            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMsg = alertMsg
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        #region Private methods
        private IQueryable<OrderDto> GetDetail(QueueOrderSearchModel searchModel)
        {

            IQueryable<OrderDto> list = null;

            //if (searchModel.CustomerAddress.IsNull() && searchModel.CustomerCode.IsNull() && searchModel.CustomerName.IsNull() && searchModel.IsActive.IsNull())
            //{
            //    list = _customerService.GetAll();
            //}
            //else
            //{
            //    var predicate = PredicateBuilder.True<CustomerDto>();

            //    if (!searchModel.CustomerCode.IsNull())
            //    {
            //        predicate = predicate.And(c => c.CustomerCode == searchModel.CustomerCode);
            //    }

            //    if (!searchModel.CustomerName.IsNull())
            //    {
            //        predicate = predicate.And(c => c.CustomerName.Contains(searchModel.CustomerName));
            //    }

            //    if (!searchModel.CustomerAddress.IsNull())
            //    {
            //        predicate = predicate.And(c => c.CustomerAddress.Contains(searchModel.CustomerAddress));
            //    }

            //    if (!searchModel.IsActive.IsNull())
            //    {
            //        predicate = predicate.And(c => c.IsActive == searchModel.IsActive);
            //    }


            //    list = _customerService.GetAll().AsExpandable().Where(predicate);

            //}

            list = _orderService.GetAllSalesOrder().Where(so => so.IsPrinted == false && so.IsCorrected == false && so.CustomerId != Constants.CustomerIdAdmin).OrderByDescending(so => so.OrderId);

            return list;

        }

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
        #endregion Private methods


    }
}
