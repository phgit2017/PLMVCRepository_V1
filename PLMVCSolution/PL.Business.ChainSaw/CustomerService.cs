using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-- Business
using PL.Business.Common;
using PL.Business.Dto.ChainSaw;
using PL.Business.Interface.ChainSaw;
using PL.Business.ChainSaw.Extensions;

//-- Core
using PL.Core.Entity.ChainSawDBV2;
using PL.Core.Entity.Repository.Interface;
using ChainSawDBEntity = PL.Core.Entity.ChainSawDBV2;

//-- Infra
using PL.Infra.DbContext.Interface;

//-- Infrastructure Utilities
using Infrastructure.Utilities.Extensions;

namespace PL.Business.ChainSaw
{
    public class CustomerService : ICustomerService
    {
        #region Declarations And Constructors
        IIOBalanceRepository<Customer> _customer;

        ChainSawDBEntity.Customer customer;
        public CustomerService(IIOBalanceRepository<Customer> customer)
        {
            this._customer = customer;
            this.customer = new ChainSawDBEntity.Customer();
        }
        #endregion Declarations And Constructors

        #region Interface Implementations
        public IQueryable<CustomerDetailsDto> GetAll()
        {
            var result = from det in _customer.GetAll()
                         select new CustomerDetailsDto() 
                         {
                             CustomerId = det.CustomerID,
                             CustomerCode = det.CustomerCode,
                             CustomerName = det.CustomerName,
                             CustomerAddress = det.CustomerAddress,
                             DateCreated = det.DateCreated,
                             CreatedBy = det.CreatedBy,
                             DateUpdated = det.DateUpdated,
                             UpdatedBy = det.UpdatedBy
                         };

            return result;
        }

        public bool SaveDetails(CustomerDetailsDto newDetails)
        {
            this.customer = newDetails.DtoToEntity();

            if (this._customer.Insert(this.customer).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateDetails(CustomerDetailsDto newDetails)
        {
            var oldDetails = GetAll().Where(d => d.CustomerId == newDetails.CustomerId).FirstOrDefault();
            var details = newDetails.DtoToEntity();

            //TODO: add here the old details
            details.DateCreated = oldDetails.DateCreated;
            details.CreatedBy = oldDetails.CreatedBy;


            if (_customer.Update2(details).IsNull())
            {
                return false;
            }


            return true;

        }
        #endregion Interface Implementations

    }
}
