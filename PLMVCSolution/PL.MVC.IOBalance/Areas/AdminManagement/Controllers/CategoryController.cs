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
    public partial class CategoryController : BaseController
    {
        #region DeclarationAndConstructors
        ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        #endregion DeclarationAndConstructors

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
        public virtual ActionResult SaveCategory(CategoryDto dto)
        {
            bool isSuccess = true;

            List<CategoryDto> duplicateList = new List<CategoryDto>();
            duplicateList = _categoryService.GetAll().Where(c => c.CategoryCode == dto.CategoryCode && c.CategoryName == dto.CategoryName && c.IsActive).ToList();

            if (ModelState.IsValid)
            {
                if (duplicateList.Count > 0)
                {
                    isSuccess = false;
                    Danger(string.Format(Messages.DuplicateItem, "Category"));
                }
                else
                {
                    if (!_categoryService.SaveCategory(dto))
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


            var alertMessage = this.RenderRazorViewToString(IOBALANCEMVC.Shared.Views._Alerts, string.Empty);
            var jsonResult = new
            {
                isSuccess = isSuccess,
                alertMessage = alertMessage
            };


            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [IOBalanceSessionExpired]
        [HttpPost]
        public virtual ActionResult UpdateCategory(CategoryDto dto)
        {
            bool isSuccess = true;

            var oldCategory = _categoryService.FindCategoryById(dto.CategoryID);
            dto.CategoryID = oldCategory.CategoryID;

            List<CategoryDto> duplicateList = new List<CategoryDto>();
            duplicateList = _categoryService.GetAll().Where(c => c.CategoryCode == dto.CategoryCode && c.CategoryName == dto.CategoryName && c.CategoryID != dto.CategoryID && c.IsActive).ToList();

            if (duplicateList.Count > 0)
            {
                isSuccess = false;
                Danger(string.Format(Messages.DuplicateItem, "Category"));
            }
            else
            {
                if (!_categoryService.UpdateCategoryDetails(dto))
                {
                    isSuccess = false;
                    Danger(Messages.ErrorOccuredDuringProcessing);
                }
                else
                {
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
        [HttpPost]
        public virtual ActionResult isActiveCategory(int categoryId, string categoryCode, string categoryName)
        {
            bool isSuccess = true;

            List<CategoryDto> duplicateList = new List<CategoryDto>();
            duplicateList = _categoryService.GetAll().Where(c => c.CategoryCode == categoryCode && c.CategoryName == categoryName && c.CategoryID != categoryId && c.IsActive).ToList();

            if (duplicateList.Count > 0)
            {
                isSuccess = false;
                Danger(string.Format(Messages.DuplicateItem, "Category"));
            }
            else
            {
                if (!_categoryService.UpdateInactive(categoryId))
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
        public virtual ActionResult GetCategory([DataSourceRequest] DataSourceRequest request, CategorySearchModel searchModel = null)
        {
            //var list = GetCategories(searchModel);
            //var list = _categoryService.GetAll().ToList();
            var list = GetCategories(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion ActionMethods

        #region PrivateMethods
        private IQueryable<CategoryDto> GetCategories(CategorySearchModel searchModel = null)
        {
            IQueryable<CategoryDto> list = null;

            if (searchModel.IsNull())
            {
                list = _categoryService.GetAll();
            }
            else
            {
                var predicate = PredicateBuilder.True<CategoryDto>();
                var hasOtherFilter = false;

                if (!searchModel.CategoryCode.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(a => a.CategoryCode.Contains(searchModel.CategoryCode));
                }

                if (!searchModel.CategoryName.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(a => a.CategoryName.Contains(searchModel.CategoryName));
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



                list = _categoryService.GetAll().AsExpandable().Where(predicate);
            }

            return list;
        }
        #endregion PrivateMethods

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_categoryService.IsNull())
                {
                    _categoryService = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion Dispose

    }
}
