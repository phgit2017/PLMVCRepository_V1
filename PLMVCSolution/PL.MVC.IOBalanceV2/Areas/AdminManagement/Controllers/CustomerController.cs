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
        private readonly ICustomerService _customerService;
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
                dto.CustomerId = 0;
                dto.DateCreated = DateTime.Now;
                dto.CreatedBy = WebSecurity.GetUserId(User.Identity.Name);
                dto.IsActive = true;
                isSuccess  = _customerService.SaveDetails(dto);

                if (!isSuccess)
                {
                    Danger(Messages.ErrorOccuredDuringProcessing);
                }
                else
                {
                    Success(Messages.InsertSuccess);
                }
            }

            alertMessage = this.RenderRazorViewToString(IOBALANCEMVCV2.Shared.Views._Alerts, string.Empty);
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
                dto.DateUpdated = DateTime.Now;
                dto.UpdatedBy = 1;
                isSuccess = _customerService.UpdateDetails(dto);

                if (!isSuccess)
                {
                    Danger(Messages.ErrorOccuredDuringProcessing);
                }
                else
                {
                    Success(Messages.InsertSuccess);
                }
            }
            

            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage
            };

            alertMessage = this.RenderRazorViewToString(IOBALANCEMVCV2.Shared.Views._Alerts, string.Empty);
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult ActivateDetails(CustomerDto dto)
        {
            bool isSuccess = false;
            string alertMessage = string.Empty;


            dto.IsActive = dto.IsActive ? false : true;
            dto.DateUpdated = DateTime.Now;
            dto.UpdatedBy = 1;

            isSuccess = _customerService.UpdateDetails(dto);

            if (!isSuccess)
            {
                Danger(Messages.ErrorOccuredDuringProcessing);
            }
            else
            {
                Success(Messages.UpdateSuccess);
            }

            alertMessage = this.RenderRazorViewToString(IOBALANCEMVCV2.Shared.Views._Alerts, string.Empty);
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
                list = _customerService.GetAll();
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


                list = _customerService.GetAll().AsExpandable().Where(predicate);

            }

            return list;
            
        }
        #endregion Private methods


    }
}
