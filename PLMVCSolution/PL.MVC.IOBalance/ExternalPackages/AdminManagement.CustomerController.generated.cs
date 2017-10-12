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
    public partial class CustomerController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected CustomerController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult SaveCustomer()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SaveCustomer);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult UpdateCustomer()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateCustomer);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult isActiveCustomer()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.isActiveCustomer);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GetCustomers()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetCustomers);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public CustomerController Actions { get { return IOBALANCEMVC.AdminManagement.Customer; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "AdminManagement";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Customer";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Customer";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string SaveCustomer = "SaveCustomer";
            public readonly string UpdateCustomer = "UpdateCustomer";
            public readonly string isActiveCustomer = "isActiveCustomer";
            public readonly string GetCustomers = "GetCustomers";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string SaveCustomer = "SaveCustomer";
            public const string UpdateCustomer = "UpdateCustomer";
            public const string isActiveCustomer = "isActiveCustomer";
            public const string GetCustomers = "GetCustomers";
        }


        static readonly ActionParamsClass_SaveCustomer s_params_SaveCustomer = new ActionParamsClass_SaveCustomer();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SaveCustomer SaveCustomerParams { get { return s_params_SaveCustomer; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SaveCustomer
        {
            public readonly string dto = "dto";
        }
        static readonly ActionParamsClass_UpdateCustomer s_params_UpdateCustomer = new ActionParamsClass_UpdateCustomer();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UpdateCustomer UpdateCustomerParams { get { return s_params_UpdateCustomer; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UpdateCustomer
        {
            public readonly string dto = "dto";
        }
        static readonly ActionParamsClass_isActiveCustomer s_params_isActiveCustomer = new ActionParamsClass_isActiveCustomer();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_isActiveCustomer isActiveCustomerParams { get { return s_params_isActiveCustomer; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_isActiveCustomer
        {
            public readonly string customerId = "customerId";
            public readonly string customerCode = "customerCode";
        }
        static readonly ActionParamsClass_GetCustomers s_params_GetCustomers = new ActionParamsClass_GetCustomers();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetCustomers GetCustomersParams { get { return s_params_GetCustomers; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetCustomers
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
                public readonly string _FrmCustomer = "_FrmCustomer";
                public readonly string _ListCustomer = "_ListCustomer";
                public readonly string _ListCustomerKendo = "_ListCustomerKendo";
                public readonly string _ModalEditCustomer = "_ModalEditCustomer";
                public readonly string _ModalSearchCustomer = "_ModalSearchCustomer";
                public readonly string _NewCustomer = "_NewCustomer";
                public readonly string Index = "Index";
            }
            public readonly string _FrmCustomer = "~/Areas/AdminManagement/Views/Customer/_FrmCustomer.cshtml";
            public readonly string _ListCustomer = "~/Areas/AdminManagement/Views/Customer/_ListCustomer.cshtml";
            public readonly string _ListCustomerKendo = "~/Areas/AdminManagement/Views/Customer/_ListCustomerKendo.cshtml";
            public readonly string _ModalEditCustomer = "~/Areas/AdminManagement/Views/Customer/_ModalEditCustomer.cshtml";
            public readonly string _ModalSearchCustomer = "~/Areas/AdminManagement/Views/Customer/_ModalSearchCustomer.cshtml";
            public readonly string _NewCustomer = "~/Areas/AdminManagement/Views/Customer/_NewCustomer.cshtml";
            public readonly string Index = "~/Areas/AdminManagement/Views/Customer/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_CustomerController : PL.MVC.IOBalance.Areas.AdminManagement.Controllers.CustomerController
    {
        public T4MVC_CustomerController() : base(Dummy.Instance) { }

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
        partial void SaveCustomerOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, PL.Business.Dto.IOBalance.CustomerDto dto);

        [NonAction]
        public override System.Web.Mvc.ActionResult SaveCustomer(PL.Business.Dto.IOBalance.CustomerDto dto)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SaveCustomer);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dto", dto);
            SaveCustomerOverride(callInfo, dto);
            return callInfo;
        }

        [NonAction]
        partial void UpdateCustomerOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, PL.Business.Dto.IOBalance.CustomerDto dto);

        [NonAction]
        public override System.Web.Mvc.ActionResult UpdateCustomer(PL.Business.Dto.IOBalance.CustomerDto dto)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateCustomer);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dto", dto);
            UpdateCustomerOverride(callInfo, dto);
            return callInfo;
        }

        [NonAction]
        partial void isActiveCustomerOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int customerId, string customerCode);

        [NonAction]
        public override System.Web.Mvc.ActionResult isActiveCustomer(int customerId, string customerCode)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.isActiveCustomer);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "customerId", customerId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "customerCode", customerCode);
            isActiveCustomerOverride(callInfo, customerId, customerCode);
            return callInfo;
        }

        [NonAction]
        partial void GetCustomersOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, PL.MVC.IOBalance.Areas.AdminManagement.Models.CustomerSearchModel searchModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetCustomers(Kendo.Mvc.UI.DataSourceRequest request, PL.MVC.IOBalance.Areas.AdminManagement.Models.CustomerSearchModel searchModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetCustomers);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "searchModel", searchModel);
            GetCustomersOverride(callInfo, request, searchModel);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
