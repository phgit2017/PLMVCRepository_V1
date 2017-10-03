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

using Infrastructure.Utilities.Extensions;
using LinqKit;
namespace PL.MVC.IOBalance.Controllers
{
    public partial class CommonController : Controller
    {
        #region DeclarationsAndConstructors
        private IUnitService _unitService;
        private ICategoryService _categoryService;
        private IModelService _modelService;
        private ISupplierService _supplierService;
        private ICustomerService _customerService;
        private IInventoryService _inventoryService;
        private IUserService _userService;
        private IBranchService _branchService;

        public CommonController(IUnitService unitService,
            ICategoryService categoryService,
            IModelService modelService,
            ISupplierService supplierService,
            ICustomerService customerService,
            IInventoryService inventoryService,
            IUserService userService,
            IBranchService branchService)
        {
            this._unitService = unitService;
            this._categoryService = categoryService;
            this._modelService = modelService;
            this._supplierService = supplierService;
            this._customerService = customerService;
            this._inventoryService = inventoryService;
            this._userService = userService;
            this._branchService = branchService;
        }
        #endregion DeclarationsAndConstructors

        #region ActionMethods
        [HttpGet]
        public virtual JsonResult GetIsActive()
        {
            List<Active> data = new List<Active>();


            data.Add(new Active()
            {
                text = "True",
                value = "true",
                BoolValue = true
            });

            data.Add(new Active()
            {

                text = "False",
                value = "false",
                BoolValue = false
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetUnit()
        {
            var model = _unitService.GetAll().OrderBy(u => u.UnitID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetCategory()
        {
            var model = _categoryService.GetAll().Where(c => c.IsActive).OrderBy(c => c.CategoryCode);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetModel()
        {
            var model = _modelService.GetAll().OrderBy(m => m.ModelName);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetSupplier()
        {
            var model = _supplierService.GetAll().Where(s => s.IsActive).OrderBy(s => s.SupplierCode);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetCustomer()
        {
            var model = _customerService.GetAll().Where(c => c.IsActive).OrderBy(c => c.LastName);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetProduct()
        {
            var model = _inventoryService.GetAll().Where(p => p.IsActive).OrderBy(p => p.ProductCode);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetProductByCategoryId(int categoryId)
        {
            var model = _inventoryService.GetAll().Where(p => p.IsActive && p.CategoryID == categoryId).OrderBy(p => p.ProductCode);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetProductByBranchId(int branchId)
        {
            var model = _inventoryService.GetAll().Where(p => p.IsActive && p.BranchID == branchId).OrderBy(p => p.ProductCode);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetUserType()
        {
            var model = _userService.GetAllUserType().OrderBy(u => u.UserTypeName);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetBranch()
        {
            var model = _branchService.GetAll().OrderBy(b => b.BranchName);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion ActionMethods

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_unitService.IsNull())
                {
                    this._unitService = null;
                }

                if (!_categoryService.IsNull())
                {
                    this._categoryService = null;
                }

                if (!_modelService.IsNull())
                {
                    this._modelService = null;
                }

                if (!_supplierService.IsNull())
                {
                    this._supplierService = null;
                }

                if (!_inventoryService.IsNull())
                {
                    this._inventoryService = null;
                }

                if (!_customerService.IsNull())
                {
                    this._customerService = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion Dispose
    }
}
