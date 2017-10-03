using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Autofac Dependency
using Autofac;
using System.Web.Mvc;
using System.Reflection;

//Core dependency
using IOBalanceV2Entity = PL.Core.Entity.IOBalanceDBV2;
using PL.Core.Entity.Repository.Interface;

//Infra Dependency
using IOBalanceDBV2Context = PL.Infra.DataAccess.IOBalanceDBV2.Context;
using PL.Infra.DbContext.Interface;
using PL.Infra.Repository;

//Business Dependency
using PL.Business.Dto.IOBalanceV2;
using PL.Business.Interface.IOBalanceV2;
using PL.Business.IOBalanceV2;

namespace PL.MVC.IOBalanceV2.Infrastructure
{
    public class DependencyInjection
    {
        public ContainerBuilder OnConfigure(ContainerBuilder builder)
        {
            //Context Injection
            builder.RegisterType<IOBalanceDBV2Context.IOBalanceDBV2Entities>().As<IIOBalanceDBV2Context>().InstancePerLifetimeScope();


            //Generic Repository Injection
            builder.RegisterGeneric(typeof(IOBalanceV2Repository<>)).As(typeof(IIOBalanceV2Repository<>)).InstancePerLifetimeScope();

            #region IOBalanceV2
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<SupplierService>().As<ISupplierService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<QuantityUnitService>().As<IQuantityUnitService>().InstancePerLifetimeScope();
            builder.RegisterType<InventoryService>().As<IInventoryService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerPriceService>().As<ICustomerPriceService>().InstancePerLifetimeScope();
            #endregion IOBalanceV2

            return builder;
        }
    }
}