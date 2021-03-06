﻿using System;
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
using PL.MVC.IOBalanceV2.Areas.AdminManagement.Models;
using LinqKit;
using WebMatrix.WebData;

namespace PL.MVC.IOBalanceV2.Areas.AdminManagement.Controllers
{
    [Authorization(Roles = "admin")]
    public partial class SupplierController : BaseController
    {

        #region Declarations and constructors
        private ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        #endregion Declarations and constructors

        #region Public methods
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult GetDetails([DataSourceRequest] DataSourceRequest request, SupplierSearchModel searchModel)
        {
            var list = GetDetail(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public virtual ActionResult SaveDetails(SupplierDto dto)
        {
            bool isSuccess = false;
            string alertMessage = string.Empty;

            if (ModelState.IsValid)
            {
                var duplicate = _supplierService.GetAll().Where(c => c.SupplierCode == dto.SupplierCode).Count();

                if (duplicate >= 1)
                {
                    alertMessage = (string.Format(Messages.DuplicateItem, "Supplier"));
                }
                else
                {
                    dto.SupplierId = 0;
                    dto.DateCreated = DateTime.Now;
                    dto.CreatedBy = WebSecurity.GetUserId(User.Identity.Name);
                    isSuccess = this._supplierService.SaveDetails(dto);
                    if (!isSuccess)
                    {
                        alertMessage = string.Format(Messages.ErrorOccuredDuringProcessingThis, "saving in supplier");
                    }
                    else
                    {
                        alertMessage = (Messages.InsertSuccess);
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
        public virtual ActionResult UpdateDetails(SupplierDto dto)
        {
            bool isSuccess = false;
            string alertMessage = string.Empty;

            if (ModelState.IsValid)
            {
                var duplicate = _supplierService.GetAll().Where(c => c.SupplierCode == dto.SupplierCode && c.SupplierId != dto.SupplierId).Count();

                if (duplicate >= 1)
                {
                    alertMessage = (string.Format(Messages.DuplicateItem, "Supplier"));
                }
                else
                {
                    dto.DateUpdated = DateTime.Now;
                    dto.UpdatedBy = WebSecurity.GetUserId(User.Identity.Name);
                    isSuccess = this._supplierService.UpdateDetails(dto);

                    if (!isSuccess)
                    {
                        alertMessage = string.Format(Messages.ErrorOccuredDuringProcessingThis, "updating in supplier");
                    }
                    else
                    {
                        alertMessage = (Messages.UpdateSuccess);
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
        #endregion Public methods

        #region Private methods
        private IQueryable<SupplierDto> GetDetail(SupplierSearchModel searchModel)
        {

            IQueryable<SupplierDto> list = null;

            if (!searchModel.HasAnyValue())
            {
                list = _supplierService.GetAll();
            }
            else
            {
                var predicate = PredicateBuilder.True<SupplierDto>();

                if (!searchModel.SupplierCode.IsNull())
                {
                    predicate = predicate.And(c => c.SupplierCode.Contains(searchModel.SupplierCode));
                }

                if (!searchModel.SupplierName.IsNull())
                {
                    predicate = predicate.And(c => c.SupplierName.Contains(searchModel.SupplierName));
                }



                list = _supplierService.GetAll().AsExpandable().Where(predicate);

            }

            return list;

        }
        #endregion Private methods

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (_supplierService.IsNull())
            {
                _supplierService = null;
            }
            base.Dispose(disposing);
        }
        #endregion Dispose
    }
}