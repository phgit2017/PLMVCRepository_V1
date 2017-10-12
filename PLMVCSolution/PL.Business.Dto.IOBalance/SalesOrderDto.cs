using PL.Business.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PL.Business.Dto.IOBalance
{
    public class SalesOrderDto
    {
        public SalesOrderDto()
        {
            SalesOrderDetails = new List<SalesOrderDetailDto>();
        }

        public long SalesOrderID { get; set; }

        [StringLength(100)]
        public string SalesOrderNum { get; set; }

        [StringLength(50)]
        public string Messenger { get; set; }

        [StringLength(200)]
        public string PaymentTerms { get; set; }

        public int? BranchID { get; set; }

        public string BranchName { get; set; }

        public string BranchAddress { get; set; }

        public string BranchDetails { get; set; }

        public int? CustomerID { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }

        public string CustomerMiddleName { get; set; }

        public string CustomerFullName
        {
            get
            {
                return string.Format("{0}, {1} {2}", CustomerLastName, CustomerFirstName, CustomerMiddleName);
            }
        }

        public string CustomerAddress { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        public string DateCreatedWithFormat
        {
            get
            {
                if (DateCreated == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Convert.ToDateTime(DateCreated).ToString(Globals.DefaultRecordDateFormat);
                }

            }
        }

        public List<SalesOrderDetailDto> SalesOrderDetails { get; set; }

        public decimal? TotalAmountPurchased
        {
            get
            {
                decimal? ttlAmount = 0;
                foreach (var amount in SalesOrderDetails)
                {
                    ttlAmount += (amount.UnitPrice * amount.Quantity);
                }

                return ttlAmount;

            }
            set { }
        }

        public decimal? TotalQtyPurchased
        {
            get {

                decimal? ttlQty = 0;
                foreach (var qty in SalesOrderDetails)
                {
                    ttlQty += qty.Quantity;
                }

                return ttlQty;
            }

            set { }
        }

        public int TotalItemsPurchased
        {
            get {
                return SalesOrderDetails.Count;
            }
            set { }
        }

        public int? DiscountPercentage { get; set; }

    }
}
