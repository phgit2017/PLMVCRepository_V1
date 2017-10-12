﻿using System;
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
    public class CustomerPriceService : ICustomerPriceService
    {
        #region DeclarationsAndConstructors
        IIOBalanceRepository<CustomerPrice> _customerprice;

        IOBalanceEntity.CustomerPrice customerprice;
        public CustomerPriceService(IIOBalanceRepository<CustomerPrice> customerprice)
        {
            this._customerprice = customerprice;
            this.customerprice = new IOBalanceEntity.CustomerPrice();
        }
        #endregion DeclarationsAndConstructors

        #region InterfaceImplementations
        public IQueryable<CustomerPriceDto> GetAll()
        {
            var list = from cp in _customerprice.GetAll()
                       select new CustomerPriceDto()
                       {
                           CustomerPriceId = cp.CustomerPriceID,
                           CustomerId = cp.Customer == null ? null : (int?)cp.Customer.CustomerID,
                           FirstName = cp.Customer == null ? null : cp.Customer.FirstName,
                           LastName = cp.Customer == null ? null : cp.Customer.LastName,
                           MiddleName = cp.Customer == null ? null : cp.Customer.MiddleName,
                           Extension = cp.Customer == null ? null : cp.Customer.Extension,
                           Address = cp.Customer == null ? null : cp.Customer.Address,
                           City = cp.Customer == null ? null : cp.Customer.City,
                           CustomerCode = cp.Customer == null ? null : cp.Customer.CustomerCode,
                           Region = cp.Customer == null ? null : cp.Customer.Region,
                           ZipCode = cp.Customer == null ? null : cp.Customer.ZipCode,
                           ProductId = cp.Product == null ? null : (int?)cp.Product.ProductID,
                           ProductCode = cp.Product == null ? null : cp.Product.ProductCode,
                           ProductName = cp.Product == null ? null : cp.Product.ProductName,
                           ProductDesc = cp.Product == null ? null : cp.Product.ProductDesc,
                           Quantity = cp.Product == null ? null : (decimal?)cp.Product.Quantity,
                           ProductPrice = cp.Product == null ? null : (decimal?)cp.Product.Price,
                           CategoryName = cp.Product == null ? null : cp.Product.Category == null ? null : cp.Product.Category.CategoryName,
                           Price = cp.Price,
                           CreatedBy = cp.CreatedBy,
                           DateCreated = cp.DateCreated,
                           DateUpdated = cp.DateUpdated,
                           UpdatedBy = cp.UpdatedBy

                       };
            return list;
        }

        public IQueryable<CustomerPriceDto> FindPriceByProductId(int productId)
        {
            var list = GetAll().Where(cp => cp.ProductId == productId);

            return list;
        }

        public IQueryable<CustomerPriceDto> FindPriceByCustomerId(int customerId)
        {
            var list = GetAll().Where(cp => cp.CustomerId == customerId);
            return list;
        }

        public IQueryable<CustomerPriceDto> FindPriceByCustomerIdAndProductId(int customerId, int productId)
        {
            var list = GetAll().Where(cp => cp.CustomerId == customerId && cp.ProductId == productId);
            return list;
        }

        public CustomerPriceDto FindCustomerPriceById(int customerPriceId)
        {
            var list = GetAll().Where(cp => cp.CustomerPriceId == customerPriceId).FirstOrDefault();
            return list;
        }

        public bool SaveCustomerPrice(CustomerPriceDto customerPriceDetails)
        {
            this.customerprice = customerPriceDetails.DtoToEntity();

            if (this._customerprice.Insert(this.customerprice).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateCustomerPrice(CustomerPriceDto newCustomerPriceDetails)
        {
            var updatedCustomerPriceDetails = this.customerprice;

            updatedCustomerPriceDetails = new CustomerPrice
            {
                CustomerPriceID = newCustomerPriceDetails.CustomerPriceId,
                CustomerID = newCustomerPriceDetails.CustomerId,
                Price = newCustomerPriceDetails.Price,
                ProductID = newCustomerPriceDetails.ProductId
            };

            return true;
        }
        #endregion InterfaceImplementations
    }
}
