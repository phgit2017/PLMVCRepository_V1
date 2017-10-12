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
namespace PL.MVC.IOBalanceV2.Areas.AdminManagement.Controllers
{
    public partial class SupplierController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected SupplierController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult GetDetails()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetDetails);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult SaveDetails()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SaveDetails);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult UpdateDetails()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateDetails);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public SupplierController Actions { get { return IOBALANCEMVCV2.AdminManagement.Supplier; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "AdminManagement";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Supplier";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Supplier";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string GetDetails = "GetDetails";
            public readonly string SaveDetails = "SaveDetails";
            public readonly string UpdateDetails = "UpdateDetails";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string GetDetails = "GetDetails";
            public const string SaveDetails = "SaveDetails";
            public const string UpdateDetails = "UpdateDetails";
        }


        static readonly ActionParamsClass_GetDetails s_params_GetDetails = new ActionParamsClass_GetDetails();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetDetails GetDetailsParams { get { return s_params_GetDetails; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetDetails
        {
            public readonly string request = "request";
            public readonly string searchModel = "searchModel";
        }
        static readonly ActionParamsClass_SaveDetails s_params_SaveDetails = new ActionParamsClass_SaveDetails();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SaveDetails SaveDetailsParams { get { return s_params_SaveDetails; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SaveDetails
        {
            public readonly string dto = "dto";
        }
        static readonly ActionParamsClass_UpdateDetails s_params_UpdateDetails = new ActionParamsClass_UpdateDetails();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UpdateDetails UpdateDetailsParams { get { return s_params_UpdateDetails; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UpdateDetails
        {
            public readonly string dto = "dto";
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
                public readonly string _Details = "_Details";
                public readonly string _Edit = "_Edit";
                public readonly string _List = "_List";
                public readonly string _New = "_New";
                public readonly string _Search = "_Search";
                public readonly string Index = "Index";
            }
            public readonly string _Details = "~/Areas/AdminManagement/Views/Supplier/_Details.cshtml";
            public readonly string _Edit = "~/Areas/AdminManagement/Views/Supplier/_Edit.cshtml";
            public readonly string _List = "~/Areas/AdminManagement/Views/Supplier/_List.cshtml";
            public readonly string _New = "~/Areas/AdminManagement/Views/Supplier/_New.cshtml";
            public readonly string _Search = "~/Areas/AdminManagement/Views/Supplier/_Search.cshtml";
            public readonly string Index = "~/Areas/AdminManagement/Views/Supplier/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_SupplierController : PL.MVC.IOBalanceV2.Areas.AdminManagement.Controllers.SupplierController
    {
        public T4MVC_SupplierController() : base(Dummy.Instance) { }

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
        partial void GetDetailsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, PL.MVC.IOBalanceV2.Areas.AdminManagement.Models.SupplierSearchModel searchModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetDetails(Kendo.Mvc.UI.DataSourceRequest request, PL.MVC.IOBalanceV2.Areas.AdminManagement.Models.SupplierSearchModel searchModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetDetails);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "searchModel", searchModel);
            GetDetailsOverride(callInfo, request, searchModel);
            return callInfo;
        }

        [NonAction]
        partial void SaveDetailsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, PL.Business.Dto.IOBalanceV2.SupplierDto dto);

        [NonAction]
        public override System.Web.Mvc.ActionResult SaveDetails(PL.Business.Dto.IOBalanceV2.SupplierDto dto)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SaveDetails);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dto", dto);
            SaveDetailsOverride(callInfo, dto);
            return callInfo;
        }

        [NonAction]
        partial void UpdateDetailsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, PL.Business.Dto.IOBalanceV2.SupplierDto dto);

        [NonAction]
        public override System.Web.Mvc.ActionResult UpdateDetails(PL.Business.Dto.IOBalanceV2.SupplierDto dto)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateDetails);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dto", dto);
            UpdateDetailsOverride(callInfo, dto);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
