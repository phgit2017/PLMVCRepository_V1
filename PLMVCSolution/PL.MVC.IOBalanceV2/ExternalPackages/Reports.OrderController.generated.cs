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
namespace PL.MVC.IOBalanceV2.Areas.Reports.Controllers
{
    public partial class OrderController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected OrderController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult GetPOReport()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetPOReport);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ExportPOExcel()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExportPOExcel);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GetSOReport()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetSOReport);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GetSOReceiptReport()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetSOReceiptReport);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ExportSOExcel()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExportSOExcel);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ExportSOReceiptExcel()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExportSOReceiptExcel);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public OrderController Actions { get { return IOBALANCEMVCV2.Reports.Order; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Reports";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Order";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Order";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string PurchaseOrder = "PurchaseOrder";
            public readonly string GetPOReport = "GetPOReport";
            public readonly string ExportPOExcel = "ExportPOExcel";
            public readonly string SalesOrder = "SalesOrder";
            public readonly string SalesOrderReceipt = "SalesOrderReceipt";
            public readonly string GetSOReport = "GetSOReport";
            public readonly string GetSOReceiptReport = "GetSOReceiptReport";
            public readonly string ExportSOExcel = "ExportSOExcel";
            public readonly string ExportSOReceiptExcel = "ExportSOReceiptExcel";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string PurchaseOrder = "PurchaseOrder";
            public const string GetPOReport = "GetPOReport";
            public const string ExportPOExcel = "ExportPOExcel";
            public const string SalesOrder = "SalesOrder";
            public const string SalesOrderReceipt = "SalesOrderReceipt";
            public const string GetSOReport = "GetSOReport";
            public const string GetSOReceiptReport = "GetSOReceiptReport";
            public const string ExportSOExcel = "ExportSOExcel";
            public const string ExportSOReceiptExcel = "ExportSOReceiptExcel";
        }


        static readonly ActionParamsClass_GetPOReport s_params_GetPOReport = new ActionParamsClass_GetPOReport();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetPOReport GetPOReportParams { get { return s_params_GetPOReport; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetPOReport
        {
            public readonly string request = "request";
            public readonly string searchModel = "searchModel";
        }
        static readonly ActionParamsClass_ExportPOExcel s_params_ExportPOExcel = new ActionParamsClass_ExportPOExcel();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ExportPOExcel ExportPOExcelParams { get { return s_params_ExportPOExcel; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ExportPOExcel
        {
            public readonly string searchModel = "searchModel";
        }
        static readonly ActionParamsClass_GetSOReport s_params_GetSOReport = new ActionParamsClass_GetSOReport();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetSOReport GetSOReportParams { get { return s_params_GetSOReport; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetSOReport
        {
            public readonly string request = "request";
            public readonly string searchModel = "searchModel";
        }
        static readonly ActionParamsClass_GetSOReceiptReport s_params_GetSOReceiptReport = new ActionParamsClass_GetSOReceiptReport();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetSOReceiptReport GetSOReceiptReportParams { get { return s_params_GetSOReceiptReport; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetSOReceiptReport
        {
            public readonly string request = "request";
            public readonly string searchModel = "searchModel";
        }
        static readonly ActionParamsClass_ExportSOExcel s_params_ExportSOExcel = new ActionParamsClass_ExportSOExcel();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ExportSOExcel ExportSOExcelParams { get { return s_params_ExportSOExcel; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ExportSOExcel
        {
            public readonly string searchModel = "searchModel";
        }
        static readonly ActionParamsClass_ExportSOReceiptExcel s_params_ExportSOReceiptExcel = new ActionParamsClass_ExportSOReceiptExcel();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ExportSOReceiptExcel ExportSOReceiptExcelParams { get { return s_params_ExportSOReceiptExcel; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ExportSOReceiptExcel
        {
            public readonly string salesOrderId = "salesOrderId";
            public readonly string customerId = "customerId";
            public readonly string salesNo = "salesNo";
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
                public readonly string _List = "_List";
                public readonly string _ListSalesOrder = "_ListSalesOrder";
                public readonly string _ListSalesReceipt = "_ListSalesReceipt";
                public readonly string _Search = "_Search";
                public readonly string _SearchSalesOrder = "_SearchSalesOrder";
                public readonly string PurchaseOrder = "PurchaseOrder";
                public readonly string SalesOrder = "SalesOrder";
                public readonly string SalesOrderReceipt = "SalesOrderReceipt";
            }
            public readonly string _List = "~/Areas/Reports/Views/Order/_List.cshtml";
            public readonly string _ListSalesOrder = "~/Areas/Reports/Views/Order/_ListSalesOrder.cshtml";
            public readonly string _ListSalesReceipt = "~/Areas/Reports/Views/Order/_ListSalesReceipt.cshtml";
            public readonly string _Search = "~/Areas/Reports/Views/Order/_Search.cshtml";
            public readonly string _SearchSalesOrder = "~/Areas/Reports/Views/Order/_SearchSalesOrder.cshtml";
            public readonly string PurchaseOrder = "~/Areas/Reports/Views/Order/PurchaseOrder.cshtml";
            public readonly string SalesOrder = "~/Areas/Reports/Views/Order/SalesOrder.cshtml";
            public readonly string SalesOrderReceipt = "~/Areas/Reports/Views/Order/SalesOrderReceipt.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_OrderController : PL.MVC.IOBalanceV2.Areas.Reports.Controllers.OrderController
    {
        public T4MVC_OrderController() : base(Dummy.Instance) { }

        [NonAction]
        partial void PurchaseOrderOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult PurchaseOrder()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PurchaseOrder);
            PurchaseOrderOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetPOReportOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, PL.MVC.IOBalanceV2.Areas.Reports.Models.PurchaseOrderSearchModel searchModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetPOReport(Kendo.Mvc.UI.DataSourceRequest request, PL.MVC.IOBalanceV2.Areas.Reports.Models.PurchaseOrderSearchModel searchModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetPOReport);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "searchModel", searchModel);
            GetPOReportOverride(callInfo, request, searchModel);
            return callInfo;
        }

        [NonAction]
        partial void ExportPOExcelOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, PL.MVC.IOBalanceV2.Areas.Reports.Models.PurchaseOrderSearchModel searchModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult ExportPOExcel(PL.MVC.IOBalanceV2.Areas.Reports.Models.PurchaseOrderSearchModel searchModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExportPOExcel);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "searchModel", searchModel);
            ExportPOExcelOverride(callInfo, searchModel);
            return callInfo;
        }

        [NonAction]
        partial void SalesOrderOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult SalesOrder()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SalesOrder);
            SalesOrderOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void SalesOrderReceiptOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult SalesOrderReceipt()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SalesOrderReceipt);
            SalesOrderReceiptOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetSOReportOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, PL.MVC.IOBalanceV2.Areas.Reports.Models.SalesOrderSearchModel searchModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetSOReport(Kendo.Mvc.UI.DataSourceRequest request, PL.MVC.IOBalanceV2.Areas.Reports.Models.SalesOrderSearchModel searchModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetSOReport);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "searchModel", searchModel);
            GetSOReportOverride(callInfo, request, searchModel);
            return callInfo;
        }

        [NonAction]
        partial void GetSOReceiptReportOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, PL.MVC.IOBalanceV2.Areas.Reports.Models.SalesOrderSearchModel searchModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetSOReceiptReport(Kendo.Mvc.UI.DataSourceRequest request, PL.MVC.IOBalanceV2.Areas.Reports.Models.SalesOrderSearchModel searchModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetSOReceiptReport);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "searchModel", searchModel);
            GetSOReceiptReportOverride(callInfo, request, searchModel);
            return callInfo;
        }

        [NonAction]
        partial void ExportSOExcelOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, PL.MVC.IOBalanceV2.Areas.Reports.Models.SalesOrderSearchModel searchModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult ExportSOExcel(PL.MVC.IOBalanceV2.Areas.Reports.Models.SalesOrderSearchModel searchModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExportSOExcel);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "searchModel", searchModel);
            ExportSOExcelOverride(callInfo, searchModel);
            return callInfo;
        }

        [NonAction]
        partial void ExportSOReceiptExcelOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int salesOrderId, int customerId, string salesNo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ExportSOReceiptExcel(int salesOrderId, int customerId, string salesNo)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExportSOReceiptExcel);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "salesOrderId", salesOrderId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "customerId", customerId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "salesNo", salesNo);
            ExportSOReceiptExcelOverride(callInfo, salesOrderId, customerId, salesNo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
