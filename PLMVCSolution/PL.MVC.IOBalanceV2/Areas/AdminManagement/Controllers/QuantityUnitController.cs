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

namespace PL.MVC.IOBalanceV2.Areas.AdminManagement.Controllers
{
    [Authorization(Roles = "admin")]
    public partial class QuantityUnitController : BaseController
    {
        #region Declarations and constructors
        private IQuantityUnitService _quantityUnitService;
        public QuantityUnitController(IQuantityUnitService categoryService)
        {
            _quantityUnitService = categoryService;
        }
        #endregion Declarations and constructors

        #region Public methods
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult GetDetails([DataSourceRequest] DataSourceRequest request, QuantityUnitSearchModel searchModel)
        {
            var list = GetDetail(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public virtual ActionResult SaveDetails(QuantityUnitDto dto)
        {
            bool isSuccess = false;
            string alertMessage = string.Empty;

            if (ModelState.IsValid)
            {
                

                var duplicate = _quantityUnitService.GetAll().Where(c => c.UnitName == dto.UnitName).Count();

                if (duplicate >= 1)
                {
                    alertMessage = (string.Format(Messages.DuplicateItem, "Unit"));
                }
                else
                {
                    dto.QuantityUnitID = 0;
                    isSuccess = this._quantityUnitService.SaveDetails(dto);
                    if (!isSuccess)
                    {
                        alertMessage = string.Format(Messages.ErrorOccuredDuringProcessingThis, "saving in unit");
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
        public virtual ActionResult UpdateDetails(QuantityUnitDto dto)
        {
            bool isSuccess = false;
            string alertMessage = string.Empty;


            if (ModelState.IsValid)
            {
                var duplicate = _quantityUnitService.GetAll().Where(c => c.UnitName == dto.UnitName && c.QuantityUnitID != dto.QuantityUnitID).Count();

                if (duplicate >= 1)
                {
                    alertMessage = (string.Format(Messages.DuplicateItem, "Unit"));
                }
                else
                {
                    isSuccess = this._quantityUnitService.UpdateDetails(dto);
                    if (!isSuccess)
                    {
                        alertMessage = string.Format(Messages.ErrorOccuredDuringProcessingThis, "updating in unit");
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
        private IQueryable<QuantityUnitDto> GetDetail(QuantityUnitSearchModel searchModel)
        {
            IQueryable<QuantityUnitDto> list = null;

            if (!searchModel.HasAnyValue())
            {
                list = _quantityUnitService.GetAll();
            }
            else
            {
                var predicate = PredicateBuilder.True<QuantityUnitDto>();

                if (!searchModel.UnitName.IsNull())
                {
                    predicate = predicate.And(c => c.UnitName.Contains(searchModel.UnitName));
                }

                list = _quantityUnitService.GetAll().AsExpandable().Where(predicate);
            }

            return list;

        }
        #endregion Private methods

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (_quantityUnitService.IsNull())
            {
                _quantityUnitService = null;
            }
            base.Dispose(disposing);
        }
        #endregion Dispose
    }
}