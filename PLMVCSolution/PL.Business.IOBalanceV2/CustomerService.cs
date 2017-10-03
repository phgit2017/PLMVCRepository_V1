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
    public class CustomerService : ICustomerService
    {
        #region Declarations And Constructors
        IIOBalanceV2Repository<Customer> _customer;

        IOBalanceDBV2Entity.Customer customer;
        public CustomerService(IIOBalanceV2Repository<Customer> customer)
        {
            this._customer = customer;
            this.customer = new IOBalanceDBV2Entity.Customer();
        }
        #endregion Declarations And Constructors

        #region Interface Implementations
        public IQueryable<CustomerDto> GetAll()
        {
            var result = from det in this._customer.GetAll()
                         select new CustomerDto()
                         {
                             CustomerId = det.CustomerID,
                             CustomerCode = det.CustomerCode,
                             CustomerName = det.CustomerName,
                             CustomerAddress = det.CustomerAddress,
                             DateCreated = det.DateCreated,
                             CreatedBy = det.CreatedBy,
                             DateUpdated = det.DateUpdated,
                             UpdatedBy = det.UpdatedBy,
                             IsActive = det.IsActive
                         };

            return result;
        }

        public CustomerDto FindById(long customerId)
        {
            var results = GetAll().Where(c => c.CustomerId == customerId).FirstOrDefault();
            return results;
        }

        public bool SaveDetails(CustomerDto newDetails)
        {
            this.customer = newDetails.DtoToEntity();

            if (this._customer.Insert(this.customer).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateDetails(CustomerDto newDetails)
        {
            //var oldDetails = FindById(newDetails.CustomerId);
            var details = newDetails.DtoToEntity();

            if (_customer.Update2(details).IsNull())
            {
                return false;
            }


            return true;

        }

        public bool UpdateInactive(long customerId, int? updatedBy)
        {

            var ActiveCustomer = FindById(customerId);

            if (ActiveCustomer.IsNull())
            {
                return false;
            }

            ActiveCustomer.IsActive = ActiveCustomer.IsActive ? false : false;
            ActiveCustomer.UpdatedBy = updatedBy;
            var details = ActiveCustomer.DtoToEntity();
            

            if (this._customer.Update2(details).IsNull())
            {
                return false;
            }


            return true;
        }
        #endregion Interface Implementations
    }
}
