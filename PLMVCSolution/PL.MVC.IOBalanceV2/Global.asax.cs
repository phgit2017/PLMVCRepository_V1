﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Autofac;
using Autofac.Integration.Mvc;
using PL.MVC.IOBalanceV2.Infrastructure;
using System.Reflection;
using WebMatrix.WebData;

namespace PL.MVC.IOBalanceV2
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            DependencyInjection resolver = new DependencyInjection();
            ContainerBuilder builder = new ContainerBuilder();
            IContainer myContainer = null;
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            builder = resolver.OnConfigure(builder);

            if (myContainer == null)
            {
                myContainer = builder.Build();
            }
            else
            {
                builder.Update(myContainer);
            }

            DependencyResolver.SetResolver(new AutofacDependencyResolver(myContainer));
        }
    }
}