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
using PL.MVC.IOBalanceV2.Areas.AdminManagement.Models;
using LinqKit;
using WebMatrix.WebData;

namespace PL.MVC.IOBalanceV2.Areas.AdminManagement.Controllers
{
    [Authorization(Roles = "admin")]
    public partial class CustomerController : BaseController
    {
        #region Declarations and constructors
        private ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        #endregion Declarations and constructors

        #region Public methods
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult GetDetails([DataSourceRequest] DataSourceRequest request, CustomerSearchModel searchModel)
        {
            var list = GetDetail(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public virtual ActionResult SaveDetails(CustomerDto dto)
        {
            bool isSuccess = false;
            string alertMessage = string.Empty;

            if (ModelState.IsValid)
            {



                var duplicate = _customerService.GetAll().Where(c => c.CustomerCode == dto.CustomerCode && c.IsActive).Count();

                if (duplicate >= 1)
                {
                    alertMessage = string.Format(Messages.DuplicateItem, "Customer");
                }
                else
                {
                    dto.CustomerId = 0;
                    dto.DateCreated = DateTime.Now;
                    dto.CreatedBy = WebSecurity.GetUserId(User.Identity.Name);
                    dto.IsActive = true;

                    isSuccess = _customerService.SaveDetails(dto);

                    if (!isSuccess)
                    {
                        alertMessage = string.Format(Messages.ErrorOccuredDuringProcessingThis, "saving in customer");
                    }
                    else
                    {
                        alertMessage = Messages.InsertSuccess;
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
        public virtual ActionResult UpdateDetails(CustomerDto dto)
        {
            bool isSuccess = false;
            string alertMessage = string.Empty;

            if (ModelState.IsValid)
            {

                var duplicate = _customerService.GetAll().Where(c => c.CustomerCode == dto.CustomerCode && c.CustomerId != dto.CustomerId && c.IsActive).Count();

                if (duplicate >= 1)
                {
                    alertMessage = string.Format(Messages.DuplicateItem, "Customer");
                }
                else
                {
                    dto.DateUpdated = DateTime.Now;
                    dto.UpdatedBy = WebSecurity.GetUserId(User.Identity.Name);
                    isSuccess = _customerService.UpdateDetails(dto);

                    if (!isSuccess)
                    {
                        alertMessage = string.Format(Messages.ErrorOccuredDuringProcessingThis, "updating in customer");
                    }
                    else
                    {
                        alertMessage = Messages.UpdateSuccess;
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

        public virtual ActionResult ActivateDetails(CustomerDto dto)
        {
            bool isSuccess = false;
            string alertMessage = string.Empty;

            var duplicate = _customerService.GetAll().Where(c => c.CustomerCode == dto.CustomerCode && c.CustomerId != dto.CustomerId && c.IsActive).Count();

            if (duplicate >= 1)
            {
                alertMessage = string.Format(Messages.DuplicateItem, "Customer");
            }
            else
            {
                dto.IsActive = dto.IsActive ? false : true;
                dto.DateUpdated = DateTime.Now;
                dto.UpdatedBy = WebSecurity.GetUserId(User.Identity.Name);

                isSuccess = _customerService.UpdateDetails(dto);

                if (!isSuccess)
                {
                    alertMessage = string.Format(Messages.ErrorOccuredDuringProcessingThis, "activate/deactivate in customer");
                }
                else
                {
                    alertMessage = Messages.UpdateSuccess;
                }
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
        private IQueryable<CustomerDto> GetDetail(CustomerSearchModel searchModel)
        {

            IQueryable<CustomerDto> list = null;

            if (searchModel.CustomerAddress.IsNull() && searchModel.CustomerCode.IsNull() && searchModel.CustomerName.IsNull() && searchModel.IsActive.IsNull())
            {
                list = _customerService.GetAll().Where(c => c.CustomerId != 0);
            }
            else
            {
                var predicate = PredicateBuilder.True<CustomerDto>();

                if (!searchModel.CustomerCode.IsNull())
                {
                    predicate = predicate.And(c => c.CustomerCode == searchModel.CustomerCode);
                }

                if (!searchModel.CustomerName.IsNull())
                {
                    predicate = predicate.And(c => c.CustomerName.Contains(searchModel.CustomerName));
                }

                if (!searchModel.CustomerAddress.IsNull())
                {
                    predicate = predicate.And(c => c.CustomerAddress.Contains(searchModel.CustomerAddress));
                }

                if (!searchModel.IsActive.IsNull())
                {
                    predicate = predicate.And(c => c.IsActive == searchModel.IsActive);
                }


                predicate = predicate.And(c => c.CustomerId != 0);


                list = _customerService.GetAll().AsExpandable().Where(predicate);

            }

            return list;

        }
        #endregion Private methods

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (_customerService.IsNull())
            {
                _customerService = null;
            }
            base.Dispose(disposing);
        }
        #endregion Dispose
    }
}
