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
using System.Drawing;
using Infrastructure.Utilities;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using OfficeOpenXml;
using System.IO;

namespace PL.MVC.IOBalance.Areas.OrderManagement.Controllers
{
    public partial class PurchaseOrderController : BaseController
    {
        #region DeclarationAndConstructors
        IInventoryService _inventoryService;
        IOrderService _orderService;
        public PurchaseOrderController(IInventoryService inventoryService,
            IOrderService orderService)
        {
            this._inventoryService = inventoryService;
            this._orderService = orderService;

        }
        #endregion DeclarationAndConstructors

        #region ActionMethods
        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult Index()
        {
            //var model = GetProduct();
            //var model = _inventoryService.GetAll();
            return View();
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        [HttpPost]
        public virtual ActionResult AddProduct(ProductDto dto)
        {
            string inventory = string.Empty, alertMessage = string.Empty;
            bool isSuccess = true;

            //List<ProductDto> productsToAdd = new List<ProductDto>();

            if (!dto.IsNull())
            {
                isSuccess = true;
                inventory = this.RenderRazorViewToString(IOBALANCEMVC.OrderManagement.PurchaseOrder.Views._ListOfOrders, dto);
            }
            else
            {
                isSuccess = false;
                Danger("cannot be duplicate with your data");
            }


            alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage,
                inventory = inventory
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        [HttpPost]
        public virtual ActionResult SaveProductAndPurchaseOrder(ProductDto dto)
        {
            string inventory = string.Empty, alertMessage = string.Empty;
            bool isSuccess = true, isPurchaseOrderNew = false;


            var createdBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();
            var branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            var userTypeId = Session[SessionVariables.UserDetails].GetUserTypeIdFromSession();


            DateTime? dateNow = System.DateTime.Now;
            int PONumber = _orderService.GeneratePONumber();
            string pONumber = string.Format(Constants.POSOFormat, BusinessSettings.CreatePOSONumber(Enums.OrderTypeAbbrev.PO), PONumber == 0 ? 1 : PONumber += 1);

            //decimal oldProductQty = 0;
            long? identityProductId = 0;

            ProductDto productList = new ProductDto();
            if (userTypeId == Constants.UserTypeAdminId)
            {
                productList = GetProduct().Where(p => p.ProductCode == dto.ProductCode && p.ProductName == dto.ProductName && p.ProductExtension == dto.ProductExtension && p.BranchID == dto.BranchID && p.Size == dto.Size).FirstOrDefault();
            }
            else
            {
                productList = GetProduct().Where(p => p.ProductCode == dto.ProductCode && p.ProductName == dto.ProductName && p.ProductExtension == dto.ProductExtension && p.BranchID == branchId && p.Size == dto.Size).FirstOrDefault();
            }


            if (productList.IsNull())
            {
                dto.CreatedBy = createdBy;
                dto.DateCreated = dateNow;
                dto.ProductID = 0;
                dto.IsActive = true;
                dto.BranchID = userTypeId == Constants.UserTypeUserId ? branchId : dto.BranchID;
                isPurchaseOrderNew = true;
                //oldProductQty = dto.Quantity;
                identityProductId = _inventoryService.SaveProduct(dto);

                if (identityProductId <= 0)
                {
                    isSuccess = false;
                    Danger(Messages.ErrorOccuredDuringProcessing);
                }
            }
            else
            {
                dto.UpdatedBy = createdBy;
                dto.DateUpdated = dateNow;
                //oldProductQty = productList.Quantity;

                if (!_inventoryService.UpdateProductQty(productList.ProductID, dto))
                {
                    isSuccess = false;
                    Danger(Messages.ErrorOccuredDuringProcessing);
                }
                else
                {
                    identityProductId = productList.ProductID;
                }
            }

            OrderDto orderDto = new OrderDto()
            {
                OrderId = 0,
                OrderNum = pONumber,
                OrderDetailId = 0,
                BranchId = userTypeId == Constants.UserTypeUserId ? branchId : dto.BranchID,
                CreatedBy = createdBy,
                DateCreated = dateNow
            };
            var identityPurchaseOrderId = _orderService.SavePurchaseOrder(orderDto);

            if (identityPurchaseOrderId <= 0)
            {
                isSuccess = false;
                Danger(Messages.ErrorOccuredDuringProcessing);
            }
            else
            {
                OrderDetailDto orderDetailDto = new OrderDetailDto()
                {
                    ProductId = identityProductId,
                    PurchaseOrderId = identityPurchaseOrderId,
                    SupplierId = dto.SupplierID,
                    OverrideDisplay = dto.OverrideDisplay,
                    OverrideExtDisplay = dto.OverrideExtDisplay,
                    Quantity = dto.Quantity,
                    isNewPurchaseOrder = isPurchaseOrderNew
                };

                if (!_orderService.SavePurchaseOrderDetail(orderDetailDto))
                {
                    isSuccess = false;
                }


                if (!_orderService.SaveReportCombination(orderDto, orderDetailDto, Enums.OrderTypeAbbrev.PO.ToString()))
                {
                    isSuccess = false;
                }

                if (!isSuccess)
                {
                    Danger(Messages.ErrorOccuredDuringProcessing);
                }
                else
                {
                    isSuccess = true;
                    //var list = GetProduct();
                    //inventory = this.RenderRazorViewToString(IOBALANCEMVC.OrderManagement.PurchaseOrder.Views._ListInventory, list);
                    Success(Messages.InsertSuccess);
                }

            }



            alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage,
                inventory = inventory
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        [HttpPost]
        public virtual ActionResult UpdateProductDetails(ProductDto dto)
        {
            string alertMessage = string.Empty, inventory = string.Empty;
            bool isSuccess = true;

            var branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            var userTypeId = Session[SessionVariables.UserDetails].GetUserTypeIdFromSession();

            dto.UpdatedBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();
            dto.DateUpdated = DateTime.Now;
            dto.BranchID = userTypeId == Constants.UserTypeUserId ? branchId : dto.BranchID;


            //if (ModelState.IsValid)
            //{
            if (!_inventoryService.UpdateProductDetails(dto.ProductID, dto))
            {
                isSuccess = false;
                Danger(Messages.ErrorOccuredDuringProcessing);
            }
            else
            {
                isSuccess = true;
                Success(Messages.UpdateSuccess);
            }
            //}
            //else
            //{
            //    isSuccess = false;
            //    Danger(Messages.ErrorOccuredDuringProcessing);
            //}



            alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage,
                inventory = inventory
            };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        [HttpPost]
        public virtual ActionResult VoidProductQty(ProductDto dto)
        {
            string alertMessage = string.Empty;
            bool isSuccess = true;

            var createdBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();
            var branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            var userTypeId = Session[SessionVariables.UserDetails].GetUserTypeIdFromSession();

            DateTime? dateNow = System.DateTime.Now;

            dto.UpdatedBy = createdBy;
            dto.DateUpdated = dateNow;
            if (!_inventoryService.VoidQty(dto))
            {
                isSuccess = false;
                Danger(Messages.ErrorOccuredDuringProcessing);
            }
            else
            {
                OrderDto orderDto = new OrderDto()
                {
                    OrderNum = string.Empty,
                    Remarks = dto.Remarks
                };

                OrderDetailDto orderDetailDto = new OrderDetailDto()
                {
                    ProductId = dto.ProductID,
                    Quantity = dto.Quantity
                };

                if (!_orderService.SaveReportCombination(orderDto, orderDetailDto, Enums.OrderTypeAbbrev.CR.ToString(), dto.CRType))
                {
                    isSuccess = false;
                    Danger(Messages.ErrorOccuredDuringProcessing);
                }
                else
                {
                    isSuccess = true;
                    Success(Messages.UpdateSuccess);
                }
            }


            alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage
            };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        [HttpPost]
        public virtual ActionResult AddQtyProduct(ProductDto dto)
        {
            string alertMessage = string.Empty, inventory = string.Empty;
            bool isSuccess = true;

            var branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            var createdBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();
            var dateNow = System.DateTime.Now;

            int PONumber = _orderService.GeneratePONumber();
            string pONumber = string.Format(Constants.POSOFormat, BusinessSettings.CreatePOSONumber(Enums.OrderTypeAbbrev.PO), PONumber == 0 ? 1 : PONumber++);

            if (!_inventoryService.AddQty(dto))
            {
                isSuccess = false;
                Danger(Messages.ErrorOccuredDuringProcessing);
            }
            else
            {

                OrderDto orderDto = new OrderDto()
                {
                    OrderId = 0,
                    OrderNum = pONumber,
                    OrderDetailId = 0,
                    BranchId = branchId,
                    CreatedBy = createdBy,
                    DateCreated = dateNow
                };
                var identityPurchaseOrderId = _orderService.SavePurchaseOrder(orderDto);

                if (identityPurchaseOrderId <= 0)
                {
                    isSuccess = false;
                    Danger(Messages.ErrorOccuredDuringProcessing);
                }
                else
                {
                    OrderDetailDto orderDetailDto = new OrderDetailDto()
                    {
                        ProductId = dto.ProductID,
                        PurchaseOrderId = identityPurchaseOrderId,
                        SupplierId = dto.SupplierID,
                        OverrideDisplay = dto.OverrideDisplay,
                        OverrideExtDisplay = dto.OverrideExtDisplay,
                        Quantity = dto.Quantity
                    };

                    if (!_orderService.SavePurchaseOrderDetail(orderDetailDto))
                    {
                        isSuccess = false;
                        Danger(Messages.ErrorOccuredDuringProcessing);
                    }
                    else
                    {
                        if (!_orderService.SaveReportCombination(orderDto, orderDetailDto, Enums.OrderTypeAbbrev.PO.ToString()))
                        {
                            isSuccess = false;
                            Danger(Messages.ErrorOccuredDuringProcessing);
                        }
                        else
                        {
                            isSuccess = true;
                            Success(Messages.UpdateSuccess);
                        }

                    }
                }

            }



            alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage,
                inventory = inventory
            };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        [HttpPost]
        public virtual ActionResult UploadProductImage(ProductDto dto)
        {
            string alertMessage = string.Empty, inventory = string.Empty, test = string.Empty;
            bool isSuccess = true;
            HttpPostedFileBase file;

            dto.UpdatedBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();
            dto.DateUpdated = System.DateTime.Now;

            alertMessage = string.Empty;
            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage
            };

            try
            {
                file = Request.Files[0];
            }
            catch (Exception e)
            {
                isSuccess = false;
                alertMessage = Messages.NoSelectedFile;
                jsonResult = new
                {
                    isSuccess = isSuccess,
                    alertMessage = alertMessage
                };
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }

            if (file.ContentLength != 0)
            {


                if (!file.IsValidProfilePictureFileType())
                {
                    isSuccess = false;
                    alertMessage = Messages.FileTypeNotAccepted;
                    jsonResult = new
                    {
                        isSuccess = isSuccess,
                        alertMessage = alertMessage
                    };
                    return Json(jsonResult, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var dir = Server.MapPath(string.Format("~/{0}", Constants.ProductImageContentDir));
                    var fileName = string.Format("{0}{1}{2}", dto.ProductID, DateTime.Now.ToString("MMddyyyy"), System.IO.Path.GetExtension(file.FileName));
                    var path = System.IO.Path.Combine(dir, fileName);
                    var img = Image.FromStream(file.InputStream);

                    dto.ProductImage = fileName;
                    if (!_inventoryService.UpdateProductImage(dto.ProductID, dto))
                    {
                        isSuccess = false;
                        alertMessage = Messages.ErrorOccuredDuringProcessing;
                        jsonResult = new
                        {
                            isSuccess = isSuccess,
                            alertMessage = alertMessage
                        };
                        return Json(jsonResult, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        var productDetails = _inventoryService.FindByProductId(dto.ProductID);
                        if (!productDetails.ProductImage.IsNull())
                        {
                            var existingDir = Server.MapPath(string.Format("~/{0}", Constants.ProductImageContentDir));
                            var existingFileName = productDetails.ProductImage;
                            var existingPath = System.IO.Path.Combine(existingDir, existingFileName);
                            System.IO.File.Delete(existingPath);
                        }

                        //img = img.Resize(new Size(Constants.ProfilePictureDimensionWidth, Constants.ProfilePictureDimensionHeight));
                        img.Save(path);
                        Success(Messages.UpdateSuccess);
                        alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
                        //var list = GetProduct();
                        //inventory = this.RenderRazorViewToString(IOBALANCEMVC.OrderManagement.PurchaseOrder.Views._ListInventory, list);
                        //alertMessage = Messages.UpdateSuccess;
                        var jsonResults = new
                        {
                            isSuccess = isSuccess,
                            alertMessage = alertMessage,
                            inventory = inventory
                        };
                        return Json(jsonResults, JsonRequestBehavior.AllowGet);
                    }
                }



            }
            else
            {
                isSuccess = false;
                alertMessage = Messages.ErrorOccuredDuringProcessing;
                //ModelState.AddModelError("ProductImage", Messages.ErrorOccuredDuringProcessing);
                //alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
                jsonResult = new
                {
                    isSuccess = isSuccess,
                    alertMessage = alertMessage
                };
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
                //return View(IOBALANCEMVC.OrderManagement.PurchaseOrder.Views._FormEditProductImage, dto);
            }


            //return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult GetInventory([DataSourceRequest] DataSourceRequest request, ProductSearchModel searchModel)
        {
            IQueryable<ProductDto> list = null;
            list = GetProductQueryable(searchModel);

            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        [HttpPost]
        public virtual ActionResult ExtractInventory(ProductSearchModel searchModel)
        {
            List<ProductDto> listOfInventory = new List<ProductDto>();
            listOfInventory = GetProductQueryable(searchModel).ToList();

            if (listOfInventory.Count > 0)
            {
                return ReportInventoryExtract(searchModel, listOfInventory);
            }
            else
            {
                return View();
            }
        }

        #region Batch Upload
        public virtual ActionResult BatchInventory()
        {
            return View();
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        [HttpPost]
        public virtual ActionResult UploadInventory(HttpPostedFileBase file)
        {
            string content = string.Empty, messageResult = string.Empty;
            bool isSuccess = true;
            int rowId = 2;

            var branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            var createdBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();
            var dateNow = System.DateTime.Now;

            List<BatchProductLogDto> listDto = new List<BatchProductLogDto>();

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
                    else if (noOfCol != 15)
                    {
                        isSuccess = false;
                        messageResult = Messages.ExcelUploadNumberOfColumns;
                    }
                    else
                    {

                        var dto = new BatchSummariesDto()
                        {
                            FileName = file.FileName,
                            FilePath = Constants.ProductBatchExcelUploads,
                            TotalRecords = (noOfRow - 1),
                            Successful = 0,
                            Failed = 0,
                            UploadStatus = Globals.Uploading.ToUpper(),
                            UploadedBy = createdBy,
                            StartUpload = dateNow
                        };

                        if (dto.IsNull())
                        {
                            isSuccess = false;
                            messageResult = Messages.ErrorOccuredDuringProcessing;
                        }
                        else
                        {
                            for (int i = 0; i < (noOfRow - 1); i++)
                            {
                                var branchName = workSheet.Cells["A" + rowId.ToString()].Value;
                                var categoryCode = workSheet.Cells["B" + rowId.ToString()].Value;
                                var categoryName = workSheet.Cells["C" + rowId.ToString()].Value;
                                var productCode = workSheet.Cells["D" + rowId.ToString()].Value;
                                var productName = workSheet.Cells["E" + rowId.ToString()].Value;
                                var extension = workSheet.Cells["F" + rowId.ToString()].Value;
                                var quantity = workSheet.Cells["G" + rowId.ToString()].Value;
                                var originalPrice = workSheet.Cells["H" + rowId.ToString()].Value;
                                var price = workSheet.Cells["I" + rowId.ToString()].Value;
                                var size = workSheet.Cells["J" + rowId.ToString()].Value;
                                var supplierCode = workSheet.Cells["K" + rowId.ToString()].Value;
                                var supplierName = workSheet.Cells["L" + rowId.ToString()].Value;
                                var description = workSheet.Cells["M" + rowId.ToString()].Value;
                                var barCode = workSheet.Cells["N" + rowId.ToString()].Value;
                                var remarks = workSheet.Cells["O" + rowId.ToString()].Value;

                                listDto.Add(new BatchProductLogDto()
                                {
                                    BranchName = branchName == null ? string.Empty : branchName.ToString(),
                                    CategoryCode = categoryCode == null ? string.Empty : categoryCode.ToString(),
                                    CategoryName = categoryName == null ? string.Empty : categoryName.ToString(),
                                    ProductCode = productCode == null ? string.Empty : productCode.ToString(),
                                    ProductName = productName == null ? string.Empty : productName.ToString(),
                                    Extension = extension == null ? string.Empty : extension.ToString(),
                                    Quantity = quantity == null ? string.Empty : quantity.ToString(),
                                    OriginalPrice = originalPrice == null ? string.Empty : originalPrice.ToString(),
                                    Price = price == null ? string.Empty : price.ToString(),
                                    Size = size == null ? string.Empty : size.ToString(),
                                    SupplierCode = supplierCode == null ? string.Empty : supplierCode.ToString(),
                                    SupplierName = supplierName == null ? string.Empty : supplierName.ToString(),
                                    Description = description == null ? string.Empty : description.ToString(),
                                    BarCode = barCode == null ? string.Empty : barCode.ToString(),
                                    Remarks = remarks == null ? string.Empty : remarks.ToString()
                                });

                                rowId++;
                            }


                            var resultSave = _inventoryService.SaveBulkProducts(dto, listDto);

                            if (resultSave == 1)
                            {
                                isSuccess = false;
                                messageResult = Messages.ErrorOccuredDuringProcessing;
                            }
                            else if (resultSave == 2)
                            {
                                isSuccess = false;
                                messageResult = Messages.BatchNoEmpty;
                            }
                            else if (resultSave == 3)
                            {
                                isSuccess = false;
                                messageResult = Messages.BulkInsertError;
                            }
                            else if (resultSave == 4)
                            {
                                isSuccess = false;
                                messageResult = Messages.ErrorOccuredDuringBatchProcessing;
                            }
                            else if (resultSave == 6)
                            {
                                isSuccess = false;
                                messageResult = Messages.InsertSuccessButWithError;
                            }
                            else if (resultSave == 7)
                            {
                                isSuccess = false;
                                messageResult = Messages.BatchUploadErrorAllRecords;
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
                messageResult = messageResult
            };

            return Json(json, Globals.ContentTypeTextPlain, JsonRequestBehavior.AllowGet);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult GetBatchSummaries([DataSourceRequest] DataSourceRequest request, BatchSummariesSearchModel searchModel)
        {
            IQueryable<BatchSummariesDto> list = null;
            list = GetBatchSummaries(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion Batch Upload

        #endregion ActionMethods

        #region PrivateMethods
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

        private System.Web.Mvc.FileResult ReportInventoryExtract(ProductSearchModel searchModel, List<ProductDto> listOfInventory)
        {
            int rowId = 0;
            int colId = 0;

            var fileNameGenerated = string.Format("{0}{1}", Constants.InventoryList, ".xlsx");
            var contentType = "application/vnd.ms-excel";

            //var templateFile = new FileInfo(path);
            //var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);

            var package = new ExcelPackage();
            var workSheet = package.Workbook.Worksheets.Add(Constants.InventoryList);

            workSheet.Cells["A1"].Value = "Product Name";
            workSheet.Cells["B1"].Value = "Product Size";
            workSheet.Cells["C1"].Value = "Product Description";
            workSheet.Cells["D1"].Value = "Quantity";
            workSheet.Cells["E1"].Value = "Product Price";
            workSheet.Cells["F1"].Value = "Branch Name";

            rowId = 2;
            for (int i = 0; i < listOfInventory.Count; i++)
            {

                workSheet.Cells["A" + rowId.ToString()].Value = listOfInventory[i].ProductFullDisplayWithExtension;
                workSheet.Cells["B" + rowId.ToString()].Value = listOfInventory[i].Size;
                workSheet.Cells["C" + rowId.ToString()].Value = listOfInventory[i].ProductDesc;
                workSheet.Cells["D" + rowId.ToString()].Value = listOfInventory[i].QtyWithFormat;
                workSheet.Cells["E" + rowId.ToString()].Value = listOfInventory[i].Price;
                workSheet.Cells["F" + rowId.ToString()].Value = listOfInventory[i].BranchName;
                rowId++;

            }


            var memoryStream = new MemoryStream();
            //package.Save();
            package.SaveAs(memoryStream);
            memoryStream.Position = 0;

            return File(memoryStream, contentType, fileNameGenerated);
        }

        private IQueryable<BatchSummariesDto> GetBatchSummaries(BatchSummariesSearchModel searchModel = null)
        {
            IQueryable<BatchSummariesDto> list = null;

            if (searchModel.IsNull())
            {
                list = _inventoryService.GetAllBatchSummaries().OrderByDescending(b => b.BatchNo);
            }
            else
            {
                var predicate = PredicateBuilder.True<BatchSummariesDto>();

                if (!searchModel.FileNameUploaded.IsNull())
                {
                    predicate = predicate.And(p => p.FileName.Contains(searchModel.FileNameUploaded));
                }

                if (!searchModel.ResultFileNameGenerated.IsNull())
                {
                    predicate = predicate.And(p => p.ResultFileName.Contains(searchModel.ResultFileNameGenerated));
                }



                list = _inventoryService.GetAllBatchSummaries().AsExpandable().Where(predicate).OrderByDescending(b => b.BatchNo);


            }

            return list;
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
            }
            base.Dispose(disposing);
        }
        #endregion Dispose


    }
}
