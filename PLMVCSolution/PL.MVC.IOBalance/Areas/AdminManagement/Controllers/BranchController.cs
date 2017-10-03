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
    [IOBalanceSessionExpired]
    public partial class BranchController : BaseController
    {
        #region DeclarationsAndConstructors
        IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            this._branchService = branchService;
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
        public virtual ActionResult SaveBranch(BranchDto dto)
        {
            bool isSuccess = true;
            string alertMessage = string.Empty;
            dto.CreatedBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();
            dto.CreatedDate = System.DateTime.Now;

            List<BranchDto> duplicateList = new List<BranchDto>();
            duplicateList = _branchService.GetAll().Where(b => b.BranchName == dto.BranchName).ToList();

            if (ModelState.IsValid)
            {
                if (duplicateList.Count > 0)
                {
                    isSuccess = false;
                    Danger(string.Format(Messages.DuplicateItem, "Branch"));
                }
                else
                {
                    if (!_branchService.SaveBranch(dto))
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
        public virtual ActionResult UpdateBranch(BranchDto dto)
        {
            bool isSuccess = true;
            string alertMessage = string.Empty;

            var oldBranch = _branchService.FindBranchById(dto.BranchId);
            dto.BranchId = oldBranch.BranchId;

            dto.UpdatedBy = Session[SessionVariables.UserDetails].GetUserIdFromSession();

            List<BranchDto> duplicateList = new List<BranchDto>();
            duplicateList = _branchService.GetAll().Where(b => b.BranchName == dto.BranchName && b.BranchId != dto.BranchId).ToList();

            if (duplicateList.Count > 0)
            {
                isSuccess = false;
                Danger(string.Format(Messages.DuplicateItem, "Branch"));
            }
            else
            {
                if (!_branchService.UpdateBranch(dto))
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
        public virtual ActionResult GetBranches([DataSourceRequest] DataSourceRequest request, BranchSearchModel searchModel)
        {
            var list = GetBranch(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion ActionMethods

        #region PrivateMethods
        private IQueryable<BranchDto> GetBranch(BranchSearchModel searchModel = null)
        {
            IQueryable<BranchDto> list = null;
            if (searchModel.IsNull())
            {
                list = _branchService.GetAll();
            }
            else
            {
                if (!searchModel.BranchName.IsNull())
                {
                    list = _branchService.GetAll().Where(b => b.BranchName.Contains(searchModel.BranchName));
                }
                else
                {
                    list = _branchService.GetAll();
                }
            }

            return list;
        }
        #endregion PrivateMethods

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_branchService.IsNull())
                {
                    _branchService = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion Dispose


    }
}
