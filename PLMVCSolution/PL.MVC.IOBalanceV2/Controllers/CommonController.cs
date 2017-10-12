using PL.Business.Interface.IOBalanceV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.MVC.IOBalanceV2.Controllers
{
    public partial class CommonController : Controller
    {
        #region Declarations and constructor
        private readonly ICategoryService _categoryService;
        private readonly IQuantityUnitService _quantityUnitService;
        private readonly ISupplierService _supplierService;
        private readonly ICustomerService _customerService;
        private readonly IInventoryService _inventoryService;

        public CommonController(ICategoryService categoryService,
            IQuantityUnitService quantityUnitService,
            ISupplierService supplierService,
            ICustomerService customerService,
            IInventoryService inventoryService)
        {
            this._categoryService = categoryService;
            this._quantityUnitService = quantityUnitService;
            this._supplierService = supplierService;
            this._customerService = customerService;
            this._inventoryService = inventoryService;
        }
        #endregion
        
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
        public virtual JsonResult GetEditProductEditType()
        {
            List<QtyType> data = new List<QtyType>();

            data.Add(new QtyType() { QuantityTypeIntValue = 1, QuantityTypeName = "Add",QuantityTypeValue = "+" });
            data.Add(new QtyType() { QuantityTypeIntValue = 2, QuantityTypeName = "Minus", QuantityTypeValue = "-" });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetCategories()
        {
            var result = _categoryService.GetAll();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetQuantityUnit()
        {
            var result = _quantityUnitService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetSupplier()
        {
            var result = _supplierService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetCustomers()
        {

            var result = _customerService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetCustomersWithoutAdmin()
        {

            var result = _customerService.GetAll().Where(c => c.CustomerId != 0);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetProducts()
        {
            var result = _inventoryService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult GetProductsPerProductId(int productId)
        {
            var result = _inventoryService.GetAll().Where(p => p.ProductId == productId).FirstOrDefault();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }

    public class Active
    {
        public string text { get; set; }
        public string value { get; set; }
        public bool BoolValue { get; set; }
    }

    public class QtyType
    {
        public string QuantityTypeValue { get; set; }
        public string QuantityTypeName { get; set; }
        public int QuantityTypeIntValue { get; set; }
    }
}