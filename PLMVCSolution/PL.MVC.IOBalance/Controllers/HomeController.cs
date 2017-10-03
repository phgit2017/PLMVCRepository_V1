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
using PL.MVC.IOBalance.Models;
using Infrastructure.Utilities;
namespace PL.MVC.IOBalance.Controllers
{
    public partial class HomeController : BaseController
    {
        #region DeclarationsAndConstructors
        IInventoryService _inventoryService;
        ISalesService _salesService;
        IOrderService _orderService;
        ICustomerService _customerService;
        public HomeController(IInventoryService inventoryService,
            ISalesService salesService,
            IOrderService orderService,
            ICustomerService customerService)
        {
            this._inventoryService = inventoryService;
            this._salesService = salesService;
            this._orderService = orderService;
            this._customerService = customerService;
        }
        #endregion DeclarationsAndConstructors

        #region ActionMethods
        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult Index()
        {

            int? branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            int? userTypeId = Session[SessionVariables.UserDetails].GetUserTypeIdFromSession();

            List<ProductDto> productList = new List<ProductDto>();
            List<CustomerDto> customerList = new List<CustomerDto>();
            List<SalesOrderDto> salesOrderList = new List<SalesOrderDto>();

            if (!branchId.IsNull() || userTypeId == Constants.UserTypeUserId)
            {
                productList = _inventoryService.GetAll().Where(p => p.BranchID == branchId).OrderBy(p => p.BranchName).ToList();
                customerList = _customerService.GetAll().Where(c => c.IsActive).ToList();
                salesOrderList = _orderService.GetAllSalesOrder().Where(s => s.BranchID == branchId).ToList();
            }
            else
            {
                productList = _inventoryService.GetAll().OrderBy(p => p.ProductCode).ThenBy(p => p.BranchName).ToList();
                customerList = _customerService.GetAll().Where(c => c.IsActive).ToList();
                salesOrderList = _orderService.GetAllSalesOrder().ToList();
            }

            var productTotalQty = productList.Sum(p => p.Quantity);

            TransactionsModel model = new TransactionsModel()
            {
                NumCustomers = customerList.Count,
                NumQty = productList.Count,
                NumTotalQty = productTotalQty,
                NumSalesOrders = salesOrderList.Count
            };
            return View(model);
        }
        #endregion ActionMethods

        #region PrivateMethods

        #endregion PrivateMethods

        #region Dispose

        #endregion Dispose



    }
}
