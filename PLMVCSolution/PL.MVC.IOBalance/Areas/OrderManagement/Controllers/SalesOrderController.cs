using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Business
using PL.Business.Common;
using PL.Business.Dto.IOBalance;
using PL.Business.Interface.IOBalance;

//MVC
using PL.MVC.IOBalance.Controllers;
using PL.MVC.IOBalance.Areas.AdminManagement.Models;
using PL.MVC.IOBalance.Areas.OrderManagement.Models;

using PL.MVC.IOBalance.Infrastructure;
using Infrastructure.Utilities.Extensions;
using LinqKit;

using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.IO;
using System.Reflection;
using System.Data.Entity;
using Infrastructure.Utilities;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace PL.MVC.IOBalance.Areas.OrderManagement.Controllers
{
    /// <summary>
    /// </summary>
    public partial class SalesOrderController : BaseController
    {

        #region DeclarationsAndConstructors
        IInventoryService _inventoryService;
        IOrderService _orderService;
        ISalesService _salesService;
        IUnitService _unitService;
        IDiscountService _discountService;
        public SalesOrderController(IInventoryService inventoryService,
            IOrderService orderService,
            ISalesService salesService,
            IUnitService unitService,
            IDiscountService discountService)
        {
            this._inventoryService = inventoryService;
            this._orderService = orderService;
            this._salesService = salesService;
            this._unitService = unitService;
            this._discountService = discountService;
        }
        #endregion DeclarationsAndConstructors

        #region ActionMethods
        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult Index()
        {
            int discountPercentage = 0;
            discountPercentage = GetDiscount();




            var listOfProduct = GetProduct();
            var listOfSavedOrder = GetSalesOrder();

            SOSubmitModel list = new SOSubmitModel()
            {
                productDto = listOfProduct,
                salesOrderDto = listOfSavedOrder,
                DiscountAmount = discountPercentage
            };

            return View(list);
            //return View();
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult AddSalesOrder(SalesOrderDetailDto dto)
        {
            string alertMessage = string.Empty, salesOrder = string.Empty;
            bool isSuccess = true;
            List<SalesOrderDetailDto> listOfOrders = new List<SalesOrderDetailDto>();

            var inventoryList = _inventoryService.FindByProductId(dto.ProductID);
            var unitList = _unitService.FindUnitById((int)dto.UnitID);

            if (inventoryList.IsNull())
            {
                isSuccess = false;
                Danger(Messages.NoProductFound);
            }
            else
            {
                if (!dto.IsNull())
                {

                    listOfOrders.Add(new SalesOrderDetailDto()
                    {
                        ProductID = dto.ProductID,
                        ProductCode = inventoryList == null ? string.Empty : inventoryList.ProductCode,
                        ProductName = inventoryList == null ? string.Empty : inventoryList.ProductName,
                        ProductExt = inventoryList == null ? string.Empty : inventoryList.ProductExtension,
                        UnitID = dto.UnitID,
                        UnitDesc = unitList == null ? string.Empty : unitList.UnitDesc,
                        UnitPrice = dto.UnitPrice,
                        Quantity = dto.Quantity

                    });
                    isSuccess = true;
                    salesOrder = this.RenderRazorViewToString(IOBALANCEMVC.OrderManagement.SalesOrder.Views._ListOfOrderSales, listOfOrders);
                }
            }




            alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage,
                salesOrder = salesOrder

            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult ClickAddSalesOrder(long productId, int? branchId = null)
        {
            string alertMessage = string.Empty, salesOrder = string.Empty;
            bool isSuccess = true;
            int? userTypeId = Session[SessionVariables.UserDetails].GetUserTypeIdFromSession();

            decimal discountAmount = 0;
            decimal originalAmount = 0;
            decimal priceAmount = 0;


            List<SalesOrderDetailDto> listOfOrders = new List<SalesOrderDetailDto>();

            var inventoryList = _inventoryService.FindByProductId(productId);
            //var unitList = _unitService.FindUnitById((int)dto.UnitID);

            if (inventoryList.IsNull())
            {
                isSuccess = false;
                Danger(Messages.NoProductFound);
            }
            else
            {


                if (userTypeId == Constants.UserTypeAdminId)
                {
                    discountAmount = GetDiscount(branchId);
                }
                else
                {
                    discountAmount = GetDiscount();
                }


                if (discountAmount > 0)
                {
                    discountAmount = discountAmount / 100;
                    originalAmount = (inventoryList == null ? 0 : inventoryList.Price);
                    priceAmount = originalAmount - (originalAmount * discountAmount);
                }
                else
                {
                    priceAmount = (inventoryList == null ? 0 : inventoryList.Price);
                }

                listOfOrders.Add(new SalesOrderDetailDto()
                {
                    ProductID = productId,
                    ProductCode = inventoryList == null ? string.Empty : inventoryList.ProductCode,
                    ProductName = inventoryList == null ? string.Empty : inventoryList.ProductName,
                    ProductExt = inventoryList == null ? string.Empty : inventoryList.ProductExtension,
                    //UnitID = dto.UnitID,
                    //UnitDesc = unitList == null ? string.Empty : unitList.UnitDesc,
                    UnitPrice = priceAmount,
                    Quantity = 1

                });
                isSuccess = true;
                salesOrder = this.RenderRazorViewToString(IOBALANCEMVC.OrderManagement.SalesOrder.Views._ListOfOrderSales, listOfOrders);
            }

            alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage,
                salesOrder = salesOrder,
                salesOrderCount = listOfOrders.Count(),
                salesOrderTotal = listOfOrders.Sum(e => e.TotalPricePerItemQty)
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult SaveSalesOrder(List<SalesOrderDetailDto> salesOrderDetailDto, int customerId, int? branchIdAdmin)
        {

            string alertMessage = string.Empty, salesOrder = string.Empty, products = string.Empty;
            string orderNum = string.Empty;
            bool isSuccess = true;

            long identitySalesOrderId = 0;
            int SONumber = _orderService.GenerateSONumber();


            string sONumber = string.Format("{0}{1:D4}", BusinessSettings.CreatePOSONumber(Enums.OrderTypeAbbrev.SO), SONumber == 0 ? 1 : SONumber += 1);

            var createdBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();
            DateTime? dateNow = System.DateTime.Now;
            int? branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            int? userTypeId = Session[SessionVariables.UserDetails].GetUserTypeIdFromSession();

            int? discountAmount = 0;

            if (userTypeId == Constants.UserTypeAdminId)
            {
                branchId = branchIdAdmin;
                discountAmount = GetDiscount(branchIdAdmin);
            }
            else
            {
                discountAmount = GetDiscount();
            }

            List<OrderDetailDto> orderDetailsDto = new List<OrderDetailDto>();
            List<ProductDto> productDto = new List<ProductDto>();

            alertMessage = isValidSalesOrder(customerId);

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(alertMessage))
                {
                    isSuccess = false;
                    Danger(alertMessage);
                }
                else
                {
                    if (salesOrderDetailDto.IsNull())
                    {
                        isSuccess = false;
                        Danger(string.Format(Messages.PleaseSelectItem, "Product"));
                    }
                    else
                    {
                        OrderDto orderDto = new OrderDto()
                        {
                            OrderNum = sONumber,
                            CustomerId = customerId,
                            CreatedBy = createdBy,
                            DateCreated = dateNow,
                            BranchId = branchId,
                            DiscountPercentage = discountAmount
                        };
                        identitySalesOrderId = _orderService.SaveSalesOrder(orderDto);

                        if (identitySalesOrderId == 0)
                        {

                            isSuccess = false;
                            Danger(Messages.ErrorOccuredDuringProcessing);
                        }
                        else
                        {
                            var unitList = _unitService.GetAll().ToList();

                            foreach (var item in salesOrderDetailDto)
                            {

                                orderDetailsDto.Add(new OrderDetailDto()
                                {
                                    SalesOrderId = identitySalesOrderId,
                                    ProductId = item.ProductID,
                                    Quantity = item.Quantity,
                                    UnitId = unitList.Where(u => u.UnitDesc == item.UnitDesc).FirstOrDefault() == null ? null : (int?)unitList.Where(u => u.UnitDesc == item.UnitDesc).FirstOrDefault().UnitID,
                                    UnitPrice = item.UnitPrice
                                });

                                productDto.Add(new ProductDto()
                                {
                                    ProductID = item.ProductID,
                                    Quantity = (decimal)item.Quantity
                                });
                            }
                            if (!_orderService.SaveSalesOrderDetail(orderDetailsDto))
                            {
                                isSuccess = false;
                                Danger(Messages.ErrorOccuredDuringProcessing);
                            }
                            else
                            {
                                foreach (var item in productDto)
                                {
                                    if (!_inventoryService.UpdateProductQtyOrder(item.ProductID, item, Enums.OrderTypeAbbrev.SO.ToString()))
                                    {
                                        isSuccess = false;
                                    }
                                }

                                foreach (var item in orderDetailsDto)
                                {
                                    if (!_orderService.SaveReportCombination(orderDto, item, Enums.OrderTypeAbbrev.SO.ToString()))
                                    {
                                        isSuccess = false;
                                    }
                                }




                                if (!isSuccess)
                                {
                                    Danger(Messages.ErrorOccuredDuringProcessing);
                                }
                                else
                                {
                                    Success(Messages.InsertSuccess);
                                }


                            }
                        }

                    }
                }

            }
            else
            {
                isSuccess = false;
            }



            alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage,
                salesOrder = salesOrder
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult SearchProductDetails(ProductSearchModel searchModel)
        {
            var model = GetProduct(searchModel);
            return PartialView(IOBALANCEMVC.OrderManagement.SalesOrder.Views._ListOfProduct, model);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult SearchSavedSalesOrder(SavedSalesOrderSearchModel searchModel)
        {
            var list = GetSalesOrder(searchModel);
            return PartialView(IOBALANCEMVC.OrderManagement.SalesOrder.Views._ListOfSavedSalesOrder, list);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult ExtractReceipt(long salesOrderId)
        {
            SalesOrderDto salesOrderDto = new SalesOrderDto();
            List<SalesOrderDetailDto> salesOrderDetail = new List<SalesOrderDetailDto>();

            int baseId = 0;

            salesOrderDto = _orderService.FindBySalesOrderID(salesOrderId);
            salesOrderDetail = _orderService.FindSalesOrderDetailBySalesOrderID(salesOrderId).ToList();

            var dir = Server.MapPath(string.Format("~/{0}", Constants.ProductExcelTemplateDir));
            var fileNameTemplate = string.Format("{0}{1}", Constants.ReceiptTemplate, ".xlsx");
            var path = System.IO.Path.Combine(dir, fileNameTemplate);
            var fileNameGenerated = string.Format("{0}{1}", salesOrderDto.SalesOrderNum, ".xlsx");

            var contentType = "application/vnd.ms-excel";

            var templateFile = new FileInfo(path);
            //var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);

            var package = new ExcelPackage(templateFile);
            var workSheet = package.Workbook.Worksheets[1];

            workSheet.Cells["A1:H1"].Value = salesOrderDto.BranchName;
            workSheet.Cells["A2:H2"].Value = salesOrderDto.BranchAddress;
            workSheet.Cells["A3:H3"].Value = salesOrderDto.BranchDetails;


            workSheet.Cells["B5:E5"].Value = salesOrderDto.CustomerFullName;
            workSheet.Cells["B6:E6"].Value = string.Empty;
            workSheet.Cells["B7:E7"].Value = salesOrderDto.CustomerAddress;
            //workSheet.Cells["B8:E8"].Value = "BUSINESS STYLE";

            workSheet.Cells["H4"].Value = salesOrderDto.SalesOrderNum;
            workSheet.Cells["H5"].Value = salesOrderDto.DateCreated;
            //workSheet.Cells["H6"].Value = salesOrderDto.PaymentTerms;

            baseId = 11;
            for (int i = 0; i < salesOrderDetail.Count; i++)
            {
                workSheet.Cells["A" + baseId.ToString()].Value = salesOrderDetail[i].Quantity;
                workSheet.Cells["C" + baseId.ToString()].Value = salesOrderDetail[i].UnitDesc;
                workSheet.Cells["E" + baseId.ToString()].Value = salesOrderDetail[i].ProductFullDisplayWithExtension;
                workSheet.Cells["G" + baseId.ToString()].Value = salesOrderDetail[i].UnitPrice;
                workSheet.Cells["H" + baseId.ToString()].Value = salesOrderDetail[i].TotalPricePerItemQty;

                baseId++;
            }

            workSheet.Cells["H27"].Value = salesOrderDetail.Sum(e => e.TotalPricePerItemQty);

            var memoryStream = new MemoryStream();
            //package.Save();
            package.SaveAs(memoryStream);
            memoryStream.Position = 0;

            return File(memoryStream, contentType, fileNameGenerated);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult RefreshProductDetails(ProductSearchModel searchModel)
        {
            var model = GetProduct(searchModel);
            return PartialView(IOBALANCEMVC.OrderManagement.SalesOrder.Views._ListOfProduct, model);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult RefreshSavedSalesOrder(SavedSalesOrderSearchModel searchModel)
        {
            var list = GetSalesOrder(searchModel);
            return PartialView(IOBALANCEMVC.OrderManagement.SalesOrder.Views._ListOfSavedSalesOrder, list);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult GetProductList([DataSourceRequest] DataSourceRequest request, ProductSearchModel searchModel)
        {
            IQueryable<ProductDto> list = null;
            list = GetProductQueryable(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult GetSalesOrderList([DataSourceRequest] DataSourceRequest request, SavedSalesOrderSearchModel searchModel)
        {
            IQueryable<SalesOrderDto> list = null;
            list = GetSalesOrderQueryable(searchModel).OrderByDescending(s => s.SalesOrderID);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult SearchProductPerBarCode(string barCode, int? branchIdAdmin)
        {
            bool hasProduct = false;
            int? branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            int? userTypeId = Session[SessionVariables.UserDetails].GetUserTypeIdFromSession();

            if (userTypeId == Constants.UserTypeAdminId)
            {
                branchId = branchIdAdmin;
            }

            var list = _inventoryService.GetAll().Where(p => p.BarCode == barCode && p.BranchID == branchId).FirstOrDefault();

            if (!list.IsNull())
            {
                hasProduct = true;
            }

            var jsonResult = new
            {
                hasProduct = hasProduct,
                list = list
            };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
        #endregion ActionMethods

        #region PrivateMethods
        private string isValidSalesOrder(int customerId)
        {
            string msg = string.Empty;

            if ((customerId.IsNull() || customerId == 0))
            {
                msg = "Please select a customer <br/>";
            }

            return msg;
        }
        private List<ProductDto> GetProduct(ProductSearchModel searchModel = null)
        {
            int? branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            int? userTypeId = Session[SessionVariables.UserDetails].GetUserTypeIdFromSession();

            List<ProductDto> list = new List<ProductDto>();
            if ((searchModel.IsNull() && !branchId.IsNull() && (userTypeId == Constants.UserTypeUserId)))
            {
                list = _inventoryService.GetAll().Where(p => p.BranchID == branchId).ToList();
            }
            else if ((searchModel.IsNull() && branchId.IsNull() && (userTypeId == Constants.UserTypeAdminId)))
            {
                list = _inventoryService.GetAll().ToList();
            }
            else
            {
                var predicate = PredicateBuilder.True<ProductDto>();
                var hasOtherFilter = false;

                if (!searchModel.ProductCode.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(p => p.ProductCode.Contains(searchModel.ProductCode));
                }

                if (!searchModel.ProductName.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(p => p.ProductName.Contains(searchModel.ProductName));
                }

                if (!searchModel.ProductExt.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(p => p.ProductExtension.Contains(searchModel.ProductExt));
                }

                if (!searchModel.CategoryId.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(p => p.CategoryID == searchModel.CategoryId);
                }

                if (userTypeId == Constants.UserTypeAdminId)
                {
                    if (!searchModel.BranchId.IsNull())
                    {
                        hasOtherFilter = true;
                        predicate = predicate.And(p => p.BranchID == searchModel.BranchId);
                    }

                    list = _inventoryService.GetAll().AsExpandable().Where(predicate).ToList();
                }
                else
                {
                    predicate = predicate.And(p => p.BranchID == branchId);
                    list = _inventoryService.GetAll().AsExpandable().Where(predicate).ToList();
                }

            }



            return list;
        }
        private IQueryable<ProductDto> GetProductQueryable(ProductSearchModel searchModel = null)
        {
            int? branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            int? userTypeId = Session[SessionVariables.UserDetails].GetUserTypeIdFromSession();

            IQueryable<ProductDto> list = null;
            if ((searchModel.IsNull() && !branchId.IsNull() && (userTypeId == Constants.UserTypeUserId)))
            {
                list = _inventoryService.GetAll().Where(p => p.BranchID == branchId);
            }
            else if ((searchModel.IsNull() && branchId.IsNull() && (userTypeId == Constants.UserTypeAdminId)))
            {
                list = _inventoryService.GetAll();
            }
            else
            {
                var predicate = PredicateBuilder.True<ProductDto>();
                var hasOtherFilter = false;

                if (!searchModel.ProductCode.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(p => p.ProductCode.Contains(searchModel.ProductCode));
                }

                if (!searchModel.ProductName.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(p => p.ProductName.Contains(searchModel.ProductName));
                }

                if (!searchModel.ProductExt.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(p => p.ProductExtension.Contains(searchModel.ProductExt));
                }

                if (!searchModel.CategoryId.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(p => p.CategoryID == searchModel.CategoryId);
                }

                if (!searchModel.Size.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(p => p.Size == searchModel.Size);

                }

                if (userTypeId == Constants.UserTypeAdminId)
                {
                    if (!searchModel.BranchId.IsNull())
                    {
                        hasOtherFilter = true;
                        predicate = predicate.And(p => p.BranchID == searchModel.BranchId);
                    }

                    list = _inventoryService.GetAll().AsExpandable().Where(predicate);
                }
                else
                {
                    predicate = predicate.And(p => p.BranchID == branchId);
                    list = _inventoryService.GetAll().AsExpandable().Where(predicate);
                }

            }



            return list;
        }
        private List<SalesOrderDto> GetSalesOrder(SavedSalesOrderSearchModel searchModel = null)
        {
            int? branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            int? userTypeId = Session[SessionVariables.UserDetails].GetUserTypeIdFromSession();

            List<SalesOrderDto> list = new List<SalesOrderDto>();
            if ((searchModel.IsNull() && !branchId.IsNull() && (userTypeId == Constants.UserTypeUserId)))
            {
                list = _orderService.GetAllSalesOrder().Where(s => s.BranchID == branchId).OrderByDescending(s => s.SalesOrderID).ToList();
            }
            else if ((searchModel.IsNull() && branchId.IsNull() && (userTypeId == Constants.UserTypeAdminId)))
            {
                list = _orderService.GetAllSalesOrder().OrderByDescending(s => s.SalesOrderID).ToList();
            }
            else
            {
                var predicate = PredicateBuilder.True<SalesOrderDto>();
                var hasOtherFilter = false;

                if (!searchModel.ReceiptNumber.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(p => p.SalesOrderNum.Contains(searchModel.ReceiptNumber));
                }
                //searchModel.DateFrom
                //searchModel.DateTo
                if (!searchModel.DateFrom.IsNull() && searchModel.DateTo.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(a => DbFunctions.TruncateTime(a.DateCreated) >= DbFunctions.TruncateTime(searchModel.DateFrom));
                }
                else if (searchModel.DateFrom.IsNull() && !searchModel.DateTo.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(a => DbFunctions.TruncateTime(a.DateCreated) <= DbFunctions.TruncateTime(searchModel.DateTo));
                }
                else if (!searchModel.DateFrom.IsNull() && !searchModel.DateTo.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(a => DbFunctions.TruncateTime(a.DateCreated) >= DbFunctions.TruncateTime(searchModel.DateFrom) && DbFunctions.TruncateTime(a.DateCreated) <= DbFunctions.TruncateTime(searchModel.DateTo));
                }

                if (userTypeId == Constants.UserTypeAdminId)
                {
                    if (!searchModel.BranchId.IsNull())
                    {
                        hasOtherFilter = true;
                        predicate = predicate.And(p => p.BranchID == searchModel.BranchId);
                    }

                    list = _orderService.GetAllSalesOrder().AsExpandable().Where(predicate).ToList();
                }
                else
                {
                    predicate = predicate.And(p => p.BranchID == branchId);
                    list = _orderService.GetAllSalesOrder().AsExpandable().Where(predicate).ToList();
                }


            }



            return list;
        }
        private IQueryable<SalesOrderDto> GetSalesOrderQueryable(SavedSalesOrderSearchModel searchModel = null)
        {
            int? branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            int? userTypeId = Session[SessionVariables.UserDetails].GetUserTypeIdFromSession();

            IQueryable<SalesOrderDto> list = null;
            if ((searchModel.IsNull() && !branchId.IsNull() && (userTypeId == Constants.UserTypeUserId)))
            {
                list = _orderService.GetAllSalesOrder().Where(s => s.BranchID == branchId);
            }
            else if ((searchModel.IsNull() && branchId.IsNull() && (userTypeId == Constants.UserTypeAdminId)))
            {
                list = _orderService.GetAllSalesOrder();
            }
            else
            {
                var predicate = PredicateBuilder.True<SalesOrderDto>();
                var hasOtherFilter = false;

                if (!searchModel.ReceiptNumber.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(p => p.SalesOrderNum.Contains(searchModel.ReceiptNumber));
                }
                //searchModel.DateFrom
                //searchModel.DateTo
                if (!searchModel.DateFrom.IsNull() && searchModel.DateTo.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(a => DbFunctions.TruncateTime(a.DateCreated) >= DbFunctions.TruncateTime(searchModel.DateFrom));
                }
                else if (searchModel.DateFrom.IsNull() && !searchModel.DateTo.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(a => DbFunctions.TruncateTime(a.DateCreated) <= DbFunctions.TruncateTime(searchModel.DateTo));
                }
                else if (!searchModel.DateFrom.IsNull() && !searchModel.DateTo.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(a => DbFunctions.TruncateTime(a.DateCreated) >= DbFunctions.TruncateTime(searchModel.DateFrom) && DbFunctions.TruncateTime(a.DateCreated) <= DbFunctions.TruncateTime(searchModel.DateTo));
                }

                if (userTypeId == Constants.UserTypeAdminId)
                {
                    if (!searchModel.BranchId.IsNull())
                    {
                        hasOtherFilter = true;
                        predicate = predicate.And(p => p.BranchID == searchModel.BranchId);
                    }

                    list = _orderService.GetAllSalesOrder().AsExpandable().Where(predicate);
                }
                else
                {
                    predicate = predicate.And(p => p.BranchID == branchId);
                    list = _orderService.GetAllSalesOrder().AsExpandable().Where(predicate);
                }


            }



            return list;
        }
        private int GetDiscount(int? BranchId = null)
        {
            int discountPercentage = 0;

            int? branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            int? userTypeId = Session[SessionVariables.UserDetails].GetUserTypeIdFromSession();

            if (userTypeId == Constants.UserTypeUserId)
            {
                discountPercentage = this._discountService.GetAll().Where(d => d.BranchID == branchId && d.IsActive).FirstOrDefault() == null ? 0 : this._discountService.GetAll().Where(d => d.BranchID == branchId && d.IsActive).FirstOrDefault().DiscountPercentage;
            }
            else
            {
                discountPercentage = this._discountService.GetAll().Where(d => d.BranchID == BranchId && d.IsActive).FirstOrDefault() == null ? 0 : this._discountService.GetAll().Where(d => d.BranchID == BranchId && d.IsActive).FirstOrDefault().DiscountPercentage;
            }

            return discountPercentage;
        }
        #endregion PrivateMethods

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

                if (!_salesService.IsNull())
                {
                    _salesService = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion Dispose

    }
}
