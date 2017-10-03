using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Autofac Dependency
using Autofac;
using System.Web.Mvc;
using System.Reflection;

//Core dependency
using IOBalanceEntity = PL.Core.Entity.IOBalanceDB;
using PL.Core.Entity.Repository.Interface;

//Infra Dependency
using IOBalanceDBContext = PL.Infra.DataAccess.IOBalanceDB.Context;
using PL.Infra.DbContext.Interface;
using PL.Infra.Repository;

//Business Dependency
using PL.Business.Dto.IOBalance;
using PL.Business.Interface.IOBalance;
using PL.Business.IOBalance;


namespace PL.MVC.IOBalance.Infrastructure
{
    public class DependencyInjection
    {
        public ContainerBuilder OnConfigure(ContainerBuilder builder)
        {
            //Context Injection
            builder.RegisterType<IOBalanceDBContext.IOBalanceDBEntities>().As<IIOBalanceDBContext>().InstancePerLifetimeScope();

            //Generic Repository Injection
            builder.RegisterGeneric(typeof(IOBalanceRepository<>)).As(typeof(IIOBalanceRepository<>)).InstancePerLifetimeScope();

            #region IOBalance
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<UnitService>().As<IUnitService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<SupplierService>().As<ISupplierService>().InstancePerLifetimeScope();
            builder.RegisterType<BranchService>().As<IBranchService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerPriceService>().As<ICustomerPriceService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<InventoryService>().As<IInventoryService>().InstancePerLifetimeScope();
            builder.RegisterType<ModelService>().As<IModelService>().InstancePerLifetimeScope();
            builder.RegisterType<SalesService>().As<ISalesService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<ReportCombinationService>().As<IReportCombinationService>().InstancePerLifetimeScope();
            builder.RegisterType<DiscountService>().As<IDiscountService>().InstancePerLifetimeScope();
            #endregion IOBalance

            return builder;
        }

    }
}