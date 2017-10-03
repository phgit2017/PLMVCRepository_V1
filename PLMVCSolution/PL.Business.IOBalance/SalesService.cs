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
    public class SalesService : ISalesService
    {
        #region DeclarationsAndConstructors
        IIOBalanceRepository<Product> _product;
        IOrderService _orderService;

        IOBalanceEntity.Product product;
        IOBalanceEntity.SalesOrder salesOrder;
        IOBalanceEntity.SalesOrderDetail salesOrderDetail;
        
        public SalesService(IIOBalanceRepository<Product> product,
            IOrderService orderService)
        {
            this._product = product;
            this._orderService = orderService;

            this.product = new Product();
            this.salesOrder = new SalesOrder();
            this.salesOrderDetail = new SalesOrderDetail();
        }
        #endregion DeclarationsAndConstructors

        #region InterfaceImplementations

        #endregion InterfaceImplementations

        #region PrivateMethods

        #endregion PrivateMethods
    }
}
