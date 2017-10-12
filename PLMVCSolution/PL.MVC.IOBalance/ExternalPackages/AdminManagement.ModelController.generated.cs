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
    public partial class ModelController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ModelController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult SaveModel()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SaveModel);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult UpdateModel()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateModel);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult SearchModel()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SearchModel);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ModelController Actions { get { return IOBALANCEMVC.AdminManagement.Model; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "AdminManagement";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Model";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Model";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string SaveModel = "SaveModel";
            public readonly string UpdateModel = "UpdateModel";
            public readonly string RefreshModel = "RefreshModel";
            public readonly string SearchModel = "SearchModel";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string SaveModel = "SaveModel";
            public const string UpdateModel = "UpdateModel";
            public const string RefreshModel = "RefreshModel";
            public const string SearchModel = "SearchModel";
        }


        static readonly ActionParamsClass_SaveModel s_params_SaveModel = new ActionParamsClass_SaveModel();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SaveModel SaveModelParams { get { return s_params_SaveModel; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SaveModel
        {
            public readonly string dto = "dto";
        }
        static readonly ActionParamsClass_UpdateModel s_params_UpdateModel = new ActionParamsClass_UpdateModel();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UpdateModel UpdateModelParams { get { return s_params_UpdateModel; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UpdateModel
        {
            public readonly string dto = "dto";
        }
        static readonly ActionParamsClass_SearchModel s_params_SearchModel = new ActionParamsClass_SearchModel();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SearchModel SearchModelParams { get { return s_params_SearchModel; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SearchModel
        {
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
                public readonly string _EditModel = "_EditModel";
                public readonly string _ListModel = "_ListModel";
                public readonly string _ModalSearchModel = "_ModalSearchModel";
                public readonly string _NewModel = "_NewModel";
                public readonly string Index = "Index";
            }
            public readonly string _EditModel = "~/Areas/AdminManagement/Views/Model/_EditModel.cshtml";
            public readonly string _ListModel = "~/Areas/AdminManagement/Views/Model/_ListModel.cshtml";
            public readonly string _ModalSearchModel = "~/Areas/AdminManagement/Views/Model/_ModalSearchModel.cshtml";
            public readonly string _NewModel = "~/Areas/AdminManagement/Views/Model/_NewModel.cshtml";
            public readonly string Index = "~/Areas/AdminManagement/Views/Model/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ModelController : PL.MVC.IOBalance.Areas.AdminManagement.Controllers.ModelController
    {
        public T4MVC_ModelController() : base(Dummy.Instance) { }

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
        partial void SaveModelOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, PL.Business.Dto.IOBalance.ModelDto dto);

        [NonAction]
        public override System.Web.Mvc.ActionResult SaveModel(PL.Business.Dto.IOBalance.ModelDto dto)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SaveModel);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dto", dto);
            SaveModelOverride(callInfo, dto);
            return callInfo;
        }

        [NonAction]
        partial void UpdateModelOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, PL.Business.Dto.IOBalance.ModelDto dto);

        [NonAction]
        public override System.Web.Mvc.ActionResult UpdateModel(PL.Business.Dto.IOBalance.ModelDto dto)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateModel);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dto", dto);
            UpdateModelOverride(callInfo, dto);
            return callInfo;
        }

        [NonAction]
        partial void RefreshModelOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult RefreshModel()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RefreshModel);
            RefreshModelOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void SearchModelOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, PL.MVC.IOBalance.Areas.AdminManagement.Models.ModelSearchModel searchModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult SearchModel(PL.MVC.IOBalance.Areas.AdminManagement.Models.ModelSearchModel searchModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SearchModel);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "searchModel", searchModel);
            SearchModelOverride(callInfo, searchModel);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
