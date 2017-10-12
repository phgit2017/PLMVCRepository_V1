using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-- Business
using PL.Business.Common;
using PL.Business.Dto.IOBalance;
using PL.Business.Interface.IOBalance;
using PL.Business.IOBalance.Extensions;
//-- Core
using PL.Core.Entity.IOBalanceDB;
using PL.Core.Entity.Repository.Interface;
using IOBalanceEntity = PL.Core.Entity.IOBalanceDB;

//-- Infra
using PL.Infra.DbContext.Interface;

//-- Infrastructure Utilities
using Infrastructure.Utilities.Extensions;

namespace PL.Business.IOBalance
{
    public class CustomerService : ICustomerService
    {
        #region DeclarationsAndConstructors
        IIOBalanceRepository<Customer> _customer;

        IOBalanceEntity.Customer customer;
        public CustomerService(IIOBalanceRepository<Customer> customer)
        {
            this._customer = customer;
            this.customer = new IOBalanceEntity.Customer();
        }
        #endregion DeclarationsAndConstructors

        #region InterfaceImplementation
        public IQueryable<CustomerDto> GetAll()
        {
            var list = from c in this._customer.GetAll()
                       select new CustomerDto()
                       {
                           CustomerID = c.CustomerID,
                           City = c.City.Trim(),
                           CustomerCode = c.CustomerCode.Trim(),
                           ZipCode = c.ZipCode.Trim(),
                           Address = c.Address.Trim(),
                           IsActive = c.IsActive,
                           BirthDate = c.BirthDate,
                           DateUpdated = c.DateUpdated,
                           Extension = c.Extension.Trim(),
                           FirstName = c.FirstName.Trim(),
                           LastName = c.LastName.Trim(),
                           MiddleName = c.MiddleName.Trim(),
                           Region = c.Region.Trim(),
                           UpdatedBy = c.UpdatedBy
                       };
            return list;
        }

        public CustomerDto FindCustomerById(int customerId)
        {
            var list = GetAll().Where(c => c.CustomerID == customerId).FirstOrDefault();
            return list;
        }

        public IQueryable<CustomerDto> FindCustomerByCustomerCode(string customerCode)
        {
            var list = GetAll().Where(c => c.CustomerCode == customerCode);
            return list;
        }

        public bool SaveCustomer(CustomerDto customerDetails)
        {
            this.customer = customerDetails.DtoToEntity();

            if (this._customer.Insert(this.customer).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateCustomerDetails(CustomerDto newCustomerDetails)
        {
            var updatedCustomerDetails = this.customer;

            updatedCustomerDetails = new Customer()
            {
                CustomerID = newCustomerDetails.CustomerID,
                CustomerCode = newCustomerDetails.CustomerCode.Trim(),
                FirstName = newCustomerDetails.FirstName.Trim(),
                LastName = newCustomerDetails.LastName.Trim(),
                MiddleName = newCustomerDetails.MiddleName,
                Extension = newCustomerDetails.Extension,
                BirthDate = newCustomerDetails.BirthDate,
                Address = newCustomerDetails.Address,
                City = newCustomerDetails.City,
                Region = newCustomerDetails.Region,
                ZipCode = newCustomerDetails.ZipCode,
                IsActive = newCustomerDetails.IsActive,
                UpdatedBy = newCustomerDetails.UpdatedBy,
                DateUpdated = System.DateTime.Now
            };

            if (this._customer.Update2(updatedCustomerDetails).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateInactive(int customerId, int? updatedBy)
        {
            var ActiveCustomer = FindCustomerById(customerId);

            if (ActiveCustomer.IsNull())
            {
                return false;
            }

            this.customer = new Customer()
            {
                CustomerID = customerId,
                CustomerCode = ActiveCustomer.CustomerCode,
                FirstName = ActiveCustomer.FirstName,
                LastName = ActiveCustomer.LastName,
                MiddleName = ActiveCustomer.MiddleName,
                Extension = ActiveCustomer.Extension,
                BirthDate = ActiveCustomer.BirthDate,
                Address = ActiveCustomer.Address,
                City = ActiveCustomer.City,
                Region = ActiveCustomer.Region,
                ZipCode = ActiveCustomer.ZipCode,
                IsActive = ActiveCustomer.IsActive ? false : true,
                UpdatedBy = updatedBy,
                DateUpdated = System.DateTime.Now
            };

            if (this._customer.Update(this.customer, c => c.CustomerID == customerId).IsNull())
            {
                return false;
            }


            return true;
        }
        #endregion InterfaceImplementation

    }
}
