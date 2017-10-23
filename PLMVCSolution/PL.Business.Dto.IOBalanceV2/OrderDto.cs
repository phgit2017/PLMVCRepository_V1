using PL.Business.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Dto.IOBalanceV2
{
    public class OrderDto
    {

        public OrderDto()
        {
            customer = new CustomerDto();
        }

        public long OrderId { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? CreatedBy { get; set; }
        public string SalesNo { get; set; }
        public bool IsPrinted { get; set; }
        public bool IsCorrected { get; set; }
        public long CustomerId { get; set; }

        public CustomerDto customer { get; set; }

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
    }

    public class OrderDetailDto
    {
        public OrderDetailDto()
        {
            product = new ProductDto();
            supplier = new SupplierDto();
        }
        public long OrderDetailId { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }

        public ProductDto product { get; set; }
        public SupplierDto supplier { get; set; }
        
        public int? SupplierId { get; set; }
        public decimal Quantity { get; set; }
        public decimal SalesPrice { get; set; }
        public DateTime? DateCreated { get; set; }

    }


    public class SalesOrderDto
    {
        public CustomerDto customer { get; set; }
        public SalesOrderDto()
        {
            customer = new CustomerDto();
        }

        public long SalesOrderId { get; set; }
        public string SalesNo { get; set; }
        public long CustomerId { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsPrinted { get; set; }
        public bool IsCorrected { get; set; }
    }

    public class SalesOrderDetailDto
    {
        public long SalesOrderId { get; set; }
        public long ProductId { get; set; }

        [Display(Name="Price")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Price; Max 18 digits")]
        public decimal SalesPrice { get; set; }

        public decimal UnitPrice { get; set; }

        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Quantity; Max 18 digits")]
        public decimal Quantity { get; set; }


        public DateTime? DateCreated { get; set; }
        public int? CreatedBy { get; set; }
    }

    public class SalesOrderDetailEditDto
    {

        [Display(Name = "Price")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Price; Max 18 digits")]
        public decimal SalesPrice { get; set; }

        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Quantity; Max 18 digits")]
        public decimal Quantity { get; set; }
    }

    public class ReportPurchaseOrderDto
    {
        public ReportPurchaseOrderDto()
        {
            product = new ProductDto();
            supplier = new SupplierDto();
        }

        public long ProductId { get; set; }
        public int? SupplierId { get; set; }
        public decimal Quantity { get; set; }
        public ProductDto product { get; set; }
        public SupplierDto supplier { get; set; }
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
    }

    public class ReportSalesOrderDto
    {
        public ReportSalesOrderDto()
        {
            salesOderDetails = new List<SalesOrderListDto>();
            customer = new CustomerDto();
        }

        public long SalesOrderId { get; set; }
        public string SalesNo { get; set; }
        public long CustomerId { get; set; }
        public bool IsPrinted { get; set; }
        public bool IsCorrected { get; set; }

        public long ProductId { get; set; }

        public List<SalesOrderListDto> salesOderDetails { get; set; }
        public CustomerDto customer { get; set; }


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
    }

    public class ReportSalesOrderReceiptDto
    {
        public long SalesOrderId { get; set; }
        public string SalesNo { get; set; }
        public long CustomerId { get; set; }
        public CustomerDto customer { get; set; }
        public DateTime? DateCreated { get; set; }

        public bool IsPrinted { get; set; }
        public bool IsCorrected { get; set; }

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
    }
}
