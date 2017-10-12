using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Autofac Dependency
using Autofac;
using System.Web.Mvc;
using System.Reflection;

//Core dependency
using IOBalanceEntity = PL.Core.Entity.ChainSawDBV2;
using PL.Core.Entity.Repository.Interface;

//Infra Dependency
using ChainSawDBContext = PL.Infra.DataAccess.ChainSawDBV2;
using PL.Infra.DbContext.Interface;
using PL.Infra.Repository;

//Business Dependency
using PL.Business.Dto.ChainSaw;
using PL.Business.Interface.ChainSaw;
using PL.Business.ChainSaw;


namespace PL.MVC.CSInventory.Infrastructure
{
    public class DependencyInjection
    {
        public ContainerBuilder OnConfigure(ContainerBuilder builder)
        {
            //Context Injection
            builder.RegisterType<ChainSawDBContext.ChainSawDBV2Entities>().As<IChainSawDBContext>().InstancePerLifetimeScope();

            //Generic Repository Injection
            builder.RegisterGeneric(typeof(ChainSawRepository<>)).As(typeof(IIOBalanceRepository<>)).InstancePerLifetimeScope();

            #region IOBalance
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            #endregion IOBalance

            return builder;
        }

    }
}