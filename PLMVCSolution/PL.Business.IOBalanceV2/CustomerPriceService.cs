using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-- Business
using PL.Business.Common;
using PL.Business.Dto.IOBalanceV2;
using PL.Business.Interface.IOBalanceV2;
using PL.Business.IOBalanceV2.Extensions;

//-- Core
using PL.Core.Entity.IOBalanceDBV2;
using PL.Core.Entity.Repository.Interface;
using IOBalanceDBV2Entity = PL.Core.Entity.IOBalanceDBV2;

//-- Infra
using PL.Infra.DbContext.Interface;

//-- Infrastructure Utilities
using Infrastructure.Utilities.Extensions;

namespace PL.Business.IOBalanceV2
{
    public class CustomerPriceService : ICustomerPriceService
    {
        #region Declarations And Constructors
        IIOBalanceV2Repository<CustomerPrice> _customerPrice;

        private readonly IInventoryService _inventoryService;
        private readonly ICustomerService _customerService;

        IOBalanceDBV2Entity.CustomerPrice customerPrice;
        public CustomerPriceService(IIOBalanceV2Repository<CustomerPrice> customerPrice, 
            IInventoryService inventoryService, 
            ICustomerService customerService)
        {
            this._customerPrice = customerPrice;
            this._inventoryService = inventoryService;
            this._customerService = customerService;
            this.customerPrice = new IOBalanceDBV2Entity.CustomerPrice();
        }
        #endregion Declarations And Constructors

        #region Interface Implementations
        public IQueryable<CustomerPriceDto> GetAll()
        {
            var products = _inventoryService.GetAll();
            var customers = _customerService.GetAll();

            var result = from det in this._customerPrice.GetAll()
                         select new CustomerPriceDto()
                         {
                             CustomerId = det.CustomerID,
                             ProductId = det.ProductID,
                             Price = det.Price,
                             CreatedBy = det.CreatedBy,
                             DateCreated = det.DateCreated,
                             product = products.Where(prod => prod.ProductId == det.ProductID).FirstOrDefault(),
                             customer= customers.Where(cust => cust.CustomerId == det.CustomerID).FirstOrDefault()
                         };

            return result;
        }

        public bool SaveDetails(CustomerPriceDto newDetails)
        {
            this.customerPrice = newDetails.DtoToEntity();

            if (this._customerPrice.Insert(this.customerPrice).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool DeleteDetails(long customerId, long productId)
        {
            if (this._customerPrice.Delete(c => c.CustomerID == customerId && c.ProductID == productId).IsNull())
            {
                return false;
            }

            return true;
        }
        #endregion Interface Implementations
    }
}
