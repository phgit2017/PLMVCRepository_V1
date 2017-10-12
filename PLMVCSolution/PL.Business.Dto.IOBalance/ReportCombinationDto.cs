using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PL.Business.Dto.IOBalance
{
    public class ReportCombinationDto
    {
        public long TrackingID { get; set; }

        public long ProductID { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string ProductDesc { get; set; }

        public string ProductExtension { get; set; }

        public string ProductSize { get; set; }

        public int? CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string CategoryCode { get; set; }


        public decimal? PurchaseOrderQty { get; set; }

        public decimal? SalesOrderQty { get; set; }

        public decimal? ProductQty { get; set; }

        public string RequestType { get; set; }

        public string RequestNum { get; set; }

        public DateTime? DateCreated { get; set; }

        public decimal? Qty { get; set; }

        public decimal? TotalQty { get; set; }

        public int? BranchId { get; set; }

        public string BranchName { get; set; }

        public string BranchAddress { get; set; }

        public string BranchDetails { get; set; }

        public string ProductFullDisplay
        {
            get
            {
                string fulldisplay = string.Format("{0} - {1}", ProductCode, ProductName);
                return fulldisplay;
            }
        }

        public string ProductFullDisplayWithExtension
        {
            get
            {
                string fulldisplay = string.Format("{0} - {1} - {2}", ProductCode, ProductName, ProductExtension);
                return fulldisplay;
            }
        }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string Remarks { get; set; }


    }
}
