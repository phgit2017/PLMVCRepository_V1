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
    public partial class CustomerController : BaseController
    {
        #region DeclarationsAndConstructors
        ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }
        #endregion DeclarationsAndConstructors

        #region ActionMethods
        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult Index()
        {
            return View();
        }

        [IOBalanceSessionExpired]
        [HttpPost]
        public virtual ActionResult SaveCustomer(CustomerDto dto)
        {
            string alertMessage = string.Empty;
            bool isSuccess = true;

            List<CustomerDto> duplicateList = new List<CustomerDto>();
            duplicateList = _customerService.GetAll().Where(c => c.CustomerCode == dto.CustomerCode && c.IsActive).ToList();

            if (ModelState.IsValid)
            {
                if (duplicateList.Count > 0)
                {
                    isSuccess = false;
                    Danger(string.Format(Messages.DuplicateItem, "Customer"));
                }
                else
                {
                    if (!_customerService.SaveCustomer(dto))
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
        public virtual ActionResult UpdateCustomer(CustomerDto dto)
        {
            string alertMessage = string.Empty;
            bool isSuccess = true;

            var oldCustomer = _customerService.FindCustomerById(dto.CustomerID);
            dto.CustomerID = oldCustomer.CustomerID;
            dto.IsActive = oldCustomer.IsActive;
            dto.UpdatedBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();

            List<CustomerDto> duplicateList = new List<CustomerDto>();
            duplicateList = _customerService.GetAll().Where(c => c.CustomerCode == dto.CustomerCode && c.IsActive && c.CustomerID != dto.CustomerID).ToList();


            if (duplicateList.Count > 0)
            {
                isSuccess = false;
                Danger(string.Format(Messages.DuplicateItem, "Customer"));
            }
            else
            {
                if (!_customerService.UpdateCustomerDetails(dto))
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
        public virtual ActionResult isActiveCustomer(int customerId,string customerCode)
        {
            string alertMessage = string.Empty;
            bool isSuccess = true;
            int? updatedBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();


            List<CustomerDto> duplicateList = new List<CustomerDto>();
            duplicateList = _customerService.GetAll().Where(c => c.CustomerCode == customerCode && c.CustomerID != customerId && c.IsActive).ToList();

            if (duplicateList.Count > 0)
            {
                isSuccess = false;
                Danger(string.Format(Messages.DuplicateItem, "Customer"));
            }
            else
            {
                if (!_customerService.UpdateInactive(customerId, updatedBy))
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
        public virtual ActionResult GetCustomers([DataSourceRequest] DataSourceRequest request, CustomerSearchModel searchModel)
        {
            var list = GetCustomer(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion ActionMethods

        #region PrivateMethods
        private IQueryable<CustomerDto> GetCustomer(CustomerSearchModel searchModel = null)
        {
            IQueryable<CustomerDto> list = null;

            if (searchModel.IsNull())
            {
                list = _customerService.GetAll();
            }
            else
            {
                var predicate = PredicateBuilder.True<CustomerDto>();
                var hasOtherFilter = false;

                if (!searchModel.CustomerCode.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(c => c.CustomerCode.Contains(searchModel.CustomerCode));
                }

                if (!searchModel.FirstName.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(c => c.FirstName.Contains(searchModel.FirstName));
                }

                if (!searchModel.LastName.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(c => c.LastName.Contains(searchModel.LastName));
                }

                if (!searchModel.Address.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(c => c.Address.Contains(searchModel.Address));
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

                list = _customerService.GetAll().AsExpandable().Where(predicate);
            }


            return list;
        }
        #endregion PrivateMethods

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_customerService.IsNull())
                {
                    _customerService = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion Dispose
    }
}
