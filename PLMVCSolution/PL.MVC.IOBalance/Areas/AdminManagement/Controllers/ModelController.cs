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
    public partial class ModelController : BaseController
    {
        #region DeclarationsAndConstructors
        IModelService _iModelService;

        public ModelController(IModelService iModelService)
        {
            this._iModelService = iModelService;
        }
        #endregion DeclarationsAndConstructors

        #region ActionMethods
        [IOBalanceAuthorizeUser]
        public virtual ActionResult Index()
        {
            var model = GetModel();
            return View(model);
        }

        public virtual ActionResult SaveModel(ModelDto dto)
        {
            string model = string.Empty, alertMessage = string.Empty;
            bool isSuccess = true;


            if (!_iModelService.SaveModel(dto))
            {
                isSuccess = false;
                Danger(Messages.ErrorOccuredDuringProcessing);
            }
            else
            {
                Success(Messages.InsertSuccess);
                model = this.RenderRazorViewToString(IOBALANCEMVC.AdminManagement.Model.Views._ListModel, GetModel());
            }


            alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage,
                model = model
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult UpdateModel(ModelDto dto)
        {
            string model = string.Empty, alertMessage = string.Empty;
            bool isSuccess = true;

            if (!_iModelService.UpdateModel(dto))
            {
                isSuccess = false;
                Danger(Messages.ErrorOccuredDuringProcessing);
            }
            else
            {
                Success(Messages.InsertSuccess);
                model = this.RenderRazorViewToString(IOBALANCEMVC.AdminManagement.Model.Views._ListModel, GetModel());
            }

            alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage,
                model = model
            };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult RefreshModel()
        {
            var model = GetModel();
            return PartialView(IOBALANCEMVC.AdminManagement.Model.Views._ListModel, model);
        }

        public virtual ActionResult SearchModel(ModelSearchModel searchModel)
        {
            var model = GetModel(searchModel);
            return PartialView(IOBALANCEMVC.AdminManagement.Model.Views._ListModel, GetModel(searchModel));
        }
        #endregion ActionMethods

        #region PrivateMethods
        private List<ModelDto> GetModel(ModelSearchModel searchModel = null)
        {
            List<ModelDto> list = new List<ModelDto>();

            if (searchModel.IsNull())
            {
                list = _iModelService.GetAll().ToList();
            }
            else
            {
                var predicate = PredicateBuilder.True<ModelDto>();
                var hasOtherFilter = false;

                if (!searchModel.ModelName.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(c => c.ModelName.Contains(searchModel.ModelName));
                }

                list = _iModelService.GetAll().AsExpandable().Where(predicate).ToList();
            }

            return list;
        }
        #endregion PrivateMethods

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_iModelService.IsNull())
                {
                    _iModelService = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion Dispose

    }
}