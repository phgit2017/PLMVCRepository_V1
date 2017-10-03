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

using PL.MVC.IOBalance.Infrastructure;
using Infrastructure.Utilities.Extensions;
using LinqKit;
using Infrastructure.Utilities;

//Kendo
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace PL.MVC.IOBalance.Areas.AdminManagement.Controllers
{
    public partial class SupplierController : BaseController
    {
        #region DeclarationAndConstructors
        ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            this._supplierService = supplierService;
        }
        #endregion DeclarationAndConstructors

        #region ActionMethods
        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult Index()
        {
            return View();
        }

        [IOBalanceSessionExpired]
        [HttpPost]
        public virtual ActionResult SaveSupplier(SupplierDto dto)
        {
            bool isSuccess = true;
            string alertMessage = string.Empty;
            int? createdBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();

            dto.CreatedBy = createdBy;


            List<SupplierDto> duplicateList = new List<SupplierDto>();
            duplicateList = _supplierService.GetAll().Where(s => s.SupplierCode == dto.SupplierCode && s.SupplierName == dto.SupplierName && s.IsActive).ToList();

            if (ModelState.IsValid)
            {

                if (duplicateList.Count > 0)
                {
                    isSuccess = false;
                    Danger(string.Format(Messages.DuplicateItem, "Supplier"));
                }
                else
                {
                    if (!_supplierService.SaveSupplier(dto))
                    {
                        isSuccess = false;
                        Danger(Messages.ErrorOccuredDuringProcessing);
                    }
                    else
                    {
                        Success(Messages.InsertSuccess);
                    }
                }



            }
            else
            {
                isSuccess = false;
                Danger(Messages.ErrorOccuredDuringProcessing);
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
        [HttpPost]
        public virtual ActionResult UpdateSupplier(SupplierDto dto)
        {
            bool isSuccess = true;
            string alertMessage = string.Empty;
            var oldSupplierDetails = _supplierService.FindSupplierById(dto.SupplierID);
            dto.SupplierID = oldSupplierDetails.SupplierID;
            dto.DateCreated = oldSupplierDetails.DateCreated;
            dto.IsActive = oldSupplierDetails.IsActive;
            dto.UpdatedBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();

            List<SupplierDto> duplicateList = new List<SupplierDto>();
            duplicateList = _supplierService.GetAll().Where(s => s.SupplierCode == dto.SupplierCode && s.SupplierName == dto.SupplierName && s.IsActive && s.SupplierID != dto.SupplierID).ToList();

            if (duplicateList.Count > 0)
            {
                isSuccess = false;
                Danger(string.Format(Messages.DuplicateItem, "Supplier"));
            }
            else
            {
                if (!_supplierService.UpdateSupplierDetails(dto))
                {
                    isSuccess = false;
                    Danger(Messages.ErrorOccuredDuringProcessing);
                }
                else
                {
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
        [HttpPost]
        public virtual ActionResult isActiveSupplier(int supplierId, string supplierCode, string supplierName)
        {
            bool isSuccess = true;
            string alertMessage = string.Empty;
            int? updatedBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();

            List<SupplierDto> duplicateList = new List<SupplierDto>();
            duplicateList = _supplierService.GetAll().Where(s => s.SupplierCode == supplierCode && s.SupplierName == supplierName && s.SupplierID != supplierId && s.IsActive).ToList();


            if (duplicateList.Count > 0)
            {
                isSuccess = false;
                Danger(string.Format(Messages.DuplicateItem, "Supplier"));
            }
            else
            {
                if (!_supplierService.UpdateInactive(supplierId, updatedBy))
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
        public virtual ActionResult GetSuppliers([DataSourceRequest] DataSourceRequest request, SupplierSearchModel searchModel)
        {
            var list = GetSupplier(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion ActionMethods

        #region PrivateMethods
        private IQueryable<SupplierDto> GetSupplier(SupplierSearchModel searchModel = null)
        {
            IQueryable<SupplierDto> list = null;
            if (searchModel.IsNull())
            {
                list = _supplierService.GetAll();
            }
            else
            {
                var predicate = PredicateBuilder.True<SupplierDto>();
                var hasOtherFilter = false;

                if (!searchModel.SupplierCode.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(s => s.SupplierCode.Contains(searchModel.SupplierCode));
                }

                if (!searchModel.SupplierName.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(s => s.SupplierName.Contains(searchModel.SupplierName));
                }

                if (!searchModel.isActive.IsNull())
                {
                    hasOtherFilter = true;
                    if (searchModel.isActive == "true")
                    {
                        predicate = predicate.And(a => a.IsActive);
                    }
                    else
                    {
                        predicate = predicate.And(a => !a.IsActive);
                    }

                }

                list = _supplierService.GetAll().AsExpandable().Where(predicate);
            }

            return list;
        }
        #endregion PrivateMethods

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_supplierService.IsNull())
                {
                    _supplierService = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion Dispose
    }
}
