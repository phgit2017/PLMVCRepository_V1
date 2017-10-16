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

namespace PL.MVC.IOBalanceV2.Areas.AdminManagement.Controllers
{
    [Authorization(Roles = "admin")]
    public partial class CategoryController : BaseController
    {

        #region Declarations and constructors
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        #endregion Declarations and constructors

        #region Public methods
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult GetDetails([DataSourceRequest] DataSourceRequest request, CategorySearchModel searchModel)
        {
            var list = GetDetail(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public virtual ActionResult SaveDetails(CategoryDto dto)
        {
            bool isSuccess = false;
            string alertMessage = string.Empty;

            if (ModelState.IsValid)
            {
                dto.CategoryId = 0;


                var duplicate = _categoryService.GetAll().Where(c => c.CategoryName == dto.CategoryName).Count();

                if (duplicate >= 1)
                {
                    Danger(string.Format(Messages.DuplicateItem, "Category"));
                }
                else
                {
                    if (!this._categoryService.SaveDetails(dto))
                    {
                        Danger(Messages.ErrorOccuredDuringProcessing);
                    }
                    else
                    {
                        isSuccess = true;
                        Success(Messages.InsertSuccess);
                    }
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
        public virtual ActionResult UpdateDetails(CategoryDto dto)
        {
            bool isSuccess = false;
            string alertMessage = string.Empty;

            if (ModelState.IsValid)
            {

                var duplicate = _categoryService.GetAll().Where(c => c.CategoryName == dto.CategoryName && c.CategoryId != dto.CategoryId).Count();

                if (duplicate >= 1)
                {
                    Danger(string.Format(Messages.DuplicateItem, "Category"));
                }
                else
                {
                    if (!this._categoryService.UpdateDetails(dto))
                    {
                        Danger(Messages.ErrorOccuredDuringProcessing);
                    }
                    else
                    {
                        isSuccess = true;
                        Success(Messages.UpdateSuccess);
                    }
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
        #endregion Public methods

        #region Private methods
        private IQueryable<CategoryDto> GetDetail(CategorySearchModel searchModel)
        {

            IQueryable<CategoryDto> list = null;

            if (!searchModel.HasAnyValue())
            {
                list = _categoryService.GetAll();
            }
            else
            {
                var predicate = PredicateBuilder.True<CategoryDto>();

                if (!searchModel.CategoryName.IsNull())
                {
                    predicate = predicate.And(c => c.CategoryName.Contains(searchModel.CategoryName));
                }

                list = _categoryService.GetAll().AsExpandable().Where(predicate);

            }

            return list;

        }
        #endregion Private methods
    }
}