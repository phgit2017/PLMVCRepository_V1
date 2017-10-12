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
using PL.MVC.IOBalance.Areas.ReportManagement.Models;

using PL.MVC.IOBalance.Infrastructure;
using Infrastructure.Utilities.Extensions;
using LinqKit;
using System.Drawing;
using Infrastructure.Utilities;
using System.Data.Entity;
using System.IO;
using OfficeOpenXml;
using System.Data;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace PL.MVC.IOBalance.Areas.ReportManagement.Controllers
{
    public partial class InventorySalesController : Controller
    {
        #region DeclarationsAndConstructor
        IReportCombinationService _reportCombinationService;
        public InventorySalesController(IReportCombinationService reportCombinationService)
        {
            this._reportCombinationService = reportCombinationService;
        }
        #endregion DeclarationsAndConstructor

        #region ActionMethods
        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult Index(ReportInventoryPerItemSearchModel model)
        {
            //if (model.BranchID.IsNull() || model.CategoryID.IsNull() || model.DateGenerated.IsNull())
            //{
            //    model = new ReportInventoryPerItemSearchModel();
            //}

            if (model.IsNull())
            {
                model = new ReportInventoryPerItemSearchModel();
            }

            return View(model);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult PurchaseOrderList(ReportInventoryPerItemSearchModel model)
        {
            //if (model.BranchID.IsNull() || model.CategoryID.IsNull() || model.DateGenerated.IsNull())
            //{
            //    model = new ReportInventoryPerItemSearchModel();
            //}
            if (model.IsNull())
            {
                model = new ReportInventoryPerItemSearchModel();
            }
            return View(model);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult SalesOrderList(ReportInventoryPerItemSearchModel model)
        {
            //if (model.BranchID.IsNull() || model.CategoryID.IsNull() || model.DateGenerated.IsNull())
            //{
            //    model = new ReportInventoryPerItemSearchModel();
            //}
            if (model.IsNull())
            {
                model = new ReportInventoryPerItemSearchModel();
            }
            return View(model);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult SalesReport(ReportInventoryPerItemSearchModel model)
        {
            //if (model.BranchID.IsNull() || model.CategoryID.IsNull() || model.DateGenerated.IsNull())
            //{
            //    model = new ReportInventoryPerItemSearchModel();
            //}
            if (model.IsNull())
            {
                model = new ReportInventoryPerItemSearchModel();
            }
            return View(model);
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult InventoryReport()
        {
            return View();
        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult ExtractReportToExcel(ReportInventoryPerItemSearchModel searchModel)
        {

            int? branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            int? userTypeId = Session[SessionVariables.UserDetails].GetUserTypeIdFromSession();
            string userName = Session[SessionVariables.UserDetails].GetUserNameFromSession();

            DataTable dt = new DataTable();

            if (userTypeId == Constants.UserTypeAdminId)
            {
                if (ModelState.IsValid)
                {
                    return ReportPerCategoryExtract(searchModel);
                }
                else
                {
                    if (searchModel.CategoryID.IsNull())
                    {
                        ModelState.AddModelError("CategoryID", string.Format(Messages.FieldRequired, "Category"));
                    }

                    if (userTypeId == Constants.UserTypeAdminId)
                    {
                        if (searchModel.BranchID.IsNull())
                        {
                            ModelState.AddModelError("BranchID", string.Format(Messages.FieldRequired, "Branch"));
                        }
                    }
                    else
                    {
                        searchModel.BranchID = branchId;
                    }

                    return View(IOBALANCEMVC.ReportManagement.InventorySales.Views.Index, searchModel);
                    //return RedirectToAction(IOBALANCEMVC.ReportManagement.InventorySales.Index(searchModel));
                }
            }
            else
            {
                if (searchModel.CategoryID.IsNull())
                {
                    ModelState.AddModelError("CategoryID", string.Format(Messages.FieldRequired, "Category"));
                    return View(IOBALANCEMVC.ReportManagement.InventorySales.Views.Index, searchModel);
                }
                else
                {
                    return ReportPerCategoryExtract(searchModel);
                }
            }





        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult ExtractReportPurchaseOrderToExcel(ReportInventoryPerItemSearchModel searchModel)
        {

            int? branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            int? userTypeId = Session[SessionVariables.UserDetails].GetUserTypeIdFromSession();
            string userName = Session[SessionVariables.UserDetails].GetUserNameFromSession();

            DataTable dt = new DataTable();

            if (userTypeId == Constants.UserTypeAdminId)
            {
                if (ModelState.IsValid)
                {
                    return ReportPerPurchaseOrderExtract(searchModel);
                }
                else
                {
                    if (searchModel.CategoryID.IsNull())
                    {
                        ModelState.AddModelError("CategoryID", string.Format(Messages.FieldRequired, "Category"));
                    }

                    if (userTypeId == Constants.UserTypeAdminId)
                    {
                        if (searchModel.BranchID.IsNull())
                        {
                            ModelState.AddModelError("BranchID", string.Format(Messages.FieldRequired, "Branch"));
                        }
                    }
                    else
                    {
                        searchModel.BranchID = branchId;
                    }

                    return View(IOBALANCEMVC.ReportManagement.InventorySales.Views.PurchaseOrderList, searchModel);
                    //return RedirectToAction(IOBALANCEMVC.ReportManagement.InventorySales.Index(searchModel));
                }
            }
            else
            {
                if (searchModel.CategoryID.IsNull())
                {
                    ModelState.AddModelError("CategoryID", string.Format(Messages.FieldRequired, "Category"));
                    return View(IOBALANCEMVC.ReportManagement.InventorySales.Views.PurchaseOrderList, searchModel);
                }
                else
                {
                    return ReportPerPurchaseOrderExtract(searchModel);
                }
            }






        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult ExtractReportSalesOrderToExcel(ReportInventoryPerItemSearchModel searchModel)
        {

            int? branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            int? userTypeId = Session[SessionVariables.UserDetails].GetUserTypeIdFromSession();
            string userName = Session[SessionVariables.UserDetails].GetUserNameFromSession();

            DataTable dt = new DataTable();

            if (userTypeId == Constants.UserTypeAdminId)
            {
                if (ModelState.IsValid)
                {
                    return ReportPerSalesOrderExtract(searchModel);
                }
                else
                {
                    if (searchModel.CategoryID.IsNull())
                    {
                        ModelState.AddModelError("CategoryID", string.Format(Messages.FieldRequired, "Category"));
                    }

                    if (userTypeId == Constants.UserTypeAdminId)
                    {
                        if (searchModel.BranchID.IsNull())
                        {
                            ModelState.AddModelError("BranchID", string.Format(Messages.FieldRequired, "Branch"));
                        }
                    }
                    else
                    {
                        searchModel.BranchID = branchId;
                    }

                    return View(IOBALANCEMVC.ReportManagement.InventorySales.Views.SalesOrderList, searchModel);
                    //return RedirectToAction(IOBALANCEMVC.ReportManagement.InventorySales.Index(searchModel));
                }
            }
            else
            {
                if (searchModel.CategoryID.IsNull())
                {
                    ModelState.AddModelError("CategoryID", string.Format(Messages.FieldRequired, "Category"));
                    return View(IOBALANCEMVC.ReportManagement.InventorySales.Views.SalesOrderList, searchModel);
                }
                else
                {
                    return ReportPerSalesOrderExtract(searchModel);
                }
            }

        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult ExtractReportSalesReportToExcel(ReportInventoryPerItemSearchModel searchModel)
        {

            int? branchId = Session[SessionVariables.UserDetails].GetBranchIdFromSession();
            int? userTypeId = Session[SessionVariables.UserDetails].GetUserTypeIdFromSession();
            string userName = Session[SessionVariables.UserDetails].GetUserNameFromSession();

            DataTable dt = new DataTable();

            if (userTypeId == Constants.UserTypeAdminId)
            {
                if (ModelState.IsValid)
                {
                    return ReportPerSalesReportExtract(searchModel);
                }
                else
                {
                    if (searchModel.CategoryID.IsNull())
                    {
                        ModelState.AddModelError("CategoryID", string.Format(Messages.FieldRequired, "Category"));
                    }

                    if (userTypeId == Constants.UserTypeAdminId)
                    {
                        if (searchModel.BranchID.IsNull())
                        {
                            ModelState.AddModelError("BranchID", string.Format(Messages.FieldRequired, "Branch"));
                        }
                    }
                    else
                    {
                        searchModel.BranchID = branchId;
                    }

                    return View(IOBALANCEMVC.ReportManagement.InventorySales.Views.SalesReport, searchModel);
                    //return RedirectToAction(IOBALANCEMVC.ReportManagement.InventorySales.Index(searchModel));
                }
            }
            else
            {
                if (searchModel.CategoryID.IsNull())
                {
                    ModelState.AddModelError("CategoryID", string.Format(Messages.FieldRequired, "Category"));
                    return View(IOBALANCEMVC.ReportManagement.InventorySales.Views.SalesReport, searchModel);
                }
                else
                {
                    return ReportPerSalesReportExtract(searchModel);
                }
            }

        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult ExtractReportInventoryReportToExcel(ReportInventorySearchModel searchModel)
        {

            List<ReportCombinationDto> listOfReport = new List<ReportCombinationDto>();
            listOfReport = GetInventoryReportQueryable(searchModel).ToList();

            if (listOfReport.Count > 0)
            {
                return ReportPerInventoryReportExtract(searchModel, listOfReport);
            }
            else
            {
                return View(IOBALANCEMVC.ReportManagement.InventorySales.Views.InventoryReport);
            }

            //return View();

        }

        [IOBalanceSessionExpired]
        [IOBalanceAuthorizeUser]
        public virtual ActionResult GetInventoryReport([DataSourceRequest] DataSourceRequest request, ReportInventorySearchModel searchModel)
        {
            IQueryable<ReportCombinationDto> list = null;
            list = GetInventoryReportQueryable(searchModel);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion ActionMethods

        #region Private Methods
        private System.Web.Mvc.FileResult ReportPerCategoryExtract(ReportInventoryPerItemSearchModel searchModel)
        {
            DataTable dt = new DataTable();
            string userName = Session[SessionVariables.UserDetails].GetUserNameFromSession();


            DateTime date = (DateTime)searchModel.DateGenerated;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var monthName = firstDayOfMonth.ToString("MMMM");



            dt = _reportCombinationService.GetAll(firstDayOfMonth, lastDayOfMonth, searchModel.CategoryID, searchModel.BranchID);

            int rowId = 0;
            int colId = 0;
            var dir = Server.MapPath(string.Format("~/{0}", Constants.ProductExcelTemplateDir));
            var fileNameTemplate = string.Format("{0}{1}", Constants.ReportTemplate, ".xlsx");
            var path = System.IO.Path.Combine(dir, fileNameTemplate);
            var fileNameGenerated = string.Format("{0}{1}", Constants.ReportPerCategory, ".xlsx");

            var contentType = "application/vnd.ms-excel";

            var templateFile = new FileInfo(path);
            //var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            ;

            var package = new ExcelPackage(templateFile);
            var workSheet = package.Workbook.Worksheets[1];

            workSheet.Cells["A1"].Value = string.Format("MONTH OF {0} {1}", monthName, date.Year);
            workSheet.Cells["A2"].Value = dt.Rows[0][Constants.BranchName].ToString();
            workSheet.Cells["A3"].Value = string.Format("PREPARED BY: {0}", userName);

            rowId = 6;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                colId = 4;
                workSheet.Cells["B" + rowId.ToString()].Value = string.Format("{0} - {1} - {2}", dt.Rows[i][Constants.ProductCode].ToString(), dt.Rows[i][Constants.ProductName].ToString(), dt.Rows[i][Constants.ProductExtension].ToString());
                workSheet.Cells["C" + rowId.ToString()].Value = dt.Rows[i][Constants.OldQuantity].ToString();
                for (int c = 13; c <= 43; c++)
                {
                    workSheet.Cells[rowId, colId].Value = dt.Rows[i][c].ToString();
                    colId++;
                }

                workSheet.Cells["AK" + rowId.ToString()].Value = dt.Rows[i][Constants.NewQuantity].ToString();

                rowId++;

            }


            var memoryStream = new MemoryStream();
            //package.Save();
            package.SaveAs(memoryStream);
            memoryStream.Position = 0;

            return File(memoryStream, contentType, fileNameGenerated);
        }
        private System.Web.Mvc.FileResult ReportPerPurchaseOrderExtract(ReportInventoryPerItemSearchModel searchModel)
        {
            DataTable dt = new DataTable();
            string userName = Session[SessionVariables.UserDetails].GetUserNameFromSession();
            DateTime date = (DateTime)searchModel.DateGenerated;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var monthName = firstDayOfMonth.ToString("MMMM");

            dt = _reportCombinationService.GetAllPurchaseOrder(firstDayOfMonth, lastDayOfMonth, searchModel.CategoryID, searchModel.BranchID);

            int rowId = 0;
            int colId = 0;
            var dir = Server.MapPath(string.Format("~/{0}", Constants.ProductExcelTemplateDir));
            var fileNameTemplate = string.Format("{0}{1}", Constants.ReportTemplate, ".xlsx");
            var path = System.IO.Path.Combine(dir, fileNameTemplate);
            var fileNameGenerated = string.Format("{0}{1}", Constants.ReportPerPurchaseOrder, ".xlsx");

            var contentType = "application/vnd.ms-excel";

            var templateFile = new FileInfo(path);
            //var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            ;

            var package = new ExcelPackage(templateFile);
            var workSheet = package.Workbook.Worksheets[1];

            workSheet.Cells["A1"].Value = string.Format("MONTH OF {0} {1}", monthName, date.Year);
            workSheet.Cells["A2"].Value = dt.Rows[0][Constants.BranchName].ToString();
            workSheet.Cells["A3"].Value = string.Format("PREPARED BY: {0}", userName);

            rowId = 6;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                colId = 4;
                workSheet.Cells["B" + rowId.ToString()].Value = string.Format("{0} - {1} - {2}", dt.Rows[i][Constants.ProductCode].ToString(), dt.Rows[i][Constants.ProductName].ToString(), dt.Rows[i][Constants.ProductExtension].ToString());
                workSheet.Cells["C" + rowId.ToString()].Value = dt.Rows[i][Constants.OldQuantity].ToString();
                for (int c = 13; c <= 43; c++)
                {
                    workSheet.Cells[rowId, colId].Value = dt.Rows[i][c].ToString();
                    colId++;
                }

                workSheet.Cells["AK" + rowId.ToString()].Value = dt.Rows[i][Constants.NewQuantity].ToString();

                rowId++;

            }


            var memoryStream = new MemoryStream();
            //package.Save();
            package.SaveAs(memoryStream);
            memoryStream.Position = 0;

            return File(memoryStream, contentType, fileNameGenerated);
        }
        private System.Web.Mvc.FileResult ReportPerSalesOrderExtract(ReportInventoryPerItemSearchModel searchModel)
        {
            DataTable dt = new DataTable();
            string userName = Session[SessionVariables.UserDetails].GetUserNameFromSession();

            DateTime date = (DateTime)searchModel.DateGenerated;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var monthName = firstDayOfMonth.ToString("MMMM");

            dt = _reportCombinationService.GetAllSalesOrder(firstDayOfMonth, lastDayOfMonth, searchModel.CategoryID, searchModel.BranchID);

            int rowId = 0;
            int colId = 0;
            var dir = Server.MapPath(string.Format("~/{0}", Constants.ProductExcelTemplateDir));
            var fileNameTemplate = string.Format("{0}{1}", Constants.ReportTemplate, ".xlsx");
            var path = System.IO.Path.Combine(dir, fileNameTemplate);
            var fileNameGenerated = string.Format("{0}{1}", Constants.ReportPerSalesOrder, ".xlsx");

            var contentType = "application/vnd.ms-excel";

            var templateFile = new FileInfo(path);
            //var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            ;

            var package = new ExcelPackage(templateFile);
            var workSheet = package.Workbook.Worksheets[1];

            workSheet.Cells["A1"].Value = string.Format("MONTH OF {0} {1}", monthName, date.Year);
            workSheet.Cells["A2"].Value = dt.Rows[0][Constants.BranchName].ToString();
            workSheet.Cells["A3"].Value = string.Format("PREPARED BY: {0}", userName);

            rowId = 6;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                colId = 4;
                workSheet.Cells["B" + rowId.ToString()].Value = string.Format("{0} - {1} - {2}", dt.Rows[i][Constants.ProductCode].ToString(), dt.Rows[i][Constants.ProductName].ToString(), dt.Rows[i][Constants.ProductExtension].ToString());
                workSheet.Cells["C" + rowId.ToString()].Value = dt.Rows[i][Constants.OldQuantity].ToString();
                for (int c = 13; c <= 43; c++)
                {
                    workSheet.Cells[rowId, colId].Value = dt.Rows[i][c].ToString();
                    colId++;
                }

                workSheet.Cells["AK" + rowId.ToString()].Value = dt.Rows[i][Constants.NewQuantity].ToString();

                rowId++;

            }


            var memoryStream = new MemoryStream();
            //package.Save();
            package.SaveAs(memoryStream);
            memoryStream.Position = 0;

            return File(memoryStream, contentType, fileNameGenerated);
        }
        private System.Web.Mvc.FileResult ReportPerSalesReportExtract(ReportInventoryPerItemSearchModel searchModel)
        {
            DataTable dt = new DataTable();
            string userName = Session[SessionVariables.UserDetails].GetUserNameFromSession();

            DateTime date = (DateTime)searchModel.DateGenerated;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var monthName = firstDayOfMonth.ToString("MMMM");

            dt = _reportCombinationService.GetAllSalesReport(firstDayOfMonth, lastDayOfMonth, searchModel.CategoryID, searchModel.BranchID);

            int rowId = 0;
            int colId = 0;
            var fileNameGenerated = string.Format("{0}{1}", Constants.ReportSalesReport, ".xlsx");

            var contentType = "application/vnd.ms-excel";

            //var templateFile = new FileInfo(path);
            //var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);

            var package = new ExcelPackage();
            var workSheet = package.Workbook.Worksheets.Add(Constants.ReportSalesReport);

            workSheet.Cells["A1"].Value = "Date Value";
            workSheet.Cells["B1"].Value = "Total Sales Amount";

            rowId = 2;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {

                workSheet.Cells["A" + rowId].Value = dt.Rows[i][Constants.DateValue].ToString();
                workSheet.Cells["B" + rowId].Value = dt.Rows[i][Constants.TotalAmount].ToString();
                

                rowId++;

            }


            var memoryStream = new MemoryStream();
            //package.Save();
            package.SaveAs(memoryStream);
            memoryStream.Position = 0;

            return File(memoryStream, contentType, fileNameGenerated);
        }
        private System.Web.Mvc.FileResult ReportPerInventoryReportExtract(ReportInventorySearchModel searchModel, List<ReportCombinationDto> listOfReport)
        {

            int rowId = 0;
            int colId = 0;

            var fileNameGenerated = string.Format("{0}{1}", Constants.InventoryReport, ".xlsx");
            var contentType = "application/vnd.ms-excel";

            //var templateFile = new FileInfo(path);
            //var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);

            var package = new ExcelPackage();
            var workSheet = package.Workbook.Worksheets.Add(Constants.InventoryReport);

            workSheet.Cells["A1"].Value = "Product Name";
            workSheet.Cells["B1"].Value = "(+)";
            workSheet.Cells["C1"].Value = "(-)";
            workSheet.Cells["D1"].Value = "Product QTY";
            workSheet.Cells["E1"].Value = "Remarks";
            workSheet.Cells["F1"].Value = "Branch Name";

            rowId = 2;
            for (int i = 0; i < listOfReport.Count; i++)
            {

                workSheet.Cells["A" + rowId.ToString()].Value = listOfReport[i].ProductFullDisplayWithExtension;
                workSheet.Cells["B" + rowId.ToString()].Value = listOfReport[i].PurchaseOrderQty;
                workSheet.Cells["C" + rowId.ToString()].Value = listOfReport[i].SalesOrderQty;
                workSheet.Cells["D" + rowId.ToString()].Value = listOfReport[i].ProductQty;
                workSheet.Cells["E" + rowId.ToString()].Value = listOfReport[i].Remarks;
                workSheet.Cells["F" + rowId.ToString()].Value = listOfReport[i].BranchName;
                rowId++;

            }


            var memoryStream = new MemoryStream();
            //package.Save();
            package.SaveAs(memoryStream);
            memoryStream.Position = 0;

            return File(memoryStream, contentType, fileNameGenerated);
        }
        private IQueryable<ReportCombinationDto> GetInventoryReportQueryable(ReportInventorySearchModel searchModel)
        {
            IQueryable<ReportCombinationDto> list = null;

            if (searchModel.IsNull())
            {
                list = _reportCombinationService.GetAll().OrderByDescending(r => r.DateCreated);
            }
            else
            {
                var predicate = PredicateBuilder.True<ReportCombinationDto>();
                var hasOtherFilter = false;

                if (!searchModel.BranchID.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(p => p.BranchId == searchModel.BranchID);

                }

                if (!searchModel.ProductID.IsNull())
                {
                    hasOtherFilter = true;
                    predicate = predicate.And(p => p.ProductID == searchModel.ProductID);
                }

                list = _reportCombinationService.GetAll().AsExpandable().Where(predicate).OrderBy(r => r.DateCreated);

            }

            return list;
        }
        #endregion Private Methods

        #region Dispose

        #endregion Dispose



    }
}
