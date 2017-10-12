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

using Infrastructure.Utilities.Extensions;
using LinqKit;

namespace PL.MVC.IOBalance.Areas.TemplateOnly.Controllers
{
    public partial class TemplateController : BaseController
    {

        #region DeclarationsAndConstructors
        //TODO: _iService

        //TODO: service
        public TemplateController()
        {
            //TODO: declare service
        }
        #endregion DeclarationsAndConstructors

        #region ActionMethods
        public virtual ActionResult Index()
        {
            //TODO: model = GetItem();

            //TODO: put model
            return View();
        }

        //[HttpPost]
        //public virtual ActionResult SearchCustomer(CustomerSearchModel searchModel)
        //{
        //    string customer = string.Empty;

        //    customer = this.RenderRazorViewToString(IOBALANCEMVC.AdminManagement.Customer.Views._ListCustomer, GetCustomer(searchModel));

        //    var jsonResult = new
        //    {
        //        customer = customer
        //    };

        //    return Json(jsonResult, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public virtual ActionResult SaveCustomer(CustomerDto dto)
        //{
        //    string customer = string.Empty, alertMessage = string.Empty;
        //    bool isSuccess = true;

        //    if (ModelState.IsValid)
        //    {
        //        if (!_iCustomerService.SaveCustomer(dto))
        //        {
        //            isSuccess = false;
        //            Danger(Messages.ErrorOccuredDuringProcessing);
        //        }
        //        else
        //        {
        //            Success(Messages.InsertSuccess);
        //            customer = this.RenderRazorViewToString(IOBALANCEMVC.AdminManagement.Customer.Views._ListCustomer, GetCustomer());
        //        }
        //    }
        //    else
        //    {
        //        isSuccess = false;
        //        Danger(Messages.ErrorOccuredDuringProcessing);
        //    }

        //    alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
        //    var jsonResult = new
        //    {
        //        isSuccess = isSuccess,
        //        alertMessage = alertMessage,
        //        customer = customer
        //    };


        //    return Json(jsonResult, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public virtual ActionResult RefreshCustomer()
        //{
        //    string customer = string.Empty;

        //    customer = this.RenderRazorViewToString(IOBALANCEMVC.AdminManagement.Customer.Views._ListCustomer, GetCustomer());
        //    var jsonResult = new
        //    {
        //        customer = customer
        //    };

        //    return Json(jsonResult, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public virtual ActionResult UpdateCustomer(CustomerDto dto)
        //{
        //    string customer = string.Empty, alertMessage = string.Empty;
        //    bool isSuccess = true;

        //    var oldCustomer = _iCustomerService.FindCustomerById(dto.CustomerID);
        //    dto.CustomerID = oldCustomer.CustomerID;
        //    dto.IsActive = oldCustomer.IsActive;

        //    if (!_iCustomerService.UpdateCustomerDetails(dto))
        //    {
        //        isSuccess = false;
        //        Danger(Messages.ErrorOccuredDuringProcessing);
        //    }
        //    else
        //    {
        //        Success(Messages.UpdateSuccess);
        //        customer = this.RenderRazorViewToString(IOBALANCEMVC.AdminManagement.Customer.Views._ListCustomer, GetCustomer());
        //    }

        //    alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
        //    var jsonResult = new
        //    {
        //        isSuccess = isSuccess,
        //        alertMessage = alertMessage,
        //        customer = customer
        //    };


        //    return Json(jsonResult, JsonRequestBehavior.AllowGet);

        //}

        //[HttpPost]
        //public virtual ActionResult isActiveCustomer(bool isActive, int customerId)
        //{
        //    string customer = string.Empty, alertMessage = string.Empty;
        //    bool isSuccess = true;

        //    //TODO: put value in updatedBy
        //    if (!_iCustomerService.UpdateInactive(isActive, customerId, null))
        //    {
        //        isSuccess = false;
        //        Danger(Messages.ErrorOccuredDuringProcessing);
        //    }
        //    else
        //    {
        //        isSuccess = true;
        //        Success(Messages.UpdateSuccess);
        //        customer = this.RenderRazorViewToString(IOBALANCEMVC.AdminManagement.Customer.Views._ListCustomer, GetCustomer());
        //    }

        //    alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
        //    var jsonResult = new
        //    {
        //        isSuccess = isSuccess,
        //        alertMessage = alertMessage,
        //        customer = customer
        //    };


        //    return Json(jsonResult, JsonRequestBehavior.AllowGet);
        //}
        #endregion ActionMethods

        #region PrivateMethods
        //private List<CustomerDto> GetCustomer(CustomerSearchModel searchModel = null)
        //{
        //    List<CustomerDto> list = new List<CustomerDto>();

        //    if (searchModel.IsNull())
        //    {
        //        list = _iCustomerService.GetAll().ToList();
        //    }
        //    else
        //    {
        //        var predicate = PredicateBuilder.True<CustomerDto>();
        //        var hasOtherFilter = false;

        //        if (!searchModel.CustomerCode.IsNull())
        //        {
        //            hasOtherFilter = true;
        //            predicate = predicate.And(c => c.CustomerCode == searchModel.CustomerCode);
        //        }

        //        if (!searchModel.FirstName.IsNull())
        //        {
        //            hasOtherFilter = true;
        //            predicate = predicate.And(c => c.FirstName == searchModel.FirstName);
        //        }

        //        if (!searchModel.LastName.IsNull())
        //        {
        //            hasOtherFilter = true;
        //            predicate = predicate.And(c => c.LastName == searchModel.LastName);
        //        }

        //        list = _iCustomerService.GetAll().AsExpandable().Where(predicate).ToList();
        //    }


        //    return list;
        //}
        #endregion PrivateMethods

        #region Dispose
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (!_iCustomerService.IsNull())
        //        {
        //            _iCustomerService = null;
        //        }
        //    }
        //    base.Dispose(disposing);
        //}
        #endregion Dispose
    }
}
