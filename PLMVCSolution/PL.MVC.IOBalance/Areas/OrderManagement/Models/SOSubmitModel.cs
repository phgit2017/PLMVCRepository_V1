using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PL.Business.Dto.IOBalance;
namespace PL.MVC.IOBalance.Areas.OrderManagement.Models
{
    public class SOSubmitModel
    {
        public SOSubmitModel()
        {
            salesOrderDetailDto = new List<SalesOrderDetailDto>();
            productDto = new List<ProductDto>();
            salesOrderDto = new List<SalesOrderDto>();
        }

        public List<SalesOrderDetailDto> salesOrderDetailDto;
        public List<ProductDto> productDto;
        public List<SalesOrderDto> salesOrderDto;
        public int CustomerId { get; set; }
        public int? BranchID { get; set; }
        public string Messenger { get; set; }
        public string PaymentTerms { get; set; }
        public int DiscountAmount { get; set; }
    }
}