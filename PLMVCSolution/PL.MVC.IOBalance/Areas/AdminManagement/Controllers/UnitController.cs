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

namespace PL.MVC.IOBalance.Areas.AdminManagement.Controllers
{
    public partial class UnitController : BaseController
    {
        #region DeclarationsAndConstructors
        public IUnitService _iUnitService;

        public UnitController(IUnitService iUnitService)
        {
            this._iUnitService = iUnitService;
        }
        #endregion DeclarationsAndConstructors

        #region ActionMethods
        [IOBalanceAuthorizeUser]
        public virtual ActionResult Index()
        {
            var model = GetUnit();
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult SearchUnit(UnitSearchModel searchModel)
        {
            var model = GetUnit(searchModel);
            return PartialView(IOBALANCEMVC.AdminManagement.Unit.Views._ListUnit, model);
        }

        [HttpPost]
        public virtual ActionResult SaveUnit(UnitDto dto)
        {
            bool isSuccess = true;
            string unit = string.Empty, alertMessage = string.Empty;

            if (ModelState.IsValid)
            {
                if (!_iUnitService.SaveUnit(dto))
                {
                    isSuccess = false;
                    Danger(Messages.ErrorOccuredDuringProcessing);
                }
                else
                {
                    Success(Messages.InsertSuccess);
                    unit = this.RenderRazorViewToString(IOBALANCEMVC.AdminManagement.Unit.Views._ListUnit, GetUnit());
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
                alertMessage = alertMessage,
                unit = unit
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult RefreshUnit()
        {
            var model = GetUnit();
            return PartialView(IOBALANCEMVC.AdminManagement.Unit.Views._ListUnit, model);
        }

        [HttpPost]
        public virtual ActionResult UpdateUnit(UnitDto dto)
        {
            bool isSuccess = true;
            string unit = string.Empty, alertMessage = string.Empty;

            var oldUnit = _iUnitService.FindUnitById(dto.UnitID);
            dto.UnitID = oldUnit.UnitID;

            if (!_iUnitService.UpdateUnitDetails(dto))
            {
                isSuccess = false;
                Danger(Messages.ErrorOccuredDuringProcessing);
            }
            else
            {
                Success(Messages.UpdateSuccess);
                unit = this.RenderRazorViewToString(IOBALANCEMVC.AdminManagement.Unit.Views._ListUnit, GetUnit());
            }

            alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage,
                unit = unit
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult DeleteUnit(int unitId)
        {
            bool isSuccess = true;
            string unit = string.Empty, alertMessage = string.Empty;


            if (!_iUnitService.DeleteUnit(unitId))
            {
                isSuccess = false;
                Danger(Messages.ErrorOccuredDuringProcessing);
            }
            else
            {
                isSuccess = true;
                Success(Messages.DeleteSuccess);
                unit = this.RenderRazorViewToString(IOBALANCEMVC.AdminManagement.Unit.Views._ListUnit, GetUnit());
            }

            var jsonResult = new
            {
                unit = unit,
                isSuccess = isSuccess,
                alertMessage = alertMessage
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
        #endregion ActionMethods

        #region PrivateMethods
        private List<UnitDto> GetUnit(UnitSearchModel searchModel = null)
        {
            List<UnitDto> list = new List<UnitDto>();

            if (searchModel.IsNull())
            {
                list = _iUnitService.GetAll().ToList();
            }
            else
            {
                list = _iUnitService.GetAll().Where(u => u.UnitDesc.Contains(searchModel.Description)).ToList();
            }

            return list;
        }
        #endregion PrivateMethods

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_iUnitService.IsNull())
                {
                    _iUnitService = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion Dispose

    }
}
