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
    public partial class UserController : BaseController
    {

        #region DeclarationsAndConstructors
        IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        #endregion DeclarationsAndConstructors

        #region ActionMethods
        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        [IOBalanceSessionExpired]
        [HttpPost]
        public virtual ActionResult SaveUser(UserDto dto)
        {
            string alertMessage = string.Empty;
            bool isSuccess = true;

            int? createdBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();
            DateTime? dateNow = System.DateTime.Now;

            dto.CreatedBy = createdBy;
            dto.DateCreated = dateNow;

            var duplicateUserInBranch = _userService.GetAll().Where(u => u.UserName == dto.UserName.Trim() && u.BranchId == dto.BranchId && u.UserTypeID == dto.UserTypeID && u.IsActive).FirstOrDefault();

            if (!duplicateUserInBranch.IsNull())
            {
                isSuccess = false;
                Danger(Messages.DuplicateUserInBranch);
            }
            else
            {
                if (!_userService.SaveUser(dto))
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
        [HttpPost]
        public virtual ActionResult UpdateUser(UserDto dto)
        {
            string alertMessage = string.Empty;
            bool isSuccess = true;


            int? updatedBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();
            DateTime? dateNow = System.DateTime.Now;

            dto.UpdatedBy = updatedBy;
            dto.DateUpdated = dateNow;

            var duplicateUserInBranch = _userService.GetAll().Where(u => u.UserName == dto.UserName.Trim() && u.BranchId == dto.BranchId && u.UserTypeID == dto.UserTypeID && u.IsActive && u.UserID != dto.UserID).FirstOrDefault();

            if (!duplicateUserInBranch.IsNull())
            {
                isSuccess = false;
                Danger(Messages.DuplicateUserInBranch);
            }
            else
            {
                if (!_userService.UpdateUser(dto.UserID, dto))
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
        public virtual ActionResult isActiveUser(int userId, int? branchId, string userName, int userTypeId)
        {
            bool isSuccess = true;

            int? updatedBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();
            DateTime? dateNow = System.DateTime.Now;


            var duplicateUserInBranch = _userService.GetAll().Where(u => u.UserName == userName.Trim() && u.BranchId == branchId && u.UserTypeID == userTypeId && u.IsActive && u.UserID != userId).FirstOrDefault();

            if (!duplicateUserInBranch.IsNull())
            {
                isSuccess = false;
                Danger(Messages.DuplicateUserInBranch);
            }
            else
            {
                if (!_userService.UpdateInactiveUser(userId, updatedBy))
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



            var alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [IOBalanceSessionExpired]
        public virtual ActionResult GetUsers([DataSourceRequest] DataSourceRequest request, UserSearchModel searchModel)
        {
            var list = GetUser(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion ActionMethods

        #region PrivateMethods
        private IQueryable<UserDto> GetUser(UserSearchModel searchModel = null)
        {
            IQueryable<UserDto> list = null;

            int? userIdOnline = Session[SessionVariables.UserDetails].GetUserIdFromSession();

            if ((searchModel.IsNull() || (!searchModel.HasAnyValue())))
            {
                list = _userService.GetAll().Where(u => u.UserID != userIdOnline);
            }
            else
            {
                var predicate = PredicateBuilder.True<UserDto>();
                var hasOtherFilter = false;

                if (!searchModel.UserName.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(a => a.UserName.Contains(searchModel.UserName) && a.UserID != userIdOnline);
                }

                if (!searchModel.UserTypeId.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(a => a.UserTypeID == searchModel.UserTypeId && a.UserID != userIdOnline);
                }

                if (!searchModel.isActive.IsNull())
                {
                    hasOtherFilter = true;
                    if (searchModel.isActive == "true")
                    {
                        predicate = predicate.And(a => a.IsActive && a.UserID != userIdOnline);
                    }
                    else
                    {
                        predicate = predicate.And(a => !a.IsActive && a.UserID != userIdOnline);
                    }

                }

                if (!searchModel.BranchId.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(a => a.BranchId == searchModel.BranchId && a.UserID != userIdOnline);
                }



                list = _userService.GetAll().AsExpandable().Where(predicate);
            }

            return list;

        }
        #endregion PrivateMethods

        #region Dispose

        #endregion Dispose



    }
}
