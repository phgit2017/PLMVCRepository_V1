// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace PL.MVC.IOBalance.Areas.AdminManagement.Controllers
{
    public partial class CategoryController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected CategoryController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult SaveCategory()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SaveCategory);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult UpdateCategory()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateCategory);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult isActiveCategory()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.isActiveCategory);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GetCategory()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetCategory);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public CategoryController Actions { get { return IOBALANCEMVC.AdminManagement.Category; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "AdminManagement";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Category";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Category";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string SaveCategory = "SaveCategory";
            public readonly string UpdateCategory = "UpdateCategory";
            public readonly string isActiveCategory = "isActiveCategory";
            public readonly string GetCategory = "GetCategory";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string SaveCategory = "SaveCategory";
            public const string UpdateCategory = "UpdateCategory";
            public const string isActiveCategory = "isActiveCategory";
            public const string GetCategory = "GetCategory";
        }


        static readonly ActionParamsClass_SaveCategory s_params_SaveCategory = new ActionParamsClass_SaveCategory();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SaveCategory SaveCategoryParams { get { return s_params_SaveCategory; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SaveCategory
        {
            public readonly string dto = "dto";
        }
        static readonly ActionParamsClass_UpdateCategory s_params_UpdateCategory = new ActionParamsClass_UpdateCategory();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UpdateCategory UpdateCategoryParams { get { return s_params_UpdateCategory; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UpdateCategory
        {
            public readonly string dto = "dto";
        }
        static readonly ActionParamsClass_isActiveCategory s_params_isActiveCategory = new ActionParamsClass_isActiveCategory();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_isActiveCategory isActiveCategoryParams { get { return s_params_isActiveCategory; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_isActiveCategory
        {
            public readonly string categoryId = "categoryId";
            public readonly string categoryCode = "categoryCode";
            public readonly string categoryName = "categoryName";
        }
        static readonly ActionParamsClass_GetCategory s_params_GetCategory = new ActionParamsClass_GetCategory();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetCategory GetCategoryParams { get { return s_params_GetCategory; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetCategory
        {
            public readonly string request = "request";
            public readonly string searchModel = "searchModel";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string _ListCategory = "_ListCategory";
                public readonly string _ListCategoryJS = "_ListCategoryJS";
                public readonly string _ListCategoryKendo = "_ListCategoryKendo";
                public readonly string _ModalEditCategory = "_ModalEditCategory";
                public readonly string _ModalSearchCategory = "_ModalSearchCategory";
                public readonly string _NewCategory = "_NewCategory";
                public readonly string Index = "Index";
            }
            public readonly string _ListCategory = "~/Areas/AdminManagement/Views/Category/_ListCategory.cshtml";
            public readonly string _ListCategoryJS = "~/Areas/AdminManagement/Views/Category/_ListCategoryJS.cshtml";
            public readonly string _ListCategoryKendo = "~/Areas/AdminManagement/Views/Category/_ListCategoryKendo.cshtml";
            public readonly string _ModalEditCategory = "~/Areas/AdminManagement/Views/Category/_ModalEditCategory.cshtml";
            public readonly string _ModalSearchCategory = "~/Areas/AdminManagement/Views/Category/_ModalSearchCategory.cshtml";
            public readonly string _NewCategory = "~/Areas/AdminManagement/Views/Category/_NewCategory.cshtml";
            public readonly string Index = "~/Areas/AdminManagement/Views/Category/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_CategoryController : PL.MVC.IOBalance.Areas.AdminManagement.Controllers.CategoryController
    {
        public T4MVC_CategoryController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void SaveCategoryOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, PL.Business.Dto.IOBalance.CategoryDto dto);

        [NonAction]
        public override System.Web.Mvc.ActionResult SaveCategory(PL.Business.Dto.IOBalance.CategoryDto dto)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SaveCategory);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dto", dto);
            SaveCategoryOverride(callInfo, dto);
            return callInfo;
        }

        [NonAction]
        partial void UpdateCategoryOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, PL.Business.Dto.IOBalance.CategoryDto dto);

        [NonAction]
        public override System.Web.Mvc.ActionResult UpdateCategory(PL.Business.Dto.IOBalance.CategoryDto dto)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateCategory);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dto", dto);
            UpdateCategoryOverride(callInfo, dto);
            return callInfo;
        }

        [NonAction]
        partial void isActiveCategoryOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int categoryId, string categoryCode, string categoryName);

        [NonAction]
        public override System.Web.Mvc.ActionResult isActiveCategory(int categoryId, string categoryCode, string categoryName)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.isActiveCategory);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "categoryId", categoryId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "categoryCode", categoryCode);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "categoryName", categoryName);
            isActiveCategoryOverride(callInfo, categoryId, categoryCode, categoryName);
            return callInfo;
        }

        [NonAction]
        partial void GetCategoryOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, PL.MVC.IOBalance.Areas.AdminManagement.Models.CategorySearchModel searchModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetCategory(Kendo.Mvc.UI.DataSourceRequest request, PL.MVC.IOBalance.Areas.AdminManagement.Models.CategorySearchModel searchModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetCategory);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "searchModel", searchModel);
            GetCategoryOverride(callInfo, request, searchModel);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
