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
using System.Security;


namespace PL.MVC.IOBalance.Areas.AdminManagement.Controllers
{

    
    public partial class DiscountController : BaseController
    {
        #region DeclarationsAndConstructors
        IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            this._discountService = discountService;
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
        public virtual ActionResult SaveDiscount(DiscountDto dto)
        {
            bool isSuccess = true;
            string alertMessage = string.Empty;
            dto.CreatedBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();

            List<DiscountDto> duplicateList = new List<DiscountDto>();
            duplicateList = _discountService.GetAll().Where(d => d.BranchID == dto.BranchID && d.IsActive).ToList();

            if (duplicateList.Count > 0)
            {
                isSuccess = false;
                Danger(string.Format(Messages.DuplicateItemInBranch, "Discount"));
            }
            else
            {
                if (!_discountService.SaveDiscount(dto))
                {
                    isSuccess = false;
                    Danger(Messages.ErrorOccuredDuringProcessing);
                }
                else
                {
                    Success(Messages.InsertSuccess);
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
        public virtual ActionResult UpdateDiscount(DiscountDto dto)
        {
            bool isSuccess = true;
            string alertMessage = string.Empty;
            dto.UpdatedBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();

            List<DiscountDto> duplicateList = new List<DiscountDto>();
            duplicateList = _discountService.GetAll().Where(d => d.BranchID == dto.BranchID && d.IsActive && d.DiscountID != dto.DiscountID).ToList();

            if (duplicateList.Count > 0)
            {
                isSuccess = false;
                Danger(string.Format(Messages.DuplicateItemInBranch, "Discount"));
            }
            else
            {
                if (!_discountService.UpdateDiscount(dto))
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
        public virtual ActionResult DeleteDiscount(int discountId, int branchId)
        {
            bool isSuccess = true;
            string alertMessage = string.Empty;

            List<DiscountDto> duplicateList = new List<DiscountDto>();
            duplicateList = _discountService.GetAll().Where(d => d.BranchID == branchId && d.IsActive && d.DiscountID != discountId).ToList();


            if (duplicateList.Count > 0)
            {
                isSuccess = false;
                Danger(string.Format(Messages.DuplicateItemInBranch, "Discount"));
            }
            else
            {
                DiscountDto dto = new DiscountDto()
                {
                    DiscountID = discountId,
                    UpdatedBy = Session[SessionVariables.UserDetails].GetUserIdFromSession()
                };

                if (!_discountService.UpdateDiscountActive(dto))
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
        public virtual ActionResult GetDiscount([DataSourceRequest] DataSourceRequest request, DiscountSearchModel searchModel)
        {
            var list = GetDiscountQueryable(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion ActionMethods

        #region PrivateMethods
        public IQueryable<DiscountDto> GetDiscountQueryable(DiscountSearchModel searchModel = null)
        {
            IQueryable<DiscountDto> list = null;

            if (searchModel.IsNull())
            {
                list = _discountService.GetAll().OrderBy(d => d.DateCreated);
            }
            else
            {
                if (!searchModel.BranchID.IsNull())
                {
                    list = _discountService.GetAll().Where(d => d.BranchID == searchModel.BranchID);
                }
                else
                {
                    list = _discountService.GetAll().OrderBy(d => d.DateCreated);
                }
            }

            return list;
        }
        #endregion PrivateMethods
    }
}
