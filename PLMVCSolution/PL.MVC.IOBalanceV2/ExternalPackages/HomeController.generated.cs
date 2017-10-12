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
namespace PL.MVC.IOBalanceV2.Controllers
{
    public partial class HomeController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected HomeController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult GetQueueOrders()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetQueueOrders);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GetQueueOrderList()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetQueueOrderList);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GetOrderAndOrderDetails()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetOrderAndOrderDetails);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult SaveSalesOrder()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SaveSalesOrder);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult QueueSalesOrder()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.QueueSalesOrder);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public HomeController Actions { get { return IOBALANCEMVCV2.Home; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Home";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Home";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string GetQueueOrders = "GetQueueOrders";
            public readonly string GetQueueOrderList = "GetQueueOrderList";
            public readonly string GetOrderAndOrderDetails = "GetOrderAndOrderDetails";
            public readonly string SaveSalesOrder = "SaveSalesOrder";
            public readonly string QueueSalesOrder = "QueueSalesOrder";
            public readonly string Index = "Index";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string GetQueueOrders = "GetQueueOrders";
            public const string GetQueueOrderList = "GetQueueOrderList";
            public const string GetOrderAndOrderDetails = "GetOrderAndOrderDetails";
            public const string SaveSalesOrder = "SaveSalesOrder";
            public const string QueueSalesOrder = "QueueSalesOrder";
            public const string Index = "Index";
        }


        static readonly ActionParamsClass_GetQueueOrders s_params_GetQueueOrders = new ActionParamsClass_GetQueueOrders();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetQueueOrders GetQueueOrdersParams { get { return s_params_GetQueueOrders; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetQueueOrders
        {
            public readonly string request = "request";
            public readonly string searchModel = "searchModel";
        }
        static readonly ActionParamsClass_GetQueueOrderList s_params_GetQueueOrderList = new ActionParamsClass_GetQueueOrderList();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetQueueOrderList GetQueueOrderListParams { get { return s_params_GetQueueOrderList; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetQueueOrderList
        {
            public readonly string request = "request";
            public readonly string salesOrderId = "salesOrderId";
        }
        static readonly ActionParamsClass_GetOrderAndOrderDetails s_params_GetOrderAndOrderDetails = new ActionParamsClass_GetOrderAndOrderDetails();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetOrderAndOrderDetails GetOrderAndOrderDetailsParams { get { return s_params_GetOrderAndOrderDetails; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetOrderAndOrderDetails
        {
            public readonly string salesOrderId = "salesOrderId";
        }
        static readonly ActionParamsClass_SaveSalesOrder s_params_SaveSalesOrder = new ActionParamsClass_SaveSalesOrder();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SaveSalesOrder SaveSalesOrderParams { get { return s_params_SaveSalesOrder; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SaveSalesOrder
        {
            public readonly string dto = "dto";
            public readonly string CustomerId = "CustomerId";
            public readonly string salesOrderId = "salesOrderId";
        }
        static readonly ActionParamsClass_QueueSalesOrder s_params_QueueSalesOrder = new ActionParamsClass_QueueSalesOrder();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_QueueSalesOrder QueueSalesOrderParams { get { return s_params_QueueSalesOrder; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_QueueSalesOrder
        {
            public readonly string dto = "dto";
            public readonly string CustomerId = "CustomerId";
            public readonly string salesOrderId = "salesOrderId";
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
                public readonly string _DetailsSalesOrder = "_DetailsSalesOrder";
                public readonly string _DetailsSalesOrderDetail = "_DetailsSalesOrderDetail";
                public readonly string _EditOrderList = "_EditOrderList";
                public readonly string _QueueOrderList = "_QueueOrderList";
                public readonly string _SalesOrderList = "_SalesOrderList";
                public readonly string About = "About";
                public readonly string Contact = "Contact";
                public readonly string Index = "Index";
            }
            public readonly string _DetailsSalesOrder = "~/Views/Home/_DetailsSalesOrder.cshtml";
            public readonly string _DetailsSalesOrderDetail = "~/Views/Home/_DetailsSalesOrderDetail.cshtml";
            public readonly string _EditOrderList = "~/Views/Home/_EditOrderList.cshtml";
            public readonly string _QueueOrderList = "~/Views/Home/_QueueOrderList.cshtml";
            public readonly string _SalesOrderList = "~/Views/Home/_SalesOrderList.cshtml";
            public readonly string About = "~/Views/Home/About.cshtml";
            public readonly string Contact = "~/Views/Home/Contact.cshtml";
            public readonly string Index = "~/Views/Home/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_HomeController : PL.MVC.IOBalanceV2.Controllers.HomeController
    {
        public T4MVC_HomeController() : base(Dummy.Instance) { }

        [NonAction]
        partial void GetQueueOrdersOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, PL.MVC.IOBalanceV2.Models.QueueOrderSearchModel searchModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetQueueOrders(Kendo.Mvc.UI.DataSourceRequest request, PL.MVC.IOBalanceV2.Models.QueueOrderSearchModel searchModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetQueueOrders);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "searchModel", searchModel);
            GetQueueOrdersOverride(callInfo, request, searchModel);
            return callInfo;
        }

        [NonAction]
        partial void GetQueueOrderListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, long salesOrderId);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetQueueOrderList(Kendo.Mvc.UI.DataSourceRequest request, long salesOrderId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetQueueOrderList);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "salesOrderId", salesOrderId);
            GetQueueOrderListOverride(callInfo, request, salesOrderId);
            return callInfo;
        }

        [NonAction]
        partial void GetOrderAndOrderDetailsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long salesOrderId);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetOrderAndOrderDetails(long salesOrderId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetOrderAndOrderDetails);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "salesOrderId", salesOrderId);
            GetOrderAndOrderDetailsOverride(callInfo, salesOrderId);
            return callInfo;
        }

        [NonAction]
        partial void SaveSalesOrderOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Collections.Generic.List<PL.Business.Dto.IOBalanceV2.SalesOrderListDto> dto, int CustomerId, long salesOrderId);

        [NonAction]
        public override System.Web.Mvc.ActionResult SaveSalesOrder(System.Collections.Generic.List<PL.Business.Dto.IOBalanceV2.SalesOrderListDto> dto, int CustomerId, long salesOrderId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SaveSalesOrder);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dto", dto);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "CustomerId", CustomerId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "salesOrderId", salesOrderId);
            SaveSalesOrderOverride(callInfo, dto, CustomerId, salesOrderId);
            return callInfo;
        }

        [NonAction]
        partial void QueueSalesOrderOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Collections.Generic.List<PL.Business.Dto.IOBalanceV2.SalesOrderListDto> dto, int CustomerId, long salesOrderId);

        [NonAction]
        public override System.Web.Mvc.ActionResult QueueSalesOrder(System.Collections.Generic.List<PL.Business.Dto.IOBalanceV2.SalesOrderListDto> dto, int CustomerId, long salesOrderId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.QueueSalesOrder);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dto", dto);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "CustomerId", CustomerId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "salesOrderId", salesOrderId);
            QueueSalesOrderOverride(callInfo, dto, CustomerId, salesOrderId);
            return callInfo;
        }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
